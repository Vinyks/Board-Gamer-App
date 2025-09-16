using System.Collections.ObjectModel;

namespace Board_Gamer_App;

public partial class OrderPage : ContentPage
{
    private ObservableCollection<Order> _Orders = new();

    public OrderPage(Menu menu)
    {
        InitializeComponent();

        _Orders = GenerateOrderListFromMenu(menu);

        ItemGrid.ItemsSource = _Orders;
        MenuName.Text = menu.Name;
    }

    private ObservableCollection<Order> GenerateOrderListFromMenu(Menu menu)
    {
        ObservableCollection<Order> orders = new();

        for (int i = 0; i < menu.Items.Count; i++)
        {
            Menu.Item item = menu.Items[i];
            orders.Add(new Order(item.Name, i, item.Price));
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