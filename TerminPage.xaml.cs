namespace Board_Gamer_App;

public partial class TerminPage : ContentPage
{
    private bool _StopCountdown;

    public TerminPage(Termine t)
    {
        _StopCountdown = false;
        InitializeComponent();
        Name.Text = t.Name;
        Time.Text = t.Uhrzeit;
        Date.Text = t.Datum;
        Task.Run(() =>
        UpdateTimer(t)
        );

    }

    private void UpdateTimer(Termine t)
    {
        while (!_StopCountdown)
        {
            DateTime now = DateTime.Now;
            TimeSpan difference = t.DateTime - now;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                TimeLeft.Text = string.Format("in {0} Tagen und {1} Stunden {2} Minuten", difference.Days.ToString(), difference.Hours.ToString(), difference.Minutes.ToString());
            });
            Task.Delay(1000).Wait();
        }
    }

    private async void NavigateToPage(object sender, EventArgs e, ContentPage page)
    {
        _StopCountdown = true;
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