using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DbEntities;

namespace BAL.Repository
{
    public class TabsRepository : BaseRepository
    {
        public TabsRepository() 
            : base() { }

        public TabsRepository(vt_QSFHEntities ContextDB) : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public List<vt_Tabs> GetTabs()
        {
            List<vt_Tabs> Tabs = DBContext.vt_Tabs.ToList();
            if (Tabs == null)
            {
                return null;
            }
            else
            {
                return Tabs;
            }
        }

    }
}
