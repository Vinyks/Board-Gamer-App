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
        private string _DisplayName;

        public Cuisine(string name, int rank, string displayName)
        {
            _Name = name;
            _Rank = rank;
            _DisplayName = displayName;
        }
        public Cuisine()
        {

        }

        public string Name { get => _Name; set => _Name = value; }
        public int Rank { get => _Rank; set => _Rank = value; }
        public string DisplayName { get => _DisplayName; set => _DisplayName = value; }
    }
}
