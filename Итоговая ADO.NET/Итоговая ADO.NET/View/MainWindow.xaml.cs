using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using Итоговая_ADO.NET.Models;

namespace Итоговая_ADO.NET
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        #region Property

        readonly DbSetProducts _dbSetProducts = new DbSetProducts();
        readonly DbSetUnitStorages _dbSetUnitStorages = new DbSetUnitStorages();
        readonly DbSetSellers _dbSetSellers = new DbSetSellers();
        readonly DbSetSales _dbSetSales = new DbSetSales();

        private int _idAddUnitStorage = -1;

        ObservableCollection<Products> _products = new ObservableCollection<Products>();
        public ObservableCollection<Products> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged("Products");
            }
        }

        ObservableCollection<UnitStorages> _unitStorages = new ObservableCollection<UnitStorages>();
        public ObservableCollection<UnitStorages> UnitStorages
        {
            get => _unitStorages;
            set
            {
                _unitStorages = value;
                OnPropertyChanged("UnitStorages");
            }
        }

        ObservableCollection<Sellers> _sellers = new ObservableCollection<Sellers>();
        public ObservableCollection<Sellers> Sellers
        {
            get => _sellers;
            set
            {
                _sellers = value;
                OnPropertyChanged("Sellers");
            }
        }

        ObservableCollection<Sales> _sales = new ObservableCollection<Sales>();
        public ObservableCollection<Sales> Sales
        {
            get => _sales;
            set
            {
                _sales = value;
                OnPropertyChanged("Sales");
            }
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Product
        private void MIShowProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductsLoad();
            ProductDataGrid.Visibility = Visibility.Visible;
            UnitStoragesDataGrid.Visibility = Visibility.Collapsed;
            SellersDataGrid.Visibility = Visibility.Collapsed;
            SalesDataGrid.Visibility = Visibility.Collapsed;
            QueryDataGrid.Visibility = Visibility.Collapsed;
            Query4Visible();
            Query5Visible();
            TBResultStatusBar.Text = "Товар показан";
        }

        private void ProductsLoad()
        {
            try
            {
                _idAddUnitStorage = -1;
                Products = _dbSetProducts.Load();            
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MIAddProducts_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addProduct = new AddProduct(this);
            addProduct.ShowDialog();
            ProductsLoad();
        }

        private void CMIDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductDataGrid.SelectedItem == null) return;
            try
            {
                _dbSetProducts.Delete((ProductDataGrid.SelectedItem as Products).Id);
                TBResultStatusBar.Text = "Товар удален";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ProductsLoad();
        }

        private void CMIUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addProduct = new AddProduct(ProductDataGrid.SelectedItem as Products, this);
            addProduct.ShowDialog();
            ProductsLoad();
        }

        private void CMISaleProduct_Click(object sender, RoutedEventArgs e)
        {
            SaleProduct saleProduct = new SaleProduct(ProductDataGrid.SelectedItem as Products, this);
            saleProduct.ShowDialog();
        }

        #endregion

        #region UnitStorage

        private void MIShowUnitStorages_Click(object sender, RoutedEventArgs e)
        {
            UnitStoragesLoad();
            UnitStoragesDataGrid.Visibility = Visibility.Visible;
            ProductDataGrid.Visibility = Visibility.Collapsed;
            SellersDataGrid.Visibility = Visibility.Collapsed;
            SalesDataGrid.Visibility = Visibility.Collapsed;
            QueryDataGrid.Visibility = Visibility.Collapsed;
            Query4Visible();
            Query5Visible();
            TBResultStatusBar.Text = "Ед. хр. показаны";
        }

        private void UnitStoragesLoad()
        {
            try
            {
                _idAddUnitStorage = -1;
                UnitStorages = _dbSetUnitStorages.Load();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UnitStoragesDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (_idAddUnitStorage != UnitStoragesDataGrid.SelectedIndex)
                {
                    _dbSetUnitStorages.Update(UnitStoragesDataGrid.SelectedItem as UnitStorages);
                    TBResultStatusBar.Text = "Ед. хр. изменена";
                }
                else
                {
                    _dbSetUnitStorages.Add(UnitStoragesDataGrid.SelectedItem as UnitStorages);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UnitStoragesLoad();
        }

        private void UnitStoragesDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            _idAddUnitStorage = UnitStoragesDataGrid.Items.Count - 1;
        }

        private void CMIDeleteUnitStorage_Click(object sender, RoutedEventArgs e)
        {
            if (UnitStoragesDataGrid.SelectedItem == null) return;
            try
            {
                _dbSetUnitStorages.Delete((UnitStoragesDataGrid.SelectedItem as UnitStorages).Id);
                TBResultStatusBar.Text = "Ед. хр. удалена";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UnitStoragesLoad();
        }

        #endregion

        #region Sellers

        private void MIShowSellers_Click(object sender, RoutedEventArgs e)
        {
            SellersLoad();
            UnitStoragesDataGrid.Visibility = Visibility.Collapsed;
            ProductDataGrid.Visibility = Visibility.Collapsed;
            SalesDataGrid.Visibility = Visibility.Collapsed;
            QueryDataGrid.Visibility = Visibility.Collapsed;
            SellersDataGrid.Visibility = Visibility.Visible;
            Query4Visible();
            Query5Visible();
            TBResultStatusBar.Text = "Продавцы показаны";
        }

        private void SellersLoad()
        {
            try
            {
                _idAddUnitStorage = -1;
                Sellers = _dbSetSellers.Load();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MIAddSellers_Click(object sender, RoutedEventArgs e)
        {
            AddSeller addSeller = new AddSeller(this);
            addSeller.ShowDialog();
            SellersLoad();
        }

        private void CMIUpdateSellers_Click(object sender, RoutedEventArgs e)
        {
            AddSeller addSeller = new AddSeller(SellersDataGrid.SelectedItem as Sellers, this);
            addSeller.ShowDialog();
            SellersLoad();
        }

        private void CMIDeleteSellers_Click(object sender, RoutedEventArgs e)
        {
            if (SellersDataGrid.SelectedItem == null) return;
            try
            {
                _dbSetSellers.Delete((SellersDataGrid.SelectedItem as Sellers).Id);
                TBResultStatusBar.Text = "Продавец удален";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            SellersLoad();
        }

        #endregion

        #region Sales
        private void MIShowSales_Click(object sender, RoutedEventArgs e)
        {
            SalesLoad();
            UnitStoragesDataGrid.Visibility = Visibility.Collapsed;
            ProductDataGrid.Visibility = Visibility.Collapsed;
            SellersDataGrid.Visibility = Visibility.Collapsed;
            QueryDataGrid.Visibility = Visibility.Collapsed;
            SalesDataGrid.Visibility = Visibility.Visible;
            Query4Visible();
            Query5Visible();
            TBResultStatusBar.Text = "Продажи показаны";
        }

        private void SalesLoad()
        {
            try
            {
                _idAddUnitStorage = -1;
                Sales = _dbSetSales.Load();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CMIUpdateSales_Click(object sender, RoutedEventArgs e)
        {
            SaleProduct saleProduct = new SaleProduct(SalesDataGrid.SelectedItem as Sales, this);
            saleProduct.ShowDialog();
            SalesLoad();
        }

        private void CMIDeleteSales_Click(object sender, RoutedEventArgs e)
        {
            if (SalesDataGrid.SelectedItem == null) return;
            try
            {
                _dbSetSales.Delete((SalesDataGrid.SelectedItem as Sales).Id);
                TBResultStatusBar.Text = "Продажа удалена";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            SalesLoad();
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductDataGrid.Visibility = Visibility.Collapsed;
            UnitStoragesDataGrid.Visibility = Visibility.Collapsed;
            SellersDataGrid.Visibility = Visibility.Collapsed;
            SalesDataGrid.Visibility = Visibility.Collapsed;
            QueryDataGrid.Visibility = Visibility.Visible;
            Query5Visible();
            Query4Visible();
            if ((string) (sender as Button).Content == "4")
                Query4Visible(true);
            else if((string)(sender as Button).Content == "5")
                Query5Visible(true);
            else Query((string)(sender as Button).Content);

        }

        private void Query4Visible(bool ch = false)
        {
            if (ch)
            {
                TBQuery4.Visibility = Visibility.Visible;
                TBXQuery4.Visibility = Visibility.Visible;
            }
            else
            {
                TBQuery4.Visibility = Visibility.Collapsed;
                TBXQuery4.Visibility = Visibility.Collapsed;
            }
        }

        private void Query5Visible(bool ch = false)
        {
            if (ch)
            {
                TBQuery5Low.Visibility = Visibility.Visible;
                TBQuery5Up.Visibility = Visibility.Visible;
                TBXQuery5Low.Visibility = Visibility.Visible;
                TBXQuery5Up.Visibility = Visibility.Visible;
            }
            else
            {
                TBQuery5Low.Visibility = Visibility.Collapsed;
                TBQuery5Up.Visibility = Visibility.Collapsed;
                TBXQuery5Low.Visibility = Visibility.Collapsed;
                TBXQuery5Up.Visibility = Visibility.Collapsed;
            }
        }

        private void Query(string num)
        {
            _idAddUnitStorage = -1;
            try
            {
                using (WholesaleStoreEntities db = new WholesaleStoreEntities())
                {
                    switch (num)
                    {
                        case "1":
                            db.Query1.Load();
                            QueryDataGrid.ItemsSource = db.Query1.Local;
                            break;
                        case "2":
                            db.Query2.Load();
                            QueryDataGrid.ItemsSource = db.Query2.Local;
                            break;
                        case "3":
                            db.Query3.Load();
                            QueryDataGrid.ItemsSource = db.Query3.Local;
                            break;
                        case "6":
                            QueryDataGrid.ItemsSource = db.Query6();
                            break;
                        case "7":
                            db.Query7.Load();
                            QueryDataGrid.ItemsSource = db.Query7.Local;
                            break;
                        case "8":
                            db.Query8.Load();
                            QueryDataGrid.ItemsSource = db.Query8.Local;
                            break;
                        case "9":
                            db.Query9();
                            db.Query9Table.Load();
                            QueryDataGrid.ItemsSource = db.Query9Table.Local;
                            break;
                        case "10":
                            db.Query10();
                            db.ProductsCopy.Load();
                            QueryDataGrid.ItemsSource = db.ProductsCopy.Local;
                            break;
                        case "11":
                            db.Query11();
                            db.ProductsCopy.Load();
                            QueryDataGrid.ItemsSource = db.ProductsCopy.Local;
                            break;
                        case "12":
                            db.Query12();
                            db.Sellers.Load();
                            QueryDataGrid.ItemsSource = db.Sellers.Local;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            TBResultStatusBar.Text = "Запрос " + num;
            Query4Visible();
        }

        private void TBXQuery4_TextChanged(object sender, TextChangedEventArgs e)
        {
            double com = 0;
            if (!double.TryParse(TBXQuery4.Text, out com)) return;
            using (WholesaleStoreEntities db = new WholesaleStoreEntities())
            {
                QueryDataGrid.ItemsSource = db.Query4(com);
                TBResultStatusBar.Text = "Запрос 4";
            }
        }

        private void TBXQuery5Up_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal up = 0, low = 0;
            if (!decimal.TryParse(TBXQuery5Up.Text, out up)) return;
            if (!decimal.TryParse(TBXQuery5Low.Text, out low)) return;
            using (WholesaleStoreEntities db = new WholesaleStoreEntities())
            {
                QueryDataGrid.ItemsSource = db.Query5(low, up);
                TBResultStatusBar.Text = "Запрос 5";
            }
        }

        private void TBXQuery5Low_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal up = 0, low = 0;
            if (!decimal.TryParse(TBXQuery5Up.Text, out up)) return;
            if (!decimal.TryParse(TBXQuery5Low.Text, out low)) return;
            using (WholesaleStoreEntities db = new WholesaleStoreEntities())
            {
                QueryDataGrid.ItemsSource = db.Query5(low, up);
                TBResultStatusBar.Text = "Запрос 5";
            }
        }
    }
}
