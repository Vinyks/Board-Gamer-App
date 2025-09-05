using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_Gamer_App
{
    public class Event 
    {
        private string _Name;
        string _Uhrzeit;
        string _Datum;
        DateTime _DateTime;
        bool _IsFirstItemInFuture = false;

        public Event(string name, TimeOnly uhrzeit, DateOnly datum)
        {
            _Name = name;
            _Uhrzeit = uhrzeit.ToString("HH:mm");
            _Datum = datum.ToString("dd.MM.yyyy");
            _DateTime = new DateTime(datum, uhrzeit);
        }

        public string Name { get => _Name; set => _Name = value; }
        public string Uhrzeit { get => _Uhrzeit; set => _Uhrzeit = value; }
        public string Datum { get => _Datum; set => _Datum = value; }
        public DateTime DateTime { get => _DateTime; set => _DateTime = value; }
        public bool IsFirstItemInFuture { get => _IsFirstItemInFuture; set => _IsFirstItemInFuture = value; }
    }
}
