#if ANDROID
using Android.OS;
using Board_Gamer_App.Platforms.Android;
#endif
using Board_Gamer_App.Resources.Values;
using System.Diagnostics;

namespace Board_Gamer_App
{
    public partial class MainPage : ContentPage
    {
        //string Spieler = Android.App.Application.Context.GetString(Resource.String.);
        public static Appointment SelectedAppointment;
        private string _Player = PlayerData.PlayerName;
        public static List<Appointment> PastItems, futureItems;



        public MainPage()
        {
            InitializeComponent();

            pastEventList.ItemsSource = PastItems;
            presentEventList.ItemsSource = futureItems;
            ExecuteFunctionAfterDelay(1, () => ScrollView.ScrollToAsync(ScrollStack.Children[1] as Element, ScrollToPosition.Start, false));
#if ANDROID
            AndroidNotification androidNotification = new("Test", "Notification", "Testing", Android.App.NotificationImportance.Default);
            androidNotification.DisplayNotification("RatingPage","Test", "Ich teste gerade");
            androidNotification.DisplayNotification("VotingPage", "Testerino", "Ich teste gerade");
#endif
        }

        private async Task ExecuteFunctionAfterDelay(float delayInSeconds, Action function)
        {
            await Task.Delay((int)delayInSeconds*1000);
            function();
        }

        private void CalculateLastItemInPast(List<Appointment> Termine)
        {
            int lastIndex=0;
            for(int i=0; i < Termine.Count; i++)
            {
                if (Termine[i].DateTime > DateTime.Now) break;
                if (Termine[i+1].DateTime > Termine[i].DateTime) lastIndex = i+1;
            }
            //Termine[lastIndex].IsFirstItemInFuture = true;
            //Trace.WriteLine(lastIndex);
        }

        private async void HandleSelectionNavigation()
        {
            if(NavigationAllowed)
                await NavigateToEvent(presentEventList);
            if (NavigationAllowed)
                await NavigateToEvent(pastEventList);
        }

        private DateTime _LastTimeNavigated;

        //Is true if a request to navigate has been made in the last 0.5 seconds
        private bool NavigationAllowed => ((DateTime.Now - _LastTimeNavigated).TotalSeconds > 0.5f);
        private async Task NavigateToEvent(ListView view)
        {
            //Stops User from clicking on two items at the same time
            if (!NavigationAllowed) return;
            
            
            if (view.SelectedItem != null)
            {
                SelectedAppointment = (Appointment)view.SelectedItem;
                _LastTimeNavigated = DateTime.Now;
                await Navigation.PushAsync(new MeetingPage());
            }
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            HandleSelectionNavigation();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            presentEventList.SelectedItem = null;
            pastEventList.SelectedItem = null;
            
        }
    }
}
