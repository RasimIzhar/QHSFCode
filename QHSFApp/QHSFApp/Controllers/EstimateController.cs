using DAL.DbEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using static DAL.Model.HttpApi;
using static DAL.Model.ViewModels;

namespace QHSFApp.Controllers
{
    public class EstimateController : Controller
    {
        [Authorize(Roles = "SuperAdmin,Admin,Contributor,ContributorChecker,ContributorDrafter")]
        // GET: Estimate
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
            //int ID = Convert.ToInt32(Session["LogedInUserID"]);
            //string strReponse = "";
            //if (User.IsInRole("Admin"))
            //{
            //    strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Estimation/GetJobs_Estimator");
            //}
            //else
            //{
            //    strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Estimation/GetJobDetails?UserID=" + ID);
            //}
            //List<Vt_JobDetails> apiResponse = JsonConvert.DeserializeObject<List<Vt_JobDetails>>(strReponse);
            //return View(apiResponse);
            return View();
        }

        public JsonResult GetJobs_List()
        {
            //string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Customers/GetCustomers");
            //GetCustomersResponse apiResponse = JsonConvert.DeserializeObject<GetCustomersResponse>(strReponse);
            //List<Vt_Customers> List = apiResponse.ModelObject;

            int ID = Convert.ToInt32(Session["LogedInUserID"]);
            string strReponse = "";
            if (User.IsInRole("Admin"))
            {
                strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Estimation/GetJobs_Estimator");
            }
            else
            {
                strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Estimation/GetJobDetails?UserID=" + ID);
            }
            List<Vt_JobDetails> List = JsonConvert.DeserializeObject<List<Vt_JobDetails>>(strReponse);

            return Json(new { List }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EstimateDetails(int JobDetailID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Customers/GetCustomers");
            GetCustomersResponse apiResponse = JsonConvert.DeserializeObject<GetCustomersResponse>(strReponse);

            string strReponse2 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Products/GetStages");
            List<Vt_ProductsStages> apiResponse2 = JsonConvert.DeserializeObject<List<Vt_ProductsStages>>(strReponse2);

            string strReponse3 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Products/GetProductByStages?ID=1");
            GetEstimationResponse apiResponse3 = JsonConvert.DeserializeObject<GetEstimationResponse>(strReponse3);

            string strReponse4 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Estimation/GetSinlgeJobDetail?JobDetailID=" + JobDetailID);
            Vt_JobDetails apiResponse4 = JsonConvert.DeserializeObject<Vt_JobDetails>(strReponse4);

            //ViewBag.DropDown = new SelectList(apiResponse2.ModelObject, "ID", "CustomerName", apiResponse.CustomerID);

            var customer = apiResponse.ModelObject;
            ViewBag.Customers = customer;//new SelectList(, "ID", "CustomerName", apiResponse4.CustomerID);
            ViewBag.Stage = new SelectList(apiResponse2, "ID", "Stage", "Select Stage");

            return View(apiResponse4);
        }


        public ActionResult EstimateApprove(int JobDetailID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Customers/GetCustomers");
            GetCustomersResponse apiResponse = JsonConvert.DeserializeObject<GetCustomersResponse>(strReponse);

            string strReponse2 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Products/GetStages");
            List<Vt_ProductsStages> apiResponse2 = JsonConvert.DeserializeObject<List<Vt_ProductsStages>>(strReponse2);

            string strReponse3 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Products/GetProductByStages?ID=1");
            GetEstimationResponse apiResponse3 = JsonConvert.DeserializeObject<GetEstimationResponse>(strReponse3);

            string strReponse4 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Estimation/GetSinlgeJobDetail?JobDetailID=" + JobDetailID);
            Vt_JobDetails apiResponse4 = JsonConvert.DeserializeObject<Vt_JobDetails>(strReponse4);

            //GET ESTIMATION DETAILS
            string strReponse5 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Estimation/GetSinlgeJobDetail_ForEstimation?JobDetailID=" + JobDetailID);
            Vt_JobEstimation apiResponse5 = JsonConvert.DeserializeObject<Vt_JobEstimation>(strReponse5);
            if (apiResponse5 != null)
            {
                ViewBag.QuoteNumber = apiResponse5.QuoteNumber;
                ViewBag.EstDate = apiResponse5.CreatedDate;
                ViewBag.RevisionNumber = apiResponse5.RevisionNumber;
            }
            //ViewBag.DropDown = new SelectList(apiResponse2.ModelObject, "ID", "CustomerName", apiResponse.CustomerID);

            var customer = apiResponse.ModelObject;
            ViewBag.Customers = customer;//new SelectList(, "ID", "CustomerName", apiResponse4.CustomerID);
            ViewBag.Stage = new SelectList(apiResponse2, "ID", "Stage", "Select Stage");

            return View(apiResponse4);
        }

        [HttpGet]
        public ActionResult GetProductByStages(int JobID, int StageID, int CustomerID)
        {
            string strReponse3 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Products/GetProductByStagesAndCustomerID?JobId=" + JobID + "&StageID=" + StageID + "&CustomerID=" + CustomerID + "");

            GetEstimationData apiResponse3 = JsonConvert.DeserializeObject<GetEstimationData>(strReponse3);

            if (apiResponse3.Estimation.Count > 0)
            {
                apiResponse3.MarkupValue = apiResponse3.Estimation.FirstOrDefault().Markup;
            }

            return PartialView("_EstimateProducts", apiResponse3);
            //return Obj;
        }

        //[HttpPost]
        //public ActionResult AddEstimationDetails(GetEstimationData data)
        //{
        //    string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Estimation/Create", data);
        //    bool apiResponse = JsonConvert.DeserializeObject<bool>(strReponse);

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public ActionResult AddEstimationDetails(GetEstimationData data)
        {
            APIResponse Response = new APIResponse();
            bool Success = false;
            string Message = "";
            try
            {
                string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Estimation/Create", data);
                bool apiResponse = JsonConvert.DeserializeObject<bool>(strReponse);
                if (apiResponse)
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
            TempData["OperationType"] = "Insert";
            TempData["Success"] = Success;
            TempData["Message"] = Message;

            return RedirectToAction("Index", "Estimate");
        }


        //SALMAN CODE
        #region SalmanCode
        public ActionResult _ApproveJob(int JobID, int CustomerID)
        {
            int TabID = 4;
            //RoleID 1 is for Admin
            int RoleID = 1;
            string strResponse_Admin = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GetUserWrtRoles?TabID=" + TabID + "&RoleID=" + RoleID);
            object apiResponse_Admin = JsonConvert.DeserializeObject<object>(strResponse_Admin);

            ViewBag.JobID = JobID;
            ViewBag.CustomerID = CustomerID;

            ViewBag.DropDownAdmin = apiResponse_Admin;
            return View();

            //USABLE CODE CHANGE A/C TO THE REQUIREMENTS
            //int TabID = 4;

            //int RoleID_Checker = 3;
            //int RoleID_Drafter = 4;
            ////For Drafter
            //string strResponse_Drafter = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GetUserWrtRoles_Drafting?TabID=" + TabID + "&Checker_RoleID=0&Drafter_RoleID=" + RoleID_Drafter);
            //object apiResponse_Drafter = JsonConvert.DeserializeObject<object>(strResponse_Drafter);

            ////For Checker
            //string strResponse_Checker = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GetUserWrtRoles_Drafting?TabID=" + TabID + "&Checker_RoleID=" + RoleID_Checker + "&Drafter_RoleID=0");
            //object apiResponse_Checker = JsonConvert.DeserializeObject<object>(strResponse_Checker);

            //ViewBag.JobID = JobID;
            //ViewBag.CustomerID = CustomerID;

            //ViewBag.DropDownDrafter = apiResponse_Drafter;
            //ViewBag.DropDownChecker = apiResponse_Checker;

            //return View();
        }

        [HttpPost]
        public bool UpdateJobDetails_Estimation(Vt_JobDetails Obj)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Estimate/UpdateJobDetails_Estimation", Obj);
            bool apiResponse = JsonConvert.DeserializeObject<bool>(strReponse);

            return apiResponse;
        }
        #endregion
    }
}