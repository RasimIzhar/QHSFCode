using BAL.Repository;
using DAL.DbEntities;
using System.Collections.Generic;
using System.Web.Http;
using static DAL.Model.ViewModels;

namespace QHSFApi.Controllers
{
    public class RequirementsController : ApiController
    {
        private RequirementsRepository RequirementsRepo;
        private DynamicFieldsRepository DynamicFieldsRepo;

        public RequirementsController()
        {
            RequirementsRepo = new RequirementsRepository(new vt_QSFHEntities());
            DynamicFieldsRepo = new DynamicFieldsRepository(new vt_QSFHEntities());
        }

        [HttpGet]
        [Route("Api/Requirements/GetJobs")]
        public GetJobsResponse GetJobs()
        {
            List<Vt_Jobs> Data = RequirementsRepo.GetJobs();
            GetJobsResponse Response = new GetJobsResponse();
            ApiResponseHeader Header = new ApiResponseHeader();
            if (Data != null)
            {
                Header.Status = true;
                Header.Message = "Success";
            }
            else
            {
                Header.Status = false;
                Header.Message = "No Record Found";
            }

            Response.Header = Header;
            Response.ModelObject = Data;

            return Response;
        }

        [HttpPost]
        [Route("Api/Requirements/Create")]
        public bool Create(Vt_Jobs Obj)
        {
            bool result = RequirementsRepo.Create(Obj);
            return result;
        }

        //Get Single Record
        [HttpGet]
        [Route("Api/Requirements/Details")]
        public GetJobResponse Details(int ID)
        {
            Vt_Jobs Data = RequirementsRepo.Details(ID);

            //TabID for Requirment is 2
            int TabID = 2;
            Vt_JobDetails JobDetails = RequirementsRepo.JobDetails_Tab(ID, TabID);

            GetJobResponse Response = new GetJobResponse();
            ApiResponseHeader Header = new ApiResponseHeader();
            if (Data != null)
            {
                Header.Status = true;
                Header.Message = "Success";
            }
            else
            {
                Header.Status = false;
                Header.Message = "No Data Found";
            }

            Response.Header = Header;
            Response.ModelObject = Data;
            Response.JobDetails = JobDetails;
            return Response;
        }

        //Edit
        [HttpPost]
        [Route("Api/Requirements/Edit")]
        public bool Edit(GetJobResponse Obj)
        {
            bool result = RequirementsRepo.Update(Obj);
            return result;
        }

        //Delete
        [HttpGet]
        [Route("Api/Requirements/Delete")]
        public GetJobResponse Delete(int ID)
        {
            GetJobResponse Response = new GetJobResponse();
            ApiResponseHeader Header = new ApiResponseHeader();

            bool Result = RequirementsRepo.Delete(ID);
            if (Result)
            {
                Header.Status = true;
                Header.Message = "Success";
                Response.Header = Header;
                return Response;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("Api/Requirements/GetJobsAdmin")]
        public List<Vt_Jobs> GetJobs_Admin()
        {
            List<Vt_Jobs> Data = RequirementsRepo.GetJobs_Admin();
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return null;
            }
        }

        #region Contributor

        [HttpGet]
        [Route("Api/Requirements/GetJobsContributor")]
        public List<Vt_Jobs> GetJobs_Contributor(int ID)
        {
            List<Vt_Jobs> Data = RequirementsRepo.GetJobs_Contributor(ID);
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
        [Route("Api/Requirements/GetJobDetails")]
        public GetJobDetailsResponse GetJobDetails(int ID)
        {
            Vt_Jobs Job = RequirementsRepo.Details(ID);
            List<Vt_DynamicFields> DynamicFields = DynamicFieldsRepo.GetDynamicFields_WithValues_All();

            GetJobDetailsResponse Response = new GetJobDetailsResponse();
            ApiResponseHeader Header = new ApiResponseHeader();

            Header.Status = true;
            Header.Message = "Success";

            Response.Job = Job;
            Response.DynamicFieldsWithValues = DynamicFields;

            return Response;
        }

        [HttpGet]
        [Route("Api/Requirements/GetJobDetailsUpdated")]
        public GetJobDetailsUpdatedResponse GetJobDetailsUpdated(int ID)
        {
            List<Vt_DynamicFields> DynamicFields = DynamicFieldsRepo.GetDynamicFields_WithValues_All();
            JobDetailsStatic JobDetailsStatic;
            List<JobDetailsDynamicField> JobDetailsDynamicField;
            Vt_Jobs Job = RequirementsRepo.Details(ID);
            int? CustomerID = 0;
            int JobID = Job.ID;

            Vt_JobDetails JobDetails = RequirementsRepo.GetJobDetail(JobID);
            if (JobDetails != null)
            {
                int JobDetailsID = JobDetails.ID;

                JobDetailsStatic = RequirementsRepo.GetJobDetailsStatic(JobDetailsID);
                JobDetailsDynamicField = RequirementsRepo.GetJobDetailsDyanmicField(JobDetailsID);
                CustomerID = JobDetails.CustomerID;
            }
            else
            {
                JobDetailsStatic = null;
                JobDetailsDynamicField = null;
                CustomerID = null;
            }
            GetJobDetailsUpdatedResponse Response = new GetJobDetailsUpdatedResponse();
            ApiResponseHeader Header = new ApiResponseHeader();

            Header.Status = true;
            Header.Message = "Success";

            Response.CustomerID = CustomerID;
            Response.Job = Job;
            Response.JobDetails = JobDetails;
            Response.JobDetailsStatic = JobDetailsStatic;
            Response.DynamicFieldsWithValues = DynamicFields;
            Response.JobDetailsDynamicField = JobDetailsDynamicField;

            return Response;
        }

        [HttpPost]
        [Route("Api/Requirements/CreateJobDetails")]
        public bool CreateJobDetails(Create_JobDetailsResponse Obj)
        {
            return RequirementsRepo.Create_Answers(Obj);
        }

        [HttpPost]
        [Route("Api/Requirements/UpdateJobStatus")]
        public bool UpdateJobStatus(Update_JobStatus Obj)
        {
            bool Result = RequirementsRepo.Update_JobStatus(Obj);
            if (Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        [Route("Api/Requirements/UpdateJobDetails")]
        public bool UpdateJobDetails(Update_JobDetails Obj)
        {
            bool Result = RequirementsRepo.Update_JobDetails(Obj);
            if (Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Contributor

        [Route("Api/Requirements/GetMaxJobID")]
        public int GetMaxJobID()
        {
            int Result = RequirementsRepo.GetMaxJobID();
            return Result + 1;
        }

        [HttpPost]
        [Route("Api/Requirements/Job_Update_Set_AssignedTo")]
        public bool Job_Update_Set_AssignedTo(Vt_Jobs Obj)
        {
            bool result = RequirementsRepo.Job_Update_Set_AssignedTo(Obj);
            return result;
        }

        [HttpPost]
        [Route("Api/Requirements/UpdateJob")]
        public bool UpdateJob(Vt_Jobs Obj)
        {
            bool result = RequirementsRepo.UpdateJob(Obj);
            return result;
        }
    }
}