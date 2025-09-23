using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

        private bool _IsPawn, _IsKing;
        private string _Person, _StatusNachricht, _ImagePath;
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

        public bool IsPawn {  get => _IsPawn; set => _IsPawn = value; }
        public bool IsKing { get => _IsKing; set => _IsKing = value; }
        public string Person { get => _Person; set => _Person = value; }
        public string ImagePath { get => _ImagePath; set => _ImagePath = value; }
        public string StatusNachricht { get => _StatusNachricht; set => _StatusNachricht = value; }
        public Statuses Status { get => _Status; set => _Status = value; }

    }
}
