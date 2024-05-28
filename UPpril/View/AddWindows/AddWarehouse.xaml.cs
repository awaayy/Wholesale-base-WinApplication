using System;
using System.Collections.ObjectModel;
using System.Windows;
using UPpril.Models;

namespace UPpril.View.AddWindows
{
    public partial class AddWarehouse : Window
    {
        private readonly opt_baseContext _context;
        private readonly ObservableCollection<Warehouse> _warehouses;

        public AddWarehouse(ObservableCollection<Warehouse> warehouses)
        {
            InitializeComponent();
            _context = new opt_baseContext();
            _warehouses = warehouses;
        }

        private void AddWarehouse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string productName = Warehouse_Product_NameTextBox.Text;
                string unitOfMeasuring = Warehouse_Unit_of_MeasuringDateTextBox.Text;

                if (!int.TryParse(Warehouse_QuantityDateTextBox.Text, out int quantity))
                {
                    MessageBox.Show("Введите правильное числовое значение для количества.");
                    return;
                }

                if (quantity <= 0)
                {
                    MessageBox.Show("Количество должно быть положительным числом.");
                    return;
                }

                Warehouse newWarehouse = new Warehouse
                {
                    Product_Name = productName,
                    Unit_of_Measuring = unitOfMeasuring,
                    Quantity = quantity
                };

                _context.Warehouses.Add(newWarehouse);
                _context.SaveChanges();

                _warehouses.Add(newWarehouse);

                MessageBox.Show("Товар на склад добавлен успешно!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления товара на склад в базе данных: {ex.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
