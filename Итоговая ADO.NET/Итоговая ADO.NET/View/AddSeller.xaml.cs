using System;
using System.Windows;
using Итоговая_ADO.NET.Models;

namespace Итоговая_ADO.NET
{
    /// <summary>
    /// Логика взаимодействия для AddSeller.xaml
    /// </summary>
    public partial class AddSeller : Window
    {
        public Sellers Seller = new Sellers();
        private bool IsUpdate = false;
        private MainWindow mainWindow;

        public AddSeller(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            DataContext = Seller;
        }

        public AddSeller(Sellers seller, MainWindow main)
        {
            Seller = seller;
            InitializeComponent();
            Title = "Изменить продавца";
            BAddProduct.Content = "Изменить";
            mainWindow = main;
            DataContext = Seller;
            IsUpdate = true;
        }

        private void BAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Seller.Name) || string.IsNullOrEmpty(Seller.Surname) ||
                string.IsNullOrEmpty(Seller.Patronymic) || Seller.Сommissions < 0) return;

            DbSetSellers dbSetSellers = new DbSetSellers();
            if (!IsUpdate)
            {
                try
                {
                    dbSetSellers.Add(Seller);
                    mainWindow.TBResultStatusBar.Text = "Продавец добавлен";
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
                    dbSetSellers.Update(Seller);
                    mainWindow.TBResultStatusBar.Text = "Продавец изменен";
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
