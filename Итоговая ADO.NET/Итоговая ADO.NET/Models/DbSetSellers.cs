using System;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace Итоговая_ADO.NET.Models
{
    class DbSetSellers : Model<Sellers>
    {
        public override ObservableCollection<Sellers> Load()
        {
            using (WholesaleStoreEntities db = new WholesaleStoreEntities())
            {
                db.Sellers.Load();
                var sellers = db.Sellers.Local;
                return new ObservableCollection<Sellers>(sellers);
            }
        }

        public override void Add(Sellers obj)
        {
            using (WholesaleStoreEntities db = new WholesaleStoreEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Sellers.Add(obj);
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

        public override void Update(Sellers obj)
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
                        var seller = db.Sellers.Find(id);
                        db.Entry(seller).State = EntityState.Deleted;
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
