using Microsoft.EntityFrameworkCore;
using System.Windows;
using UPpril.Models;
using UPpril.View;
using UPpril.View.AuthorizationWindows;

namespace UPpril
{
    public partial class GeneralDirectorMenu : Window
    {

        private readonly opt_baseContext _context;
        public GeneralDirectorMenu()
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
            // Логика для выхода
            MessageBox.Show("Выход из системы");
            ChoosingBetweenAuthorizationAndRegistration choosing = new ChoosingBetweenAuthorizationAndRegistration();
            choosing.Show();
            this.Close();
        }

        private void OpenSuppliers_Click(object sender, RoutedEventArgs e)
        {
            GeneralDirectorSuppliersWindow generalDirectorSuppliersWindow = new GeneralDirectorSuppliersWindow();
            generalDirectorSuppliersWindow.Show();
        }

        private void OpenSupplies_Click(object sender, RoutedEventArgs e)
        {
            GeneralDirectorSuppliesWindow generalDirectorSuppliesWindow = new GeneralDirectorSuppliesWindow();
            generalDirectorSuppliesWindow.Show();
        }

        private void OpenWarehouse_Click(object sender, RoutedEventArgs e)
        {
            GeneralDirectorWarehouseWindow generalDirectorWarehouseWindow = new GeneralDirectorWarehouseWindow();
            generalDirectorWarehouseWindow.Show();
        }
    }
}