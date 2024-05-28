using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;

namespace UPpril.View.EditWindows
{
    public partial class EditWarehouse : Window
    {
        private readonly opt_baseContext _context;
        private readonly int _warehouseId;
        private readonly ObservableCollection<Warehouse> _warehouses;

        public EditWarehouse(int warehouseId, ObservableCollection<Warehouse> warehouses)
        {
            InitializeComponent();
            _context = new opt_baseContext();
            _warehouseId = warehouseId;
            _warehouses = warehouses;
            LoadWarehouseData();
        }

        private void LoadWarehouseData()
        {
            try
            {
                var warehouse = _context.Warehouses.FirstOrDefault(w => w.Warehouse_ID == _warehouseId);
                if (warehouse != null)
                {
                    Warehouse_Edit_Product_NameNameTextBox.Text = warehouse.Product_Name;
                    Warehouse_Edit_Unit_of_MeasuringTextBox.Text = warehouse.Unit_of_Measuring;
                    Warehouse_Edit_QuantityTextBox.Text = warehouse.Quantity.ToString();
                }
                else
                {
                    MessageBox.Show("Склад не найден");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных склада: {ex.Message}");
                this.Close();
            }
        }

        private void EditWarehouse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var warehouse = _context.Warehouses.FirstOrDefault(w => w.Warehouse_ID == _warehouseId);
                if (warehouse != null)
                {
                    warehouse.Product_Name = Warehouse_Edit_Product_NameNameTextBox.Text;
                    warehouse.Unit_of_Measuring = Warehouse_Edit_Unit_of_MeasuringTextBox.Text;

                    if (!int.TryParse(Warehouse_Edit_QuantityTextBox.Text, out int quantity))
                    {
                        MessageBox.Show("Введите правильное числовое значение для количества.");
                        return;
                    }

                    if (quantity <= 0)
                    {
                        MessageBox.Show("Количество должно быть положительным числом.");
                        return;
                    }

                    warehouse.Quantity = quantity;

                    _context.SaveChanges();

                    var warehouseInCollection = _warehouses.FirstOrDefault(w => w.Warehouse_ID == _warehouseId);
                    if (warehouseInCollection != null)
                    {
                        warehouseInCollection.Product_Name = warehouse.Product_Name;
                        warehouseInCollection.Unit_of_Measuring = warehouse.Unit_of_Measuring;
                        warehouseInCollection.Quantity = warehouse.Quantity;
                    }

                    MessageBox.Show("Запись склада обновлена успешно");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Склад не найден");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка редактирования записи склада: {ex.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
