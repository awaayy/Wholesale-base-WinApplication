using Microsoft.EntityFrameworkCore;
using System.Windows;
using UPpril.Models; // Добавлен импорт пространства имен, где определен тип User
using UPpril.View;
using UPpril.View.AuthorizationWindows;

namespace UPpril
{
    public partial class AdminMenu : Window
    {
        private readonly opt_baseContext _context;

        public AdminMenu()
        {
            InitializeComponent();
            _context = new opt_baseContext();
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Данные успешно сохранены.", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var authorization = new Authorization();
                authorization.Show();
                this.Close();
            }
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("В данном приложении вы сможете наблюдать за учётом работы нашей оптовой базы", "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            UserOptions.Visibility = UserOptions.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void ChangeAccountButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Смена аккаунта");
            Authorization authorizationWindow = new Authorization();
            authorizationWindow.Show();
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Выход из системы");
            ChoosingBetweenAuthorizationAndRegistration choosing = new ChoosingBetweenAuthorizationAndRegistration();
            choosing.Show();
            this.Close();
        }
        private void SuppliersButton_Click(object sender, RoutedEventArgs e)
        {
            SuppliersWindow suppliersWindow = new SuppliersWindow();
            suppliersWindow.Show();
            this.Close();
        }

        private void ConsumersButton_Click(object sender, RoutedEventArgs e)
        {
            ConsumersWindow consumersWindow = new ConsumersWindow();
            consumersWindow.Show();
            this.Close();
        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            ProductsWindow productsWindow = new ProductsWindow();
            productsWindow.Show();
            this.Close();
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            OrdersWindow ordersWindow = new OrdersWindow();
            ordersWindow.Show();
            this.Close();
        }

        private void WarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            WarehouseWindow warehouseWindow = new WarehouseWindow();
            warehouseWindow.Show();
            this.Close();
        }

        private void SupplyButton_Click(object sender, RoutedEventArgs e)
        {
            SupplyWindow supplyWindow = new SupplyWindow();
            supplyWindow.Show();
            this.Close();
        }

        private void ConsumerProduct_Click(object sender, RoutedEventArgs e)
        {
            ConsumerProductWindow consumerProductWindow = new ConsumerProductWindow();
            consumerProductWindow.Show();
            this.Close();
        }

        private void OrderConsumer_Click(object sender, RoutedEventArgs e)
        {
            OrderConsumerWindow orderConsumerWindow = new OrderConsumerWindow();
            orderConsumerWindow.Show();
            this.Close();
        }

        private void ProductWarehouseSuppliers_Click(object sender, RoutedEventArgs e)
        {
            ProductWarehouseSupplierWindow productWarehouseSupplierWindow = new ProductWarehouseSupplierWindow();
            productWarehouseSupplierWindow.Show();
            this.Close();
        }


        private void UserAccountButton_Click(object sender, RoutedEventArgs e)
        {
            UserAccounts userAccounts = new UserAccounts();
            userAccounts.Show();
            this.Close();
        }


    }
}
