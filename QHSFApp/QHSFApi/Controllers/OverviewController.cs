using BAL.Repository;
using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QHSFApi.Controllers
{
    public class OverviewController : ApiController
    {
        private OverviewRepository OverviewRepo;
        public OverviewController()
        {
            OverviewRepo = new OverviewRepository(new vt_QSFHEntities());
        }

        [HttpGet]
        [Route("Api/Overview/GetOverViewJobDetailByJobID")]
        public object GetOverViewJobDetailByJobID(int JobID)
        {
            var Data = OverviewRepo.GetOverViewJobDetailByJobID(JobID);
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
        [Route("Api/Overview/GetJobsOverView")]
        public object GetJobsOverView()
        {
            var Data = OverviewRepo.GetJobsOverView();
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return null;
            }

        }
    }
}
