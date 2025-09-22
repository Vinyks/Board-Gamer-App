using Board_Gamer_App.Resources.Values;
using CommunityToolkit.Maui.Core.Extensions;

namespace Board_Gamer_App;

public partial class ParticipantPage : ContentPage
{
    private Appointment _Appointment;

    public ParticipantPage(Appointment appointment)
    {
        _Appointment = appointment;

        for (int i = 0; i < _Appointment.Participants.Count; i++)
        {

            if (_Appointment.Name == _Appointment.Participants[i].Person)
            {
                _Appointment.Participants[i].IsKing = true;
                _Appointment.Participants[i].IsPawn = false;
            }
            else 
            {
                _Appointment.Participants[i].IsKing = false;
                _Appointment.Participants[i].IsPawn = true;
            }
        }
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
        if (participant.Person == PlayerData.PlayerName && participant.Person != _Appointment.Name) //Nur der tatsächliche Spieler darf sich versp舩et melden && nicht der Gastgeber
        {
            await Navigation.PushAsync(new StatusPage(_Appointment, participant));
        }
    }

    public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        NavigateToSchedule(sender, e);
    }
}