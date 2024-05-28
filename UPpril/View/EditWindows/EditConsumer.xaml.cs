using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;

namespace UPpril.View.EditWindows
{
    public partial class EditConsumer : Window
    {
        private readonly opt_baseContext _context;
        private readonly int _consumerId;
        private readonly ObservableCollection<Consumer> _consumers;

        public EditConsumer(int consumerId, ObservableCollection<Consumer> consumers)
        {
            InitializeComponent();
            _context = new opt_baseContext();
            _consumerId = consumerId;
            _consumers = consumers;
            LoadConsumer();
        }

        private void LoadConsumer()
        {
            try
            {
                var consumer = _context.Consumers.FirstOrDefault(c => c.Consumer_ID == _consumerId);
                if (consumer != null)
                {
                    Consumer_Edit_SurnameTextBox.Text = consumer.Surname;
                    Consumer_Edit_TelephoneTextBox.Text = consumer.Telephone;
                    Consumer_Edit_CityTextBox.Text = consumer.City;
                    Consumer_Edit_StreetTextBox.Text = consumer.Street;
                    Consumer_Edit_HomeTextBox.Text = consumer.Home;
                }
                else
                {
                    MessageBox.Show("Потребитель не найден.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных о потребителе: {ex.Message}");
                this.Close();
            }
        }

        private void EditConsumer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var consumer = _context.Consumers.FirstOrDefault(c => c.Consumer_ID == _consumerId);
                if (consumer != null)
                {
                    string surname = Consumer_Edit_SurnameTextBox.Text;
                    string telephone = Consumer_Edit_TelephoneTextBox.Text;
                    string city = Consumer_Edit_CityTextBox.Text;
                    string street = Consumer_Edit_StreetTextBox.Text;
                    string home = Consumer_Edit_HomeTextBox.Text;

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

                    consumer.Surname = surname;
                    consumer.Telephone = telephone;
                    consumer.City = city;
                    consumer.Street = street;
                    consumer.Home = home;

                    _context.SaveChanges();

                    var consumerInCollection = _consumers.FirstOrDefault(c => c.Consumer_ID == _consumerId);
                    if (consumerInCollection != null)
                    {
                        consumerInCollection.Surname = consumer.Surname;
                        consumerInCollection.Telephone = consumer.Telephone;
                        consumerInCollection.City = consumer.City;
                        consumerInCollection.Street = consumer.Street;
                        consumerInCollection.Home = consumer.Home;
                    }

                    MessageBox.Show("Редактирование записи успешно завершено!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Потребитель не найден.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения данных о потребителе: {ex.Message}");
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
