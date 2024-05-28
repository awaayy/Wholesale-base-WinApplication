using Microsoft.EntityFrameworkCore;
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
using UPpril.View.AddWindows;
using UPpril.View.EditWindows;

namespace UPpril
{
    /// <summary>
    /// Логика взаимодействия для ConsumerProductWindow.xaml
    /// </summary>
    public partial class ConsumerProductWindow : Window
    {
        private readonly opt_baseContext _context;
        public ConsumerProductWindow()
        {
            InitializeComponent();
            LoadConsumerProducts();
            this.Closing += Consumers_ProductsWindow_Closing;
        }

        private void LoadConsumerProducts()
        {
            using (var _context = new opt_baseContext())
            {
                var consumerProducts = new ObservableCollection<Consumer_Product>(_context.Consumer_Products.Include(cp => cp.Consumer).Include(cp => cp.Product));
                ConsumerProductDataGrid.ItemsSource = consumerProducts;
            }
        }

        private void Consumers_ProductsWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
        }
    }
}
