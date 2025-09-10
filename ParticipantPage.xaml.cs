namespace Board_Gamer_App;

public partial class ParticipantPage : ContentPage
{
	public ParticipantPage()
	{
		InitializeComponent();
        var Participant = new List<Participant>
            {
                new Participant("Laura", "Kommt"),
                new Participant("Willhelm", "Verspätung"),
                new Participant("Dirk", "Kommt nicht")
            };
        TeilnehmerListe.ItemsSource = Participant;
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