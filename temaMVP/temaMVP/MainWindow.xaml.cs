using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace temaMVP
{
    public partial class MainWindow : Window
    {
        int currentImageIndex = 0;
        public MainWindow()
        {
            InitializeComponent();

            User u = new User();
            List<Image> images = u.createUsersAvatar();
       
            avatarBox.Source = images[currentImageIndex].Source;
        }      
       private void New_User(object sender, RoutedEventArgs e)
        {
            
            NewUser newUser = new NewUser();
            this.Hide();
           
            newUser.ShowDialog();
            List<String> list = new List<String>();
            list = File.ReadAllLines("users.txt").ToList();
            listOfUsers.ItemsSource= list;

            this.Show();
        }

        private void Delete_User(object sender, RoutedEventArgs e)
        {
            string message = "under development";
            MessageBox.Show(message);
        }
        public String getUsername()
        {
            return listOfUsers.SelectedItem.ToString();
        }
        public ImageSource getAvatar()
        {
            return avatarBox.Source;
        }
        private void Play(object sender, RoutedEventArgs e)
        {
            Game tiles = new Game();
            tiles.userName.Text =getUsername();
            tiles.avatarSpot.Source = getAvatar();

            this.Hide();
            tiles.ShowDialog();
            this.Show();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            string message = "Goodbye, Hope you like it";
            MessageBox.Show(message);
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            currentImageIndex--;
            User u = new User();
            List<Image> images = u.createUsersAvatar();
            if (currentImageIndex <= 0)
            {
                currentImageIndex = images.Count - 1;
            }
            avatarBox.Source = images[currentImageIndex].Source;   
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            currentImageIndex++;
            User u = new User();
            List<Image> images = u.createUsersAvatar();
            if (currentImageIndex >= images.Count)
            {
                currentImageIndex = 0;
            }
            avatarBox.Source = images[currentImageIndex].Source;
        }
    }
}
