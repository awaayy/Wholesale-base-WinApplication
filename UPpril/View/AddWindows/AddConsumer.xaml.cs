using System.Collections.ObjectModel;
using System.Windows;
using UPpril.Models;

namespace UPpril.View.AddWindows
{
    public partial class AddConsumer : Window
    {
        private readonly opt_baseContext _context;
        private readonly ObservableCollection<Consumer> _consumers;

        public AddConsumer(ObservableCollection<Consumer> consumers)
        {
            InitializeComponent();
            _context = new opt_baseContext();
            _consumers = consumers;
        }

        private void AddConsumer_Click(object sender, RoutedEventArgs e)
        {
            string surname = SurnameConsumerTextBox.Text;
            string telephone = TelephoneTextBox.Text;
            string city = CityTextBox.Text;
            string street = StreetTextBox.Text;
            string home = HomeTextBox.Text;

            if (!IsValidSurname(surname))
            {
                MessageBox.Show("Некорректная фамилия. Фамилия должна содержать только буквы.");
                return;
            }

            if (!IsValidPhoneNumber(telephone))
            {
                MessageBox.Show("Некорректный номер телефона. Номер должен содержать 11 цифр.");
                return;
            }

            if (!IsValidLettersOnly(city))
            {
                MessageBox.Show("Некорректное название города. Город должен содержать только буквы.");
                return;
            }

            int homeNumber;
            if (!int.TryParse(home, out homeNumber))
            {
                MessageBox.Show("Некорректный номер дома. Дом должен быть целым числом.");
                return;
            }

            Consumer newConsumer = new Consumer
            {
                Surname = surname,
                Telephone = telephone,
                City = city,
                Street = street,      
                Home = home
            };

            try
            {
                _context.Consumers.Add(newConsumer);
                _context.SaveChanges();

                _consumers.Add(newConsumer);

                MessageBox.Show("Потребитель добавлен успешно!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления потребителя в базу данных: {ex.Message}");
            }
        }

        private bool IsValidSurname(string surname)
        {
            return !string.IsNullOrWhiteSpace(surname) && surname.All(char.IsLetter);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.Length == 11 && phoneNumber.All(char.IsDigit);
        }

        private bool IsValidLettersOnly(string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
