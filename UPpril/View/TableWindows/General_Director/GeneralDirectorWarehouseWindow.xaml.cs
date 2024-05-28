using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UPpril.Models;
using UPpril.View;

namespace UPpril
{
    public partial class GeneralDirectorWarehouseWindow : Window
    {
        private readonly opt_baseContext _context;

        public ObservableCollection<Warehouse> Warehouses { get; set; }
        public GeneralDirectorWarehouseWindow()
        {
            InitializeComponent();
            _context = new opt_baseContext();     
            LoadWarehouses();
            Closing += GeneralDirectorWarehouseWindow_Closing;
        }

        private void LoadWarehouses()
        {
            Warehouses = new ObservableCollection<Warehouse>(_context.Warehouses.ToList());
            GeneralDirectorWarehouseDataGrid.ItemsSource = Warehouses;
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

        private void RefreshWarehouses()
        {
            var warehousesList = _context.Warehouses.ToList();
            Warehouses.Clear();
            foreach (var warehouse in warehousesList)
            {
                Warehouses.Add(warehouse);
            }
        }


        private void GeneralDirectorWarehouseWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GeneralDirectorMenu generalDirectorMenu = new GeneralDirectorMenu();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;

            RefreshWarehouses();
        }
    }
}