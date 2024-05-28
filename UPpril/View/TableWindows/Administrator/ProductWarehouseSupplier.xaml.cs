using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;
using UPpril.View;

namespace UPpril
{
    public partial class ProductWarehouseSupplierWindow : Window
    {
        private readonly opt_baseContext _context;

        public ProductWarehouseSupplierWindow()
        {
            InitializeComponent();

            _context = new opt_baseContext();

            LoadProductWarehouseSuppliers();

            this.Closing += ProductWarehouseSupplier_Closing;
        }

        private void LoadProductWarehouseSuppliers()
        {
            var productWarehouseSuppliers = new ObservableCollection<Product_Warehouse_Supplier>(_context.Product_Warehouse_Suppliers
                .Include(pws => pws.Product)
                .Include(pws => pws.Warehouse)
                .Include(pws => pws.Supplier)
                .ToList());

            productWarehouseSupplierDataGrid.ItemsSource = productWarehouseSuppliers;
        }


        private void ProductWarehouseSupplier_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
        }
    }
}
