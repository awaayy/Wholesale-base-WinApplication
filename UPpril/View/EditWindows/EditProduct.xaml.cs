using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;

namespace UPpril.View.EditWindows
{
    public partial class EditProduct : Window
    {
        private readonly opt_baseContext _context;
        private readonly int _productId;
        private readonly ObservableCollection<Product> _products;

        public EditProduct(int productId, ObservableCollection<Product> products)
        {
            InitializeComponent();
            _context = new opt_baseContext();
            _productId = productId;
            _products = products;
            LoadProductData();
        }

        private void LoadProductData()
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.Product_ID == _productId);
                if (product != null)
                {
                    Product_Edit_Product_NameNameTextBox.Text = product.Product_Name;
                    Product_Edit_QuantityTextBox.Text = product.Quantity.ToString();
                    Product_Edit_CostTextBox.Text = product.Cost.ToString();
                    Product_Edit_Delivery_DateTextBox.Text = product.Delivery_Date.ToString("dd-MM-yyyy");
                    Product_Edit_Order_IDTextBox.Text = product.Order_ID.ToString();
                }
                else
                {
                    MessageBox.Show("Продукт не найден.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных о продукте: {ex.Message}");
                this.Close();
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.Product_ID == _productId);
                if (product != null)
                {
                    product.Product_Name = Product_Edit_Product_NameNameTextBox.Text;

                    if (!IsLettersOnly(product.Product_Name))
                    {
                        MessageBox.Show("Название продукта должно содержать только буквы.");
                        return;
                    }

                    if (!int.TryParse(Product_Edit_QuantityTextBox.Text, out int quantity) || quantity <= 0)
                    {
                        MessageBox.Show("Введите правильное положительное значение для количества.");
                        return;
                    }
                    product.Quantity = quantity;

                    if (!decimal.TryParse(Product_Edit_CostTextBox.Text, out decimal cost) || cost <= 0)
                    {
                        MessageBox.Show("Введите правильное положительное значение для стоимости.");
                        return;
                    }
                    product.Cost = cost;

                    DateOnly deliveryDate;
                    if (DateOnly.TryParse(Product_Edit_Delivery_DateTextBox.Text, out deliveryDate))
                    {
                        product.Delivery_Date = deliveryDate;
                    }
                    else
                    {
                        MessageBox.Show("Неверный формат даты.");
                        return;
                    }

                    if (!string.IsNullOrEmpty(Product_Edit_Order_IDTextBox.Text))
                    {
                        if (int.TryParse(Product_Edit_Order_IDTextBox.Text, out int orderId))
                        {
                            product.Order_ID = orderId;
                        }
                        else
                        {
                            MessageBox.Show("Введите правильное числовое значение для ID заказа.");
                            return;
                        }
                    }

                    _context.SaveChanges();

                    var productInCollection = _products.FirstOrDefault(p => p.Product_ID == _productId);
                    if (productInCollection != null)
                    {
                        productInCollection.Product_Name = product.Product_Name;
                        productInCollection.Quantity = product.Quantity;
                        productInCollection.Cost = product.Cost;
                        productInCollection.Delivery_Date = product.Delivery_Date;
                        productInCollection.Order_ID = product.Order_ID;
                    }

                    MessageBox.Show("Данные продукта обновлены!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Продукт не найден.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка редактирования данных продукта: {ex.Message}");
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
