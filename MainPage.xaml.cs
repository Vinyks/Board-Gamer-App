using System.Diagnostics;

namespace Board_Gamer_App
{
    public partial class MainPage : ContentPage
    {

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
            };
            terminListe.ItemsSource = Items;
        }

        private async void NavigateToSchedule(object sender, EventArgs e)
        {

            Termine termin = (Termine)terminListe.SelectedItem;
            
            await Navigation.PushAsync(new TerminPage(termin));

        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            NavigateToSchedule(sender, e);
        }
    }
}
