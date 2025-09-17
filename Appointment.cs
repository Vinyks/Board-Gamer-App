using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_Gamer_App
{
    public class Appointment 
    {
        private string _Name;
        string _Uhrzeit;
        string _Datum;
        DateTime _DateTime;
        AppointmentStatusEnum _AppointmentStatus;
        private List<Participant> _Participant;
        List<Cuisine> _Cuisine;
        List<Order> _Order;

        public enum AppointmentStatusEnum
        {
            DaysLeft,
            HoursLeft,
            Past,
            HoursPast,
        }

        public Appointment()
        {

        }

        public Appointment(string name, TimeOnly uhrzeit, DateOnly datum, List<Participant> participant, List<Cuisine> cuisine, List<Order> order)
        {
            _Name = name;
            _Uhrzeit = uhrzeit.ToString("HH:mm");
            _Datum = datum.ToString("dd.MM.yyyy");
            _DateTime = new DateTime(datum, uhrzeit);
            _Participant = participant;
            _Cuisine = cuisine;
            _Order = order;
        }

        public string Name { get => _Name; set => _Name = value; }
        public string Uhrzeit { get => _Uhrzeit; set => _Uhrzeit = value; }
        public string Datum { get => _Datum; set => _Datum = value; }
        public DateTime DateTime { get => _DateTime; set => _DateTime = value; }
        public AppointmentStatusEnum AppointmentStatus { get => _AppointmentStatus; set => _AppointmentStatus = value; }
        public List<Participant> Participant { get => _Participant; set => _Participant = value; }
        public List<Cuisine> Cuisine { get => _Cuisine; set => _Cuisine = value;}
        public List<Order> Order { get => _Order; set => _Order = value; }

        public void UpdateAppointmentStatus()
        {
            DateTime now = DateTime.Now;
            TimeSpan difference = _DateTime - now;
            difference = TimeSpan.FromMinutes(Math.Ceiling(difference.TotalMinutes));

            if (difference.Days == 0 && !difference.ToString().Contains("-")) _AppointmentStatus = AppointmentStatusEnum.HoursLeft;
            else if (difference.Days > 0) _AppointmentStatus = AppointmentStatusEnum.DaysLeft;
            else if (difference.Days == 0 && difference.Hours > - 12 && difference.ToString().Contains("-")) _AppointmentStatus = AppointmentStatusEnum.HoursPast;
            else _AppointmentStatus = AppointmentStatusEnum.Past;
        }
    }
}
