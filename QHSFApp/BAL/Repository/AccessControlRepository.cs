using DAL.DbEntities;
using DAL.Model;
using System.Data;
using System.Linq;
using System.Web.Security;

namespace BAL.Repository
{
    public class AccessControlRepository : BaseRepository
    {
        private DbClass DbClass;

        public AccessControlRepository()
            : base()
        {
            DbClass = new DbClass();
        }

        public AccessControlRepository(vt_QSFHEntities ContextDB) : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public Vt_Users Verification(Vt_Users user)
        {
            var usersinfo = (from x in DBContext.Vt_Users where x.Email == user.Email && x.Password == user.Password select x).FirstOrDefault();
            if (usersinfo != null)
            {
                FormsAuthentication.SetAuthCookie(usersinfo.ID.ToString(), false);
                return usersinfo;
            }

            return null;
        }
    }
}