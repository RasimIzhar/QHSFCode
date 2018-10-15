using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DbEntities;

namespace BAL.Repository
{
    public class BaseRepository
    {
        public vt_QSFHEntities DBContext;

        public BaseRepository()
        {
            DBContext = new vt_QSFHEntities();
        }

        public BaseRepository(vt_QSFHEntities ContextDB)
        {
            DBContext = ContextDB;
            DBContext.Configuration.LazyLoadingEnabled = false;
            DBContext.Configuration.ProxyCreationEnabled = false;
        }
        public void SaveChanges()
        {
            DBContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DBContext != null)
                {
                    DBContext.Dispose();
                    DBContext = null;
                }
            }
        }
        ~BaseRepository()
        {
            Dispose(false);
        }
    }
}
