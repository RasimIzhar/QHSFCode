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
    public class DraftingController : Controller
    {
        [Authorize(Roles = "SuperAdmin,Admin,Contributor,ContributorChecker,ContributorDrafter")]
        // GET: Drafting
        public ActionResult Index()
        {
            //int ID = Convert.ToInt32(Session["LogedInUserID"]);
            //string strReponse = "";
            //if (User.IsInRole("Admin"))
            //{
            //    strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/GetJobs_Admin?ID=" + ID);
            //    JobModel apiResponse = JsonConvert.DeserializeObject<JobModel>(strReponse);
            //    return View(apiResponse);
            //}
            //else if (User.IsInRole("ContributorDrafter"))
            //{
            //    strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/GetJobs_Drafter?ID=" + ID);
            //    JobModel apiResponse = JsonConvert.DeserializeObject<JobModel>(strReponse);
            //    return View(apiResponse);
            //}
            //else
            //{
            //    strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/GetJobs_Checker?ID=" + ID);
            //    JobModel apiResponse = JsonConvert.DeserializeObject<JobModel>(strReponse);
            //    return View(apiResponse);
            //}
            return View();
        }

        public JsonResult GetJobsList()
        {
            int ID = Convert.ToInt32(Session["LogedInUserID"]);
            string strReponse = "";
            JobModel apiResponse = new JobModel();
            if (User.IsInRole("Admin"))
            {
                strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/GetJobs_Admin?ID=" + ID);
            }
            else if (User.IsInRole("ContributorDrafter"))
            {
                strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/GetJobs_Drafter?ID=" + ID);
            }
            else
            {
                strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/GetJobs_Checker?ID=" + ID);
            }

            //Return
            if (apiResponse.ListofJobDetails != null)
            {
                apiResponse = JsonConvert.DeserializeObject<JobModel>(strReponse);
                List<Vt_JobDetails> List = apiResponse.ListofJobDetails;
                return Json(new { List }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                apiResponse = JsonConvert.DeserializeObject<JobModel>(strReponse);
                List<Vt_JobDrafting> List = apiResponse.ListofJobDrafting;
                return Json(new { List }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult _AssignJob_Drafting(int JobID)
        {
            int TabID = 4;

            int RoleID_Checker = 3;
            int RoleID_Drafter = 4;
            //For Drafter
            string strResponse_Drafter = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GetUserWrtRoles_Drafting?TabID=" + TabID + "&Checker_RoleID=0&Drafter_RoleID=" + RoleID_Drafter);
            object apiResponse_Drafter = JsonConvert.DeserializeObject<object>(strResponse_Drafter);

            //For Checker
            string strResponse_Checker = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GetUserWrtRoles_Drafting?TabID=" + TabID + "&Checker_RoleID=" + RoleID_Checker + "&Drafter_RoleID=0");
            object apiResponse_Checker = JsonConvert.DeserializeObject<object>(strResponse_Checker);

            ViewBag.JobID = JobID;

            ViewBag.DropDownDrafter = apiResponse_Drafter;
            ViewBag.DropDownChecker = apiResponse_Checker;

            return View();
        }

        public bool UpdateJobDetails_Drafting(Update_JobDetails_Drafting Obj)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/UpdateJobDetails_Drafting", Obj);
            bool apiResponse = JsonConvert.DeserializeObject<bool>(strReponse);
            return apiResponse;
        }

        public ActionResult DraftingDetails(int JobID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/GetJobDraftingDetails?JobID=" + JobID);
            Vt_JobDrafting apiResponse = JsonConvert.DeserializeObject<Vt_JobDrafting>(strReponse);

            //DrafterDetails
            string strDrafterReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GetUserDetails?UserID=" + apiResponse.DrafterID);
            Vt_Users apiDrafterResponse = JsonConvert.DeserializeObject<Vt_Users>(strDrafterReponse);

            //CheckerDetails
            string strCheckerReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Users/GetUserDetails?UserID=" + apiResponse.CheckerID);
            Vt_Users apiCheckerResponse = JsonConvert.DeserializeObject<Vt_Users>(strCheckerReponse);

            ViewBag.DrafterName = apiDrafterResponse.FirstName;
            ViewBag.CheckerName = apiCheckerResponse.FirstName;
            return View(apiResponse);
        }

        public ActionResult _Drafter_ProgressBar(int JobDraftingID)
        {
            string QuestionsCount = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/Sp_Count_JobDrafting_Drafter_Questions");
            string QuesCountResponse = JsonConvert.DeserializeObject<string>(QuestionsCount);

            string CheckListCount = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/Sp_Count_JobDrafting_Drafter_Checklist?JobDraftingID=" + JobDraftingID);
            string CheckListCountResponse = JsonConvert.DeserializeObject<string>(CheckListCount);

            ViewBag.Count_Questions = QuesCountResponse;
            ViewBag.Count_Checklist = CheckListCountResponse;

            return View();
        }

        public bool CheckUpdate(Vt_JobDrafting_Drafter_Checklist Obj)
        {
            string strResponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/CheckUpdate", Obj);
            bool apiResonse = JsonConvert.DeserializeObject<bool>(strResponse);
            return apiResonse;
        }

        public bool CheckUpdate_SubQuestion(Vt_JobDrafting_Drafter_Checklist Obj)
        {
            string strResponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/CheckUpdate_SubQuestion", Obj);
            bool apiResonse = JsonConvert.DeserializeObject<bool>(strResponse);
            return apiResonse;
        }

        public ActionResult _Drafter(int JobDraftingID)
        {
            string QuestionsCount = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/Sp_Count_JobDrafting_Drafter_Questions");
            string QuesCountResponse = JsonConvert.DeserializeObject<string>(QuestionsCount);

            string CheckListCount = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/Sp_Count_JobDrafting_Drafter_Checklist?JobDraftingID=" + JobDraftingID);
            string CheckListCountResponse = JsonConvert.DeserializeObject<string>(CheckListCount);

            string strReponse1 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/DrafterQuestions?JobDraftingID= " + JobDraftingID);
            Drafter_Response apiResponse1 = JsonConvert.DeserializeObject<Drafter_Response>(strReponse1);

            ViewBag.Count_Drafter_Questions = QuesCountResponse;
            ViewBag.Count_Drafter_Questions_Checklist = CheckListCountResponse;

            if (Convert.ToInt32(QuesCountResponse) == Convert.ToInt32(CheckListCountResponse))
            {
                string strReponse2 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/Update_DraftingCompletionDate?JobDraftingID= " + JobDraftingID);
                bool apiResponse2 = JsonConvert.DeserializeObject<bool>(strReponse2);
            }

            return View(apiResponse1);
        }

        public ActionResult _Checker(int JobDraftingID)
        {
            string QuestionsCount = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/Sp_Count_JobDrafting_Checker_Questions");
            string QuesCountResponse = JsonConvert.DeserializeObject<string>(QuestionsCount);

            string CheckListCount = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/Sp_Count_JobDrafting_Checker_Checklist?JobDraftingID=" + JobDraftingID);
            string CheckListCountResponse = JsonConvert.DeserializeObject<string>(CheckListCount);

            string strReponse1 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/Checker_CheckList_ForDrafter?JobDraftingID=" + JobDraftingID);
            Checker_Response apiResponse1 = JsonConvert.DeserializeObject<Checker_Response>(strReponse1);

            ViewBag.Count_Checker_Questions = QuesCountResponse;
            ViewBag.Count_Checker_Questions_Checklist = CheckListCountResponse;

            if (Convert.ToInt32(QuesCountResponse) == Convert.ToInt32(CheckListCountResponse))
            {
                string strReponse2 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/Update_CheckerCompletionDate?JobDraftingID= " + JobDraftingID);
                bool apiResponse2 = JsonConvert.DeserializeObject<bool>(strReponse2);
            }

            return View(apiResponse1);
        }

        public bool CheckUpdate_Checker(Vt_JobDrafting_Checker_Checklist Obj)
        {
            string strResponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/CheckUpdate_Checker", Obj);
            bool apiResonse = JsonConvert.DeserializeObject<bool>(strResponse);
            return apiResonse;
        }

        public bool CheckUpdate_FixUpList(Vt_JobDrafting_Checker_Checklist Obj)
        {
            string strResponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/CheckUpdate_FixUpList", Obj);
            bool apiResonse = JsonConvert.DeserializeObject<bool>(strResponse);
            return apiResonse;
        }

        public bool CheckUpdate_FixUpConfirmedList(Vt_JobDrafting_Checker_Checklist Obj)
        {
            string strResponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/CheckUpdate_FixUpConfirmedList", Obj);
            bool apiResonse = JsonConvert.DeserializeObject<bool>(strResponse);
            return apiResonse;
        }

        //Checker CheckList Partial View
        public ActionResult _Checker_CheckList_ForDrafter(int JobDraftingID)
        {
            string strReponse1 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/Checker_CheckList_ForDrafter?JobDraftingID=" + JobDraftingID);
            Checker_Response apiResponse1 = JsonConvert.DeserializeObject<Checker_Response>(strReponse1);
            return View(apiResponse1);
        }

        public ActionResult _Drafter_CheckList_ForChecker(int JobDraftingID)
        {
            string strReponse1 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/DrafterQuestions?JobDraftingID=" + JobDraftingID);
            Drafter_Response apiResponse1 = JsonConvert.DeserializeObject<Drafter_Response>(strReponse1);

            string CheckListCount = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/Sp_Count_JobDrafting_Drafter_Checklist?JobDraftingID=" + JobDraftingID);
            string CheckListCountResponse = JsonConvert.DeserializeObject<string>(CheckListCount);
            ViewBag.Count_Drafter_Questions_Checklist = CheckListCountResponse;

            return View(apiResponse1);
        }

        //Fixup List Partial View
        public ActionResult _FixUpList_ForDrafter(int JobDraftingID)
        {
            string strReponse1 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/_FixUpList_ForDrafter?JobDraftingID=" + JobDraftingID);
            Checker_Response apiResponse1 = JsonConvert.DeserializeObject<Checker_Response>(strReponse1);
            return View(apiResponse1);
        }

        public ActionResult _FixUpList_ForChecker(int JobDraftingID)
        {
            string strReponse1 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/_FixUpList_ForChecker?JobDraftingID=" + JobDraftingID);
            Checker_Response apiResponse1 = JsonConvert.DeserializeObject<Checker_Response>(strReponse1);
            return View(apiResponse1);
        }

        //FixUp Confirm List Partial View
        public ActionResult _FixUpConfirmList_ForDrafter(int JobDraftingID)
        {
            string strReponse1 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/_FixUpConfirmList_ForDrafter?JobDraftingID=" + JobDraftingID);
            Checker_Response apiResponse1 = JsonConvert.DeserializeObject<Checker_Response>(strReponse1);
            return View(apiResponse1);
        }

        public ActionResult _FixUpConfirmList_ForChecker(int JobDraftingID)
        {
            string strReponse1 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/_FixUpConfirmList_ForChecker?JobDraftingID=" + JobDraftingID);
            Checker_Response apiResponse1 = JsonConvert.DeserializeObject<Checker_Response>(strReponse1);
            return View(apiResponse1);
        }

        public bool Update_EnggDate(int JobDraftingID, string Date)
        {
            string strReponse1 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/Update_EnggDate?JobDraftingID=" + JobDraftingID + "&Date=" + Date);
            bool apiResponse1 = JsonConvert.DeserializeObject<bool>(strReponse1);
            return apiResponse1;
        }

        public bool Update_BookletDate(int JobDraftingID, string Date)
        {
            string strReponse1 = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Drafting/Update_BookletDate?JobDraftingID=" + JobDraftingID + "&Date=" + Date);
            bool apiResponse1 = JsonConvert.DeserializeObject<bool>(strReponse1);
            return apiResponse1;
        }
    }
}