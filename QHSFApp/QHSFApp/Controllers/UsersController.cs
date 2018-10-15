using BAL.Repository;
using DAL.DbEntities;
using Newtonsoft.Json;
using System.Configuration;
using System.Web.Mvc;
using static DAL.Model.HttpApi;
using static DAL.Model.ViewModels;

namespace QHSFApp.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class UsersController : Controller
    {
        private UsersRepository UserRepo;
        private TabsRepository TabsRepo;
        private RolesRepository RolesRepo;

        public UsersController()
        {
            UserRepo = new UsersRepository(new vt_QSFHEntities());
            TabsRepo = new TabsRepository(new vt_QSFHEntities());
            RolesRepo = new RolesRepository(new vt_QSFHEntities());
        }

        // GET: Users
        [HttpGet]
        public ActionResult Index()
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GetUsers");
            GetResponse apiResponse = JsonConvert.DeserializeObject<GetResponse>(strReponse);
            return View(apiResponse);
        }

        [HttpGet]
        public ActionResult UserRoles()
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GettTabs");
            GetTabsResponse apiResponse = JsonConvert.DeserializeObject<GetTabsResponse>(strReponse);
            return View(apiResponse);
        }

        //Is Email Exists
        [HttpPost]
        public ActionResult IsEmailExists(Vt_Users User)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/IsEmailExists", User);
            GetTabsResponse apiResponse = JsonConvert.DeserializeObject<GetTabsResponse>(strReponse);
            return View(apiResponse);
        }

        [HttpPost]
        public ActionResult UserRoles(AddTabsDataNew Obj)
        {
            string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/SaveUserRoles", Obj);
            AddTabsDataNew apiResponse = JsonConvert.DeserializeObject<AddTabsDataNew>(strReponses);
            return Json(Obj, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UserRolesEdit(int id)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/EditUserRoles?id=" + id.ToString());
            GetEditRecord apiResponse = JsonConvert.DeserializeObject<GetEditRecord>(strReponse);

            return View(apiResponse);
        }

        [HttpPost]
        public ActionResult UserRolesEditObject(AddTabsDataNew Obj)
        {
            string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/EditSaveUserRoles", Obj);
            AddTabsDataNew apiResponse = JsonConvert.DeserializeObject<AddTabsDataNew>(strReponses);

            return View(Obj);
        }

        public ActionResult Create()
        {
            var items = RolesRepo.GetRoles();
            if (items != null)
            {
                ViewBag.data = items;
            }
            return View();
        }

        //[HttpPost]
        //public ActionResult Create(Vt_Users User)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Vt_Users UserObject = new Vt_Users();

        //        //User.Password = vt_Common.Encrypt(User.Password);
        //        bool Result = UserRepo.Create(User);
        //        if (Result)
        //        {
        //            return Json(new { Success = true, Message = "Data Added" }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new { Success = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        var items = RolesRepo.GetRoles();
        //        if (items != null)
        //        {
        //            ViewBag.data = items;
        //        }
        //        return View(User);
        //    }
        //}

        public ActionResult Details(int? id)
        {
            var User = UserRepo.GetUsers(id);
            //User.Password = vt_Common.Decrypt(User.Password);
            return View(User);
        }

        public ActionResult Edit(int? id)
        {
            //var items = RolesRepo.GetRoles();
            //if (items != null)
            //{
            //    ViewBag.data = items;
            //}

            var User = UserRepo.GetUsers(id);
            return View(User);
        }

        [HttpPost]
        public ActionResult Edit(Vt_Users User)
        {
            Vt_Users UserObject = new Vt_Users();
            // vt
            // UserObject.Password = vt_(User.Password);
            bool Result = UserRepo.Update(User);
            if (Result)
            {
                return Json(new { Success = true, Message = "Data Updated" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            var User = UserRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}