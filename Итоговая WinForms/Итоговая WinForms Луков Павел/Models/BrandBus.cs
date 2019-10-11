using System.Runtime.Serialization;
using Итоговая_WinForms_Луков_Павел.Interfaces;

namespace Итоговая_WinForms_Луков_Павел.Models
{
    [DataContract]
    public class BrandBus : Updating, IValidating
    {
        public event UpdatingHandler UpdateHandler;

        private int _id;
        [DataMember]
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                UpdateHandler?.Invoke(this, new UpdateEventArgs(IsValidated));
            }
        }

        private string _title;
        [DataMember]
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                UpdateHandler?.Invoke(this, new UpdateEventArgs(IsValidated));
            }
        }

        public bool IsValidated => Id != 0 && !string.IsNullOrEmpty(Title);
    }
}
