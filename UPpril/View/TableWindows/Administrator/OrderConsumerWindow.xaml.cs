using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using UPpril.Models;
using UPpril.View;

namespace UPpril
{
    public partial class OrderConsumerWindow : Window
    {
        private readonly opt_baseContext _context;

        public OrderConsumerWindow()
        {
            InitializeComponent();
            LoadOrderConsumers();
            this.Closing += OrderConsumersWindow_Closing;
        }

        private void LoadOrderConsumers()
        {
            using (var _context = new opt_baseContext())
            {
                var orderConsumers = new ObservableCollection<Order_Consumer>(_context.Order_Consumers
                            .Include(oc => oc.Order)
                                .ThenInclude(o => o.Products)
                            .Include(oc => oc.Consumer)
                            .ToList());

                OrderConsumerDataGrid.ItemsSource = orderConsumers;
            }
        }

        private void OrderConsumersWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
        }
    }
}
