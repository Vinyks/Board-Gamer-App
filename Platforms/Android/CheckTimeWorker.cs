using Android.App;
using Android.Content;
using AndroidX.Work;
using Board_Gamer_App.Resources.Values;

namespace Board_Gamer_App.Platforms.Android
{
    public class CheckTimeWorker : Worker
    {
        private Appointment _NextAppointment;
        //ggf. Bool WasSending, oder Worker nach einer Ausführung beenden lassen?
        string _PlayerName = PlayerData.PlayerName;

        public CheckTimeWorker(Context context, WorkerParameters workerParams) : base(context, workerParams)
        {
        }

        public override Result DoWork()
        {
            ReadNextAppointment();
            CheckTime();
            return Result.InvokeSuccess();
        }

        private void ReadNextAppointment()
        {

            _NextAppointment = SaveManagement.ReadObjectFromXML<Appointment>("NextAppointment.xml");
        }

        private void CheckTime()
        {
            
            _NextAppointment.UpdateAppointmentStatus();
            DateTime now = DateTime.Now;
            TimeSpan difference = _NextAppointment.DateTime - now;
            if (difference.Minutes < 30 && difference.Hours == 0 && difference.Days == 0)
            {
                _NextAppointment.IsCuisineVoted = true;
                AndroidNotification androidNotification = new("TimerAppointment", "AppointmentNotification", "Timer for checking next Appointment", NotificationImportance.Default);
                androidNotification.DisplayNotification("MeetingPage", "Nächster Termin", "Der Spieleabend beginnt bald!");

            } 
            if(((difference.Minutes > 30 && difference.Hours == 0 && difference.Days == 0) || (difference.Hours == 1 && difference.Days == 0)) && !_NextAppointment.IsCuisineVoted)
            {
                AndroidNotification androidNotification = new("TimerCuisine", "CuisineNotification", "Timer for remembering Cuisine", NotificationImportance.Default);
                androidNotification.DisplayNotification("FoodPage", "Essensrichtungswahl", "Bitte wählen Sie die Essensrichtung!");
            }
            foreach(Participant p in _NextAppointment.Participant)
            {
                if (p.Status == Participant.Statuses.Verspaetet)
                {
                    AndroidNotification androidNotification = new("DelayedParticipants", "DelayNotification", "Notification for Delays", NotificationImportance.Default);
                    androidNotification.DisplayNotification("ParticipantPage", "Verspätung", p.Person + " verspätet sich!");
                }
                else if (p.Status == Participant.Statuses.Verhindert)
                {
                    AndroidNotification androidNotification = new("AbsentParticipants", "AbsentNotification", "Notification for Absent", NotificationImportance.Default);
                    androidNotification.DisplayNotification("ParticipantPage", "Verhindert", p.Person + " ist verhindert!");
                }
            }
            if(_PlayerName ==  _NextAppointment.Name && _NextAppointment.IsCuisineVoted)
            {
                AndroidNotification androidNotification = new("CuisineReady", "CuisineReadyNotification", "Notification for when Cuisine is Ready", NotificationImportance.Default);
                androidNotification.DisplayNotification("FoodPage", "Essensrichtungswahl", "Die Essensrichtung wurde gewählt!");
            }

        }
    }
}
