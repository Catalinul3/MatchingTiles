using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace temaMVP
{
    internal class User
    { public List<string> users { get; set; }
       public List<Image> images { get; set; }
        public User() { 
            users = File.ReadAllLines(@"users.txt").ToList();

       
        }
        public List<Image> createUsersAvatar()
        {List<Image> avatars = new List<Image>();
            string[] imageNames = File.ReadAllLines("avatar.txt");
            foreach (string imageName in imageNames)
            {
                Image image= new Image();
                image.Source = new BitmapImage(new Uri(imageName, UriKind.Relative));
                image.Width = 166;
                image.Height=153;
                avatars.Add(image);
            }
            return avatars;
          
        }
       

    }
}
