using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Work;
using Board_Gamer_App.Platforms.Android;

namespace Board_Gamer_App
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HandleIntent(Intent);
            InitializeAppointments();
            OneTimeWorkRequest oneTimeCheck = OneTimeWorkRequest.Builder.From<CheckTimeWorker>().Build();
            WorkManager.GetInstance(this).Enqueue(oneTimeCheck);
            PeriodicWorkRequest workerRequest = PeriodicWorkRequest.Builder.From<CheckTimeWorker>(TimeSpan.FromMinutes(15)).Build();
            WorkManager.GetInstance(this).EnqueueUniquePeriodicWork("TimeChecking", ExistingPeriodicWorkPolicy.Update, workerRequest);
        }
        private void InitializeAppointments()
        {
            List<Appointment> pastItems = GetPastAppointments().OrderBy(x => x.DateTime).ToList();
            List<Appointment> futureItems = GetFutureAppointments().OrderBy(x => x.DateTime).ToList();

            SaveManagement.SaveObjectAsXML("NextAppointment.xml", futureItems[0]);
            MainPage.PastItems = pastItems;
            MainPage.futureItems = futureItems;
        }


        private readonly string[] _CuisinesReadonly = ["Turkish", "Greek", "Italian", "Chinese", "Japanese", "German"];
        private readonly string[] _PlayersReadonly = ["Huber", "Laura", "Willhelm", "Mustermann", "Gleiss", "Müller"];

        private List<Appointment> GetFutureAppointments()
        {
            Appointment[] appointments = new Appointment[_PlayersReadonly.Length];
            for(int i = 0; i < appointments.Length; i++)
            {
                int daysInTheFuture = i * 7;

                DateOnly dateOnly = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                dateOnly = dateOnly.AddDays(daysInTheFuture);

                appointments[i] = new Appointment(_PlayersReadonly[i], new TimeOnly(18,00),
                    dateOnly, HostOnTop(GetParticipants(), _PlayersReadonly[i]), GetCuisines(), GetOrders(), new(), GetRatings());
            }
            return appointments.ToList();
        }

        private List<Rating> GetRatings()
        {
            return new List<Rating>
            {
                new Rating("Wie war der Abend?", 0, 0),
                new Rating("Wie war das Spiel?", 0, 1),
                new Rating("Wie war das Essen?", 0, 2),
            };
        }

        private List<Participant> GetParticipants()
        {
            Participant[] participants = new Participant[_PlayersReadonly.Length];

            for(int i = 0; i < _PlayersReadonly.Length; i++)
            {
                participants[i] = new Participant(_PlayersReadonly[i], Participant.Statuses.Kommt);
            }

            return participants.ToList();
        }

        private List<Participant> HostOnTop(List<Participant> participants, string hostName)
        {
            List<Participant> partipants = participants.ToList();
            for (int i = 0; i < partipants.Count(); i++)
            {
                if (hostName == partipants[i].Person)
                {
                    (partipants[i], partipants[0]) = (partipants[0], partipants[i]);
                }
            }

            return partipants;
        }

        private List<Cuisine> GetCuisines()
        {
            Cuisine[] cuisines = new Cuisine[_CuisinesReadonly.Length];

            for (int i = 0; i < _CuisinesReadonly.Length; i++)
            {
                cuisines[i] = new Cuisine(_CuisinesReadonly[i], i + 1);
            }

            return cuisines.ToList();
        }

        private List<Order> GetOrders()
        {
            Order[] orders = new Order[8];

            for (int i = 0; i < orders.Length; i++)
            {
                orders[i] = new Order(0);
            }

            return orders.ToList();
        }

        private List<Appointment> GetPastAppointments()
        {
            Appointment[] appointments = new Appointment[25];

            for (int i = 0; i < appointments.Length; i++)
            {
                appointments[i] = new Appointment();
                appointments[i].Randomize();
            }

            return appointments.ToList();
        }

        private static Tuple<List<Appointment>, List<Appointment>> SplitListByTime(List<Appointment> events, DateTime spliTtime)
        {
            List<Appointment> pastEvents = new();
            List<Appointment> futureEvents = new();

            foreach (Appointment e in events)
            {
                if (e.DateTime < spliTtime)
                {
                    pastEvents.Add(e);
                }
                else
                {
                    futureEvents.Add(e);
                }
            }

            return new Tuple<List<Appointment>, List<Appointment>>(pastEvents, futureEvents);
        }

        protected override void OnNewIntent(Intent? intent)
        {
            base.OnNewIntent(intent);

            HandleIntent(intent);
        }

        private void HandleIntent(Intent intent)
        {
            if (intent?.HasExtra("navigateTo") == true)
            {
                MainPage.SelectedAppointment = SaveManagement.ReadObjectFromXML<Appointment>("NextAppointment.xml");
                string targetPage = intent.GetStringExtra("navigateTo");
                MainThread.BeginInvokeOnMainThread(async () =>
                await Shell.Current.GoToAsync(targetPage)
                );
            }
        }
    }
}
