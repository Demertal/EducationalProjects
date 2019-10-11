using System;
using System.Windows;
using Итоговая_ADO.NET.Models;

namespace Итоговая_ADO.NET
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        readonly DbSetUnitStorages _dbSetUnitStorages = new DbSetUnitStorages();
        public Products Product = new Products();
        private bool IsUpdate = false;
        private MainWindow mainWindow;

        public AddProduct(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            CBUnitStorages.ItemsSource = _dbSetUnitStorages.Load();
            CBUnitStorages.DisplayMemberPath = "Title";
            DataContext = Product;
        }

        public AddProduct(Products prod, MainWindow main)
        {
            IsUpdate = true;
            Product = prod;
            InitializeComponent();
            Title = "Изменить продукт";
            mainWindow = main;
            CBUnitStorages.ItemsSource = _dbSetUnitStorages.Load();
            CBUnitStorages.DisplayMemberPath = "Title";
            DataContext = Product;
            BAddProduct.Content = "Изменить";
            CBUnitStorages.SelectedItem = Product.UnitStorages;
        }

        private void BAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (CBUnitStorages.SelectedItem == null || Product.Title == null || Product.Title == "" ||
                Product.PurchasePrice <= 0 || Product.SalesPrice <= 0) return;

            Product.IdUnitStorage = (CBUnitStorages.SelectedItem as UnitStorages).Id;
            DbSetProducts dbSetProducts = new DbSetProducts();
            if (!IsUpdate)
            {
                try
                {
                    dbSetProducts.Add(Product);
                    mainWindow.TBResultStatusBar.Text = "Товар добавлен";
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
                    dbSetProducts.Update(Product);
                    mainWindow.TBResultStatusBar.Text = "Товар изменен";
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
