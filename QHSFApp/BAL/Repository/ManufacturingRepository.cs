using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static DAL.Model.ViewModels;
namespace BAL.Repository
{
    public class ManufacturingRepository : BaseRepository
    {
        private int TabID = 5;

        public ManufacturingRepository()
            : base() { }

        public ManufacturingRepository(vt_QSFHEntities ContextDB) : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public IEnumerable<ManufacturingOrderDetails> GetCalendarData()
        {
            var Manufacturing = new List<ManufacturingOrderDetails>();
            var manufacturingOrder = DBContext.Vt_ManufacturingOrderDetails.Select(s => new { s.Vt_Jobs.ID, s.Vt_Jobs.JobTitle, s.Start, s.End, s.Class, s.Priority }).OrderBy(o => o.Priority).ThenBy(t => t.ID).ToList();
        
            if (manufacturingOrder.Count() != 0)
            {
                foreach (var key in manufacturingOrder)
                {
                    ManufacturingOrderDetails obj = new ManufacturingOrderDetails()
                    {
                        ID = key.ID,
                        JobTitle = key.JobTitle,
                        Start = key.Start,
                        End = key.End,
                        Class = key.Class,
                        Priority = key.Priority
                    };
                    Manufacturing.Add(obj);
                }
                return Manufacturing;
            }
            else
            {
                return null;
            }
        }
    }
}
