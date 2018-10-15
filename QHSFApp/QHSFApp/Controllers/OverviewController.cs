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
    public class OverviewController : Controller
    {
        // GET: Overview
        [Authorize(Roles = "SuperAdmin,Admin,Contributor,ContributorChecker,ContributorDrafter")]
        public ActionResult Index()
        {
            //string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Overview/GetJobsOverView");
            //List<JobsOverview> apiResponse = JsonConvert.DeserializeObject<List<JobsOverview>>(strReponse);
            //Get_JobsOverview_Response res = new Get_JobsOverview_Response();
            //res.ModelObject = apiResponse;
            //return View(res);
            return View();
        }

        public JsonResult GetOverviewList()
        {
            

            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Overview/GetJobsOverView");
            List<JobsOverview> List = JsonConvert.DeserializeObject<List<JobsOverview>>(strReponse);
            

            return Json(new { List }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult JobOverViewDetail(int JobID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Overview/GetOverViewJobDetailByJobID?JobID=" + JobID);
            Get_JobOverview_Response apiResponse = new Get_JobOverview_Response();
            if (strReponse != "null")
            {
                List<JobOverview> apiRes = JsonConvert.DeserializeObject<List<JobOverview>>(strReponse);

                apiResponse.ModelObject = apiRes[0];
            }
            else
            {
                JobOverview obj = new JobOverview();
                apiResponse.ModelObject = obj;
                //ViewBag.NoData = "No Data Found.";
            }
            return View(apiResponse);
        }

    }
}