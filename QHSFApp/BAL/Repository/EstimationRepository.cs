using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using static DAL.Model.ViewModels;

namespace BAL.Repository
{
    public class EstimationRepository : BaseRepository
    {
        public EstimationRepository() : base()
        {
        }

        //This is for Estimation Tab
        private int TabID = 3;

        public EstimationRepository(vt_QSFHEntities ContextDB) : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public bool Create(GetEstimationData Obj)
        {
            var JobEstimationID = DBContext.Vt_JobEstimation.Where(x => x.JobID == Obj.JobEstimation.JobID).FirstOrDefault();

            Vt_JobEstimation JobEstimation = new Vt_JobEstimation();
            List<Vt_JobEstimationDetails> JobEstimationDetails = new List<Vt_JobEstimationDetails>();

            //Select Job Estimation
            if (JobEstimationID != null)
            {
                JobEstimation = DBContext.Vt_JobEstimation.Where(x => x.ID == JobEstimationID.ID).FirstOrDefault();

                //Update Job Estimation

                JobEstimation.JobID = Obj.JobEstimation.JobID;
                JobEstimation.CustomerID = Obj.JobEstimation.CustomerID;
                JobEstimation.CreatedDate = Obj.JobEstimation.CreatedDate;
                JobEstimation.QuoteNumber = Obj.JobEstimation.QuoteNumber;
                JobEstimation.RevisionNumber = Obj.JobEstimation.RevisionNumber;
                DBContext.Entry(JobEstimation).State = System.Data.Entity.EntityState.Modified;

                DBContext.SaveChanges();
            }
            //Create Job Estimation
            else
            {
                DBContext.Entry(Obj.JobEstimation).State = System.Data.Entity.EntityState.Added;

                DBContext.SaveChanges();
            }

            var JobEstimationID2 = DBContext.Vt_JobEstimation.Where(x => x.JobID == Obj.JobEstimation.JobID).FirstOrDefault();
            var StageIDRecord = DBContext.Vt_JobEstimationDetails.Where(x => x.JobEstimationID == JobEstimationID2.ID).FirstOrDefault();

            //Select Job Estimation Details
            if (StageIDRecord != null)
            {
                JobEstimationDetails = DBContext.Vt_JobEstimationDetails.Where(x => x.JobEstimationID == JobEstimationID2.ID && x.StageID == Obj.StageID).ToList();

                //Update and Create Job Estimation Details
                if (JobEstimationDetails != null && JobEstimationDetails.Count > 0)
                {
                    DBContext.Vt_JobEstimationDetails.RemoveRange(JobEstimationDetails);
                    DBContext.SaveChanges();
                }
            }
            // List<Vt_JobEstimationDetails> NewData = new List<Vt_JobEstimationDetails>();

            foreach (var item in Obj.Estimation)
            {
                Vt_JobEstimationDetails List = new Vt_JobEstimationDetails();
                List.JobID = Obj.JobEstimation.JobID;
                List.JobEstimationID = JobEstimationID2.ID;
                List.ProductID = Convert.ToInt32(item.ProductID);
                List.Quantity = Convert.ToInt32(item.Qty);
                List.StageID = Obj.StageID;
                List.UnitPrice = Convert.ToDecimal(item.UnitPrice);
                List.MarkupValue = Convert.ToInt32(Obj.MarkupValue);
                List.Notes = Obj.Notes;
                //Calculate Total
                var TotalPrice = Convert.ToInt32(item.Qty) * Convert.ToDecimal(item.UnitPrice);
                List.TotalPrice = TotalPrice;

                // NewData.Add(List);
                DBContext.Entry(List).State = System.Data.Entity.EntityState.Added;
            }

            DBContext.SaveChanges();
            return true;
        }

        public List<Vt_JobDetails> GetJobDetails(int UserID)
        {
            List<Vt_JobDetails> Data = DBContext.Vt_JobDetails.Include("Vt_Jobs").Where(x => x.UserAssignedID == UserID && x.TabID == TabID).ToList();
            if (Data != null && Data.Count > 0)
            {
                return Data;
            }
            else
            {
                return null;
            }
        }

        public Vt_JobDetails GetSinlgeJobDetail(int JobDetailID)
        {
            Vt_JobDetails Data = DBContext.Vt_JobDetails.Include("Vt_Jobs").Where(x => x.ID == JobDetailID && x.TabID == TabID).FirstOrDefault();
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return null;
            }
        }

        public Vt_JobEstimation GetSinlgeJobDetail_ForEstimation(int JobDetailID)
        {
            Vt_JobDetails JobDetails = DBContext.Vt_JobDetails.Where(x => x.ID == JobDetailID && x.TabID == TabID).FirstOrDefault();
            Vt_JobEstimation Data = DBContext.Vt_JobEstimation.Where(x => x.JobID == JobDetails.JobID).FirstOrDefault();
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return null;
            }
        }

        public List<Vt_JobDetails> GetJobs_Estimator()
        {
            //This ID is for Estimation Tab
            int TabID = 3;
            List<Vt_JobDetails> Data = DBContext.Vt_JobDetails.Include("Vt_Jobs").Where(x => x.TabID == TabID).ToList();
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return null;
            }
        }

        public bool UpdateJobDetails_Estimation(Vt_JobDetails Obj)
        {
            Vt_JobDetails JobDetails = new Vt_JobDetails();

            //if Record is in Estimation Level
            Vt_JobDetails UpdateObj = DBContext.Vt_JobDetails.Where(x => x.JobID == Obj.JobID && x.TabID == 3).FirstOrDefault();
            if (UpdateObj != null)
            {
                UpdateObj.IsApproved = true;
                DBContext.Entry(UpdateObj).State = System.Data.Entity.EntityState.Modified;
                DBContext.SaveChanges();
            }

            //if Record is in Drafting Level
            Vt_JobDetails ExistingRecord = DBContext.Vt_JobDetails.Where(x => x.JobID == Obj.JobID && x.TabID == 4).FirstOrDefault();
            if (ExistingRecord != null)
            {
                ExistingRecord.UserAssignedID = Obj.UserAssignedID;
                DBContext.Entry(ExistingRecord).State = System.Data.Entity.EntityState.Modified;
                DBContext.SaveChanges();
            }

            JobDetails.JobID = Obj.JobID;
            JobDetails.CustomerID = Obj.CustomerID;
            JobDetails.TabID = Obj.TabID;
            JobDetails.UserAssignedID = Obj.UserAssignedID;

            DBContext.Entry(JobDetails).State = System.Data.Entity.EntityState.Added;

            if (DBContext.SaveChanges() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}