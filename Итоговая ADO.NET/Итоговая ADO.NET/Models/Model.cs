using System.Collections.ObjectModel;

namespace Итоговая_ADO.NET.Models
{
    abstract class Model<T>: NotifyPropertyChanged
    {
        public abstract ObservableCollection<T> Load();

        public abstract void Add(T obj);

        public abstract void Update(T obj);

        public abstract void Delete(int id);
    }
}
