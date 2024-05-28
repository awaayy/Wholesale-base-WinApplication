using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UPpril.Models;
using UPpril.View.AddWindows;
using UPpril.View.EditWindows;
using ClosedXML;
using ClosedXML.Excel;
using Microsoft.Win32;
using UPpril.View;

namespace UPpril
{
    public partial class ConsumersWindow : Window
    {
        public ObservableCollection<Consumer> Consumers { get; set; }
        private readonly opt_baseContext _context;
        public ConsumersWindow()
        {
            InitializeComponent();
            _context = new opt_baseContext();
            LoadConsumers();
            this.Closing += ConsumersWindow_Closing;
        }

        private void LoadConsumers()
        {
            Consumers = new ObservableCollection<Consumer>(_context.Consumers.ToList());
            ConsumersDataGrid.ItemsSource = Consumers;
        }

        private void RefreshConsumers()
        {
            var consumersList = _context.Consumers.ToList();
            Consumers.Clear();
            foreach (var consumer in consumersList)
            {
                Consumers.Add(consumer);
            }
        }

        private void ConsumersWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
        }

        private void AddConsumer_Click(object sender, RoutedEventArgs e)
        {
            AddConsumer addConsumer = new AddConsumer(Consumers);
            addConsumer.ShowDialog();
        }

        private void EditConsumer_Click(object sender, RoutedEventArgs e)
        {
            if (ConsumersDataGrid.SelectedItem is Consumer selectedConsumer)
            {
                EditConsumer editConsumer = new EditConsumer(selectedConsumer.Consumer_ID, Consumers);
                editConsumer.ShowDialog();

                RefreshConsumers();
            }
            else
            {
                MessageBox.Show("Необходимо выбрать строку таблицы для дальнейшего редактирования.");
            }
        }

        private void DeleteConsumer_Click(object sender, RoutedEventArgs e)
        {
            DbUtility.DeleteItem(Consumers, ConsumersDataGrid.SelectedItem, _context);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(searchText))
            {
                var filteredConsumers = _context.Consumers
                    .Where(c => c.Telephone.ToLower().Contains(searchText))
                    .ToList();

                Consumers.Clear();
                foreach (var consumer in filteredConsumers)
                {
                    Consumers.Add(consumer);
                }
            }
            else
            {
                RefreshConsumers();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;
            RefreshConsumers();
        }

        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                Title = "Отчёт",
                DefaultExt = "xlsx",
                FileName = "ConsumersReport"
            };

            var result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Consumers Report");

                worksheet.Cell(1, 1).Value = "Consumer_ID";
                worksheet.Cell(1, 2).Value = "Surname";
                worksheet.Cell(1, 3).Value = "Telephone";
                worksheet.Cell(1, 4).Value = "City";
                worksheet.Cell(1, 5).Value = "Street";
                worksheet.Cell(1, 6).Value = "Home";

                int rowIndex = 2;
                foreach (var consumer in Consumers)
                {
                    worksheet.Cell(rowIndex, 1).Value = consumer.Consumer_ID;
                    worksheet.Cell(rowIndex, 2).Value = consumer.Surname;
                    worksheet.Cell(rowIndex, 3).Value = consumer.Telephone;
                    worksheet.Cell(rowIndex, 4).Value = consumer.City;
                    worksheet.Cell(rowIndex, 5).Value = consumer.Street;
                    worksheet.Cell(rowIndex, 6).Value = consumer.Home;

                    rowIndex++;
                }

                var headerRange = worksheet.Range(1, 1, 1, 6);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.Yellow;

                workbook.SaveAs(saveFileDialog.FileName);
                MessageBox.Show("Отчёт таблицы Потребители успешно сохранён!");
            }
        }
    }
}