using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace UPpril
{
    public partial class AddUser : Window
    {
        public string UserLogin { get; private set; }
        public string UserPassword { get; private set; }
        public string UserRole { get; private set; }

        public AddUser()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            UserLogin = LoginTextBox.Text;
            UserPassword = PasswordBox.Password;
            UserRole = (RolesComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(); // Получение выбранной роли

            if (IsPasswordValid(UserPassword))
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Пароль не соответствует требованиям безопасности:\n" +
                                "- Минимум 6 символов\n" +
                                "- Минимум 1 прописная буква\n" +
                                "- Минимум 1 цифра\n" +
                                "- Минимум один символ из набора: ! @ # $ % ^.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private bool IsPasswordValid(string password)
        {
            // Password must be at least 6 characters long, contain at least one uppercase letter, one digit, and one of the special characters: !@#$%^
            return password.Length >= 6 &&
                   Regex.IsMatch(password, @"[A-Z]") &&
                   Regex.IsMatch(password, @"\d") &&
                   Regex.IsMatch(password, @"[!@#$%^]");
        }
    }
}
