namespace Board_Gamer_App;

public partial class VotingPage : ContentPage
{
    private readonly List<BoardGame> _BoardGames = new();

    private bool _DeleteButtonVisible = false;
    private bool _VoteButtonVisible = true;

    private Color _VotedColor = Color.FromArgb("#91db69");
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
        if (IsGameAlreadyInList(gameName)) return;

        // Check if user entered a name and didn't cancel
        if (string.IsNullOrWhiteSpace(gameName)) return;

        AddBoardGame(gameName);
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

    private void UpdateMostPopularGame()
    {
        int[] voteCounts = new int[_Appointment.BoardGames.Count()];
        for (int i = 0; i < _Appointment.BoardGames.Count; i++)
        {
            BoardGame g = _Appointment.BoardGames[i];
            voteCounts[i] = 0;

            voteCounts[i] += g.IsVoted ? 1 : 0;

            //Code for grabbing the votes from non-local players
        }
        int highestCount = GetHighestVoteCount(voteCounts);
        List<BoardGame> winnerBoardGames = new();
        for (int i = 0; i < voteCounts.Length; i++)
        {
            if (voteCounts[i] == highestCount) winnerBoardGames.Add(_Appointment.BoardGames[i]);
        }

        winnerBoardGames = winnerBoardGames.OrderBy(x => x.Name).ToList();
        if (winnerBoardGames.Count > 0)
        {
            PopularChoiceLabel.Text = winnerBoardGames[0].Name;
        }
    }

    private int GetHighestVoteCount(int[] voteCounts)
    {
        int highestCount = -1;
        for (int i = 0; i < voteCounts.Length; i++)
        {
            if (voteCounts[i] > highestCount) highestCount = voteCounts[i];
        }
        return highestCount;
    }
    private bool IsGameAlreadyInList(string gameName)
    {
        for (int i = 0; i < _Appointment.BoardGames.Count(); i++)
        {
            if (_Appointment.BoardGames[i].Name == gameName)
            {
                return true;
            }
        }
        return false;
    }
    private void RefreshList()
    {
        UpdateMostPopularGame();
        GamesCollectionView.ItemsSource = null;
        GamesCollectionView.ItemsSource = _Appointment.BoardGames;
    }
}