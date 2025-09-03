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
                new Termine(1,"Haslach", "12uhr"),
                new Termine(5,"Zell", "18uhr"),
                new Termine(10,"Dirk", "6uhr")
            };
            terminListe.ItemsSource = Items;
        }

        private async void NavigateToSchedule(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TerminPage());
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            NavigateToSchedule(sender, e);
        }
    }
}
