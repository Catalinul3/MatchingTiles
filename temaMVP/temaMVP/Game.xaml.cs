using System;
using System.Collections.Generic;
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

namespace temaMVP
{
    
    public partial class Game : Window
    {
        public Game()
        {
            InitializeComponent();
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            int level = 1;
            Tiles tiles=new Tiles(level,userName.Text);
            this.Hide();
            tiles.ShowDialog();
            tiles.name.Text=userName.Text;
            tiles.name.Visibility = Visibility.Hidden;
            this.Show();
        }

       

        private void Load(object sender, RoutedEventArgs e)
        {
            Tiles t = new Tiles(userName.Text);
          
            t.ShowDialog();


        }
        private void Statistics(object sender, RoutedEventArgs e)
        {
            Statistics st = new Statistics();
            Tiles tiles = new Tiles(userName.Text);
            tiles.statistici();
            st.StatisticsList.ItemsSource=tiles.des();
            st.ShowDialog();
        }

        private void Standard(object sender, RoutedEventArgs e)
        {int level = 1;
            Tiles tiles=new Tiles(level,userName.Text);
            tiles.name.Text= userName.Text;
            tiles.ShowDialog();
            this.Show();
        }

        private void Custom(object sender, RoutedEventArgs e)
        {MatrixType matrix=new MatrixType(userName.Text);
            this.Close();
            matrix.ShowDialog();
            

        }
    }
}
