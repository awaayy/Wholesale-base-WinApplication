using System;
using System.Windows;
using UPpril.Models;
using System.Collections.ObjectModel;

namespace UPpril.View.AddWindows
{
    public partial class AddSupplier : Window
    {
        private readonly opt_baseContext _context;
        private readonly ObservableCollection<Supplier> _suppliers;

        public AddSupplier(ObservableCollection<Supplier> suppliers)
        {
            InitializeComponent();
            _context = new opt_baseContext();
            _suppliers = suppliers;
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string companyName = CompanyNameTextBox.Text;
                string telephone = TelephoneTextBox.Text;
                string city = CityTextBox.Text;
                string street = StreetTextBox.Text;
                string home = HomeTextBox.Text;
                string surnameDirector = SurnameDirectorTextBox.Text;

                if (!IsTelephoneValid(telephone))
                {
                    MessageBox.Show("Введите правильный номер телефона (11 цифр).");
                    return;
                }

                if (!IsLettersOnly(surnameDirector))
                {
                    MessageBox.Show("Фамилия директора должна содержать только буквы.");
                    return;
                }

                if (!int.TryParse(home, out int homeNumber))
                {
                    MessageBox.Show("Введите правильное числовое значение для номера дома.");
                    return;
                }

                Supplier newSupplier = new Supplier
                {
                    Company_Name = companyName,
                    Telephone = telephone,
                    City = city,
                    Street = street,
                    Home = home,
                    Surname_Director = surnameDirector
                };

                _context.Suppliers.Add(newSupplier);
                _context.SaveChanges();

                _suppliers.Add(newSupplier);

                System.Windows.MessageBox.Show("Поставщик добавлен успешно!");
                this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка добавления поставщика в базу данных: {ex.Message}");
            }
        }

        private bool IsLettersOnly(string input)
        {
            return input.All(char.IsLetter);
        }

        private bool IsTelephoneValid(string input)
        {
            return input.Length == 11 && input.All(char.IsDigit);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
