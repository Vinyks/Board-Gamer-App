using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;

namespace Board_Gamer_App.Platforms.Android
{
    public class AndroidNotification
    {
        string _ChannelID, _ChannelName, _ChannelDescription;
        NotificationImportance _NotificationImportance;

        public AndroidNotification(string channelID, string channelName, string channelDescription, NotificationImportance notificationImportance)
        {
            _ChannelID = channelID;
            _ChannelName = channelName;
            _ChannelDescription = channelDescription;
            _NotificationImportance = notificationImportance;
            CreateNotificationChannel();
        }

        public void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationChannel channel = new NotificationChannel(_ChannelID, _ChannelName, _NotificationImportance)
                {
                    Description = _ChannelDescription
                };

                NotificationManager notificationManager = (NotificationManager)Platform.AppContext.GetSystemService(Context.NotificationService);
                notificationManager.CreateNotificationChannel(channel);

            }
        }

        public void DisplayNotification(string notificationTitel, string notificationText, int id=1000, int icon = Resource.Drawable.icon)
        {
            NotificationCompat.Builder builder = new NotificationCompat.Builder(Platform.AppContext, _ChannelID).SetContentTitle(notificationTitel).SetContentText(notificationText).SetSmallIcon(icon).SetAutoCancel(true);
            NotificationManagerCompat manager = NotificationManagerCompat.From(Platform.AppContext);
            manager.Notify(id, builder.Build());
        }
    }
}
