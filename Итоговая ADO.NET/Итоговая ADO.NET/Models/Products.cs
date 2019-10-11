namespace Итоговая_ADO.NET
{
    partial class Products : NotifyPropertyChanged
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

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        private decimal _purchasePrice;
        public decimal PurchasePrice
        {
            get => _purchasePrice;
            set
            {
                _purchasePrice = value;
                OnPropertyChanged("PurchasePrice");
            }
        }

        private decimal _salesPrice;
        public decimal SalesPrice
        {
            get => _salesPrice;
            set
            {
                _salesPrice = value;
                OnPropertyChanged("SalesPrice");
            }
        }

        private int _idUnitStorage;
        public int IdUnitStorage
        {
            get => _idUnitStorage;
            set
            {
                _idUnitStorage = value;
                OnPropertyChanged("IdUnitStorage");
            }
        }

        private UnitStorages _unitStorages;
        public UnitStorages UnitStorages
        {
            get => _unitStorages;
            set
            {
                _unitStorages = value;
                OnPropertyChanged("UnitStorages");
            }
        }
    }
}
