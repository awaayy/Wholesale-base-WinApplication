using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;

namespace UPpril
{
    public partial class GeneralDirectorSuppliersWindow : Window
    {
        private readonly opt_baseContext _context;

        public ObservableCollection<Supplier> Suppliers { get; set; }

        public GeneralDirectorSuppliersWindow()
        {
            InitializeComponent();
            _context = new opt_baseContext();
            LoadSuppliers();
            Closing += GeneralDirectorSuppliersWindow_Closing;
        }

        private void LoadSuppliers()
        {
            Suppliers = new ObservableCollection<Supplier>(_context.Suppliers.ToList());
            GeneralDirectorSuppliersDataGrid.ItemsSource = Suppliers;
        }

        private void RefreshSuppliers()
        {
            Suppliers = new ObservableCollection<Supplier>(_context.Suppliers.ToList());
            GeneralDirectorSuppliersDataGrid.ItemsSource = Suppliers;
        }

        private void GeneralDirectorSuppliersWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GeneralDirectorMenu generalDirectorMenu = new GeneralDirectorMenu();
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
