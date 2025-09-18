namespace Board_Gamer_App;

public partial class ParticipantPage : ContentPage
{
	public ParticipantPage()
	{
		InitializeComponent();
        var Participant = new List<Participant>
            {
                new Participant("Laura", Board_Gamer_App.Participant.Statuses.Kommt),
                new Participant("Willhelm", Board_Gamer_App.Participant.Statuses.Verspaetet),
                new Participant("Dirk", Board_Gamer_App.Participant.Statuses.Verhindert)
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