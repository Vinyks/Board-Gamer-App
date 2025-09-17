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
            Data inputData = new Data.Builder().PutString("a", "b").Build();
            OneTimeWorkRequest oneTimeCheck = OneTimeWorkRequest.Builder.From<CheckTimeWorker>().Build();
            WorkManager.GetInstance(this).Enqueue(oneTimeCheck);
            PeriodicWorkRequest workerRequest = PeriodicWorkRequest.Builder.From<CheckTimeWorker>(TimeSpan.FromMinutes(15)).Build();
            WorkManager.GetInstance(this).EnqueueUniquePeriodicWork("TimeChecking", ExistingPeriodicWorkPolicy.Update, workerRequest);
        }
        private void InitializeAppointments()
        {
            List<Participant> participant = new List<Participant>{
                new Participant("Laura", "Kommt"),
                new Participant("Willhelm", "Verspätung"),
                new Participant("Dirk", "Kommt nicht")
            };
            List<Order> orders = new List<Order>
            {
                new Order("test12", 0, 5),
                new Order("test123", 0, 5),
                new Order("test1", 0, 5)
            };
            List<Cuisine> cuisines = new List<Cuisine>
            {
                new Cuisine("Italian", 1),
                new Cuisine("Chinese", 1),
                new Cuisine("Italian", 1),
            };

            var items = new List<Appointment>
            {
                new Appointment("Huber", new TimeOnly(18, 30), new DateOnly(2025,9,1), participant, cuisines, orders),
                new Appointment("Müller", new TimeOnly(16, 45), new DateOnly(2025,9,4), participant, cuisines, orders),
                new Appointment("Musterman", new TimeOnly(17, 00), new DateOnly(2025, 9, 8), participant, cuisines, orders),
                new Appointment("Gleiss", new TimeOnly(19, 15), new DateOnly(2025, 9, 10), participant, cuisines, orders),
                new Appointment("Huber", new TimeOnly(18, 30), new DateOnly(2025, 9, 16), participant, cuisines, orders),
                new Appointment("Müller", new TimeOnly(17, 45), new DateOnly(2025, 9, 24), participant, cuisines, orders),
                new Appointment("Musterman", new TimeOnly(18, 10), new DateOnly(2025, 9, 26), participant, cuisines, orders),
                new Appointment("Gleiss", new TimeOnly(14, 25), new DateOnly(2025, 9, 30), participant, cuisines, orders),

                new Appointment("Huber", new TimeOnly(14, 30), new DateOnly(2025, 9, 1), participant, cuisines, orders),
                new Appointment("Müller", new TimeOnly(12, 45), new DateOnly(2025, 9, 4), participant, cuisines, orders),
                new Appointment("Musterman", new TimeOnly(14, 00), new DateOnly(2025, 4, 8), participant, cuisines, orders),
                new Appointment("Gleiss", new TimeOnly(13, 15), new DateOnly(2025, 6, 10), participant, cuisines, orders),
                new Appointment("Huber", new TimeOnly(14, 30), new DateOnly(2025, 2, 16), participant, cuisines, orders),
                new Appointment("Müller", new TimeOnly(10, 45), new DateOnly(2025, 3, 24), participant, cuisines, orders),
                new Appointment("Musterman", new TimeOnly(13, 10), new DateOnly(2025, 7, 26), participant, cuisines, orders),
                new Appointment("Gleiss", new TimeOnly(19, 25), new DateOnly(2025, 1, 30), participant, cuisines, orders),

                new Appointment("Huber", new TimeOnly(14, 30), new DateOnly(2026, 9, 1), participant, cuisines, orders),
                new Appointment("Müller", new TimeOnly(12, 45), new DateOnly(2026, 9, 4), participant, cuisines, orders),
                new Appointment("Musterman", new TimeOnly(14, 00), new DateOnly(2026, 4, 8), participant, cuisines, orders),
                new Appointment("Gleiss", new TimeOnly(13, 15), new DateOnly(2026, 6, 10), participant, cuisines, orders),
                new Appointment("Huber", new TimeOnly(14, 30), new DateOnly(2026, 2, 16), participant, cuisines, orders),
                new Appointment("Müller", new TimeOnly(10, 45), new DateOnly(2026, 3, 24), participant, cuisines, orders),
                new Appointment("Musterman", new TimeOnly(13, 10), new DateOnly(2026, 7, 26), participant, cuisines, orders),
                new Appointment("Gleiss", new TimeOnly(19, 25), new DateOnly(2026, 1, 30), participant, cuisines, orders),

                new Appointment("Huber", new TimeOnly(14, 30), new DateOnly(2026, 9, 1), participant, cuisines, orders),
                new Appointment("Müller", new TimeOnly(12, 45), new DateOnly(2026, 9, 4), participant, cuisines, orders),
                new Appointment("Musterman", new TimeOnly(14, 00), new DateOnly(2026, 4, 8), participant, cuisines, orders),
                new Appointment("Gleiss", new TimeOnly(13, 15), new DateOnly(2026, 6, 10), participant, cuisines, orders),
                new Appointment("Huber", new TimeOnly(14, 30), new DateOnly(2026, 2, 16), participant, cuisines, orders),
                new Appointment("Müller", new TimeOnly(10, 45), new DateOnly(2026, 3, 24), participant, cuisines, orders),
                new Appointment("Musterman", new TimeOnly(13, 10), new DateOnly(2026, 7, 26), participant, cuisines, orders),
                new Appointment("Gleiss", new TimeOnly(19, 25), new DateOnly(2026, 1, 30), participant, cuisines, orders),

                new Appointment("Huber", new TimeOnly(17, 30), new DateOnly(2025, 9, 1), participant, cuisines, orders),
                new Appointment("Müller", new TimeOnly(11, 45), new DateOnly(2025, 9, 4), participant, cuisines, orders),
                new Appointment("Musterman", new TimeOnly(8, 00), new DateOnly(2025, 9, 8), participant, cuisines, orders),
                new Appointment("Gleiss", new TimeOnly(9, 15), new DateOnly(2025, 9, 10), participant, cuisines, orders),
                new Appointment("Huber", new TimeOnly(1, 30), new DateOnly(2025, 9, 16), participant, cuisines, orders),
                new Appointment("Müller", new TimeOnly(3, 45), new DateOnly(2025, 9, 24), participant, cuisines, orders),
                new Appointment("Musterman", new TimeOnly(4, 10), new DateOnly(2025, 9, 26), participant, cuisines, orders),
                new Appointment("Gleiss", new TimeOnly(4, 25), new DateOnly(2025, 9, 30), participant, cuisines, orders),

                new Appointment("Huber", new TimeOnly(17, 30), new DateOnly(2025, 2, 1), participant, cuisines, orders),
                new Appointment("Müller", new TimeOnly(11, 45), new DateOnly(2025, 2, 4), participant, cuisines, orders),
                new Appointment("Musterman", new TimeOnly(8, 00), new DateOnly(2025, 2, 8), participant, cuisines, orders),
                new Appointment("Gleiss", new TimeOnly(9, 15), new DateOnly(2025, 2, 10), participant, cuisines, orders),
                new Appointment("Huber", new TimeOnly(1, 30), new DateOnly(2025, 2, 16), participant, cuisines, orders),
                new Appointment("Müller", new TimeOnly(3, 45), new DateOnly(2025, 2, 24), participant, cuisines, orders),
                new Appointment("Musterman", new TimeOnly(4, 10), new DateOnly(2025, 2, 26), participant, cuisines, orders),
                new Appointment("Gleiss", new TimeOnly(4, 25), new DateOnly(2025, 3, 30), participant, cuisines, orders),

                new Appointment("Gleiss", new TimeOnly(20, 25), new DateOnly(2025, 9, 6), participant, cuisines, orders),
                new Appointment("Gleiss", new TimeOnly(00, 00), new DateOnly(2025, 9, 6), participant, cuisines, orders),
                new Appointment("Gleiss", new TimeOnly(01, 45), new DateOnly(2025, 9, 18), participant, cuisines, orders),
            };
            items = items.OrderBy(x => x.DateTime).ToList();
            List<Appointment> pastItems, futureItems;
            (pastItems, futureItems) = SplitListByTime(items, DateTime.Now);
            SaveManagement.SaveObjectAsXML("NextAppointment.xml", futureItems[0]);
            MainPage.futureItems = futureItems;
            MainPage.PastItems = pastItems;
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
            if(intent?.HasExtra("navigateTo") == true)
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
