using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_Gamer_App
{
    public class Cuisine
    {
        private string _Name;
        private int _Rank;

        public Cuisine(string name, int rank)
        {
            _Name = name;
            _Rank = rank;
        }
        public Cuisine()
        {

        }

        public string Name { get => _Name; set => _Name = value; }
        public int Rank { get => _Rank; set => _Rank = value; }
    }
}
