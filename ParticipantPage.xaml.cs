namespace Board_Gamer_App;

public partial class ParticipantPage : ContentPage
{
	public ParticipantPage()
	{
		InitializeComponent();
        var Items = new List<Teilnehmer>
            {
                new Teilnehmer("Laura", "Kommt"),
                new Teilnehmer("Wilhelm", "Versp�tung"),
                new Teilnehmer("Dirk", "Kommt nicht")
            };
        teilnehmerListe.ItemsSource = Items;
    }

}