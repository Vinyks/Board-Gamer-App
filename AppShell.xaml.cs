namespace Board_Gamer_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("FoodPage", typeof(FoodPage));
            Routing.RegisterRoute("ParticipantPage", typeof(ParticipantPage));
            Routing.RegisterRoute("RatingPage", typeof(RatingPage));
            Routing.RegisterRoute("VotingPage", typeof(VotingPage));
            Routing.RegisterRoute("MainPage", typeof(MainPage));
        }
    }
}
