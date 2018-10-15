using BAL.Repository;
using DAL.DbEntities;
using System.Collections.Generic;
using System.Web.Http;
using static DAL.Model.ViewModels;

namespace QHSFApi.Controllers
{
    public class DraftingController : ApiController
    {
        private DraftingRepository DraftingRepo;

        public DraftingController()
        {
            DraftingRepo = new DraftingRepository(new vt_QSFHEntities());
        }

        [HttpPost]
        [Route("Api/Drafting/UpdateJobDetails_Drafting")]
        public bool UpdateJobDetails_Drafting(Update_JobDetails_Drafting Obj)
        {
            bool Result = DraftingRepo.UpdateJobDetails_Drafting(Obj);
            if (Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Drafter

        [HttpGet]
        [Route("Api/Drafting/GetJobs_Admin")]
        public JobModel GetJobs_Admin(int ID)
        {
            JobModel Response = new JobModel();
            List<Vt_JobDetails> Data = DraftingRepo.GetJobs_Admin(ID);

            Response.ListofJobDetails = Data;

            return Response;
        }

        [HttpGet]
        [Route("Api/Drafting/GetJobs_Drafter")]
        public JobModel GetJobs_Drafter(int ID)
        {
            JobModel Response = new JobModel();
            List<Vt_JobDrafting> Data = DraftingRepo.GetJobs_Drafter(ID);

            Response.ListofJobDrafting = Data;

            return Response;
        }

        [HttpGet]
        [Route("Api/Drafting/GetJobDraftingDetails")]
        public Vt_JobDrafting GetJobDraftingDetails(int JobID)
        {
            Vt_JobDrafting Data = DraftingRepo.GetJobDraftingDetails(JobID);
            return Data;
        }

        [HttpGet]
        [Route("Api/Drafting/Sp_Count_JobDrafting_Drafter_Questions")]
        public string Sp_Count_JobDrafting_Drafter_Questions()
        {
            string Data = DraftingRepo.Sp_Count_JobDrafting_Drafter_Questions();
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return "0";
            }
        }

        [HttpGet]
        [Route("Api/Drafting/Sp_Count_JobDrafting_Drafter_Checklist")]
        public string Sp_Count_JobDrafting_Drafter_Checklist(int JobDraftingID)
        {
            string Data = DraftingRepo.Sp_Count_JobDrafting_Drafter_Checklist(JobDraftingID);
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return "0";
            }
        }

        [HttpGet]
        [Route("Api/Drafting/Update_DraftingCompletionDate")]
        public bool Update_DraftingCompletionDate(int JobDraftingID)
        {
            bool Result = DraftingRepo.Update_DraftingCompletionDate(JobDraftingID);
            return Result;
        }

        [HttpGet]
        [Route("Api/Drafting/Update_CheckerCompletionDate")]
        public bool Update_CheckerCompletionDate(int JobDraftingID)
        {
            bool Result = DraftingRepo.Update_CheckerCompletionDate(JobDraftingID);
            return Result;
        }

        [HttpGet]
        [Route("Api/Drafting/Update_EnggDate")]
        public bool Update_EnggDate(int JobDraftingID, string Date) 
        {
            bool Result = DraftingRepo.Update_EnggDate(JobDraftingID, Date);
            return Result;
        }
        [HttpGet]
        [Route("Api/Drafting/Update_BookletDate")]
        public bool Update_BookletDate(int JobDraftingID, string Date) 
        {
            bool Result = DraftingRepo.Update_BookletDate(JobDraftingID, Date);
            return Result;
        }

        [HttpGet]
        [Route("Api/Drafting/DrafterQuestions")]
        public object DrafterQuestions(int JobDraftingID)
        {
            var Data = DraftingRepo.DrafterQuestions(JobDraftingID);
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return null;
            }
        }

