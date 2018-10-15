using BAL.Repository;
using DAL.DbEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DAL.Model.HttpApi;
using static DAL.Model.ViewModels;

namespace QHSFApp.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class UserNewController : Controller
    {
        private UsersRepository UserRepo;
        private TabsRepository TabsRepo;
        private RolesRepository RolesRepo;

        public UserNewController()
        {
            UserRepo = new UsersRepository(new vt_QSFHEntities());
            TabsRepo = new TabsRepository(new vt_QSFHEntities());
            RolesRepo = new RolesRepository(new vt_QSFHEntities());
        }
        // GET: UserNew
        public ActionResult Index()
        {
            if (TempData["OperationType"] != null)
            {
                ViewBag.OperationType = TempData["OperationType"];
                ViewBag.Success = TempData["Success"];
                ViewBag.Message = TempData["Message"];

                TempData.Remove("OperationType");
                TempData.Remove("Success");
                TempData.Remove("Message");
            }
            //string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GetUsers");
            //GetResponse apiResponse = JsonConvert.DeserializeObject<GetResponse>(strReponse);
            //return View(apiResponse);
            return View();
        }

        public JsonResult GetCustomers_List()
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GetUsers");
            GetResponse apiResponse = JsonConvert.DeserializeObject<GetResponse>(strReponse);
            List<Vt_Users> List = apiResponse.ModelObject;

            return Json(new { List }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult UserRoles()
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GettTabs");
            GetTabsResponse apiResponse = JsonConvert.DeserializeObject<GetTabsResponse>(strReponse);
            return View(apiResponse);
        }

        [HttpPost]
        public ActionResult UserRoles(AddTabsDataNew Obj)
        {
            APIResponse Response = new APIResponse();
            bool Success = false;
            string Message = "";
            try
            {
                string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/SaveUserRoles", Obj);
                AddTabsDataNew apiResponse = JsonConvert.DeserializeObject<AddTabsDataNew>(strReponses);
                if (apiResponse.Status)
                {
                    Success = true;
                    Message = "Record Inserted Successfully";
                }
                else
                {
                    Success = false;
                    Message = "Error Occured ! While Inserting Record";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }



            ViewBag.OperationType = "Insert";
            ViewBag.Success = Success;
            ViewBag.Message = Message;

            return RedirectToAction("Index", "UserNew");


            //if (ModelState.IsValid)
            //{
            //    string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/SaveUserRoles", Obj);
            //    AddTabsDataNew apiResponse = JsonConvert.DeserializeObject<AddTabsDataNew>(strReponses);
            //    return Json(Obj, JsonRequestBehavior.AllowGet);
            //}
            //return Json(Obj);
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
            APIResponse Response = new APIResponse();
            bool Success = false;
            string Message = "";
            try
            {
                string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/EditSaveUserRoles", Obj);
                AddTabsDataNew apiResponse = JsonConvert.DeserializeObject<AddTabsDataNew>(strReponses);
                if (apiResponse.Status)
                {
                    Success = true;
                    Message = "Record Update Successfully";
                }
                else
                {
                    Success = false;
                    Message = "Error Occured! Updating Record";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            TempData["OperationType"] = "Update";
            TempData["Success"] = Success;
            TempData["Message"] = Message;

            return RedirectToAction("Index", "UserNew");


        }

    }
}