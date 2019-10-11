using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Итоговая_ADO.NET.Models
{
    class DbSetUnitStorages: Model<UnitStorages>
    {
        public override ObservableCollection<UnitStorages> Load()
        {
            using (WholesaleStoreEntities db = new WholesaleStoreEntities())
            {
                var unitStorages = db.UnitStorages.ToList();
                return new ObservableCollection<UnitStorages>(unitStorages);
            }
        }

        public override void Add(UnitStorages obj)
        {
            using (WholesaleStoreEntities db = new WholesaleStoreEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.UnitStorages.Add(obj);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public override void Update(UnitStorages obj)
        {
            using (WholesaleStoreEntities db = new WholesaleStoreEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public override void Delete(int id)
        {
            using (WholesaleStoreEntities db = new WholesaleStoreEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var unitStorage = db.UnitStorages.Find(id);
                        db.Entry(unitStorage).State = EntityState.Deleted;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
