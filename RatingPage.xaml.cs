using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
namespace Board_Gamer_App;

public partial class RatingPage : ContentPage
{
    private Appointment _Appointment;
    public RatingPage(Appointment appointment)
    {
        _Appointment = appointment;

        InitializeComponent();
        RatingCollectionView.ItemsSource = _Appointment.Ratings;
        UpdateAverageRating();
    }
    void RatingViewRatingChanged(object sender, RatingChangedEventArgs e)
    {
        if (sender is RatingView ratingView)
        {
            Rating rating = ratingView.BindingContext as Rating;

            _Appointment.Ratings[rating.ID].RatingValue = (int)e.Rating;
        }
        UpdateAverageRating();
    }

    private void UpdateAverageRating()
    {
        AverageRatingView.Rating = AverageRating;
    }

    private float AverageRating
    {
        get
        {
            float total = 0;

            foreach (Rating r in _Appointment.Ratings)
            {
                total += r.RatingValue;
            }

            return total / (float)_Appointment.Ratings.Count();
        }
    }
}