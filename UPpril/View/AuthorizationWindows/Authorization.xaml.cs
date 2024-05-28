using System.Linq;
using System.Windows;
using UPpril.Models;
using UPpril.View;
using UPpril.View.AuthorizationWindows;

namespace UPpril
{
    public partial class Authorization : Window
    {
        private readonly opt_baseContext _context;

        public Authorization()
        {
            InitializeComponent();
            _context = new opt_baseContext();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var login = txtLogin.Text;
            var password = txtPassword.Password;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
                if (user != null)
                {
                    MessageBox.Show("Авторизация прошла успешно!");

                    switch (user.Role)
                    {
                        case "Administrator":
                            AdminMenu adminmenu = new AdminMenu();
                            adminmenu.Show();
                            this.Close();
                            break;
                        case "General_Director":
                            GeneralDirectorMenu generalDirectorMenu = new GeneralDirectorMenu();
                            generalDirectorMenu.Show();
                            this.Close();
                            break;
                        case "Staff":
                            StorekeeperMenu storekeeperMenu = new StorekeeperMenu();
                            storekeeperMenu.Show();
                            this.Close();
                            break;
                        default:
                            MessageBox.Show("Не удалось определить роль пользователя.");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль. Попробуйте снова.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при подключении к базе данных: {ex.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ChoosingBetweenAuthorizationAndRegistration choosingBetweenAuthorizationAndRegistration = new ChoosingBetweenAuthorizationAndRegistration();
            choosingBetweenAuthorizationAndRegistration.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
