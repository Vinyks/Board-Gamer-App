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


        public enum AppointmentStatusEnum
        {
            DaysLeft,
            HoursLeft,
            Past,
            HoursPast,
        }

        public Appointment(string name, TimeOnly uhrzeit, DateOnly datum)
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
        public AppointmentStatusEnum AppointmentStatus { get => _AppointmentStatus; set => _AppointmentStatus = value; }

        public void UpdateAppointmentStatus()
        {
            DateTime now = DateTime.Now;
            TimeSpan difference = _DateTime - now;
            difference = TimeSpan.FromMinutes(Math.Ceiling(difference.TotalMinutes));

            if (0 == difference.Days && !difference.ToString().Contains("-")) _AppointmentStatus = AppointmentStatusEnum.HoursLeft;
            else if (0 < difference.Days) _AppointmentStatus = AppointmentStatusEnum.DaysLeft;
            else if (-12 < difference.Hours && difference.ToString().Contains("-")) _AppointmentStatus = AppointmentStatusEnum.HoursPast;
            else _AppointmentStatus = AppointmentStatusEnum.Past;
        }
    }
}
