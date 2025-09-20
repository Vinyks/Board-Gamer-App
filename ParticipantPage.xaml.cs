using Board_Gamer_App.Resources.Values;
using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;

namespace Board_Gamer_App;

public partial class ParticipantPage : ContentPage
{
    private Appointment _Appointment;

    public ParticipantPage()
	{
        _Appointment = MainPage.SelectedAppointment;

        InitializeComponent();
        ParticipantList.ItemsSource = _Appointment.Participants.ToObservableCollection();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        ParticipantList.ItemsSource = _Appointment.Participants.ToObservableCollection();
        base.OnNavigatedTo(args);
    }

    private async void NavigateToSchedule(object sender, EventArgs e)
    {
        Participant participant = (Participant)ParticipantList.SelectedItem;
        if (participant.Person == PlayerData.PlayerName && participant.Person != _Appointment.Name) //Nur der tatsächliche Spieler darf sich verspätet melden && nicht der Gastgeber
        {
            await Navigation.PushAsync(new StatusPage(_Appointment, participant));
        }
    }

    public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        NavigateToSchedule(sender, e);
    }
}