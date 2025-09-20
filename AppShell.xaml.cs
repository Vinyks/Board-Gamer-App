namespace Board_Gamer_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(FoodPage), typeof(FoodPage));
            Routing.RegisterRoute(nameof(ParticipantPage), typeof(ParticipantPage));
            Routing.RegisterRoute(nameof(RatingPage), typeof(RatingPage));
            Routing.RegisterRoute(nameof(VotingPage), typeof(VotingPage));
            Routing.RegisterRoute(nameof(AppointmentPage), typeof(AppointmentPage));
        }
    }
}
