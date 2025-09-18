using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_Gamer_App
{
    public class Participant
    {
        public enum Statuses
        {
            Kommt,
            Verspaetet,
            Verhindert
        }

        private string _Person, _StatusNachricht;
        private Statuses _Status;

        public Participant(string person, Statuses status)
        {
            _Person = person;
            _Status = status;
            switch (_Status)
            {
                case Statuses.Kommt: _StatusNachricht = "Kommt"; break;
                case Statuses.Verspaetet: _StatusNachricht = "Verspaetet"; break;
                case Statuses.Verhindert: _StatusNachricht = "Verhindert"; break;
            }
        }
        public Participant()
        {

        }

        public string Person { get => _Person; set => _Person = value; }
        public string StatusNachricht { get => _StatusNachricht; set => _StatusNachricht = value; }
        public Statuses Status { get => _Status; set => _Status = value; }
    }
}
