using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SalinasWPF
{
    public partial class MainWindow : Window
    {
        ObservableCollection<OrderItem> orderList = new ObservableCollection<OrderItem>();

        public MainWindow()
        {
            InitializeComponent();
            OrderDataGrid.ItemsSource = orderList;
        }

        private void ItemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemComboBox.SelectedItem is ComboBoxItem selected)
            {
                PriceTextBox.Text = selected.Tag.ToString();
            }
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (ItemComboBox.SelectedItem is ComboBoxItem selectedItem &&
                int.TryParse(QuantityTextBox.Text, out int qty) &&
                double.TryParse(PriceTextBox.Text, out double price))
            {
                orderList.Add(new OrderItem
                {
                    Item = selectedItem.Content.ToString(),
                    Qty = qty,
                    Price = price
                });

                UpdateTotal();
            }
            else
            {
                MessageBox.Show("Please fill in item, quantity, and price correctly.");
            }
        }

        private void UpdateTotal()
        {
            double total = 0;

            foreach (var item in orderList)
            {
                total += item.Total;
            }

            TotalText.Text = $"₱{total:0.00}";
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            orderList.Clear();
            TotalText.Text = "₱0.00";
            QuantityTextBox.Text = "";
            PriceTextBox.Text = "";
            ItemComboBox.SelectedIndex = -1;
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is OrderItem item)
            {
                // Populate fields with selected item
                ItemComboBox.Text = item.Item;
                QuantityTextBox.Text = item.Qty.ToString();
                PriceTextBox.Text = item.Price.ToString();

                // Remove the item so user can re-add with new values
                orderList.Remove(item);
                UpdateTotal();
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is OrderItem item)
            {
                var result = MessageBox.Show($"Delete {item.Item}?", "Confirm Delete", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    orderList.Remove(item);
                    UpdateTotal();
                }
            }
        }
    }

    public class OrderItem
    {
        public string Item { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Total => Qty * Price;
    }
}
