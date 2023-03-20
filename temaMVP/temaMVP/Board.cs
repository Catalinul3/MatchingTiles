using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace temaMVP
{
    [Serializable]
    public class Board
    {
      
         public string user { get; set; }
            public int nrElements { get; set; }
            public int linii { get; set; }
            public int coloane { get; set; }
            public int level { get; set; }
        public int attemps { get; set; }
        [XmlArray]
        public List<Point> unknownImagePosition { get; set; } 
        public Board()
        {
            unknownImagePosition= new List<Point>();
        }

        
    }
}
