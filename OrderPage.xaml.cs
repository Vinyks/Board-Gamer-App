using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;

namespace Board_Gamer_App;

public partial class OrderPage : ContentPage
{
    private ObservableCollection<Order> _Orders = new();

    private Appointment _CurrentLoadedAppointment;

    public OrderPage(Appointment appointment, Menu menu)
    {
        InitializeComponent();

        _CurrentLoadedAppointment = appointment;
        _Orders = GenerateOrderListFromMenu(menu);

        ItemGrid.ItemsSource = _Orders;
        MenuName.Text = menu.Name;
        UpdateTotal();
    }
    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        _CurrentLoadedAppointment.Orders = _Orders.ToList();
        SaveManagement.SaveObjectAsXML("NextAppointment.xml", _CurrentLoadedAppointment);

        base.OnNavigatedFrom(args);
    }
    private ObservableCollection<Order> GenerateOrderListFromMenu(Menu menu)
    {
        ObservableCollection<Order> orders = _CurrentLoadedAppointment.Orders.ToObservableCollection();

        for (int i = 0; i < orders.Count; i++)
        {
            Menu.Item item = menu.Items[i];
            orders[i] = new Order(item.Name, i, item.Price, _CurrentLoadedAppointment.Orders[i].Amount);
        }

        return orders;
    }

    public void OnAddItemClicked(object sender, EventArgs e)
    {
        if(sender is Button button && button.CommandParameter is Order order)
        {
            Order o = new Order(order.Name, order.ID, order.Price)
            {
                Amount = order.Amount + 1
            };

            _Orders[order.ID] = o;

            UpdateTotal();

        }
    }

    public void OnRemoveItemClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Order order)
        {
            Order o = new Order(order.Name, order.ID, order.Price)
            {
                Amount = order.Amount - 1
            };
            if (o.Amount < 0) o.Amount = 0;
            _Orders[order.ID] = o;

            UpdateTotal();

            ItemGrid.SelectedItem = null;
        }
    }

    private void UpdateTotal()
    {
        float totalPrice = GetCurrentTotal();

        Total.Text = totalPrice.ToString("n2")+"€";
    }

    private float GetCurrentTotal()
    {
        float total = 0;

        foreach (Order o in _Orders)
        {
            total += o.Amount * o.Price;
        }

        return total;
    }
}