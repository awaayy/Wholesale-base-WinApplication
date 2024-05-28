using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;

namespace UPpril.View.EditWindows
{
    public partial class EditSupply : Window
    {
        private readonly opt_baseContext _context;
        private readonly int _supplyId;
        private readonly ObservableCollection<Supply> _supplies;

        public EditSupply(int supplyId, ObservableCollection<Supply> supplies)
        {
            InitializeComponent();
            _context = new opt_baseContext();
            _supplyId = supplyId;
            _supplies = supplies;
            LoadSupplyData();
        }

        private void LoadSupplyData()
        {
            try
            {
                var supply = _context.Supplies.FirstOrDefault(s => s.Supply_ID == _supplyId);
                if (supply != null)
                {
                    Supply_Edit_Product_IDTextBox.Text = supply.Product_ID.ToString();
                    Supply_Edit_Supplier_IDTextBox.Text = supply.Supplier_ID.ToString();
                    Supply_Edit_Unit_of_MeasuringTextBox.Text = supply.Unit_of_Measuring;
                    Supply_Edit_TaxTextBox.Text = supply.Tax.ToString();
                }
                else
                {
                    MessageBox.Show("Поставка не найдена");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных поставки: {ex.Message}");
                this.Close();
            }
        }

        private void EditSupply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var supply = _context.Supplies.FirstOrDefault(s => s.Supply_ID == _supplyId);
                if (supply != null)
                {
                    supply.Product_ID = int.Parse(Supply_Edit_Product_IDTextBox.Text);
                    supply.Supplier_ID = int.Parse(Supply_Edit_Supplier_IDTextBox.Text);
                    supply.Unit_of_Measuring = Supply_Edit_Unit_of_MeasuringTextBox.Text;
                    supply.Tax = double.Parse(Supply_Edit_TaxTextBox.Text);      

                    _context.SaveChanges();

                    var supplyInCollection = _supplies.FirstOrDefault(s => s.Supply_ID == _supplyId);
                    if (supplyInCollection != null)
                    {
                        supplyInCollection.Product_ID = supply.Product_ID;
                        supplyInCollection.Supplier_ID = supply.Supplier_ID;
                        supplyInCollection.Unit_of_Measuring = supply.Unit_of_Measuring;
                        supplyInCollection.Tax = supply.Tax;
                    }

                    MessageBox.Show("Запись поставки обновлена успешно");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Поставка не найдена");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка редактирования записи поставки: {ex.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
