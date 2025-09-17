using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_Gamer_App
{
    public class Order
    {
        private string _Name;
        private int _ID;
        private float _Price;

        private int _Amount;
        private string _PriceDisplay;

        public Order(string name, int id, float price)
        {
            _Name = name;
            _ID = id;
            _Price = price;

            _Amount = 0;
            _PriceDisplay = price.ToString("n2") + "€";
        }

        public string Name { get => _Name; set => _Name = value; }
        public int ID { get => _ID; set => _ID = value; }
        public float Price { get => _Price; set => _Price = value; }
        public int Amount { get => _Amount; set => _Amount = value; }
        public string PriceDisplay { get => _PriceDisplay; set => _PriceDisplay = value; }
    }
}
