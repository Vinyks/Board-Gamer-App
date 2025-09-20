using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_Gamer_App
{
    public class Rating
    {
        private string _Description;
        private int _RatingValue;
        private int _ID;

        public Rating(string description, int rating, int id)
        {
            _Description = description;
            _RatingValue = rating;
            _ID = id;
        }
        public Rating(){}

        public string Description { get => _Description; set => _Description = value; }
        public int RatingValue { get => _RatingValue; set => _RatingValue = value; }
        public int ID { get => _ID; set => _ID = value; }
    }
}
