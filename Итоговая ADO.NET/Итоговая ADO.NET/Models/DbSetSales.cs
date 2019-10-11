using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Итоговая_ADO.NET.Models
{
    class DbSetSales : Model<Sales>
    {
        public override ObservableCollection<Sales> Load()
        {
            using (WholesaleStoreEntities db = new WholesaleStoreEntities())
            {
                var sales = db.Sales.Include("Products").Include("Sellers").ToList();
                return new ObservableCollection<Sales>(sales);
            }
        }

        public override void Add(Sales obj)
        {
            using (WholesaleStoreEntities db = new WholesaleStoreEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        obj.Products = null;
                        obj.Sellers = null;
                        db.Sales.Add(obj);
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

        public override void Update(Sales obj)
        {
            using (WholesaleStoreEntities db = new WholesaleStoreEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        obj.Products = null;
                        obj.Sellers = null;
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
                        var sale = db.Sales.Find(id);
                        db.Entry(sale).State = EntityState.Deleted;
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
