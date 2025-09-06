namespace Board_Gamer_App;

public partial class ParticipantPage : ContentPage
{
	public ParticipantPage()
	{
		InitializeComponent();
        var Items = new List<Teilnehmer>
            {
                new Teilnehmer("Laura", "Kommt"),
                new Teilnehmer("Willhelm", "Verspätung"),
                new Teilnehmer("Dirk", "Kommt nicht")
            };
        teilnehmerListe.ItemsSource = Items;
    }

    private async void NavigateToSchedule(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StatusPage());
    }

    public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        NavigateToSchedule(sender, e);
    }
}