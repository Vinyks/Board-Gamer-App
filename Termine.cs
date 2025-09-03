using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_Gamer_App
{
    internal class Termine 
    {
        private int _Tag;
        private string _Ort, _Uhrzeit;

        public Termine(int tag, string ort, string uhrzeit)
        {
            _Tag = tag;
            _Ort = ort;
            _Uhrzeit = uhrzeit;
        }

        public int Tag { get => _Tag; set => _Tag = value; }
        public string Ort { get => _Ort; set => _Ort = value; }
        public string Uhrzeit { get => _Uhrzeit; set => _Uhrzeit = value; }
    }
}
