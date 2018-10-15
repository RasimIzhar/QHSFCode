using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using static DAL.Model.ViewModels;

namespace BAL.Repository
{
    public class PriceListRepository : BaseRepository
    {
        public PriceListRepository() : base() { }

        public PriceListRepository(vt_QSFHEntities ContextDB) : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public List<Vt_PriceList> GetPriceLists(int CustomerID)
        {
            IEnumerable<Vt_PriceList> Data = DBContext.Vt_PriceList.OrderByDescending(x => x.ProductID).Where(x => x.CustomerID == CustomerID);

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data.ToList();
            }
        }

        public bool Create(List<Vt_PriceList> Data)
        {
            if (Data != null)
            {
                var CustomerData = Data.LastOrDefault();
                int CustomerID = CustomerData.CustomerID;

                List<Vt_PriceList> PreviousData = GetPriceLists(CustomerID);
                if (PreviousData.Count > 0)
                {
                    DBContext.Vt_PriceList.RemoveRange(PreviousData);
                    DBContext.SaveChanges();
                }


                List<Vt_PriceList> NewData = new List<Vt_PriceList>();
                foreach (var item in Data)
                {
                    Vt_PriceList list = new Vt_PriceList();
                    list.CustomerID = item.CustomerID;
                    list.ProductID = item.ProductID;
                    list.UnitPrice = item.UnitPrice;
                    list.SellPrice = item.SellPrice;

                    NewData.Add(list);
                    DBContext.Entry(list).State = System.Data.Entity.EntityState.Added;
                }
                DBContext.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
