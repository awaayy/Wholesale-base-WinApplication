using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;

namespace UPpril
{
    public partial class UserAccounts : Window
    {
        private readonly opt_baseContext _context;

        public ObservableCollection<User> Users { get; set; }

        public UserAccounts()
        {
            InitializeComponent();
            _context = new opt_baseContext(); // Initialize the database context
            LoadUserAccounts();
            Closing += UserAccounts_Closing;
        }
        private void UserAccounts_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
        }

        private void LoadUserAccounts()
        {
            Users = new ObservableCollection<User>(_context.Users.ToList());
            UserAccountsDataGrid.ItemsSource = Users;
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var addUser = new AddUser();
            if (addUser.ShowDialog() == true)
            {
                var newUser = new User
                {
                    Login = addUser.UserLogin,
                    Password = addUser.UserPassword,
                    Role = addUser.UserRole
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();
                Users.Add(newUser);
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserAccountsDataGrid.SelectedItem is User selectedAccount)
            {
                _context.Users.Remove(selectedAccount);
                _context.SaveChanges();
                Users.Remove(selectedAccount);
            }
        }

    }
}
