using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_Gamer_App
{
    class RatingItem
    {
        private string _Description;
        private int _Rating;

        public RatingItem(string description, int rating)
        {
            _Description = description;
            _Rating = rating;
        }

        public string Description { get => _Description; set => _Description = value; }
        public int Rating { get => _Rating; set => _Rating = value; }
    }
}
