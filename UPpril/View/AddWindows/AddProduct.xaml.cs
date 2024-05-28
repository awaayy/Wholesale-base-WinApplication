
using System;
using System.Windows;
using UPpril.Models;
using System.Collections.ObjectModel;

namespace UPpril.View.AddWindows
{
    public partial class AddProduct : Window
    {
        private readonly opt_baseContext _context;
        private readonly ObservableCollection<Product> _products;

        public AddProduct(ObservableCollection<Product> products)
        {
            InitializeComponent();
            _context = new opt_baseContext();
            _products = products;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string product_name = Product_NameTextBox.Text;

                if (!IsLettersOnly(product_name))
                {
                    MessageBox.Show("Название продукта должно содержать только буквы.");
                    return;
                }

                if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Введите правильное положительное значение для количества.");
                    return;
                }

                if (!decimal.TryParse(CostTextBox.Text, out decimal cost) || cost <= 0)
                {
                    MessageBox.Show("Введите правильное положительное значение для стоимости.");
                    return;
                }

                if (!DateOnly.TryParse(Delivery_DateTextBox.Text, out DateOnly deliveryDate))
                {
                    MessageBox.Show("Введите правильное значение даты в формате ДД.ММ.ГГГГ.");
                    return;
                }

                int? orderId = null;
                if (!string.IsNullOrEmpty(Product_Order_IDTextBox.Text))
                {
                    if (int.TryParse(Product_Order_IDTextBox.Text, out int parsedOrderId))
                    {
                        orderId = parsedOrderId;
                    }
                    else
                    {
                        MessageBox.Show("Введите правильное числовое значение для ID заказа.");
                        return;
                    }
                }

                Product newProduct = new Product
                {
                    Product_Name = product_name,
                    Quantity = quantity,
                    Cost = cost,
                    Delivery_Date = deliveryDate,
                    Order_ID = orderId
                };

                _context.Products.Add(newProduct);
                _context.SaveChanges();

                _products.Add(newProduct);

                MessageBox.Show("Товар добавлен успешно!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления товара в базу данных: {ex.Message}");
            }
        }

        private bool IsLettersOnly(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
