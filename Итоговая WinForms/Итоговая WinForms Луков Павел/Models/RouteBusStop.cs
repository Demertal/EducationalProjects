using System.Runtime.Serialization;
using Итоговая_WinForms_Луков_Павел.Interfaces;

namespace Итоговая_WinForms_Луков_Павел.Models
{
    [DataContract]
    public class RouteBusStop : Updating, IValidating
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

        private int _idBusStop;
        [DataMember]
        public int IdBusStop
        {
            get => _idBusStop;
            set
            {
                _idBusStop = value;
                UpdateHandler?.Invoke(this, new UpdateEventArgs(IsValidated));
            }
        }

        public RouteBusStop()
        {
            _idBusStop = 1;
        }
        public RouteBusStop(int id, int idBusStop) 
        {
            _id = id;
            _idBusStop = idBusStop;
        }

        public void SetId(int newId)
        {
            _id = newId;
        }

        public bool IsValidated => Id != 0 && IdBusStop != 0;
    }
}
