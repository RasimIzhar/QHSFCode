using DAL.DbEntities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BAL.Repository
{
    public class OverviewRepository : BaseRepository
    {
        public OverviewRepository() : base()
        {
        }

        public OverviewRepository(vt_QSFHEntities ContextDB) : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public object GetJobsOverView()
        {
            var Data = (dynamic)null;

            DataTable dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetJobsOverView");
            if (dt.Rows.Count > 0)
            {
                Data = (from DataRow row in dt.Rows

                        select new
                        {
                            ID = row["ID"],
                            JobNumber = row["Job Number"],
                            JobTitle = row["Job Title"],
                            StartDate = row["Start Date"],
                            EndDate = row["End Date"],
                            Phase = row["Phase"]
                        }).ToList();
            }
            return Data;
        }

        public object GetOverViewJobDetailByJobID(int JobID)
        {
            var Data = (dynamic)null;
            SqlParameter[] param = {
                                    new SqlParameter("@JobID",JobID)
                               };
            DataTable dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetOverViewJobDetailByJobID", param);
            if (dt.Rows.Count > 0)
            {
                Data = (from DataRow row in dt.Rows

                        select new
                        {
                            ID = row["ID"],
                            CustomerName = row["Customer Name"],
                            CustomerContactDetails = row["Customer Contact Details"],
                            DateRecieved = row["Date Recieved"],
                            QuoteNumber = row["Quote Number"],
                            RevisionNumber = row["Revision Number"],
                            Stage = row["Stage"],
                            Installation = row["Installation"],
                            EstimateAmount = row["Estimate Amount"],
                            DraftingCompleteDate = row["Drafting Complete Date"],
                            Payment = row["Payment"]
                        }).ToList();
            }
            return Data;
        }
    }
}