namespace Board_Gamer_App;

public partial class TerminPage : ContentPage
{
	public TerminPage()
	{
		InitializeComponent();
	}
    private async void NavigateToPage(object sender, EventArgs e, ContentPage page)
    {
        await Navigation.PushAsync(page);
    }

    public void OnVotingClicked(object sender, EventArgs e)
	{
		NavigateToPage(sender, e, new VotingPage());
	}

    public void OnParticipantsClicked(object sender, EventArgs e)
    {
        NavigateToPage(sender, e, new ParticipantPage());
    }

    public void OnRatingClicked(object sender, EventArgs e)
    {
        NavigateToPage(sender, e, new RatingPage());
    }
}