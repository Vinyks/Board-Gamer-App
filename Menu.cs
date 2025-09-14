using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_Gamer_App
{
    public class Menu
    {
        private string _Name;
        private List<Item> _Items;

        public Menu(string name, List<Item> items)
        {
            _Name = name;
            _Items = items;
        }
        public Menu()
        {

        }
        public string Name { get => _Name; set => _Name = value; }
        public List<Item> Items { get => _Items; set => _Items = value; }

        public class Item
        {
            private string _Name;
            private float _Price;

            public Item(string name, float price)
            {
                _Name = name;
                _Price = price;
            }
            public Item()
            {

            }

            public string Name { get => _Name; set => _Name = value; }
            public float Price { get => _Price; set => _Price = value; }
        }

    }
}
