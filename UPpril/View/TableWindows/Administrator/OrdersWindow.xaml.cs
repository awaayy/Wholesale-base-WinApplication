using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using UPpril.Models;
using UPpril.View.AddWindows;
using UPpril.View.EditWindows;

namespace UPpril
{
    public partial class OrdersWindow : Window
    {
        private readonly opt_baseContext _context;
        public ObservableCollection<Order> Orders { get; set; }
        public OrdersWindow()
        {
            _context = new opt_baseContext();

            InitializeComponent();
            LoadOrders();
            Closing += OrdersWindow_Closing;
        }

        private void LoadOrders()
        {
            Orders = new ObservableCollection<Order>(_context.Orders.ToList());
            OrdersDataGrid.ItemsSource = Orders;
        }

        private void RefreshOrders()
        {
            Orders = new ObservableCollection<Order>(_context.Orders.ToList());
            OrdersDataGrid.ItemsSource = Orders;
        }

        private void OrdersWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            AddOrder addOrder = new AddOrder(Orders);
            addOrder.ShowDialog();
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem is Order selectedOrder)
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
            DbUtility.DeleteItem(Orders, OrdersDataGrid.SelectedItem, _context);
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