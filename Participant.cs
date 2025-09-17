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
        private string _Person, _Status;

        public Participant(string person, string status)
        {
            _Person = person;
            _Status = status;
        }
        public Participant()
        {

        }

        public string Person { get => _Person; set => _Person = value; }
        public string Status { get => _Status; set => _Status = value; }
    }
}
