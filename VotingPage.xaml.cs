namespace Board_Gamer_App;

public partial class VotingPage : ContentPage
{
    private readonly List<string> _BoardGames = new();

    public VotingPage()
    {
        InitializeComponent();
        GamesCollectionView.ItemsSource = _BoardGames;
    }

    private async void OnAddButtonClicked(object sender, EventArgs e)
    {
        // Show prompt to get board game name
        string gameName = await DisplayPromptAsync(
            "Add Board Game",
            "Enter the name of the board game:",
            "Add",
            "Cancel",
            "Board game name...",
            maxLength: 50,
            keyboard: Keyboard.Text
        );

        // Check if user entered a name and didn't cancel
        if (!string.IsNullOrWhiteSpace(gameName))
        {
            AddBoardGame(gameName);
        }
    }

    private void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string game)
        {
            _BoardGames.Remove((string)button.CommandParameter);

            RefreshList();
        }
    }

    private void AddBoardGame(string gameName)
    {
        _BoardGames.Add(gameName);

        RefreshList();
    }

    private void RefreshList()
    {
        GamesCollectionView.ItemsSource = null;
        GamesCollectionView.ItemsSource = _BoardGames;
    }
}