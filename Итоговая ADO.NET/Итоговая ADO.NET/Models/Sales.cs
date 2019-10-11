using System;

namespace Итоговая_ADO.NET
{
    public partial class Sales : NotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        private int _idProduct;
        public int IdProduct
        {
            get => _idProduct;
            set
            {
                _idProduct = value;
                OnPropertyChanged("IdProduct");
            }
        }

        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged("Count");
            }
        }

        private int _idSeller;
        public int IdSeller
        {
            get => _idSeller;
            set
            {
                _idSeller = value;
                OnPropertyChanged("IdSeller");
            }
        }

        private DateTime _dateSale;
        public DateTime DateSale
        {
            get => _dateSale;
            set
            {
                _dateSale = value;
                OnPropertyChanged("DateSale");
                OnPropertyChanged("DateSaleDisplay");
            }
        }

        public string DateSaleDisplay => _dateSale.Date.ToShortDateString();

        private Products _products;
        public Products Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged("Products");
            }
        }

        private Sellers _sellers;
        public Sellers Sellers
        {
            get => _sellers;
            set
            {
                _sellers = value;
                OnPropertyChanged("Sellers");
            }
        }

        public double СommissionsFromProduct
        {
            get
            {
                if (Products == null || Sellers == null) return 0;
                return (double)Products.SalesPrice * Sellers.Сommissions / 100 * Count;
            }
        }


    }
}
