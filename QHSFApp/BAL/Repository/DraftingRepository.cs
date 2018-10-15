using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static DAL.Model.ViewModels;

namespace BAL.Repository
{
    public class DraftingRepository : BaseRepository
    {
        private int TabID = 4;

        public DraftingRepository()
            : base() { }

        public DraftingRepository(vt_QSFHEntities ContextDB) : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public bool UpdateJobDetails_Drafting(Update_JobDetails_Drafting Obj)
        {
            //Vt_JobDetails JobDetails = new Vt_JobDetails();
            //Vt_JobDetails UpdateObj = DBContext.Vt_JobDetails.Where(x => x.JobID == Obj.JobID && x.TabID == 3).FirstOrDefault();
            //if (UpdateObj != null)
            //{
            //    UpdateObj.IsApproved = true;
            //    DBContext.Entry(UpdateObj).State = System.Data.Entity.EntityState.Modified;
            //    DBContext.SaveChanges();
            //}
            ////JobDetails = DBContext.Vt_JobDetails.Where(x => x.ID == Obj.JobID).FirstOrDefault();

            //JobDetails.JobID = Obj.JobID;
            //JobDetails.CustomerID = Obj.CustomerID;
            //JobDetails.TabID = Obj.TabID;
            //JobDetails.UserAssignedID = null;

            //DBContext.Entry(JobDetails).State = System.Data.Entity.EntityState.Added;

            //Vt_JobDrafting JobDrafting = new Vt_JobDrafting();
            //JobDrafting.JobID = Obj.JobID;
            //JobDrafting.DrafterID = Obj.DrafterID;
            //JobDrafting.CheckerID = Obj.CheckerID;
            //JobDrafting.DraftingDueDate = Obj.DraftingDueDate;
            //JobDrafting.CheckerDueDate = Obj.CheckerDueDate;

            //DBContext.Entry(JobDrafting).State = System.Data.Entity.EntityState.Added;

            //if (DBContext.SaveChanges() == 0)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}

            Vt_JobDrafting JobDrafting = new Vt_JobDrafting();
            JobDrafting = DBContext.Vt_JobDrafting.Where(x => x.JobID == Obj.JobID).FirstOrDefault();
            if (JobDrafting != null)
            {
                JobDrafting.DrafterID = Obj.DrafterID;
                JobDrafting.CheckerID = Obj.CheckerID;
                JobDrafting.DraftingDueDate = Obj.DraftingDueDate;
                JobDrafting.CheckerDueDate = Obj.CheckerDueDate;

                DBContext.Entry(JobDrafting).State = System.Data.Entity.EntityState.Modified;
                DBContext.SaveChanges();
            }
            else
            {
                Vt_JobDrafting Data = new Vt_JobDrafting();
                Data.JobID = Obj.JobID;
                Data.DrafterID = Obj.DrafterID;
                Data.CheckerID = Obj.CheckerID;
                Data.DraftingDueDate = Obj.DraftingDueDate;
                Data.CheckerDueDate = Obj.CheckerDueDate;

                DBContext.Entry(Data).State = System.Data.Entity.EntityState.Added;
                DBContext.SaveChanges();
            }
            return true;
        }

        #region Drafter
        public bool Update_DraftingCompletionDate(int JobDraftingID)
        {
            Vt_JobDrafting JobDrafting = DBContext.Vt_JobDrafting.Find(JobDraftingID);
            if (JobDrafting != null)
            {
                JobDrafting.DraftingCompletionDate = DateTime.Now;

                DBContext.Entry(JobDrafting).State = System.Data.Entity.EntityState.Modified;
                DBContext.SaveChanges();
                
            }
            return true;
        }

        public bool Update_CheckerCompletionDate(int JobDraftingID)
        {
            Vt_JobDrafting JobDrafting = DBContext.Vt_JobDrafting.Find(JobDraftingID);
            if (JobDrafting != null)
            {
                JobDrafting.CheckerCompletionDate = DateTime.Now;

                DBContext.Entry(JobDrafting).State = System.Data.Entity.EntityState.Modified;
                DBContext.SaveChanges();

            }
            return true;
        }

        public bool Update_EnggDate(int JobDraftingID, string Date)
        {
            Vt_JobDrafting JobDrafting = DBContext.Vt_JobDrafting.Find(JobDraftingID);
            if (JobDrafting != null)
            {
                JobDrafting.EngineeringCompletionDate = Convert.ToDateTime(Date);
                DBContext.Entry(JobDrafting).State = System.Data.Entity.EntityState.Modified;
                DBContext.SaveChanges();

            }
            return true;
        }

