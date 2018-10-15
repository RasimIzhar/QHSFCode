using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DbEntities;

namespace BAL.Repository
{
    public class UserRolesRepository : BaseRepository
    {
        public UserRolesRepository()
            : base() { }
        public UserRolesRepository(vt_QSFHEntities ContextDB) : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public List<UserRole> GetUserRoles()
        {

            List<UserRole> UserRole = DBContext.UserRoles.Where(x => x.UserID == 1).ToList();
            if (UserRole == null)
            {
                return null;
            }
            else
            {
                return UserRole;
            }
        }
    }
}
