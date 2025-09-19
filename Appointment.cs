namespace Board_Gamer_App
{
    public class Appointment : IRandomizeable
    {
        private string _Name;
        string _Time;
        string _Day;
        DateTime _DateTime;
        AppointmentStatusEnum _AppointmentStatus;
        private List<Participant> _Participants;
        List<Cuisine> _Cuisines;
        List<Order> _Orders;
        bool _IsCuisineVoted = false;

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

        public Appointment(string name, TimeOnly uhrzeit, DateOnly datum, List<Participant> participants, List<Cuisine> cuisines, List<Order> orders)
        {
            _Name = name;
            _Time = uhrzeit.ToString("HH:mm");
            _Day = datum.ToString("dd.MM.yyyy");
            _DateTime = new DateTime(datum, uhrzeit);
            _Participants = participants;
            _Cuisines = cuisines;
            _Orders = orders;
        }

        public string Name { get => _Name; set => _Name = value; }
        public string Uhrzeit { get => _Time; set => _Time = value; }
        public string Datum { get => _Day; set => _Day = value; }
        public DateTime DateTime { get => _DateTime; set => _DateTime = value; }
        public AppointmentStatusEnum AppointmentStatus { get => _AppointmentStatus; set => _AppointmentStatus = value; }
        public List<Participant> Participant { get => _Participants; set => _Participants = value; }
        public List<Cuisine> Cuisine { get => _Cuisines; set => _Cuisines = value; }
        public List<Order> Order { get => _Orders; set => _Orders = value; }
        public bool IsCuisineVoted { get => _IsCuisineVoted; set => _IsCuisineVoted = value; }

        public void UpdateAppointmentStatus()
        {
            DateTime now = DateTime.Now;
            TimeSpan difference = _DateTime - now;
            difference = TimeSpan.FromMinutes(Math.Ceiling(difference.TotalMinutes));

            if (difference.Days == 0 && !difference.ToString().Contains("-")) _AppointmentStatus = AppointmentStatusEnum.HoursLeft;
            else if (difference.Days > 0) _AppointmentStatus = AppointmentStatusEnum.DaysLeft;
            else if (difference.Days == 0 && difference.Hours > -12 && difference.ToString().Contains("-")) _AppointmentStatus = AppointmentStatusEnum.HoursPast;
            else _AppointmentStatus = AppointmentStatusEnum.Past;
        }

        private readonly string[] _CuisinesReadonly = ["Turkish", "Greek", "Italian", "Chinese", "Japanese", "German"];
        public void Randomize()
        {
            Random random = new();

            _Name = "Random";
            TimeOnly time = new TimeOnly(random.Next(0, 24), 5 * random.Next(0, 12));
            DateOnly date = new DateOnly(2025,random.Next(1, 9), random.Next(1, 29));
            _Day = date.ToString("dd.MM.yyyy");
            _Time = time.ToString("HH:mm");
            _DateTime = new DateTime(date, time);

            Participant[] participants = new Participant[random.Next(1, 6)];
            for (int i = 0; i < participants.Length; i++)
            {
                participants[i] = new Participant();
            }
            _Participants = [.. participants];

            Cuisine[] cuisines = new Cuisine[6];
            List<int> ranks = new List<int>() {1,2,3,4,5,6};
            for (int i = 0; i < cuisines.Length; i++)
            {
                cuisines[i] = new Cuisine(_CuisinesReadonly[i], ranks[random.Next(0, ranks.Count())]);
                ranks.Remove(cuisines[i].Rank);
            }
            _Cuisines = [.. cuisines];

            Order[] orders = new Order[8];
            for (int i = 0; i < participants.Length; i++)
            {
                orders[i] = new Order();
                orders[i].Randomize();
            }
            _Orders = [.. orders];
        }
    }
}
