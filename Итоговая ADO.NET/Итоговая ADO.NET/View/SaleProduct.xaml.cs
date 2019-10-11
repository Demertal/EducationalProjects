using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Итоговая_ADO.NET.Models;

namespace Итоговая_ADO.NET
{
    /// <summary>
    /// Логика взаимодействия для SaleProduct.xaml
    /// </summary>
    public partial class SaleProduct : Window
    {
        readonly DbSetSellers _dbSetSellers = new DbSetSellers();
        public Sales Sale = new Sales();
        private bool IsUpdate = false;
        private MainWindow mainWindow;

        public SaleProduct(Products prod, MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            CBSellers.ItemsSource = _dbSetSellers.Load();
            CBSellers.DisplayMemberPath = "FullNameDisplay";
            Sale.Products = prod;
            Sale.IdProduct = prod.Id;
            DataContext = Sale;
            Sale.DateSale = DateTime.Today;
        }

        public SaleProduct(Sales sale, MainWindow main)
        {
            IsUpdate = true;
            Sale = sale;
            InitializeComponent();
            Title = "Изменить продажу";
            mainWindow = main;
            CBSellers.ItemsSource = _dbSetSellers.Load();
            CBSellers.DisplayMemberPath = "FullNameDisplay";
            DataContext = Sale;
            BAddProduct.Content = "Изменить";
            CBSellers.SelectedItem = Sale.Sellers;
        }

        private void BAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (CBSellers.SelectedItem == null || Sale.Count <= 0) return;

            Sale.IdSeller = (CBSellers.SelectedItem as Sellers).Id;
            DbSetSales dbSetSales = new DbSetSales();
            if (!IsUpdate)
            {
                try
                {
                    dbSetSales.Add(Sale);
                    mainWindow.TBResultStatusBar.Text = "Продажа добавлена";
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    dbSetSales.Update(Sale);
                    mainWindow.TBResultStatusBar.Text = "Продажа изменена";
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
