using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;
#if ANDROID
using Board_Gamer_App.Platforms.Android;
#endif

namespace Board_Gamer_App;

public partial class StatusPage : ContentPage
{
    private Appointment _CurrentLoadedAppointment;
    private Participant _Participant;

    public StatusPage(Appointment appointment, Participant participant)
	{
        _CurrentLoadedAppointment = appointment;
        _Participant = participant;

        InitializeComponent();
        StatusMessage.Text = _Messages[(int)participant.Status];
    }

    private string[] _Messages = ["Pünktlich", "Verspätet", "Kommt nicht"];

    async void OnOnTimeButtonClicked(object sender, EventArgs e)
	{
        StatusMessage.Text = "Pünktlich";
        _Participant.Status = Participant.Statuses.Kommt;
        _Participant.StatusNachricht = _Messages[(int)_Participant.Status];
#if ANDROID
        AndroidNotification androidNotification = new("Test", "Notification", "Testing", Android.App.NotificationImportance.Default);
        androidNotification.DisplayNotification("ParticipantPage", "Status Aktualisiert", "Ist pünktlich");
#endif
        await DisplayAlert("Status Aktualisiert", "Ich bin pünktlich", "OK");
    }

    async void OnLateButtonClicked(object sender, EventArgs e)
    {
        StatusMessage.Text = "Verspätet";
        string verspaetungsZeit = await DisplayPromptAsync(
            "Verspätung",
            "Geschätzte Verspätung in Minuten:",
            "OK",
            "Abbrechen",
            "... min",
            maxLength: 50
            );
        _Participant.Status = Participant.Statuses.Verspaetet;
        _Participant.StatusNachricht = _Messages[(int)_Participant.Status];
#if ANDROID
        AndroidNotification androidNotification = new("Test", "Notification", "Testing", Android.App.NotificationImportance.Default);
        androidNotification.DisplayNotification("ParticipantPage", "Status Aktualisiert", "Verspätet sich um: "+verspaetungsZeit);
#endif
        await DisplayAlert("Status Aktualisiert", "Ich verspäte mich", "OK");
    }

    async void OnNotArrivingButtonClicked(object sender, EventArgs e)
    {
        StatusMessage.Text = "Kommt nicht";
        _Participant.Status = Participant.Statuses.Verhindert;
        _Participant.StatusNachricht = _Messages[(int)_Participant.Status];
#if ANDROID
        AndroidNotification androidNotification = new("Test", "Notification", "Testing", Android.App.NotificationImportance.Default);
        androidNotification.DisplayNotification("ParticipantPage", "Status Aktualisiert", "Kommt nicht");
#endif
        await DisplayAlert("Status Aktualisiert", "Ich komme nicht", "OK");
    }
}