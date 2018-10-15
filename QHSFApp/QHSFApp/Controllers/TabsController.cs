using BAL.Repository;
using DAL.DbEntities;
using static DAL.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QHSFApp.Controllers
{
    public class TabsController : Controller
    {

        private TabsRepository TabsRepo;
        private UserRolesRepository UserRolesRepo;
        private RolesRepository RolesRepo;

        public TabsController()
        {
            TabsRepo = new TabsRepository(new vt_QSFHEntities());
            UserRolesRepo = new UserRolesRepository(new vt_QSFHEntities());
            RolesRepo = new RolesRepository(new vt_QSFHEntities());
        }
        // GET: Tabs
        public ActionResult Index()
        {

            UserRolesViewModel UserRole = new UserRolesViewModel();
            

            List<vt_Tabs> Tab = TabsRepo.GetTabs();
            List<vt_Roles> Roles = RolesRepo.GetRoles();
            List<UserRole> UserRoles = UserRolesRepo.GetUserRoles();

            UserRole.TabName = Tab;
            UserRole.Roles = Roles;
            UserRole.UserRoles = UserRoles;
            //var Roles = RolesRepo.GetRoles();
            //ViewBag.RolesBag = Roles;

            return View(UserRole);
        }
    }
}