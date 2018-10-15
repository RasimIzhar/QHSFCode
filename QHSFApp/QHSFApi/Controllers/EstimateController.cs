using BAL.Repository;
using DAL.DbEntities;
using System.Collections.Generic;
using System.Web.Http;
using static DAL.Model.ViewModels;

namespace QHSFApi.Controllers
{
    public class EstimateController : ApiController
    {
        private EstimationRepository EstimationRepo;
        public EstimateController()
        {
            EstimationRepo = new EstimationRepository(new vt_QSFHEntities());
        }

        [HttpPost]
        [Route("Api/Estimation/Create")]
        public bool Create(GetEstimationData Obj)
        {
            bool Result = EstimationRepo.Create(Obj);
            return Result;
        }

        [HttpGet]
        [Route("Api/Estimation/GetJobDetails")]
        public List<Vt_JobDetails> GetJobDetails(int UserID)
        {
            List<Vt_JobDetails> Data = EstimationRepo.GetJobDetails(UserID);
            return Data;
        }


        [HttpGet]
        [Route("Api/Estimation/GetSinlgeJobDetail")]
        public Vt_JobDetails GetSinlgeJobDetail(int JobDetailID)
        {
            Vt_JobDetails Data = EstimationRepo.GetSinlgeJobDetail(JobDetailID);
            return Data;
        }

        [HttpGet]
        [Route("Api/Estimation/GetSinlgeJobDetail_ForEstimation")]
        public Vt_JobEstimation GetSinlgeJobDetail_ForEstimation(int JobDetailID)
        {
            Vt_JobEstimation Data = EstimationRepo.GetSinlgeJobDetail_ForEstimation(JobDetailID);
            return Data;
        }

        [HttpGet]
        [Route("Api/Estimation/GetJobs_Estimator")]
        public List<Vt_JobDetails> GetJobs_Estimator()
        {
            List<Vt_JobDetails> Data = EstimationRepo.GetJobs_Estimator();
            return Data;
        }
        

        public bool UpdateJobDetails_Estimation(Vt_JobDetails Obj)
        {
            bool Result = EstimationRepo.UpdateJobDetails_Estimation(Obj);
            if (Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
