namespace Board_Gamer_App;

public partial class MeetingPage : ContentPage
{
    private bool _StopCountdown;

    public MeetingPage(Appointment t)
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

    private void UpdateTimer(Appointment a)
    {
        while (!_StopCountdown)
        {
            a.UpdateAppointmentStatus();
            DateTime now = DateTime.Now;
            TimeSpan difference = a.DateTime - now;
            if (difference.Days == 0) difference = TimeSpan.FromMinutes(Math.Ceiling(difference.TotalMinutes));
            else difference = TimeSpan.FromDays(Math.Round(difference.TotalDays));

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    TimeLeft.Text = GetTimeString(difference, a.AppointmentStatus);
                });
            if (a.AppointmentStatus == Appointment.AppointmentStatusEnum.Past) return; //Wenn Termin in der Vergangenheit liegt, Methode beenden.
            Task.Delay(1000).Wait();
        }
    }

    //GGF Umbenennen zu "UpdateGUI" und dann später Farben der Buttons Verändern (Grau=> Blockiert, Purple => Klickbar)
    private static string GetTimeString(TimeSpan difference, Appointment.AppointmentStatusEnum appointmentStatus)
    {
        if (appointmentStatus == Appointment.AppointmentStatusEnum.DaysLeft) return string.Format("In {0} Tagen", difference.Days.ToString(), difference.Hours.ToString(), difference.Minutes.ToString());
        else if (appointmentStatus == Appointment.AppointmentStatusEnum.HoursLeft) return string.Format("In {1} Stunden {2} Minuten", difference.Days.ToString(), difference.Hours.ToString(), difference.Minutes.ToString());
        else if (appointmentStatus == Appointment.AppointmentStatusEnum.HoursPast) return "Dieser Termin hat begonnen";
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

    public void OnFoodClicked(object sender, EventArgs e)
    {
        NavigateToPage(sender, e, new FoodPage());
    }
}