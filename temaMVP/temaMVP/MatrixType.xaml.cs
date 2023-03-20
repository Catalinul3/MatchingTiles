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
 public partial class MatrixType : Window
    { private TextBlock user { get; set; }
        public MatrixType(string u)
        {
            InitializeComponent();
            user = new TextBlock();
            user.Text = u;
            user.Visibility= Visibility.Hidden;
        }
        public int Rows()
        { int row = int.Parse(numberOfRows.Text);
            return row;
        }
        public int Columns()
        {
            int cols=int.Parse(numberOfColumns.Text);
            return cols;
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool validation(int row,int cols)
        {
             cols = Columns();
            row = Rows();
            User u = new User();
            int capacity=u.createUsersAvatar().Count();
            bool ok = true;
            if (row == 0 && cols == 0)
            {
                MessageBox.Show("Invalid input");
            }
            if(row*cols>capacity)
            {
                ok = false;
            }
            int prod = row * cols;
            if(prod%2==1)
            {
                MessageBox.Show("Table capacity must be an even number");
                ok= false;
            }
            return ok;
        }
        private void Enter(object sender, RoutedEventArgs e)
        {
            int cols = Columns();
            int rows=Rows();
            int level = 1;
            bool verification = validation(rows, cols);
            if (verification)
            {
                Tiles tiles = new Tiles(cols, rows,level,user.Text);
                tiles.name.Text = user.Text;
                this.Close();
                tiles.ShowDialog();
            }         
        }
    }
}
