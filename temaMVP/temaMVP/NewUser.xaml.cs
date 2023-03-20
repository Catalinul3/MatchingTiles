using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public partial class NewUser : Window
    {

        public NewUser()
        {
            InitializeComponent();

        }
        public String username()
        {
            string name = textUsername.Text;
            return name;
        }
        public String coUsername()
        {
            string confirm = confirmBox.Text;
            return confirm;
        }
        public Boolean confirmName()
        {
            string name = username();
            string confirm = confirmBox.Text;
            bool valoareComparatie = String.Equals(name, confirm);
            return valoareComparatie;
        }
        public void addName()
        {
            User u = new User();
            string user = username();
            using(StreamWriter sw=new StreamWriter(@"users.txt",true))
            {
                sw.WriteLine(user);
            }
           
        }
        public void updateUsers()
        {
            MainWindow main=new MainWindow();
            main.listOfUsers.Items.Refresh();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string input = username();
            string co = coUsername();
            bool confirm = confirmName();
            if (confirm)
            {
                addName();
               MessageBox.Show(input + " added");
                this.Close();
            }
            else
            {
                MessageBox.Show(input + " and " + co + " is not the same ");
            }
        }
    }
}
