namespace Board_Gamer_App;

public partial class VotingPage : ContentPage
{
    private readonly List<BoardGame> _BoardGames = new();

    private bool _DeleteButtonVisible = false;
    private bool _VoteButtonVisible = true;

    private Color _VotedColor = new Color(10, 220, 30);
    private Color _NotVotedColor = new Color(248, 249, 250);

    private Appointment _Appointment;
    public VotingPage(Appointment appointment)
    {
        _Appointment = appointment;
        InitializeComponent();
        GamesCollectionView.ItemsSource = _Appointment.BoardGames;
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
            maxLength: 50
        );


        // Check if user entered a name and didn't cancel
        if (!string.IsNullOrWhiteSpace(gameName))
        {
            AddBoardGame(gameName);
        }
    }

    private void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is BoardGame game)
        {
            _Appointment.BoardGames.Remove(game);

            RefreshList();
        }
    }

    private void AddBoardGame(string gameName)
    {
        BoardGame game = new(gameName, _DeleteButtonVisible, _VoteButtonVisible, _VotedColor);

        _Appointment.BoardGames.Add(game);

        RefreshList();
    }

    public void OnDeleteVisbilityButtonClicked(object sender, EventArgs e)
    {
        _DeleteButtonVisible = !_DeleteButtonVisible;
        _VoteButtonVisible = !_DeleteButtonVisible;

        foreach (BoardGame g in _Appointment.BoardGames)
        {
            g.DeleteButtonVisible = _DeleteButtonVisible;
            g.VoteButtonVisible = _VoteButtonVisible;
        }

        RefreshList();
    }

    public void OnVoteButtonClicked(object sender, EventArgs e)
    {
        if (sender is not Button b) return;
        if (b.CommandParameter is not BoardGame game) return;

        game.IsVoted = !game.IsVoted;
        game.BorderColor = game.IsVoted ? _VotedColor : _NotVotedColor;

        RefreshList();
    }

    private void RefreshList()
    {
        GamesCollectionView.ItemsSource = null;
        GamesCollectionView.ItemsSource = _Appointment.BoardGames;
    }
}