namespace Board_Gamer_App;

public partial class TerminPage : ContentPage
{
    private bool _StopCountdown;

    public TerminPage(Event t)
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
    enum TimeWaiting
    {
        DaysLeft,
        NoDaysLeft,
        Past
    }

    private void UpdateTimer(Event t)
    {
        TimeWaiting timeWaiting;
        while (!_StopCountdown)
        {
            DateTime now = DateTime.Now;
            TimeSpan difference = t.DateTime - now;
            difference = TimeSpan.FromMinutes(Math.Ceiling(difference.TotalMinutes));

            if (0 > difference.Days) timeWaiting = TimeWaiting.Past; 
            else if (0 < difference.Days) timeWaiting = TimeWaiting.DaysLeft;
            else timeWaiting = TimeWaiting.NoDaysLeft;

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    TimeLeft.Text = GetTimeString(difference, timeWaiting);
                });

            if (timeWaiting == TimeWaiting.Past) return; //Wenn Termin in der Vergangenheit liegt, Methode beenden.
            Task.Delay(1000).Wait();
        }
    }

    private static string GetTimeString(TimeSpan difference, TimeWaiting timeWaiting)
    {
        if (timeWaiting == TimeWaiting.DaysLeft) return string.Format("In {0} Tagen", difference.Days.ToString(), difference.Hours.ToString(), difference.Minutes.ToString());
        else if (timeWaiting == TimeWaiting.NoDaysLeft) return string.Format("In {1} Stunden {2} Minuten", difference.Days.ToString(), difference.Hours.ToString(), difference.Minutes.ToString());
        else return "Dieser Termin liegt in der Vergangenheit";
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