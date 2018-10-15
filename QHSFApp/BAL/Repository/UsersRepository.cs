using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static DAL.Model.ViewModels;

namespace BAL.Repository
{
    public class UsersRepository : BaseRepository
    {
        public UsersRepository()
            : base() { }

        public UsersRepository(vt_QSFHEntities ContextDB) : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        //Get List of Records
        public List<Vt_Users> GetUsers()
        {
            IEnumerable<Vt_Users> Users = DBContext.Vt_Users.OrderByDescending(x => x.ID);

            if (Users == null)
            {
                return null;
            }
            else
            {
                return Users.ToList();
            }
        }

        //Get Single Record By ID
        public Vt_Users GetUsers(int? Id)
        {
            var User = DBContext.Vt_Users.Where(a => a.ID == Id).FirstOrDefault();
            if (User == null)
            {
                return null;
            }
            else
            {
                return User;
            }
        }

        public bool SaveUserRoles(AddTabsDataNew User)
        {
            //First Delete All Record from UserRoles Where UserID
            Delete(User);
            //Then Add New Record
            Create(User);
            return true;
        }

        public bool Delete(AddTabsDataNew User)
        {
            Vt_Users user = new Vt_Users();
            UserRole Roles = new UserRole();
            user.ID = User.TabUser.UserID;
            List<UserRole> UserRoles = DBContext.UserRoles.Where(x => x.UserID == user.ID).ToList();
            if (UserRoles.Count > 0)
            {
                DBContext.UserRoles.RemoveRange(UserRoles);
                DBContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Insert Record

        public bool Create(AddTabsDataNew User)
        {
            var GetUserID = new Vt_Users();
            if (User.TabUser.UserID == 0)
            {
                Vt_Users user = new Vt_Users();

                user.FirstName = User.TabUser.UserName;
                user.Email = User.TabUser.UserEmail;
                user.Password = User.TabUser.Password;
                user.IsActive = User.TabUser.IsActive;
                user.IsDeleted = false;
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;

                DBContext.Entry(user).State = System.Data.Entity.EntityState.Added;

                DBContext.SaveChanges();

                GetUserID = DBContext.Vt_Users.OrderByDescending(x => x.ID).FirstOrDefault();
            }
            else
            {
                Vt_Users user = DBContext.Vt_Users.Where(x => x.ID == User.TabUser.UserID).FirstOrDefault();
                GetUserID = DBContext.Vt_Users.Where(x => x.ID == User.TabUser.UserID).FirstOrDefault();

                user.FirstName = User.TabUser.UserName;
                user.Email = User.TabUser.UserEmail;
                user.Password = User.TabUser.Password;
                user.IsActive = User.TabUser.IsActive;
                user.IsDeleted = false;
                user.ModifiedDate = DateTime.Now;
                DBContext.Entry(user).State = System.Data.Entity.EntityState.Modified;

                DBContext.SaveChanges();
            }

            List<UserRole> lisrUserRole = new List<UserRole>();

            foreach (var item in User.Tabs)
            {
                int tId = 0;
                bool isRoleChck = false;
                UserRole ur = new UserRole();
                if (item.Admin)
                {
                    tId = 1;
                    isRoleChck = true;
                }
                if (item.Contributor)
                {
                    tId = 2;
                    isRoleChck = true;
                }
                if (item.ContributorChecker)
                {
                    tId = 3;
                    isRoleChck = true;
                }
                if (item.ContributorDrafter)
                {
                    tId = 4;
                    isRoleChck = true;
                }
                //if (isRoleChck || !isRoleChck)
                {
                    ur.RolesID = tId;
                    ur.UserID = GetUserID.ID;
                    ur.TabID = Convert.ToInt32(item.tabid);
                    lisrUserRole.Add(ur);

                    DBContext.Entry(ur).State = System.Data.Entity.EntityState.Added;
                }
            }
            DBContext.SaveChanges();

            //var data = (from m in DBContext.Vt_Users )

            //DBContext.Entry(User).State = System.Data.Entity.EntityState.Added;
            //DBContext.SaveChanges();
            return true;
        }

        public AddTabsDataNew EditUser(int id)
        {
            AddTabsDataNew ut = new AddTabsDataNew();
            ut.TabUser = new user();
            ut.Tabs = new List<TabsNew>();
            var getUser = (from u in DBContext.Vt_Users where u.ID == id select u).FirstOrDefault();

            ut.TabUser.UserName = getUser.FirstName;
            ut.TabUser.UserEmail = getUser.Email;
            ut.TabUser.Password = getUser.Password;
            ut.TabUser.IsActive = getUser.IsActive;
            ut.TabUser.UserID = getUser.ID;

            #region OldWorkingCode

            var getPermisions = (from r in DBContext.UserRoles
                                 join t in DBContext.vt_Tabs on r.TabID equals t.ID
                                 where r.UserID == id
                                 select
            new
            {
                r.RolesID,
                r.TabID,
                r.UserID,
                t.Title
            }
                                 ).ToList();
            var getAlltabs = (from alltab in DBContext.vt_Tabs select alltab).ToList();

            foreach (var gp in getPermisions)
            {
                getAlltabs.Remove(getAlltabs.Find(x => x.ID == (gp.TabID)));
            }

            foreach (var itemPermission in getPermisions)
            {
                TabsNew item = new TabsNew();
                if (itemPermission.RolesID == 1)
                {
                    item.Admin = true;
                }
                else
                {
                    item.Admin = false;
                }

                if (itemPermission.RolesID == 2)
                {
                    item.Contributor = true;
                }
                else
                {
                    item.Contributor = false;
                }

                if (itemPermission.RolesID == 3)
                {
                    item.ContributorChecker = true;
                }
                else
                {
                    item.ContributorChecker = false;
                }

                if (itemPermission.RolesID == 4)
                {
                    item.ContributorDrafter = true;
                }
                else
                {
                    item.ContributorDrafter = false;
                }
                //TAB ID AND TITLE
                item.tabid = itemPermission.TabID.ToString();
                item.TabName = itemPermission.Title.ToString();
                ut.Tabs.Add(item);
            }

            #endregion OldWorkingCode

            return ut;
        }

        //Update Record
        public bool Update(Vt_Users User)
        {
            DBContext.Entry(User).State = System.Data.Entity.EntityState.Modified;
            DBContext.SaveChanges();
            return true;
        }

        //Delete Record
        public bool Delete(int Id)
        {
            Vt_Users User = DBContext.Vt_Users.Find(Id);
            DBContext.Vt_Users.Remove(User);
            DBContext.SaveChanges();
            return true;
        }

        //IF Email Exists
        public bool IsEmailExists(string Email)
        {
            string Result = DBContext.Vt_Users.Where(x => x.Email == Email).Select(x => x.Email).FirstOrDefault();
            if (!string.IsNullOrEmpty(Result))
            {
                return true;
            }
            return false;
        }

        #region SALMAN_CODE

        public object UsersWrtRoles(int TabID, int RoleID)
        {
            var Data = (dynamic)null;
            SqlParameter[] param = {
                                    new SqlParameter("@TabID",TabID),
                                    new SqlParameter("@RoleID",RoleID)
                               };
            DataTable dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetUsersByTabIDandRoleID", param);
            if (dt.Rows.Count > 0)
            {
                Data = (from DataRow row in dt.Rows

                        select new
                        {
                            FirstName = row["FirstName"],
                            ID = row["ID"]
                        }).ToList();
            }
            return Data;
        }

        public object UsersWrtRoles_Drafting(int TabID, int Checker_RoleID, int Drafter_RoleID)
        {
            var Data = (dynamic)null;
            int RoleID = 0;
            DataTable dt;
            if (Drafter_RoleID != 0)
            {
                RoleID = Drafter_RoleID;

                SqlParameter[] param = {
                    new SqlParameter("@TabID", TabID),
                    new SqlParameter("@RoleID", RoleID)
                               };

                dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetUsersByTabIDandRoleID", param);
            }
            else
            {
                RoleID = Checker_RoleID;

                SqlParameter[] param = {
                    new SqlParameter("@TabID", TabID),
                    new SqlParameter("@RoleID", RoleID)
                               };

                dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetUsersByTabIDandRoleID", param);
            }
           
            if (dt.Rows.Count > 0)
            {
                Data = (from DataRow row in dt.Rows

                        select new
                        {
                            FirstName = row["FirstName"],
                            ID = row["ID"]
                        }).ToList();
            }
            return Data;
        }

        #endregion SALMAN_CODE
    }
}