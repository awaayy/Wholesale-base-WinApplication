using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;

namespace UPpril.View.EditWindows
{
    public partial class EditSupplier : Window
    {
        private readonly opt_baseContext _context;
        private readonly int _supplierId;
        private readonly ObservableCollection<Supplier> _suppliers;

        public EditSupplier(int supplierId, ObservableCollection<Supplier> suppliers)
        {
            InitializeComponent();
            _context = new opt_baseContext();
            _supplierId = supplierId;
            _suppliers = suppliers;
            LoadSupplierData();
        }

        private void LoadSupplierData()
        {
            try
            {
                var supplier = _context.Suppliers.FirstOrDefault(s => s.Supplier_ID == _supplierId);
                if (supplier != null)
                {
                    CompanyNameTextBox.Text = supplier.Company_Name;
                    Edit_TelephoneTextBox.Text = supplier.Telephone;
                    Edit_CityTextBox.Text = supplier.City;
                    Edit_StreetTextBox.Text = supplier.Street;
                    Edit_HomeTextBox.Text = supplier.Home;
                    Edit_SurnameDirectorTextBox.Text = supplier.Surname_Director;
                }
                else
                {
                    MessageBox.Show("Поставщик не найден.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных о поставщике: {ex.Message}");
                this.Close();
            }
        }

        private void EditSupplier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var supplier = _context.Suppliers.FirstOrDefault(s => s.Supplier_ID == _supplierId);
                if (supplier != null)
                {
                    supplier.Company_Name = CompanyNameTextBox.Text;
                    supplier.Telephone = Edit_TelephoneTextBox.Text;
                    supplier.City = Edit_CityTextBox.Text;
                    supplier.Street = Edit_StreetTextBox.Text;
                    supplier.Home = Edit_HomeTextBox.Text;
                    supplier.Surname_Director = Edit_SurnameDirectorTextBox.Text;

                    if (!IsTelephoneValid(supplier.Telephone))
                    {
                        MessageBox.Show("Введите правильный номер телефона (11 цифр).");
                        return;
                    }

                    if (!IsLettersOnly(supplier.Surname_Director))
                    {
                        MessageBox.Show("Фамилия директора должна содержать только буквы.");
                        return;
                    }

                    if (!int.TryParse(supplier.Home, out int homeNumber))
                    {
                        MessageBox.Show("Введите правильное числовое значение для номера дома.");
                        return;
                    }

                    _context.SaveChanges();

                    var supplierInCollection = _suppliers.FirstOrDefault(s => s.Supplier_ID == _supplierId);
                    if (supplierInCollection != null)
                    {
                        supplierInCollection.Company_Name = supplier.Company_Name;
                        supplierInCollection.Telephone = supplier.Telephone;
                        supplierInCollection.City = supplier.City;
                        supplierInCollection.Street = supplier.Street;
                        supplierInCollection.Home = supplier.Home;
                        supplierInCollection.Surname_Director = supplier.Surname_Director;
                    }

                    MessageBox.Show("Данные поставщика обновлены!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Поставщик не найден.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка редактирования данных поставщика: {ex.Message}");
            }
        }

        private bool IsTelephoneValid(string input)
        {
            return input.Length == 11 && input.All(char.IsDigit);
        }

        private bool IsLettersOnly(string input)
        {
            return input.All(char.IsLetter);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
