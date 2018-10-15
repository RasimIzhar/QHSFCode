using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DbEntities;

namespace BAL.Repository
{
    public class RolesRepository : BaseRepository
    {
        public RolesRepository()
            : base() { }
        public RolesRepository(vt_QSFHEntities ContextDB) : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        //Get List of Records
        public List<vt_Roles> GetRoles()
        {

            List<vt_Roles> Roles = DBContext.vt_Roles.ToList();
            if (Roles == null)
            {
                return null;
            }
            else
            {
                return Roles;
            }
        }

    }
}
