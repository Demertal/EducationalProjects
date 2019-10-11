namespace Итоговая_ADO.NET
{
    public partial class Sellers : NotifyPropertyChanged
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

        private string _surname;
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged("Surname");
                OnPropertyChanged("FullNameDisplay");
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
                OnPropertyChanged("FullNameDisplay");
            }
        }

        private string _patronymic;
        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                OnPropertyChanged("Patronymic");
                OnPropertyChanged("FullNameDisplay");
            }
        }

        private double _commissions;
        public double Сommissions
        {
            get => _commissions;
            set
            {
                _commissions = value;
                OnPropertyChanged("Сommissions");
            }
        }

        public string FullNameDisplay => Surname + " " + Name + " " + Patronymic;

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Sellers s = (Sellers)obj;
                return Id == s.Id && Name == s.Name && Surname == s.Surname && Patronymic == s.Patronymic &&
                       Сommissions == s.Сommissions;
            }
        }
    }
}
