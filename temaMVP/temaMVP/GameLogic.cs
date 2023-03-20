using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace temaMVP
{
    internal class GameLogic
    {
        public List<List<Image>> matrixStandard { get; set; }
        public List<List<Image>> matrixGenerator { get; set; }
        public int column { get; set; }
        public int rows { get; set; }
        public GameLogic()
        {
            column = 4;
            rows = 5;
            matrixStandard = new List<List<Image>>();
            for (int i = 0; i < rows; i++)
            {
                List<Image> images = new List<Image>();
                for (int j = 0; j < column; j++)
                {
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri(@"unknown/spate.png", UriKind.Relative));
                    image.Width = 300;
                    image.Height = 300;
                    Grid.SetRow(image, i);
                    Grid.SetColumn(image, j);
                    images.Add(image);
                }
                matrixStandard.Add(images);
            }

        }
        public GameLogic(int columns, int row)
        {
            rows = row;
            column = columns;
            matrixStandard = new List<List<Image>>();

            for (int i = 0; i < rows; i++)
            {
                List<Image> images = new List<Image>();
                for (int j = 0; j < column; j++)
                {
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri(@"unknown/spate.png", UriKind.Relative));
                    image.Width = 300;
                    image.Height = 300;
                    Grid.SetRow(image, i);
                    Grid.SetColumn(image, j);
                    images.Add(image);
                }
                matrixStandard.Add(images);
            }
        }
        public List<List<Image>> createMatrix()
        {
            User images = new User();
            List<Image> reveal = images.createUsersAvatar();
            List<Image> n = new List<Image>();
            Random random = new Random();
            string url = "unknown/spate.png";
            int numberOfPictures = (rows * column) / 2;
            for (int i = 0; i < numberOfPictures; i++)
            {
                int aux = random.Next(reveal.Count);
                n.Add(reveal[aux]);
                reveal.RemoveAt(aux);
            }
            foreach (Image image in n.ToList())
            {
                n.Add(image);
            }
            int k = 0;
            for (int i = 0; i < matrixStandard.Count; i++)
            {
                for (int j = 0; j < matrixStandard[i].Count; j++)
                {
                    int randomRows = random.Next(0, rows);
                    int randomCols = random.Next(0, column);
                    BitmapImage b = (BitmapImage)matrixStandard[randomRows][randomCols].Source;
                    while (!b.ToString().Equals(url))
                    {
                        randomRows = random.Next(0, rows);
                        randomCols = random.Next(0, column);
                        b = (BitmapImage)matrixStandard[randomRows][randomCols].Source;

                    }
                    matrixStandard[randomRows][randomCols] = n[k];
                    k++;
                }
            }
            return matrixStandard;
        }
        public List<List<Image>> createMatrixSerializable(int capacity, List<Point> positions)
        {
            User images = new User();
            List<Image> reveal = images.createUsersAvatar();
            List<Image> n = new List<Image>();
            Random random = new Random();
            string url = "unknown/spate.png";
            int numberOfPictures = capacity / 2;
            for (int i = 0; i < numberOfPictures; i++)
            {
                int aux = random.Next(reveal.Count);
                n.Add(reveal[aux]);
                reveal.RemoveAt(aux);
            }
            foreach (Image image in n.ToList())
            {
                n.Add(image);
            }
            int k = 0;
            for (int i = 0; i < matrixStandard.Count; i++)
            {
                for (int j = 0; j < matrixStandard[i].Count; j++)
                {
                    int randomRows = random.Next(0, rows);
                    int randomCols = random.Next(0, column);
                    BitmapImage b = (BitmapImage)matrixStandard[randomRows][randomCols].Source;



                    int r = (int)positions[k].X;
                    int c = (int)positions[k].Y;
                        while (!b.ToString().Equals(url))
                        {
                            randomRows = random.Next(0, rows);
                            randomCols = random.Next(0, column);
                            b = (BitmapImage)matrixStandard[randomRows][randomCols].Source;


                        }
                        matrixStandard[r][c] = n[k];
                        k++;
                        
                       if(k==capacity)
                    {
                        break;
                    }
                    }
                if (k == capacity)
                {
                    break;
                }


            }
              
            return matrixStandard;
        }
    }
}
