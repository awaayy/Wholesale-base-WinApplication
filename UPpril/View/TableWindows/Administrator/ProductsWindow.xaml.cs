using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;
using UPpril.View.AddWindows;
using UPpril.View.EditWindows;

namespace UPpril
{
    public partial class ProductsWindow : Window
    {
        private readonly opt_baseContext _context;

        public ObservableCollection<Product> Products { get; set; }
        public ProductsWindow()
        {
            InitializeComponent();
            _context = new opt_baseContext();     
            LoadProducts();
            Closing += ProductsWindow_Closing;
        }

        private void LoadProducts()
        {
            Products = new ObservableCollection<Product>(_context.Products.ToList());
            ProductsDataGrid.ItemsSource = Products;
        }

        private void RefreshProducts()
        {
            Products = new ObservableCollection<Product>(_context.Products.ToList());
            ProductsDataGrid.ItemsSource = Products;
        }

        private void ProductsWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addProduct = new AddProduct(Products);
            addProduct.ShowDialog();            
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Product selectedProduct)
            {
                EditProduct editProductWindow = new EditProduct(selectedProduct.Product_ID, Products);
                editProductWindow.ShowDialog();

                RefreshProducts();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продукт для редактирования.");
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Product selectedProduct)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот продукт?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Products.Remove(selectedProduct);
                        _context.SaveChanges();

                        Products.Remove(selectedProduct);
                        MessageBox.Show("Продукт успешно удален!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления продукта: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продукт для удаления.");
            }
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(searchText))
            {
                var filteredProducts = _context.Products
                    .Where(p => p.Product_Name.ToLower().Contains(searchText))
                    .ToList();

                Products.Clear();
                foreach (var product in filteredProducts)
                {
                    Products.Add(product);
                }
            }
            else
            {
                LoadProducts();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;

            RefreshProducts();
        }
    }
}
