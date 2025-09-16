using Board_Gamer_App.Resources.Values;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace Board_Gamer_App;

public partial class FoodPage : ContentPage
{
    private List<Cuisine> _Cuisines = new();

    private Dictionary<string, List<Menu.Item>> _Menus = new();
    public FoodPage()
    {
        InitializeComponent();

        _Cuisines = new List<Cuisine> {
            new Cuisine("Turkish", 1),
            new Cuisine("Greek", 2),
            new Cuisine("Italian", 3),
            new Cuisine("Chinese", 4),
            new Cuisine("Japanese", 5),
            new Cuisine("German", 6)
        };

        _Menus = MenuListToDictionary(SaveManagement.XMLByteStreamToObject<List<Menu>>(MenusResources.Menus));

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
            _Cuisines[i].Rank = i + 1;
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

    private Menu GetMostPopularMenu()
    {
        string key = GetMostPopularChoiceString();
        return new Menu(key, _Menus[key]);
    }

    private string GetMostPopularChoiceString()
    {
        return _Cuisines[0].Name;
    }

    private void OnOrderButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OrderPage(GetMostPopularMenu()));
    }

    private Dictionary<string, List<Menu.Item>> MenuListToDictionary(List<Menu> menus)
    {
        Dictionary<string, List<Menu.Item>> dictionary = new Dictionary<string, List<Menu.Item>>();
        foreach (Menu menu in menus)
        {
            dictionary[menu.Name] = menu.Items;
        }
        return dictionary;
    }
}