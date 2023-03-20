using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace temaMVP
{
    public partial class Tiles : Window

    {
        private GameLogic logic;
        private List<List<Image>> images { set; get; }
        public TextBlock name;
        private Image image1 = null;
        private int attemps;
        private int level = 1;
        private Board board;
        private StatisticsOfPlayers st;
        private List<Point> unknown { get; set; }
        public void setName(string n)
        {
            name.Text = n;
        }
        public string getName() { return name.Text; }
        public Tiles(int levels, string uname)
        {
            InitializeComponent();
            level = levels;
            st= new StatisticsOfPlayers();
            st.username= uname;
            MessageBox.Show("level " + level);
            GameLogic game = new GameLogic();
            logic = game;
            images = new List<List<Image>>();
            name = new TextBlock();
            name.Text = uname;
            name.Visibility = Visibility.Hidden;
            unknown = new List<Point>();
            attemps = (game.rows * game.column) / level;
            for (int i = 0; i < game.rows; i++)
            {
                RowDefinition rw = new RowDefinition();
                GameTiles.RowDefinitions.Add(rw);
            }
            for (int j = 0; j < game.column; j++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                GameTiles.ColumnDefinitions.Add(cd);

            }

            for (int i = 0; i < game.rows; i++)
            {
                for (int j = 0; j < game.column; j++)
                {

                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("unknown/spate.png", UriKind.Relative));

                    image.MouseLeftButtonDown += new MouseButtonEventHandler(Image_MouseLeftButtonDown);

                    Grid.SetRow(image, i);
                    Grid.SetColumn(image, j);
                    GameTiles.Children.Add(image);

                    Point t = new Point(i, j);

                    unknown.Add(t);

                }
            }
            images = logic.createMatrix();
            Closing += Tiles_Closing;


        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GameLogic game = new GameLogic();
            User u = new User();
            List<Image> imagini = u.createUsersAvatar();
            Random random = new Random();
            st=new StatisticsOfPlayers();

            Image image = sender as Image;
            int row = Grid.GetRow(image);
            int col = Grid.GetColumn(image);

            Image newImage = new Image();
            newImage.Source = images[row][col].Source;
            newImage.MouseLeftButtonDown += new MouseButtonEventHandler(Image_MouseLeftButtonDown);
            Grid.SetColumn(newImage, col);
            Grid.SetRow(newImage, row);
            GameTiles.Children.Remove(image);
            GameTiles.Children.Add(newImage);
            if (image1 == null)
            {
                image1 = newImage;

            }
            else if (image1 != null)
            {
                bool trigger = false;
                bool samePosition = false;
                bool validation = checkMatch(image1, newImage);
                bool validationSamePosition = checkPosition(image1, newImage);
                if (validationSamePosition)
                {
                    MessageBox.Show("Same position,try harder");
                    newImage.Source = new BitmapImage(new Uri("unknown/spate.png", UriKind.Relative));
                }


                if (validation && !validationSamePosition)
                {
                    int r = Grid.GetRow(image1);
                    int c = Grid.GetColumn(image1);
                    int r2 = Grid.GetRow(newImage);
                    int c2 = Grid.GetColumn(newImage);
                    Point known = new Point(r, c);
                    Point known2 = new Point(r2, c2);
                    unknown.Remove(known);
                    unknown.Remove(known2);

                    GameTiles.Children.Remove(image1);
                    GameTiles.Children.Remove(newImage);

                    image1 = null;
                    newImage = null;


                }
                else if (!validationSamePosition)
                {

                    attemps = attemps - 1;
                    MessageBox.Show("Wrong, you have " + attemps + " attemps");
                    if (attemps == 0)
                    {
                        MessageBox.Show("Game Over");
                        this.Close();
                        st.numberOfPlay++;
                        statistici();
                    }

                    trigger = true;
                }
                if (trigger == true)
                {
                    image1.Source = new BitmapImage(new Uri("unknown/spate.png", UriKind.Relative));
                    newImage.Source = new BitmapImage(new Uri("unknown/spate.png", UriKind.Relative));
                }
                image1 = null;
            }

            if (GameTiles.Children.Count == 0)
            {
                int levels = level;
                levels++;
                if (levels <= 3)
                {
                    restartGame(levels, name.Text);
                }
                else
                {
                    MessageBox.Show("You beat this game 🍺🍺");
                    st.victories++;
                    st.numberOfPlay++;
                    statistici();
                    this.Close();
                }


            }

        }
        private void Tiles_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Do you want to save your progress " + name.Text, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = false;

            }
            else
            {
                saveBoard();
            }
        }
        public void saveBoard()
        {
            board = new Board();
            board.nrElements = GameTiles.Children.Count;
            board.user = name.Text;
            board.coloane = logic.column;

            board.linii = logic.rows;
            board.level = level;
            board.unknownImagePosition = unknown;
            board.attemps = attemps;

            XmlSerializer serializer = new XmlSerializer(typeof(Board));
            StreamWriter writer;
            using (writer = new StreamWriter(name.Text + "config.xml"))
            {
                serializer.Serialize(writer, board);
            }
        }
        public void statistici()
        {
            st = new StatisticsOfPlayers();
            st.username = name.Text;
            XmlSerializer serializer = new XmlSerializer(typeof(StatisticsOfPlayers));
            StreamWriter writer;
            using (writer = new StreamWriter( "statistics.xml", append: true))
            {
                serializer.Serialize(writer, st);
            }

        }
        public List<StatisticsOfPlayers> des()
        {
            StatisticsOfPlayers st;
            List<StatisticsOfPlayers>players= new List<StatisticsOfPlayers>();
            XmlSerializer serializer = new XmlSerializer(typeof(StatisticsOfPlayers));
            using (FileStream fileStream = new FileStream("statistics.xml", FileMode.Open))
            {
                st = (StatisticsOfPlayers)serializer.Deserialize(fileStream);
                
            }
            players.Add(st);
            return players;
        }
        public void restartGame(int level, string uname)
        {
            name = new TextBlock();
            name.Text = uname;

            if (level <= 3)
            {
                Tiles tiles = new Tiles(logic.column, logic.rows, level, name.Text);
                this.Close();
                tiles.ShowDialog();
            }
            else
            {
                return;
            }



        }
        public bool checkPosition(Image image1, Image image2)
        {
            int rowImage1 = Grid.GetRow(image1);
            int rowImage2 = Grid.GetRow(image2);
            int colImage1 = Grid.GetColumn(image1);
            int colImage2 = Grid.GetColumn(image2);
            bool verify = false;
            if ((rowImage1 == rowImage2) && (colImage1 == colImage2))
            {
                verify = true;
            }
            return verify;
        }
        public Tiles(int cols, int rows, int levels, string uname)
        {
            InitializeComponent();

            GameLogic game = new GameLogic(cols, rows);

            st = new StatisticsOfPlayers();
            st.username = uname;

            level = levels;
            MessageBox.Show("level " + level);
            logic = game;
            images = new List<List<Image>>();
            unknown = new List<Point>();
            name = new TextBlock();
            name.Text = uname;
            name.Visibility = Visibility.Hidden;
            attemps = (game.rows * game.column) / level;
            for (int i = 0; i < game.rows; i++)
            {
                RowDefinition rw = new RowDefinition();
                GameTiles.RowDefinitions.Add(rw);
            }
            for (int j = 0; j < game.column; j++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                GameTiles.ColumnDefinitions.Add(cd);

            }
            for (int i = 0; i < game.rows; i++)
            {
                for (int j = 0; j < game.column; j++)
                {
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("unknown/spate.png", UriKind.Relative));
                    image.MouseLeftButtonDown += new MouseButtonEventHandler(Image_MouseLeftButtonDown);

                    Grid.SetRow(image, i);
                    Grid.SetColumn(image, j);
                    GameTiles.Children.Add(image);
                    Point t = new Point(i, j);

                    unknown.Add(t);
                }
            }
            images = logic.createMatrix();
            Closing += Tiles_Closing;

        }

        public bool checkMatch(Image imag1, Image imag2)
        {
            BitmapImage bitmap1 = (BitmapImage)imag1.Source;
            BitmapImage bitmap2 = (BitmapImage)imag2.Source;
            return bitmap1.UriSource.Equals(bitmap2.UriSource);
        }
        public Tiles(string nume)
        {
            InitializeComponent();
            Board board;
            st = new StatisticsOfPlayers();
            st.username = nume;
            XmlSerializer serializer = new XmlSerializer(typeof(Board));
            using (FileStream fileStream = new FileStream(nume + "config.xml", FileMode.Open))
            {
                board = (Board)serializer.Deserialize(fileStream);
            }
            level = board.level;
            MessageBox.Show("level " + level);
            GameLogic game = new GameLogic(board.coloane, board.linii);
            logic = game;
            name = new TextBlock();
            name.Text = nume;
            name.Visibility = Visibility.Hidden;
            unknown = new List<Point>();
            images = new List<List<Image>>();
            attemps = board.attemps;
            for (int i = 0; i < board.linii; i++)
            {
                RowDefinition rw = new RowDefinition();
                GameTiles.RowDefinitions.Add(rw);
            }
            for (int j = 0; j < board.coloane; j++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                GameTiles.ColumnDefinitions.Add(cd);

            }

            int o = 0;
            for (int k = 0; k < board.linii; k++)
            {
                for (int l = 0; l < board.coloane; l++)
                {
                   
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("unknown/spate.png", UriKind.Relative));

                    image.MouseLeftButtonDown += new MouseButtonEventHandler(Image_MouseLeftButtonDown);
                    int row = (int)board.unknownImagePosition[o].X;
                    int column = (int)board.unknownImagePosition[o].Y;
                    if (k == row && l == column)
                    {
                        Grid.SetRow(image, k);
                        Grid.SetColumn(image, l);
                        GameTiles.Children.Add(image);

                        Point t = new Point(k, l);

                        unknown.Add(t);
                        o++;

                    }
                   
                }
            }


            images = logic.createMatrixSerializable(board.nrElements, board.unknownImagePosition);
            Closing += Tiles_Closing;

        }

    }
}