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
    public StatusPage()
	{
		InitializeComponent();
    }

    async void OnOnTimeButtonClicked(object sender, EventArgs e)
	{
        StatusMessage.Text = "Pünktlich";
#if ANDROID
        AndroidNotification androidNotification = new("Test", "Notification", "Testing", Android.App.NotificationImportance.Default);
        androidNotification.DisplayNotification("ParticipantPage", "Status Aktualisiert", "Ist pünktlich");
#endif
        await DisplayAlert("Status Aktualisiert", "Ich bin pünktlich", "OK");
    }

    async void OnLateButtonClicked(object sender, EventArgs e)
    {
        StatusMessage.Text = "Verspätet";
#if ANDROID
        AndroidNotification androidNotification = new("Test", "Notification", "Testing", Android.App.NotificationImportance.Default);
        androidNotification.DisplayNotification("ParticipantPage", "Status Aktualisiert", "Verspätet sich");
#endif
        await this.ShowPopupAsync(new Label
        {
            Text = "Status aktualisiert zu: Ich verspäte mich"
        }, new PopupOptions
        {
            CanBeDismissedByTappingOutsideOfPopup = true,
            Shape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(20, 20, 20, 20),
                StrokeThickness = 2,
                Stroke = Colors.White
            }
        });
    }

    async void OnNotArrivingButtonClicked(object sender, EventArgs e)
    {
        StatusMessage.Text = "Kommt nicht";
#if ANDROID
        AndroidNotification androidNotification = new("Test", "Notification", "Testing", Android.App.NotificationImportance.Default);
        androidNotification.DisplayNotification("ParticipantPage", "Status Aktualisiert", "Kommt nicht");
#endif
        await this.ShowPopupAsync(new Label
        {
            Text = "Status aktualisiert zu: Ich komme nicht"
        }, new PopupOptions
        {
            CanBeDismissedByTappingOutsideOfPopup = true,
            Shape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(20, 20, 20, 20),
                StrokeThickness = 2,
                Stroke = Colors.White
            }
        });
    }
}