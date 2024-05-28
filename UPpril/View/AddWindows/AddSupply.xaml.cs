using System;
using System.Windows;
using UPpril.Models;
using System.Collections.ObjectModel;

namespace UPpril.View.AddWindows
{
    public partial class AddSupply : Window
    {
        private readonly opt_baseContext _context;
        private readonly ObservableCollection<Supply> _supplies;

        public AddSupply(ObservableCollection<Supply> supplies)
        {
            InitializeComponent();
            _context = new opt_baseContext();
            _supplies = supplies;
        }

        private void AddSupply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Supply_Product_IDTextBox.Text))
                {
                    MessageBox.Show("Product_ID не должен быть пустым.");
                    return;
                }
                if (!int.TryParse(Supply_Product_IDTextBox.Text, out int productId))
                {
                    MessageBox.Show("Product_ID должен быть целым числом.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Supply_Supplier_IDTextBox.Text))
                {
                    MessageBox.Show("Supplier_ID не должен быть пустым.");
                    return;
                }
                if (!int.TryParse(Supply_Supplier_IDTextBox.Text, out int supplierId))
                {
                    MessageBox.Show("Supplier_ID должен быть целым числом.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Supply_Unit_of_MeasuringTextBox.Text))
                {
                    MessageBox.Show("Поле 'Единица измерения' не должно быть пустым.");
                    return;
                }

                if (!decimal.TryParse(Supply_TaxTextBox.Text, out decimal tax))
                {
                    MessageBox.Show("Неправильный формат поля 'Налог'.");
                    return;
                }

                Supply newSupply = new Supply
                {
                    Product_ID = productId,
                    Supplier_ID = supplierId,
                    Unit_of_Measuring = Supply_Unit_of_MeasuringTextBox.Text,
                    Tax = (double)tax
                };

                _context.Supplies.Add(newSupply);
                _context.SaveChanges();

                _supplies.Add(newSupply);

                MessageBox.Show("Поставка добавлена успешно!");
                this.Close();
            }
            catch (Exception ex)
            {
                string errorMessage = "Ошибка добавления поставки в базу данных: ";
                if (ex.InnerException != null)
                {
                    errorMessage += ex.InnerException.Message;
                }
                else
                {
                    errorMessage += ex.Message;
                }
                MessageBox.Show(errorMessage);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
