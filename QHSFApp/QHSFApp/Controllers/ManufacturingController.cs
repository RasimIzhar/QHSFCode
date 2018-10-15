using DAL.DbEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using static DAL.Model.HttpApi;
using static DAL.Model.ViewModels;

namespace QHSFApp.Controllers
{
    public class ManufacturingController : Controller
    {
        [Authorize(Roles = "SuperAdmin,Admin,Contributor,ContributorChecker,ContributorDrafter")]
        // GET: Manufacturing
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCalendarData()
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Manufacturing/GetCalendarData");
            IEnumerable<ManufacturingOrderDetails> Manufacturing = JsonConvert.DeserializeObject<IEnumerable<ManufacturingOrderDetails>>(strReponse);
            
            return Json(Manufacturing, JsonRequestBehavior.AllowGet);
        }
    }
}