        //Drafter Questions CRUD
        [HttpPost]
        [Route("Api/Drafting/CheckUpdate")]
        public bool CheckUpdate(Vt_JobDrafting_Drafter_Checklist Obj)
        {
            bool Result = DraftingRepo.CheckUpdate(Obj);
            return Result;
        }

        //Drafter SubQuestions CRUD
        [HttpPost]
        [Route("Api/Drafting/CheckUpdate_SubQuestion")]
        public bool CheckUpdate_SubQuestion(Vt_JobDrafting_Drafter_Checklist Obj)
        {
            bool Result = DraftingRepo.CheckUpdate_SubQuestion(Obj);
            return Result;
        }

        #endregion Drafter

        #region Checker
        [HttpGet]
        [Route("Api/Drafting/GetJobs_Checker")]
        public JobModel GetJobs_Checker(int ID)
        {
            JobModel Response = new JobModel();
            List<Vt_JobDrafting> Data = DraftingRepo.GetJobs_Checker(ID);

            Response.ListofJobDrafting = Data;

            return Response;
        }

        [HttpGet]
        [Route("Api/Drafting/Sp_Count_JobDrafting_Checker_Questions")]
        public string Sp_Count_JobDrafting_Checker_Questions()
        {
            string Data = DraftingRepo.Sp_Count_JobDrafting_Checker_Questions();
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return "0";
            }
        }

        [HttpGet]
        [Route("Api/Drafting/Sp_Count_JobDrafting_Checker_Checklist")]
        public string Sp_Count_JobDrafting_Checker_Checklist(int JobDraftingID)
        {
            string Data = DraftingRepo.Sp_Count_JobDrafting_Checker_Checklist(JobDraftingID);
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return "0";
            }
        }

        #endregion Checker
        [HttpGet]
        [Route("Api/Drafting/Checker_CheckList_ForDrafter")]
        public object Checker_CheckList_ForDrafter(int JobDraftingID)
        {
            var Data = DraftingRepo.Checker_QuestionAndCheckList(JobDraftingID);
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return null;
            }
        }
        
        [HttpGet]
        [Route("Api/Drafting/_FixUpList_ForDrafter")]
        public object FixUpList_ForDrafter(int JobDraftingID)
        {
            var Data = DraftingRepo.Checker_QuestionAndCheckList_FixUpList(JobDraftingID);
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("Api/Drafting/_FixUpList_ForChecker")]
        public object FixUpList_ForChecker(int JobDraftingID)
        {
            var Data = DraftingRepo.Checker_QuestionAndCheckList_FixUpList(JobDraftingID);
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("Api/Drafting/_FixUpConfirmList_ForDrafter")]
        public object FixUpConfirmList_ForDrafter(int JobDraftingID)
        {
            var Data = DraftingRepo.Checker_QuestionAndCheckList_FixUpConfirmList(JobDraftingID);
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        [Route("Api/Drafting/_FixUpConfirmList_ForChecker")]
        public object FixUpConfirmList_ForChecker(int JobDraftingID)
        {
            var Data = DraftingRepo.Checker_QuestionAndCheckList_FixUpConfirmList(JobDraftingID);
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        [Route("Api/Drafting/CheckUpdate_Checker")]
        public bool CheckUpdate_Checker(Vt_JobDrafting_Checker_Checklist Obj)
        {
            bool Result = DraftingRepo.CheckUpdate_Checker(Obj);
            return Result;
        }

        [HttpPost]
        [Route("Api/Drafting/CheckUpdate_FixUpList")]
        public bool CheckUpdate_FixUpList(Vt_JobDrafting_Checker_Checklist Obj)
        {
            bool Result = DraftingRepo.CheckUpdate_Checker(Obj);
            return Result;
        }

        [HttpPost]
        [Route("Api/Drafting/CheckUpdate_FixUpConfirmedList")]
        public bool CheckUpdate_FixUpConfirmedList(Vt_JobDrafting_Checker_Checklist Obj)
        {
            bool Result = DraftingRepo.CheckUpdate_Checker(Obj);
            return Result;
        }

    }
}