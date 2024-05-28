using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;
using UPpril.View.AddWindows;
using UPpril.View.EditWindows;

namespace UPpril
{
    public partial class SuppliersWindow : Window
    {
        private readonly opt_baseContext _context;

        public ObservableCollection<Supplier> Suppliers { get; set; }
        public SuppliersWindow()
        {
            InitializeComponent();
            _context = new opt_baseContext();     
            LoadSuppliers();
            Closing += SuppliersWindow_Closing;
        }

        private void LoadSuppliers()
        {
            Suppliers = new ObservableCollection<Supplier>(_context.Suppliers.ToList());
            SuppliersDataGrid.ItemsSource = Suppliers;
        }

        private void RefreshSuppliers()
        {
            Suppliers = new ObservableCollection<Supplier>(_context.Suppliers.ToList());
            SuppliersDataGrid.ItemsSource = Suppliers;
        }

        private void SuppliersWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            AddSupplier addSupplier = new AddSupplier(Suppliers);
            addSupplier.ShowDialog();            
        }

        private void EditSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliersDataGrid.SelectedItem is Supplier selectedSupplier)
            {
                EditSupplier editSupplierWindow = new EditSupplier(selectedSupplier.Supplier_ID, Suppliers);
                editSupplierWindow.ShowDialog();

                RefreshSuppliers();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите поставщика для редактирования.");
            }
        }

        private void DeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliersDataGrid.SelectedItem is Supplier selectedSupplier)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого поставщика?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Suppliers.Remove(selectedSupplier);
                        _context.SaveChanges();

                        Suppliers.Remove(selectedSupplier);
                        MessageBox.Show("Поставщик успешно удален!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления поставщика: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите поставщика для удаления.");
            }
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(searchText))
            {
                var filteredSuppliers = _context.Suppliers
                    .Where(s => s.Telephone.ToLower().Contains(searchText))
                    .ToList();

                Suppliers.Clear();
                foreach (var supplier in filteredSuppliers)
                {
                    Suppliers.Add(supplier);
                }
            }
            else
            {
                LoadSuppliers();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;

            RefreshSuppliers();
        }
    }
}
