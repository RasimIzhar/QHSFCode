using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace QHSFApp
{


    public class Authorization : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string UserId)
        {

            using (var context = new vt_QSFHEntities())
            {
                if (UserId != "SuperAdmin")
                {
                    var Useridint = Convert.ToInt32(UserId);

                    var controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
                    var GettabId = (from tabId in context.vt_Tabs where tabId.Title == controllerName select tabId.ID).FirstOrDefault();

                    if (GettabId != 0)
                    {
                        var result = (from Getroles in context.UserRoles
                                      join Roles in context.vt_Roles on Getroles.RolesID equals Roles.ID
                                      where Getroles.TabID == GettabId && Getroles.UserID == Useridint
                                      select Roles.RoleName).ToArray();

                        return result;
                    }
                    else
                    {

                        var result = (from getroles in context.UserRoles
                                      join role in context.vt_Roles on getroles.RolesID equals role.ID
                                      where getroles.UserID == Useridint
                                      select role.RoleName).ToArray();

                        return result;

                    }


                }
                else
                {

                    HttpContext.Current.Session["LogedInuserSupeAdmin"] = "SuperAdmin";
                    return new[] { "SuperAdmin" };

                }

            }

            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}