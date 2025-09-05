using System.Diagnostics;

namespace Board_Gamer_App
{
    public partial class MainPage : ContentPage
    {
        private Termine _NextTermin;
        public MainPage()
        {
            InitializeComponent();
            var Items = new List<Termine>
            {
                new Termine("Huber", new TimeOnly(18, 30), new DateOnly(2025,9,1)),
                new Termine("Müller", new TimeOnly(16, 45), new DateOnly(2025,9,4)),
                new Termine("Musterman", new TimeOnly(17, 00), new DateOnly(2025,9,8)),
                new Termine("Gleiss", new TimeOnly(19, 15), new DateOnly(2025,9,10)),
                new Termine("Huber", new TimeOnly(18, 30), new DateOnly(2025,9,16)),
                new Termine("Müller", new TimeOnly(17, 45), new DateOnly(2025,9,24)),
                new Termine("Musterman", new TimeOnly(18, 10), new DateOnly(2025,9,26)),
                new Termine("Gleiss", new TimeOnly(14, 25), new DateOnly(2025,9,30)),

                new Termine("Huber", new TimeOnly(14, 30), new DateOnly(2025,9,1)),
                new Termine("Müller", new TimeOnly(12, 45), new DateOnly(2025,9,4)),
                new Termine("Musterman", new TimeOnly(14, 00), new DateOnly(2025,4,8)),
                new Termine("Gleiss", new TimeOnly(13, 15), new DateOnly(2025,6,10)),
                new Termine("Huber", new TimeOnly(14, 30), new DateOnly(2025,2,16)),
                new Termine("Müller", new TimeOnly(10, 45), new DateOnly(2025,3,24)),
                new Termine("Musterman", new TimeOnly(13, 10), new DateOnly(2025,7,26)),
                new Termine("Gleiss", new TimeOnly(19, 25), new DateOnly(2025,1,30)),

                new Termine("Huber", new TimeOnly(17, 30), new DateOnly(2025,9,1)),
                new Termine("Müller", new TimeOnly(11, 45), new DateOnly(2025,9,4)),
                new Termine("Musterman", new TimeOnly(8, 00), new DateOnly(2025,9,8)),
                new Termine("Gleiss", new TimeOnly(9, 15), new DateOnly(2025,9,10)),
                new Termine("Huber", new TimeOnly(1, 30), new DateOnly(2025,9,16)),
                new Termine("Müller", new TimeOnly(3, 45), new DateOnly(2025,9,24)),
                new Termine("Musterman", new TimeOnly(4, 10), new DateOnly(2025,9,26)),
                new Termine("Gleiss", new TimeOnly(4, 25), new DateOnly(2025,9,30)),

                new Termine("Huber", new TimeOnly(17, 30), new DateOnly(2025,2,1)),
                new Termine("Müller", new TimeOnly(11, 45), new DateOnly(2025,2,4)),
                new Termine("Musterman", new TimeOnly(8, 00), new DateOnly(2025,2,8)),
                new Termine("Gleiss", new TimeOnly(9, 15), new DateOnly(2025,2,10)),
                new Termine("Huber", new TimeOnly(1, 30), new DateOnly(2025,2,16)),
                new Termine("Müller", new TimeOnly(3, 45), new DateOnly(2025,2,24)),
                new Termine("Musterman", new TimeOnly(4, 10), new DateOnly(2025,2,26)),
                new Termine("Gleiss", new TimeOnly(4, 25), new DateOnly(2025,3,30)),
            };
            
            Items = Items.OrderBy(x => x.DateTime).ToList();
            CalculateLastItemInPast(Items);
            terminListe.ItemsSource = Items;

            terminListe.ScrollTo(_NextTermin, ScrollToPosition.MakeVisible, false); //Funktioniert nicht


        }

        private void CalculateLastItemInPast(List<Termine> Termine)
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

        private async void NavigateToSchedule(object sender, EventArgs e)
        {
            if (terminListe.SelectedItem == null) return;
            Termine termin = (Termine)terminListe.SelectedItem;
            await Navigation.PushAsync(new TerminPage(termin));
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            NavigateToSchedule(sender, e);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            terminListe.SelectedItem = null;
        }
    }
}
