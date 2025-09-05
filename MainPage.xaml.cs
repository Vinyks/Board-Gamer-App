using Java.Util.Functions;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics;

namespace Board_Gamer_App
{
    public partial class MainPage : ContentPage
    {
        private Event _NextTermin;
        public MainPage()
        {
            InitializeComponent();
            var items = new List<Event>
            {
                new Event("Huber", new TimeOnly(18, 30), new DateOnly(2025,9,1)),
                new Event("Müller", new TimeOnly(16, 45), new DateOnly(2025,9,4)),
                new Event("Musterman", new TimeOnly(17, 00), new DateOnly(2025,9,8)),
                new Event("Gleiss", new TimeOnly(19, 15), new DateOnly(2025,9,10)),
                new Event("Huber", new TimeOnly(18, 30), new DateOnly(2025,9,16)),
                new Event("Müller", new TimeOnly(17, 45), new DateOnly(2025,9,24)),
                new Event("Musterman", new TimeOnly(18, 10), new DateOnly(2025,9,26)),
                new Event("Gleiss", new TimeOnly(14, 25), new DateOnly(2025,9,30)),

                new Event("Huber", new TimeOnly(14, 30), new DateOnly(2025,9,1)),
                new Event("Müller", new TimeOnly(12, 45), new DateOnly(2025,9,4)),
                new Event("Musterman", new TimeOnly(14, 00), new DateOnly(2025,4,8)),
                new Event("Gleiss", new TimeOnly(13, 15), new DateOnly(2025,6,10)),
                new Event("Huber", new TimeOnly(14, 30), new DateOnly(2025,2,16)),
                new Event("Müller", new TimeOnly(10, 45), new DateOnly(2025,3,24)),
                new Event("Musterman", new TimeOnly(13, 10), new DateOnly(2025,7,26)),
                new Event("Gleiss", new TimeOnly(19, 25), new DateOnly(2025,1,30)),

                new Event("Huber", new TimeOnly(14, 30), new DateOnly(2026,9,1)),
                new Event("Müller", new TimeOnly(12, 45), new DateOnly(2026,9,4)),
                new Event("Musterman", new TimeOnly(14, 00), new DateOnly(2026,4,8)),
                new Event("Gleiss", new TimeOnly(13, 15), new DateOnly(2026,6,10)),
                new Event("Huber", new TimeOnly(14, 30), new DateOnly(2026,2,16)),
                new Event("Müller", new TimeOnly(10, 45), new DateOnly(2026,3,24)),
                new Event("Musterman", new TimeOnly(13, 10), new DateOnly(2026,7,26)),
                new Event("Gleiss", new TimeOnly(19, 25), new DateOnly(2026,1,30)),

                new Event("Huber", new TimeOnly(14, 30), new DateOnly(2026,9,1)),
                new Event("Müller", new TimeOnly(12, 45), new DateOnly(2026,9,4)),
                new Event("Musterman", new TimeOnly(14, 00), new DateOnly(2026,4,8)),
                new Event("Gleiss", new TimeOnly(13, 15), new DateOnly(2026,6,10)),
                new Event("Huber", new TimeOnly(14, 30), new DateOnly(2026,2,16)),
                new Event("Müller", new TimeOnly(10, 45), new DateOnly(2026,3,24)),
                new Event("Musterman", new TimeOnly(13, 10), new DateOnly(2026,7,26)),
                new Event("Gleiss", new TimeOnly(19, 25), new DateOnly(2026,1,30)),

                new Event("Huber", new TimeOnly(17, 30), new DateOnly(2025,9,1)),
                new Event("Müller", new TimeOnly(11, 45), new DateOnly(2025,9,4)),
                new Event("Musterman", new TimeOnly(8, 00), new DateOnly(2025,9,8)),
                new Event("Gleiss", new TimeOnly(9, 15), new DateOnly(2025,9,10)),
                new Event("Huber", new TimeOnly(1, 30), new DateOnly(2025,9,16)),
                new Event("Müller", new TimeOnly(3, 45), new DateOnly(2025,9,24)),
                new Event("Musterman", new TimeOnly(4, 10), new DateOnly(2025,9,26)),
                new Event("Gleiss", new TimeOnly(4, 25), new DateOnly(2025,9,30)),

                new Event("Huber", new TimeOnly(17, 30), new DateOnly(2025,2,1)),
                new Event("Müller", new TimeOnly(11, 45), new DateOnly(2025,2,4)),
                new Event("Musterman", new TimeOnly(8, 00), new DateOnly(2025,2,8)),
                new Event("Gleiss", new TimeOnly(9, 15), new DateOnly(2025,2,10)),
                new Event("Huber", new TimeOnly(1, 30), new DateOnly(2025,2,16)),
                new Event("Müller", new TimeOnly(3, 45), new DateOnly(2025,2,24)),
                new Event("Musterman", new TimeOnly(4, 10), new DateOnly(2025,2,26)),
                new Event("Gleiss", new TimeOnly(4, 25), new DateOnly(2025,3,30)),

                new Event("Gleiss", new TimeOnly(20, 25), new DateOnly(2025,9,6)),
            };
            
            items = items.OrderBy(x => x.DateTime).ToList();

            List<Event> pastItems, futureItems; 
            (pastItems, futureItems) = SplitListByTime(items, DateTime.Now);

            pastEventList.ItemsSource = pastItems;
            presentEventList.ItemsSource = futureItems;

            ExecuteFunctionAfterDelay(1, () => ScrollView.ScrollToAsync(ScrollStack.Children[1] as Element, ScrollToPosition.Start, false));
        }

        private async Task ExecuteFunctionAfterDelay(float delayInSeconds, Action function)
        {
            await Task.Delay((int)delayInSeconds*1000);
            function();
        }
        private static Tuple<List<Event>,List<Event>> SplitListByTime(List<Event> events, DateTime spliTtime)
        {
            List<Event> pastEvents = new();
            List<Event> futureEvents = new();

            foreach(Event e in events)
            {
                if(e.DateTime < spliTtime)
                {
                    pastEvents.Add(e);
                }
                else
                {
                    futureEvents.Add(e);
                }
            }

            return new Tuple<List<Event>, List<Event>>(pastEvents,futureEvents);
        }

        private void CalculateLastItemInPast(List<Event> Termine)
        {
            int lastIndex=0;
            for(int i=0; i < Termine.Count; i++)
            {
                if (Termine[i].DateTime > DateTime.Now) break;
                if (Termine[i+1].DateTime > Termine[i].DateTime) lastIndex = i+1;
            }
            Termine[lastIndex].IsFirstItemInFuture = true;
            _NextTermin = Termine[lastIndex];
            Trace.WriteLine(lastIndex);
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
                Event termin = (Event)view.SelectedItem;
                _LastTimeNavigated = DateTime.Now;
                await Navigation.PushAsync(new TerminPage(termin));
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
