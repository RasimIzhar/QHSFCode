using BAL.Repository;
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
    public class RequirementsController : Controller
    {
        private RequirementsRepository RequirementRepo;
        private DynamicFieldsRepository DynamicFieldsRepo;

        public RequirementsController()
        {
            RequirementRepo = new RequirementsRepository(new vt_QSFHEntities());
            DynamicFieldsRepo = new DynamicFieldsRepository(new vt_QSFHEntities());
        }

        #region Admin

        // GET: Jobs
        [HttpGet]
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
            //Get_JobResponse apiResponse = new Get_JobResponse();
            //if (User.IsInRole("Admin"))
            //{
            //    strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/GetJobsAdmin");
            //}
            //else
            //{
            //    strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/GetJobsContributor?ID=" + ID);
            //}
            //apiResponse = JsonConvert.DeserializeObject<Get_JobResponse>(strReponse);

            //return View(apiResponse);
            return View();
        }

        public JsonResult GetJobsList()
        {
            int ID = Convert.ToInt32(Session["LogedInUserID"]);
            string strReponse = "";
            if (User.IsInRole("Admin"))
            {
                strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/GetJobsAdmin");
            }
            else
            {
                strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/GetJobsContributor?ID=" + ID);
            }

            List<Vt_Jobs> List = JsonConvert.DeserializeObject<List<Vt_Jobs>>(strReponse);

            return Json(new { List }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet]
        public ActionResult AdminIndex()
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/GetJobs");
            GetJobsResponse apiResponse = JsonConvert.DeserializeObject<GetJobsResponse>(strReponse);
            return View(apiResponse);
        }

        //Create\
        public ActionResult Create()
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/GetMaxJobID");
            int apiResponse = JsonConvert.DeserializeObject<int>(strReponse);

            int TabID = 2;
            int RoleID = 2;
            string strReponse2 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GetUserWrtRoles?TabID=" + TabID + "&RoleID=" + RoleID);
            object apiResponse2 = JsonConvert.DeserializeObject<object>(strReponse2);

            ViewBag.DropDown = apiResponse2;
            TempData["MaxJobID"] = apiResponse;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Vt_Jobs Data)
        {
            APIResponse Response = new APIResponse();
            bool Success = false;
            string Message = "";
            try
            {
                string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/Create", Data);
                bool apiResponse = JsonConvert.DeserializeObject<bool>(strReponses);

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

            return RedirectToAction("Index", "Requirements");
        }

        //Get Single Record
        public ActionResult Details(int ID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/Details?ID=" + ID);
            GetJobResponse apiResponse = JsonConvert.DeserializeObject<GetJobResponse>(strReponse);
            return View(apiResponse);
        }

        //Update
        public ActionResult Edit(int? ID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/Details?ID=" + ID);
            GetJobResponse apiResponse = JsonConvert.DeserializeObject<GetJobResponse>(strReponse);

            int TabID = 2;
            int RoleID = 2;
            string strReponse2 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GetUserWrtRoles?TabID=" + TabID + "&RoleID=" + RoleID);
            object apiResponse2 = JsonConvert.DeserializeObject<object>(strReponse2);

            ViewBag.DropDown = apiResponse2;

            return View(apiResponse);
        }

        [HttpPost]
        public ActionResult Edit(GetJobResponse Obj)
        {
            APIResponse Response = new APIResponse();
            bool Success = false;
            string Message = "";

            try
            {
                string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/Edit", Obj);
                bool apiResponse = JsonConvert.DeserializeObject<bool>(strReponses);

                if (apiResponse)
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

            return RedirectToAction("Index", "Requirements");
        }

        //Delete
        public ActionResult Delete(int? ID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/Delete?ID=" + ID);
            GetJobResponse apiResponse = JsonConvert.DeserializeObject<GetJobResponse>(strReponse);
            return RedirectToAction("Index", "Requirements");
        }

        #endregion Admin

        //Contributor

        #region Contributor

        //[Authorize(Roles = "Contributor")]
        [HttpGet]
        public ActionResult ContributorIndex()
        {
            int ID = Convert.ToInt32(Session["LogedInUserID"]);
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/GetJobsContributor?ID=" + ID);
            GetJobsResponse apiResponse = JsonConvert.DeserializeObject<GetJobsResponse>(strReponse);
            return View(apiResponse);
        }

        //[Authorize(Roles = "Contributor")]
        [HttpGet]
        public ActionResult ContributorJobDetails(int? ID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/GetJobDetailsUpdated?ID=" + ID);
            GetJobDetailsUpdatedResponse apiResponse = JsonConvert.DeserializeObject<GetJobDetailsUpdatedResponse>(strReponse);

            string strReponse2 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Customers/GetCustomers");
            GetCustomersResponse apiResponse2 = JsonConvert.DeserializeObject<GetCustomersResponse>(strReponse2);

            int TabID = 2;
            int RoleID = 2;
            string strReponse3 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GetUserWrtRoles?TabID=" + TabID + "&RoleID=" + RoleID);
            object apiResponse3 = JsonConvert.DeserializeObject<object>(strReponse3);

            ViewBag.AssignedUser = apiResponse3;

            ViewBag.DropDown = new SelectList(apiResponse2.ModelObject, "ID", "CustomerName", apiResponse.CustomerID);

            return View(apiResponse);
        }

        public ActionResult CreateJobDetails(Create_JobDetailsResponse Obj)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/CreateJobDetails", Obj);
            bool apiResponse = JsonConvert.DeserializeObject<bool>(strReponse);

            return Json(Obj, JsonRequestBehavior.AllowGet);
        }

        #endregion Contributor

        [HttpGet]
        public object GetCustomerData(int ID)
        {
            string strReponse3 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Customers/Details?ID=" + ID);
            GetCustomerResponse apiResponse3 = JsonConvert.DeserializeObject<GetCustomerResponse>(strReponse3);

            Vt_Customers Obj = apiResponse3.ModelObject;
            return Json(Obj, JsonRequestBehavior.AllowGet);
            //return Obj;
        }

        [HttpPost]
        public ActionResult UpdateJobStatus(Update_JobStatus Obj)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/UpdateJobStatus", Obj);
            bool apiResponse = JsonConvert.DeserializeObject<bool>(strReponse);

            return RedirectToAction("Index", "Requirements");
        }

        public ActionResult GetCustomers()
        {
            string RoleName = "Contributor";
            string strReponse2 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GetUserWrtRoles?RoleName=" + RoleName);
            object apiResponse2 = JsonConvert.DeserializeObject<object>(strReponse2);

            ViewBag.DropDown = apiResponse2;

            return View();
        }

        public ActionResult _ApproveJob(int JobID, int CustomerID)
        {
            int TabID = 3;
            int RoleID = 2;
            string strReponse2 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GetUserWrtRoles?TabID=" + TabID + "&RoleID=" + RoleID);
            object apiResponse2 = JsonConvert.DeserializeObject<object>(strReponse2);

            ViewBag.JobID = JobID;
            ViewBag.CustomerID = CustomerID;
            ViewBag.DropDown = apiResponse2;

            return View();
        }

        [HttpPost]
        public ActionResult UpdateJobDetails(Update_JobDetails Obj)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/UpdateJobDetails", Obj);
            bool apiResponse = JsonConvert.DeserializeObject<bool>(strReponse);

            return RedirectToAction("ContributorJobDetails", "Requirements");
        }

        [HttpPost]
        public ActionResult Job_Update_Set_AssignedTo(Vt_Jobs Obj)
        {
            APIResponse Response = new APIResponse();
            bool Success = false;
            string Message = "";
            try
            {
                string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/Job_Update_Set_AssignedTo", Obj);
                bool apiResponse = JsonConvert.DeserializeObject<bool>(strReponses);

                if (apiResponse)
                {
                    Success = true;
                    Message = "Record Update Successfully";
                }
                else
                {
                    Success = false;
                    Message = "Error Occured ! While Updating Record";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            TempData["OperationType"] = "Update";
            TempData["Success"] = Success;
            TempData["Message"] = Message;

            return RedirectToAction("Index", "Requirements");
        }

        [HttpPost]
        public JsonResult UpdateJob(Vt_Jobs Obj)
        {
            try
            {
                string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Requirements/UpdateJob", Obj);
                bool apiResponse = JsonConvert.DeserializeObject<bool>(strReponses);

                return Json(apiResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}