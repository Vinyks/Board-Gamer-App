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
        int _RequestCode = 0, _Id=0;

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

        public void DisplayNotification(string pageName, string notificationTitel, string notificationText, int icon = Resource.Drawable.icon)
        {
             Intent intent = new Intent(Platform.AppContext, typeof(MainActivity));
            intent.PutExtra("navigateTo", pageName);
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);

            PendingIntent pendingIntent = PendingIntent.GetActivity(Platform.AppContext, _RequestCode, intent, PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent);
            _RequestCode++;
            NotificationCompat.Builder builder = new NotificationCompat.Builder(Platform.AppContext, _ChannelID).SetContentTitle(notificationTitel).SetContentText(notificationText).SetSmallIcon(icon).SetAutoCancel(true).SetContentIntent(pendingIntent);
            NotificationManagerCompat manager = NotificationManagerCompat.From(Platform.AppContext);
            manager.Notify(_Id, builder.Build());
            _Id++;
        }
    }
}
