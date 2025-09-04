using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
namespace Board_Gamer_App;

public partial class RatingPage : ContentPage
{
	public RatingPage()
	{
		InitializeComponent();
        var ratings = new List<RatingItem>
            {
                new RatingItem("Wie war der Abend?", -1),
                new RatingItem("Wie war das Spiel?", 3),
                new RatingItem("Wie war das Essen?", 5),
            };
        RatingCollectionView.ItemsSource = ratings;
        

    }
}