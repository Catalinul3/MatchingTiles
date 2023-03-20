using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace temaMVP
{
    [Serializable]
    public class StatisticsOfPlayers
    {
        public string username { get; set; }
        public int numberOfPlay { get; set; }
        public int victories { get; set; }
       
        
    }
}
