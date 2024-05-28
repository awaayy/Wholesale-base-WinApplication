using System;
using System.Windows;
using UPpril.Models;
using System.Collections.ObjectModel;

namespace UPpril.View.AddWindows
{
    public partial class AddOrder : Window
    {
        private readonly opt_baseContext _context;
        private readonly ObservableCollection<Order> _orders;

        public AddOrder(ObservableCollection<Order> orders)
        {
            InitializeComponent();
            _context = new opt_baseContext();
            _orders = orders;
        }


        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int product_id = int.Parse(Order_Product_IDTextBox.Text);
                DateOnly orderDate = DateOnly.Parse(Order_DateTextBox.Text);
                DateTime orderCompletionDate = DateTime.Parse(Order_Completion_DateTextBox.Text);


                DateTime dateTimeOrderDate = new DateTime(orderDate.Year, orderDate.Month, orderDate.Day);
                if (orderCompletionDate < dateTimeOrderDate)
                {
                    MessageBox.Show("Дата выполнения заказа не может быть раньше даты заказа.");
                    return;
                }

                Order newOrder = new Order
                {
                    Product_ID = product_id,
                    Order_Date = orderDate,
                    Order_Completion_Date = orderCompletionDate
                };

                _context.Orders.Add(newOrder);
                _context.SaveChanges();

                _orders.Add(newOrder);

                MessageBox.Show("Заказ добавлен успешно!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления заказа в базу данных: {ex.Message}");
            }
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
