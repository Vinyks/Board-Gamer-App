using Android.App;
using Android.Content;
using AndroidX.Work;

namespace Board_Gamer_App.Platforms.Android
{
    public class CheckTimeWorker : Worker
    {
        private Appointment _NextAppointment;
        //ggf. Bool WasSending, oder Worker nach einer Ausführung beenden lassen?

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
                AndroidNotification androidNotification = new("TimerAppointment", "AppointmentNotification", "Timer for checking next Appointment", NotificationImportance.Default);
                androidNotification.DisplayNotification("MeetingPage", "Nächster Termin", "Der Spieleabend beginnt bald!");

            }
        }
    }
}
