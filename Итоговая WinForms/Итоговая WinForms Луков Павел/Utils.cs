using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Итоговая_WinForms_Луков_Павел.Models;

namespace Итоговая_WinForms_Луков_Павел
{
    public abstract class Utils
    {
        public static int GetMaxId(List<BrandBus> data)
        {
            var list = data;

            int maxId = int.MinValue;
            foreach (var item in list)
            {
                if (item.Id > maxId) maxId = item.Id;
            }

            return maxId;
        }

        public static int GetMaxId(List<Bus> data)
        {
            var list = data;

            int maxId = int.MinValue;
            foreach (var item in list)
            {
                if (item.Id > maxId) maxId = item.Id;
            }

            return maxId;
        }

        public static int GetMaxId(List<BusStop> data)
        {
            var list = data;

            int maxId = int.MinValue;
            foreach (var item in list)
            {
                if (item.Id > maxId) maxId = item.Id;
            }

            return maxId;
        }

        public static int GetMaxId(List<Route> data)
        {
            var list = data;

            int maxId = int.MinValue;
            foreach (var item in list)
            {
                if (item.Id > maxId) maxId = item.Id;
            }

            return maxId;
        }

        public static int GetMaxId(IList<RouteBusStop> data)
        {
            var list = data;

            int maxId = int.MinValue;
            foreach (var item in list)
            {
                if (item.Id > maxId) maxId = item.Id;
            }

            return maxId;
        }

        public static void Save(List<BusStop> list, string fileName)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<BusStop>));

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, list);
            }
        }

        public static void Save(List<Bus> list, string fileName)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Bus>));

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, list);
            }
        }

        public static void Save(List<BrandBus> list, string fileName)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<BrandBus>));

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, list);
            }
        }

        public static void Save(List<Route> list, string fileName)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Route>));

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, list);
            }
        }
    }
}
