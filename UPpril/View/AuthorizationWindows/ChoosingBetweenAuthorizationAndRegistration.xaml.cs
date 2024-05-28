using System.Windows;
using UPpril;

namespace UPpril.View.AuthorizationWindows 
{ 
    public partial class ChoosingBetweenAuthorizationAndRegistration : Window
    {
        public ChoosingBetweenAuthorizationAndRegistration()
        {
            InitializeComponent();
        }

        private void Authorize_Click(object sender, RoutedEventArgs e)
        {
            Authorization authWindow = new Authorization();
            authWindow.Show();
            this.Close();
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
