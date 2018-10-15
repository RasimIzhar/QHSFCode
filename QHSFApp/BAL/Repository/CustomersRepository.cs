using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using static DAL.Model.ViewModels;

namespace BAL.Repository
{
    public class CustomersRepository : BaseRepository
    {
        public CustomersRepository() : base()
        {
        }

        public CustomersRepository(vt_QSFHEntities ContextDB) : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public List<Vt_Customers> GetCustomers()
        {
            IEnumerable<Vt_Customers> Data = DBContext.Vt_Customers.OrderByDescending(x => x.ID);

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data.ToList();
            }
        }

        //Get Single Record
        public Vt_Customers Details(int ID)
        {
            Vt_Customers Data = DBContext.Vt_Customers.Where(x => x.ID == ID).FirstOrDefault();

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data;
            }
        }

        //Create Record
        public bool Create(Vt_Customers obj)
        {
            obj.CreatedDate = DateTime.Now;
            obj.ModifiedDate = DateTime.Now;

            DBContext.Entry(obj).State = System.Data.Entity.EntityState.Added;
            if (DBContext.SaveChanges() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Update Record
        public bool Update(GetCustomerResponse Obj)
        {
            Obj.ModelObject.ModifiedDate = DateTime.Now;
            DBContext.Entry(Obj.ModelObject).State = System.Data.Entity.EntityState.Modified;
            if (DBContext.SaveChanges() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Delete Record
        public bool Delete(int? ID)
        {
            Vt_Customers Data = DBContext.Vt_Customers.Where(x => x.ID == ID).FirstOrDefault();
            DBContext.Entry(Data).State = System.Data.Entity.EntityState.Deleted;

            if (DBContext.SaveChanges() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}