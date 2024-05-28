using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;

namespace UPpril
{
    public partial class GeneralDirectorSuppliesWindow : Window
    {
        private readonly opt_baseContext _context;

        public ObservableCollection<Supply> Supplies { get; set; }

        public GeneralDirectorSuppliesWindow()
        {
            InitializeComponent();
            _context = new opt_baseContext();
            LoadSupplies();
            Closing += GeneralDirectorSuppliesWindow_Closing;
        }

        private void LoadSupplies()
        {
            Supplies = new ObservableCollection<Supply>(_context.Supplies.ToList());
            GeneralDirectorSuppliesDataGrid.ItemsSource = Supplies;
        }

        private void RefreshSupplies()
        {
            Supplies = new ObservableCollection<Supply>(_context.Supplies.ToList());
            GeneralDirectorSuppliesDataGrid.ItemsSource = Supplies;
        }

        private void GeneralDirectorSuppliesWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GeneralDirectorMenu generalDirectorMenu = new GeneralDirectorMenu();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(searchText))
            {
                var filteredSupplies = _context.Supplies
                    .Where(sup => sup.Unit_of_Measuring.ToLower().Contains(searchText))
                    .ToList();

                Supplies.Clear();
                foreach (var supplies in filteredSupplies)
                {
                    Supplies.Add(supplies);
                }
            }
            else
            {
                LoadSupplies();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;

            RefreshSupplies();
        }
    }
}
