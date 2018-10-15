using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using static DAL.Model.ViewModels;

namespace BAL.Repository
{
    public class RequirementsRepository : BaseRepository
    {
        public RequirementsRepository()
            : base() { }

        public RequirementsRepository(vt_QSFHEntities ContextDB) : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        //This ID is for Requirments Tab
        private int TabID = 2;

        public List<Vt_Jobs> GetJobs()
        {
            IEnumerable<Vt_Jobs> Data = DBContext.Vt_Jobs.OrderByDescending(x => x.ID);

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data.ToList();
            }
        }

        //Get Single Record
        public Vt_Jobs Details(int ID)
        {
            Vt_Jobs Data = DBContext.Vt_Jobs.Where(x => x.ID == ID).FirstOrDefault();

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data;
            }
        }

        public Vt_JobDetails JobDetails_Tab(int JobID, int TabID)
        {
            Vt_JobDetails Data = DBContext.Vt_JobDetails.Where(x => x.JobID == JobID && x.TabID == TabID).FirstOrDefault();

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data;
            }
        }

        //Create Record
        //Old Working Code
        public bool Create(Vt_Jobs obj)
        {
            Vt_Jobs JobObj = new Vt_Jobs();
            JobObj = obj;
            DBContext.Entry(JobObj).State = System.Data.Entity.EntityState.Added;

            Vt_JobDetails JobDetailsObj = new Vt_JobDetails();

            JobDetailsObj.JobID = JobObj.ID;
            JobDetailsObj.TabID = 2;
            JobDetailsObj.UserAssignedID = null;

            DBContext.Entry(JobDetailsObj).State = System.Data.Entity.EntityState.Added;
            if (DBContext.SaveChanges() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Update Record
        public bool Update(GetJobResponse Obj)
        {
            DBContext.Entry(Obj.ModelObject).State = System.Data.Entity.EntityState.Modified;
            if (DBContext.SaveChanges() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Delete Record
        public bool Delete(int? ID)
        {
            Vt_Jobs Data = DBContext.Vt_Jobs.Where(x => x.ID == ID).FirstOrDefault();
            DBContext.Entry(Data).State = System.Data.Entity.EntityState.Deleted;

            if (DBContext.SaveChanges() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #region OldWorkingCode
        //Old Working Code
        //public List<Vt_JobDetails> GetJobs_Admin()
        //{
        //    //IEnumerable<Vt_Jobs> Data = DBContext.Vt_Jobs.Where(x => x. == UserID).OrderByDescending(x => x.ID);
        //    IEnumerable<Vt_JobDetails> Data = DBContext.Vt_JobDetails.Include("Vt_Jobs").Where(x => x.TabID == TabID).OrderByDescending(x => x.ID).ToList();

        //    if (Data == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return Data.ToList();
        //    }
        //}

        ////Contributor Code
        //public List<Vt_JobDetails> GetJobs_Contributor(int UserID)
        //{
        //    //IEnumerable<Vt_Jobs> Data = DBContext.Vt_Jobs.Where(x => x. == UserID).OrderByDescending(x => x.ID);
        //    IEnumerable<Vt_JobDetails> Data = DBContext.Vt_JobDetails.Include("Vt_Jobs").Where(x => x.UserAssignedID == UserID && x.TabID == TabID).OrderByDescending(x => x.ID);

        //    if (Data == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return Data.ToList();
        //    }
        //}
        #endregion
        public List<Vt_Jobs> GetJobs_Admin()
        {
            IEnumerable<Vt_Jobs> Data = DBContext.Vt_Jobs.OrderByDescending(x => x.ID).ToList();

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data.ToList();
            }
        }

        //Contributor Code
        public List<Vt_Jobs> GetJobs_Contributor(int UserID)
        {
            IEnumerable<Vt_Jobs> Data = DBContext.Vt_Jobs.Where(x => x.AssignedTo == UserID).OrderByDescending(x => x.ID);

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data.ToList();
            }
        }

        public bool Update_JobDetails(Create_JobDetailsResponse Obj)
        {
            Vt_JobDetails JobDetailsObj = DBContext.Vt_JobDetails.Where(x => x.JobID == Obj.JobDetails.JobID).FirstOrDefault();
            JobDetailsObj.CustomerID = Obj.JobDetails.CustomerID;

            DBContext.Entry(JobDetailsObj).State = System.Data.Entity.EntityState.Modified;
            if (DBContext.SaveChanges() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Vt_JobDetails GetJobDetail(int JobID)
        {
            Vt_JobDetails Data = DBContext.Vt_JobDetails.Where(x => x.JobID == JobID).FirstOrDefault();

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data;
            }
        }

        public JobDetailsStatic GetJobDetailsStatic(int JobDetailsID)
        {
            JobDetailsStatic Data = DBContext.JobDetailsStatics.Where(x => x.JobDetailsID == JobDetailsID).FirstOrDefault();

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data;
            }
        }

        public List<JobDetailsDynamicField> GetJobDetailsDyanmicField(int JobDetailsID)
        {
            List<JobDetailsDynamicField> Data = DBContext.JobDetailsDynamicFields.Where(x => x.JobDetailsID == JobDetailsID).ToList();

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data;
            }
        }

        public int GetMaxJobID()
        {
            int Result = 0;
            Vt_Jobs Data = DBContext.Vt_Jobs.OrderByDescending(x => x.ID).FirstOrDefault();
            if (Data != null)
            {
                Result = Data.ID;
            }
            else
            {
                Result = 1;
            }
            return Result;
        }

        public bool Update_JobStatus(Update_JobStatus Obj)
        {
            Vt_JobDetails JobDetails = new Vt_JobDetails();
            Vt_JobDetails UpdateObj = DBContext.Vt_JobDetails.Where(x => x.JobID == Obj.JobID).FirstOrDefault();
            if (UpdateObj != null)
            {
                UpdateObj.IsApproved = true;
                DBContext.Entry(UpdateObj).State = System.Data.Entity.EntityState.Modified;
                DBContext.SaveChanges();
            }
            //JobDetails = DBContext.Vt_JobDetails.Where(x => x.ID == Obj.JobID).FirstOrDefault();

            JobDetails.JobID = Obj.JobID;
            JobDetails.CustomerID = Obj.CustomerID;
            JobDetails.TabID = Obj.TabID;
            JobDetails.UserAssignedID = Obj.UserAssigned;

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

        public bool Update_JobDetails(Update_JobDetails Obj)
        {
            Vt_JobDetails JobDetails = new Vt_JobDetails();
            JobDetails = DBContext.Vt_JobDetails.Where(x => x.ID == Obj.JobDetails.JobID).FirstOrDefault();
            JobDetails.IsApproved = Obj.JobDetails.IsApproved;

            DBContext.Entry(JobDetails).State = System.Data.Entity.EntityState.Modified;

            JobDetails = Obj.JobDetails;
            JobDetails.TabID = 3;

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

        //New Code

        #region NewCode

        public bool Create_Answers(Create_JobDetailsResponse Obj)
        {
            StaticAnswers(Obj);
            DynamicAnswers(Obj);
            Update_JobDetails(Obj);
            return true;
        }

        public bool StaticAnswers(Create_JobDetailsResponse Obj)
        {
            Vt_JobDetails JobDetailsObj = new Vt_JobDetails();
            JobDetailsStatic StaticObj = new JobDetailsStatic();

            JobDetailsObj = DBContext.Vt_JobDetails.Where(x => x.JobID == Obj.JobDetails.JobID).FirstOrDefault();
            StaticObj = DBContext.JobDetailsStatics.Where(x => x.JobDetailsID == JobDetailsObj.ID).FirstOrDefault();
            //Update Record
            if (StaticObj != null)
            {
                StaticObj.StaticEnggForm15 = Obj.JobDetailsStatic.StaticEnggForm15;
                StaticObj.StaticEnggForm16 = Obj.JobDetailsStatic.StaticEnggForm16;
                StaticObj.StaticFreightSite = Obj.JobDetailsStatic.StaticFreightSite;
                StaticObj.Installation = Obj.JobDetailsStatic.Installation;

                DBContext.Entry(StaticObj).State = System.Data.Entity.EntityState.Modified;
            }
            //Add New Record
            else
            {
                Obj.JobDetailsStatic.JobDetailsID = JobDetailsObj.ID;
                DBContext.Entry(Obj.JobDetailsStatic).State = System.Data.Entity.EntityState.Added;
            }

            return Convert.ToBoolean(DBContext.SaveChanges());
        }

        public bool DynamicAnswers(Create_JobDetailsResponse Obj)
        {
            Vt_JobDetails JobDetailsObj = new Vt_JobDetails();
            List<JobDetailsDynamicField> DynamicObj = new List<JobDetailsDynamicField>();

            JobDetailsObj = DBContext.Vt_JobDetails.Where(x => x.JobID == Obj.JobDetails.JobID).FirstOrDefault();
            DynamicObj = DBContext.JobDetailsDynamicFields.Where(x => x.JobDetailsID == JobDetailsObj.ID).ToList();

            if (Obj.JobDetailsDynamic != null && Obj.JobDetailsDynamic.Count > 0)
            {
                //Update Records
                if (DynamicObj != null && DynamicObj.Count > 0)
                {
                    foreach (var itemDynamic in DynamicObj)
                    {
                        foreach (var itemAwnser in Obj.JobDetailsDynamic.ToList())
                        {
                            if (itemAwnser.DynamicFieldID == itemDynamic.DynamicFieldID)
                            {
                                itemDynamic.Anwer = itemAwnser.Anwer;
                            }
                            DBContext.Entry(itemDynamic).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                }
                //Add Record
                foreach (var item in Obj.JobDetailsDynamic.ToList())
                {
                    item.JobDetailsID = JobDetailsObj.ID;
                    DBContext.Entry(item).State = System.Data.Entity.EntityState.Added;
                }
            }
            return Convert.ToBoolean(DBContext.SaveChanges());
        }

        public bool Job_Update_Set_AssignedTo(Vt_Jobs Obj)
        {
            Vt_Jobs Job = DBContext.Vt_Jobs.Where(x => x.ID == Obj.ID).FirstOrDefault();
            //Update Record
            if (Job != null)
            {
                Job.AssignedTo = Obj.AssignedTo;
                DBContext.Entry(Job).State = System.Data.Entity.EntityState.Modified;
            }
            //Add Record
            else
            {
            }

            return Convert.ToBoolean(DBContext.SaveChanges());
        }

        public bool UpdateJob(Vt_Jobs Obj)
        {
            Vt_Jobs Job = DBContext.Vt_Jobs.Find(Obj.ID);
            //Update Record
            if (Job != null)
            {
                Job.Owner = Obj.Owner;
                Job.SiteAddress = Obj.SiteAddress;
                Job.SiteContactName = Obj.SiteContactName;
                Job.SiteContactPhoneNumber = Obj.SiteContactPhoneNumber;
                Job.OfficeContactName = Obj.OfficeContactName;
                Job.OfficeContactPhoneNumber = Obj.OfficeContactPhoneNumber;
                Job.AssignedTo = Obj.AssignedTo;
                DBContext.Entry(Job).State = System.Data.Entity.EntityState.Modified;
            }

            return Convert.ToBoolean(DBContext.SaveChanges());
        }

        #endregion NewCode
    }
}