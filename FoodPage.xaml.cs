using Microsoft.Maui.Animations;

namespace Board_Gamer_App;

public partial class FoodPage : ContentPage
{
    private List<Cuisine> _Cuisines = new();
    public FoodPage()
    {
        InitializeComponent();

        _Cuisines = new List<Cuisine> {
            new Cuisine("Turkisch", 0),
            new Cuisine("Griechisch", 1),
            new Cuisine("Italienisch", 2),
            new Cuisine("Chinesisch", 3),
            new Cuisine("Japanisch", 4),
            new Cuisine("Deutsch", 5)
        };

        CuisineList.ItemsSource = _Cuisines;
    }

    private int _StartingIndex;
    public void OnDragStarting(object sender, DragStartingEventArgs e)
    {
        if (GetHoveredIndex(e, out int index))
        {
            _StartingIndex = index;
        }
    }

    public void OnDropCompleted(object sender, DropEventArgs e)
    {

    }

    public void OnDragOver(object sender, DragEventArgs e)
    {

    }
    public void OnDrop(object sender, DropEventArgs e)
    {
        if (GetHoveredIndex(e, out int targetIndex))
        {
            MoveItemToIndex(_StartingIndex, targetIndex);
        }

        RefreshList();
    }

    private void MoveItemToIndex(int movingIndex, int targetIndex)
    {
        if (targetIndex == movingIndex) return;

        bool movingDown = targetIndex > movingIndex;

        Cuisine tempCuisine = _Cuisines[movingIndex];

        if (movingDown)
        {
            for (int i = movingIndex; i < targetIndex; i++)
            {
                _Cuisines[i] = _Cuisines[i + 1];
            }
        }
        else
        {
            for (int i = movingIndex; i > targetIndex; i--)
            {
                _Cuisines[i] = _Cuisines[i - 1];
            }
        }
        _Cuisines[targetIndex] = tempCuisine;


        for (int i = 0; i < _Cuisines.Count(); i++)
        {
            _Cuisines[i].Rank = i;
        }
    }

    private bool GetHoveredIndex(DragStartingEventArgs e, out int index)
    {
        Point? p = e.GetPosition(CuisineList);
        if (p.HasValue)
        {
            index = (int)(p.Value.Y / (_BorderHeight + _BorderMargin.VerticalThickness));
            return true;
        }
        index = -1;
        return false;
    }

    private bool GetHoveredIndex(DragEventArgs e, out int index)
    {
        Point? p = e.GetPosition(CuisineList);
        if (p.HasValue)
        {
            index = (int)(p.Value.Y / (_BorderHeight + _BorderMargin.VerticalThickness));
            return true;
        }
        index = -1;
        return false;
    }
    private bool GetHoveredIndex(DropEventArgs e, out int index)
    {
        Point? p = e.GetPosition(CuisineList);
        if (p.HasValue)
        {
            index = (int)(p.Value.Y / (_BorderHeight + _BorderMargin.VerticalThickness));
            return true;
        }
        index = -1;
        return false;
    }

    private double _BorderHeight;
    private Thickness _BorderMargin;
    private void OnBorderSizeChanged(object sender, EventArgs e)
    {
        if (sender is Border element)
        {
            _BorderHeight = element.Height;

            _BorderMargin = element.Margin;
        }
    }
    private void RefreshList()
    {
        CuisineList.ItemsSource = null;
        CuisineList.ItemsSource = _Cuisines;
    }
}