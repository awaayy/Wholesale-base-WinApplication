using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;

namespace UPpril.View.EditWindows
{
    public partial class EditOrder : Window
    {
        private readonly opt_baseContext _context;
        private readonly int _orderId;
        private readonly ObservableCollection<Order> _orders;

        public EditOrder(int orderId, ObservableCollection<Order> orders)
        {
            InitializeComponent();
            _context = new opt_baseContext();
            _orderId = orderId;
            _orders = orders;
            LoadOrderData();
        }

        private void LoadOrderData()
        {
            try
            {
                var order = _context.Orders.FirstOrDefault(o => o.Order_ID == _orderId);
                if (order != null)
                {
                    Order_Edit_Product_IDTextBox.Text = order.Product_ID.ToString();
                    Order_Edit_Order_DateTextBox.Text = order.Order_Date.ToString("dd-MM-yyyy");
                    Order_Edit_Order_Completion_DateTextBox.Text = order.Order_Completion_Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    MessageBox.Show("Заказ не найден.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных о заказе: {ex.Message}");
                this.Close();
            }
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = _context.Orders.FirstOrDefault(o => o.Order_ID == _orderId);
                if (order != null)
                {
                    int product_id = int.Parse(Order_Edit_Product_IDTextBox.Text);
                    DateOnly orderDate = DateOnly.ParseExact(Order_Edit_Order_DateTextBox.Text, "dd-MM-yyyy", null);
                    DateTime orderCompletionDate = DateTime.ParseExact(Order_Edit_Order_Completion_DateTextBox.Text, "dd-MM-yyyy", null);

                    DateTime dateTimeOrderDate = new DateTime(orderDate.Year, orderDate.Month, orderDate.Day);
                    if (orderCompletionDate < dateTimeOrderDate)
                    {
                        MessageBox.Show("Дата выполнения заказа не может быть раньше даты заказа.");
                        return;
                    }

                    order.Product_ID = product_id;
                    order.Order_Date = orderDate;
                    order.Order_Completion_Date = orderCompletionDate;

                    _context.SaveChanges();

                    var orderInCollection = _orders.FirstOrDefault(o => o.Order_ID == _orderId);
                    if (orderInCollection != null)
                    {
                        orderInCollection.Product_ID = order.Product_ID;
                        orderInCollection.Order_Date = order.Order_Date;
                        orderInCollection.Order_Completion_Date = order.Order_Completion_Date;
                    }

                    MessageBox.Show("Данные заказа обновлены!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Заказ не найден.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка редактирования данных заказа: {ex.Message}");
            }
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
