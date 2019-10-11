using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using Итоговая_WinForms_Луков_Павел.Interfaces;

namespace Итоговая_WinForms_Луков_Павел.Models
{
    [DataContract]
    public class Route : Updating, IValidating
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

        private ObservableCollection<RouteBusStop> _idBusStopList = new ObservableCollection<RouteBusStop>();
        [DataMember]
        public ObservableCollection<RouteBusStop> IdBusStopList
        {
            get => _idBusStopList;
            set
            {
                _idBusStopList = value;
                IdBusStopList.CollectionChanged += IdBusStopListChanged;
                foreach (var ob in _idBusStopList)
                {
                    ob.UpdateHandler += UpdateRouteBusStop;
                }
                UpdateHandler?.Invoke(this, new UpdateEventArgs(IsValidated));
            }
        }

        private int _idBus;
        [DataMember]
        public int IdBus
        {
            get => _idBus;
            set
            {
                _idBus = value;
                UpdateHandler?.Invoke(this, new UpdateEventArgs(IsValidated));
            }
        }

        public bool IsValidated
        {
            get
            {
                if (Id == 0 || IdBus == 0) return false;
                foreach (var ob in IdBusStopList)
                {
                    if(!ob.IsValidated) return false;
                }

                return true;
            }
        }

        public Route()
        {
            IdBusStopList.CollectionChanged += IdBusStopListChanged;
        }

        private void IdBusStopListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (RouteBusStop ob in e.NewItems)
                    {
                        ob.UpdateHandler += UpdateRouteBusStop;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (RouteBusStop ob in e.OldItems)
                    {
                        ob.UpdateHandler += UpdateRouteBusStop;
                    }

                    for (int i = e.OldStartingIndex; i < _idBusStopList.Count; i++)
                    {
                        if (i == 0)
                        {
                            _idBusStopList[i].SetId(1);
                        }
                        else if (i == _idBusStopList.Count)
                        {
                            break;
                        }
                        else
                        {
                            _idBusStopList[i].SetId(_idBusStopList[i-1].Id+1);
                        }

                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            UpdateHandler?.Invoke(this, new UpdateEventArgs(IsValidated));
        }

        private void UpdateRouteBusStop(object sender, UpdateEventArgs e)
        {
            if (e.IsValidated)
            {
                var list = _idBusStopList.ToList();
                list.Sort(
                delegate (RouteBusStop ob1, RouteBusStop ob2)
                {
                    if (ob1.Id > ob2.Id)
                    {
                        return 1;
                    }
                    return -1;
                });
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].SetId(i + 1);
                }
                IdBusStopList = new ObservableCollection<RouteBusStop>(list);
            }
        }
    }
}
