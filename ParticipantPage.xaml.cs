using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;

namespace Board_Gamer_App;

public partial class ParticipantPage : ContentPage
{
    private Appointment _Appointment;

    public ParticipantPage(Appointment appointment)
	{
        _Appointment = appointment;

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

        await Navigation.PushAsync(new StatusPage(_Appointment, participant));
    }

    public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        NavigateToSchedule(sender, e);
    }
}