        public bool Update_BookletDate(int JobDraftingID, string Date)
        {
            Vt_JobDrafting JobDrafting = DBContext.Vt_JobDrafting.Find(JobDraftingID);
            if (JobDrafting != null)
            {
                JobDrafting.BookletSentDate = Convert.ToDateTime(Date);
                DBContext.Entry(JobDrafting).State = System.Data.Entity.EntityState.Modified;
                DBContext.SaveChanges();

            }
            return true;
        }
        public string Sp_Count_JobDrafting_Drafter_Questions()
        {
            var Data = (dynamic)null;
            DataTable dt = Entity_Common.get_SP_DataTable(DBContext, "Sp_Count_JobDrafting_Drafter_Questions");
            if (dt.Rows.Count > 0)
            {
                Data = (from DataRow row in dt.Rows

                        select new
                        {
                            Result = row["Result"]
                        }).ToList();
            }
            return Convert.ToString(Data[0].Result);
        }

        public string Sp_Count_JobDrafting_Drafter_Checklist(int JobDraftingID)
        {
            var Data = (dynamic)null;
            SqlParameter[] param = {
                                    new SqlParameter("@JobDraftingID", JobDraftingID)
                               };
            DataTable dt = Entity_Common.get_SP_DataTable(DBContext, "Sp_Count_JobDrafting_Drafter_Checklist", param);
            if (dt.Rows.Count > 0)
            {
                Data = (from DataRow row in dt.Rows

                        select new
                        {
                            Result = row["Result"]
                        }).ToList();
            }
            return Convert.ToString(Data[0].Result);
        }

        public Drafter_Response DrafterQuestions(int JobDraftingID)
        {
            Drafter_Response Response = new Drafter_Response();

            List<Vt_JobDrafting_Drafter_Questions> DataQuestion = DBContext.Vt_JobDrafting_Drafter_Questions.ToList();
            List<Vt_JobDrafting_Drafter_SubQuestions> DataSubQuestion = DBContext.Vt_JobDrafting_Drafter_SubQuestions.ToList();

            //UNCOMMENT THIS
            //JobDraftingID should be set in future
            //List<Vt_JobDrafting_Drafter_Checklist> CheckList = DBContext.Vt_JobDrafting_Drafter_Checklist.Where(x=>x.JobDraftingID == JobDraftingID).ToList();
            List<Vt_JobDrafting_Drafter_Checklist> CheckList = DBContext.Vt_JobDrafting_Drafter_Checklist.Where(x=>x.JobDraftingID == JobDraftingID).ToList();

            Response.Questions = DataQuestion;
            Response.SubQuestions = DataSubQuestion;
            Response.CheckList = CheckList;
            return Response;
        }

        public List<Vt_JobDetails> GetJobs_Admin(int UserID)
        {
            //IEnumerable<Vt_Jobs> Data = DBContext.Vt_Jobs.Where(x => x. == UserID).OrderByDescending(x => x.ID);
            IEnumerable<Vt_JobDetails> Data = DBContext.Vt_JobDetails.Include("Vt_Jobs").Where(x => x.UserAssignedID == UserID).OrderByDescending(x => x.ID);

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data.ToList();
            }
        }

