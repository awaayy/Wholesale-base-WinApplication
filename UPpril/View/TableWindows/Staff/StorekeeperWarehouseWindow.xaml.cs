using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;
using UPpril.View.AddWindows;
using UPpril.View.EditWindows;

namespace UPpril
{
    public partial class StorekeeperWarehouseWindow : Window
    {
        private readonly opt_baseContext _context;

        public ObservableCollection<Warehouse> Warehouses { get; set; }
        public StorekeeperWarehouseWindow()
        {
            InitializeComponent();
            _context = new opt_baseContext();     
            LoadWarehouses();
            Closing += WarehouseWindow_Closing;
        }

        private void LoadWarehouses()
        {
            Warehouses = new ObservableCollection<Warehouse>(_context.Warehouses.ToList());
            StorekeeperWarehouseDataGrid.ItemsSource = Warehouses;
        }

        private void RefreshWarehouses()
        {
            var warehousesList = _context.Warehouses.ToList();
            Warehouses.Clear();
            foreach (var warehouse in warehousesList)
            {
                Warehouses.Add(warehouse);
            }
        }


        private void WarehouseWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StorekeeperMenu storekeeperMenu = new StorekeeperMenu();
        }

        private void AddWarehouse_Click(object sender, RoutedEventArgs e)
        {
            AddWarehouse addWarehouse = new AddWarehouse(Warehouses);
            addWarehouse.Show();
        }

        private void EditWarehouse_Click(object sender, RoutedEventArgs e)
        {
            if (StorekeeperWarehouseDataGrid.SelectedItem is Warehouse selectedWarehouse)
            {
                EditWarehouse editWarehouseWindow = new EditWarehouse(selectedWarehouse.Warehouse_ID, Warehouses);
                editWarehouseWindow.ShowDialog();

                RefreshWarehouses();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите склад для редактирования.");
            }
        }

        private void DeleteWarehouse_Click(object sender, RoutedEventArgs e)
        {
            if (StorekeeperWarehouseDataGrid.SelectedItem is Warehouse selectedWarehouse)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот склад?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Warehouses.Remove(selectedWarehouse);
                        _context.SaveChanges();

                        Warehouses.Remove(selectedWarehouse);
                        MessageBox.Show("Склад успешно удален!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления склада: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите склад для удаления.");
            }
        }



        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(searchText))
            {
                var filteredWarehouses = _context.Warehouses
                    .Where(w => w.Product_Name.ToLower().Contains(searchText))
                    .ToList();

                Warehouses.Clear();
                foreach (var warehouse in filteredWarehouses)
                {
                    Warehouses.Add(warehouse);
                }
            }
            else
            {
                LoadWarehouses();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;

            RefreshWarehouses();
        }
    }
}
