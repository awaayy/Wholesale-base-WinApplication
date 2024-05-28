using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;
using UPpril.View.AddWindows;
using UPpril.View.EditWindows;

namespace UPpril
{
    public partial class StorekeeperOrdersWindow : Window
    {
        private readonly opt_baseContext _context;

        public ObservableCollection<Order> Orders { get; set; }
        public StorekeeperOrdersWindow()
        {
            InitializeComponent();
            _context = new opt_baseContext();     
            LoadOrders();
            Closing += OrdersWindow_Closing;
        }

        private void LoadOrders()
        {
            Orders = new ObservableCollection<Order>(_context.Orders.ToList());
            StorekeeperOrdersDataGrid.ItemsSource = Orders;
        }

        private void RefreshOrders()
        {
            Orders = new ObservableCollection<Order>(_context.Orders.ToList());
            StorekeeperOrdersDataGrid.ItemsSource = Orders;
        }

        private void OrdersWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StorekeeperMenu storeKeeperMenu = new StorekeeperMenu();
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            AddOrder addOrder = new AddOrder(Orders);
            addOrder.ShowDialog();            
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (StorekeeperOrdersDataGrid.SelectedItem is Order selectedOrder)
            {
                EditOrder editOrderWindow = new EditOrder(selectedOrder.Order_ID, Orders);
                editOrderWindow.ShowDialog();

                RefreshOrders();
            }
            else
            {
                MessageBox.Show("Необходимо выбрать строку таблицы для дальнейшего редактирования.");
            }
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (StorekeeperOrdersDataGrid.SelectedItem is Order selectedOrder)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот заказ?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Orders.Remove(selectedOrder);
                        _context.SaveChanges();

                        Orders.Remove(selectedOrder);
                        MessageBox.Show("Заказ успешно удален!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления заказа: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать строку таблицы для удаления.");
            }
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(searchText))
            {
                var filteredOrders = _context.Orders
                    .Where(o => o.Order_Date.ToString().ToLower().Contains(searchText))
                    .ToList();

                Orders.Clear();
                foreach (var order in filteredOrders)
                {
                    Orders.Add(order);
                }
            }
            else
            {
                LoadOrders();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;

            RefreshOrders();
        }
    }
}