        public List<Vt_JobDrafting> GetJobs_Drafter(int UserID)
        {
            //IEnumerable<Vt_Jobs> Data = DBContext.Vt_Jobs.Where(x => x. == UserID).OrderByDescending(x => x.ID);
            IEnumerable<Vt_JobDrafting> Data = DBContext.Vt_JobDrafting.Include("Vt_Jobs").Where(x => x.DrafterID == UserID).OrderByDescending(x => x.ID);

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data.ToList();
            }
        }

        public Vt_JobDrafting GetJobDraftingDetails(int JobID)
        {
            //IEnumerable<Vt_Jobs> Data = DBContext.Vt_Jobs.Where(x => x. == UserID).OrderByDescending(x => x.ID);
            Vt_JobDrafting Data = DBContext.Vt_JobDrafting.Include("Vt_Jobs").Where(x => x.JobID == JobID).FirstOrDefault();

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data;
            }
        }

        public bool CheckUpdate(Vt_JobDrafting_Drafter_Checklist Obj)
        {
            Vt_JobDrafting_Drafter_Checklist Data = DBContext.Vt_JobDrafting_Drafter_Checklist.Where(x => x.JobDraftingID == Obj.JobDraftingID && x.QuestionID == Obj.QuestionID).FirstOrDefault();
            if (Data != null)
            {
                Data.Status = Obj.Status;
                DBContext.Entry(Data).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                DBContext.Entry(Obj).State = System.Data.Entity.EntityState.Added;
            }

            if (DBContext.SaveChanges() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckUpdate_SubQuestion(Vt_JobDrafting_Drafter_Checklist Obj)
        {
            Vt_JobDrafting_Drafter_Checklist Data = DBContext.Vt_JobDrafting_Drafter_Checklist.Where(x => x.DrafterID == Obj.DrafterID && x.JobDraftingID == Obj.JobDraftingID && x.QuestionID == Obj.QuestionID && x.SubQuestionID == Obj.SubQuestionID).FirstOrDefault();
            if (Data != null)
            {
                Data.Status = Obj.Status;
                DBContext.Entry(Data).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                DBContext.Entry(Obj).State = System.Data.Entity.EntityState.Added;
            }

            if (DBContext.SaveChanges() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion Drafter

        #region Checker

        public string Sp_Count_JobDrafting_Checker_Questions()
        {
            var Data = (dynamic)null;
            DataTable dt = Entity_Common.get_SP_DataTable(DBContext, "Sp_Count_JobDrafting_Checker_Questions");
            if (dt.Rows.Count > 0)
            {
                Data = (from DataRow row in dt.Rows

                        select new
                        {
                            Result = row["Result"]
                        }).ToList();
            }
            return Convert.ToString(Data[0].Result);
        }

        public string Sp_Count_JobDrafting_Checker_Checklist(int JobDraftingID)
        {
            var Data = (dynamic)null;
            SqlParameter[] param = {
                                    new SqlParameter("@JobDraftingID", JobDraftingID)
                               };
            DataTable dt = Entity_Common.get_SP_DataTable(DBContext, "Sp_Count_JobDrafting_Checker_Checklist", param);
            if (dt.Rows.Count > 0)
            {
                Data = (from DataRow row in dt.Rows

                        select new
                        {
                            Result = row["Result"]
                        }).ToList();
            }
            return Convert.ToString(Data[0].Result);
        }

        public List<Vt_JobDrafting> GetJobs_Checker(int UserID)
        {
            //IEnumerable<Vt_Jobs> Data = DBContext.Vt_Jobs.Where(x => x. == UserID).OrderByDescending(x => x.ID);
            IEnumerable<Vt_JobDrafting> Data = DBContext.Vt_JobDrafting.Include("Vt_Jobs").Where(x => x.CheckerID == UserID).OrderByDescending(x => x.ID);

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data.ToList();
            }
        }

        public bool CheckUpdate_Checker(Vt_JobDrafting_Checker_Checklist Obj)
        {
            Vt_JobDrafting_Checker_Checklist Data = DBContext.Vt_JobDrafting_Checker_Checklist.Where(x => x.JobDraftingID == Obj.JobDraftingID && x.Drafting_Checker_QuestionID == Obj.Drafting_Checker_QuestionID).FirstOrDefault();
            if (Data != null)
            {
                Data.Status = Obj.Status;
                Data.Notes = Data.Notes + ", " + Obj.Notes;
                DBContext.Entry(Data).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                DBContext.Entry(Obj).State = System.Data.Entity.EntityState.Added;
            }

            if (DBContext.SaveChanges() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

            return true;
        }

        #endregion Checker

       

        public Checker_Response Checker_QuestionAndCheckList(int JobDraftingID)
        {
            Checker_Response Response = new Checker_Response();

            List<Vt_JobDrafting_Checker_Questions> DataQuestion = DBContext.Vt_JobDrafting_Checker_Questions.ToList();
            
            List<Vt_JobDrafting_Checker_Checklist> CheckList = DBContext.Vt_JobDrafting_Checker_Checklist.Where(x => x.JobDraftingID == JobDraftingID).ToList();

            Response.Questions = DataQuestion;
            Response.CheckList = CheckList;
            return Response;
        }
        

        public Checker_Response Checker_QuestionAndCheckList_FixUpList(int JobDraftingID)
        {
            Checker_Response Response = new Checker_Response();
            List<Vt_JobDrafting_Checker_Questions> DataQuestion = new List<Vt_JobDrafting_Checker_Questions>();



            List<Vt_JobDrafting_Checker_Checklist> CheckList = DBContext.Vt_JobDrafting_Checker_Checklist.Where(x => x.JobDraftingID == JobDraftingID && x.Status == "FixUp").ToList();
            foreach (var item in CheckList)
            {
                Vt_JobDrafting_Checker_Questions Data = new Vt_JobDrafting_Checker_Questions();
                Data = DBContext.Vt_JobDrafting_Checker_Questions.Where(x=>x.CheckerQuestionID==item.Drafting_Checker_QuestionID).FirstOrDefault();
                DataQuestion.Add(Data);
            }

            Response.Questions = DataQuestion;
            Response.CheckList = CheckList;
            return Response;
        }
        public Checker_Response Checker_QuestionAndCheckList_FixUpConfirmList(int JobDraftingID)
        {
            Checker_Response Response = new Checker_Response();

            List<Vt_JobDrafting_Checker_Questions> DataQuestion = new List<Vt_JobDrafting_Checker_Questions>();



            List<Vt_JobDrafting_Checker_Checklist> CheckList = DBContext.Vt_JobDrafting_Checker_Checklist.Where(x => x.JobDraftingID == JobDraftingID && x.Status == "FixUpConfirmed").ToList();
            foreach (var item in CheckList)
            {
                Vt_JobDrafting_Checker_Questions Data = new Vt_JobDrafting_Checker_Questions();
                Data = DBContext.Vt_JobDrafting_Checker_Questions.Where(x => x.CheckerQuestionID == item.Drafting_Checker_QuestionID).FirstOrDefault();
                DataQuestion.Add(Data);
            }

            Response.Questions = DataQuestion;
            Response.CheckList = CheckList;
            return Response;
        }
    }
}