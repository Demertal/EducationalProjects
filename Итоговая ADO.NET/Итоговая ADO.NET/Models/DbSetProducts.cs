using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Итоговая_ADO.NET.Models
{
    class DbSetProducts: Model<Products>
    {
        public override ObservableCollection<Products> Load()
        {
            using (WholesaleStoreEntities db = new WholesaleStoreEntities())
            {
                var products = db.Products.Include("UnitStorages").ToList();
                return new ObservableCollection<Products>(products);
            }
        }

        public override void Add(Products obj)
        {
            using (WholesaleStoreEntities db = new WholesaleStoreEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        obj.UnitStorages = null;
                        db.Products.Add(obj);
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

        public override void Update(Products obj)
        {
            using (WholesaleStoreEntities db = new WholesaleStoreEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        obj.UnitStorages = null;
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
                        var product = db.Products.Find(id);
                        db.Entry(product).State = EntityState.Deleted;
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
