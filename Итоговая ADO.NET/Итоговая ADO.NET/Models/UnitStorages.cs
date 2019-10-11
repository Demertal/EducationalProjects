namespace Итоговая_ADO.NET
{
    public partial class UnitStorages: NotifyPropertyChanged
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

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                UnitStorages p = (UnitStorages)obj;
                return Id == p.Id && Title == p.Title;
            }
        }
    }
}
