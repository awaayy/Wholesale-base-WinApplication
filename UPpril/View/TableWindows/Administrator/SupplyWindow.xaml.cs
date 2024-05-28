using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;
using UPpril.View.AddWindows;
using UPpril.View.EditWindows;

namespace UPpril
{
    public partial class SupplyWindow : Window
    {
        private readonly opt_baseContext _context;

        public ObservableCollection<Supply> Supplies { get; set; }
        public SupplyWindow()
        {
            InitializeComponent();
            _context = new opt_baseContext();     
            LoadSupplies();
            Closing += SupplyWindow_Closing;
        }

        private void LoadSupplies()
        {
            Supplies = new ObservableCollection<Supply>(_context.Supplies.ToList());
            SupplyDataGrid.ItemsSource = Supplies;
        }

        private void RefreshSupplies()
        {
            Supplies = new ObservableCollection<Supply>(_context.Supplies.ToList());
            SupplyDataGrid.ItemsSource = Supplies;
        }

        private void SupplyWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
        }

        private void AddSupply_Click(object sender, RoutedEventArgs e)
        {
            AddSupply addSupply = new AddSupply(Supplies);
            addSupply.ShowDialog();            

            RefreshSupplies();
        }

        private void EditSupply_Click(object sender, RoutedEventArgs e)
        {
            if (SupplyDataGrid.SelectedItem is Supply selectedSupply)
            {
                EditSupply editSupplyWindow = new EditSupply(selectedSupply.Supply_ID, Supplies);
                editSupplyWindow.ShowDialog();

                RefreshSupplies();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите поставку для редактирования.");
            }
        }

        private void DeleteSupply_Click(object sender, RoutedEventArgs e)
        {
            if (SupplyDataGrid.SelectedItem is Supply selectedSupply)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту поставку?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Supplies.Remove(selectedSupply);
                        _context.SaveChanges();

                        Supplies.Remove(selectedSupply);
                        MessageBox.Show("Поставка успешно удалена!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления поставки: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите поставку для удаления.");
            }
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(searchText))
            {
                var filteredSupplies = _context.Supplies
                    .Where(sup => sup.Unit_of_Measuring.ToLower().Contains(searchText))
                    .ToList();

                Supplies.Clear();
                foreach (var supply in filteredSupplies)
                {
                    Supplies.Add(supply);
                }
            }
            else
            {
                LoadSupplies();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;

            RefreshSupplies();
        }
    }
}
