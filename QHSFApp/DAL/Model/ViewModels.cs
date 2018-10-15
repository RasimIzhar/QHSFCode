using DAL.DbEntities;
using System;
using System.Collections.Generic;

namespace DAL.Model
{
    public class ViewModels
    {
        public class APIResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        #region Responses

        #region RequirementNew

        public class Create_JobResponse
        {
            public Vt_Jobs Job { get; set; }
            public Vt_JobDetails JobDetails { get; set; }
        }

        public class Get_JobResponse
        {
            public ApiResponseHeader Header { get; set; }
            public List<Vt_JobDetails> ModelObject { get; set; }
        }

        public class Create_JobDetailsResponse
        {
            public Vt_JobDetails JobDetails { get; set; }
            public JobDetailsStatic JobDetailsStatic { get; set; }
            public List<JobDetailsDynamicField> JobDetailsDynamic { get; set; }
        }

        public class Update_JobStatus
        {
            public int JobID { get; set; }
            public int CustomerID { get; set; }
            public int TabID { get; set; }
            public int UserAssigned { get; set; }
            public bool IsApproved { get; set; }
        }

        public class Update_JobDetails
        {
            public Vt_JobDetails JobDetails { get; set; }
        }

        #endregion RequirementNew

        #region RequirementContributorOld

        public class GetJobDetailsResponse
        {
            public Vt_Jobs Job { get; set; }
            public List<Vt_DynamicFields> DynamicFieldsWithValues { get; set; }
        }

        public class GetJobDetailsUpdatedResponse
        {
            public int? CustomerID { get; set; }
            public Vt_Jobs Job { get; set; }
            public Vt_JobDetails JobDetails { get; set; }
            public List<Vt_DynamicFields> DynamicFieldsWithValues { get; set; }
            public JobDetailsStatic JobDetailsStatic { get; set; }

            public List<JobDetailsDynamicField> JobDetailsDynamicField { get; set; }
        }

        public class CreateJobDetailsResponse
        {
            public Vt_JobDetails JobDetails { get; set; }
            public JobDetailsStatic JobDetailsStatic { get; set; }
            public List<JobDetailsDynamicField> JobDetailsDynamic { get; set; }
        }

        public class GetRequirementContributorResponse
        {
            public List<Vt_DynamicFields> DynamicFields { get; set; }
            public Vt_Customers Customer { get; set; }
        }

        #endregion RequirementContributorOld

        #region JobsOld

        public class GetJobsResponse
        {
            public ApiResponseHeader Header { get; set; }
            public List<Vt_Jobs> ModelObject { get; set; }
        }

        public class GetJobResponse
        {
            public ApiResponseHeader Header { get; set; }
            public Vt_Jobs ModelObject { get; set; }

            public Vt_JobDetails JobDetails { get; set; }
        }

        #endregion JobsOld

        #region DynamicFields

        public class GetDynamicFieldsResponse
        {
            public ApiResponseHeader Header { get; set; }
            public List<Vt_DynamicFields> ModelObject { get; set; }
        }

        public class GetDynamicFieldResponse
        {
            public ApiResponseHeader Header { get; set; }
            public Vt_DynamicFields ModelObject { get; set; }
        }

        public class GetDynamicFieldsValueResponse_Single
        {
            public ApiResponseHeader Header { get; set; }
            public Vt_DynamicFieldsValues ModelObject { get; set; }
        }

        public class GetDynamicFieldsValuesResponse
        {
            public ApiResponseHeader Header { get; set; }
            public List<Vt_DynamicFieldsValues> ModelObject { get; set; }
        }

        public class GetDynamicFields_WithValues_Response
        {
            public Vt_DynamicFields Field { get; set; }
            public List<Vt_DynamicFieldsValues> Values { get; set; }
        }

        public class GetDynamicFields_WithValues_All_Response
        {
            public List<Vt_DynamicFields> Field { get; set; }
            public List<Vt_DynamicFieldsValues> Values { get; set; }
        }

        #endregion DynamicFields

        #region Customer

        public class GetCustomersResponse
        {
            public ApiResponseHeader Header { get; set; }
            public List<Vt_Customers> ModelObject { get; set; }
        }

        public class GetCustomerResponse
        {
            public ApiResponseHeader Header { get; set; }
            public Vt_Customers ModelObject { get; set; }
        }

        #endregion Customer

        #region Products

        public class GetProductsResponse
        {
            public ApiResponseHeader Header { get; set; }
            public List<Vt_Products> ModelObject { get; set; }
        }

        public class GetProductResponse
        {
            public ApiResponseHeader Header { get; set; }
            public Vt_Products ModelObject { get; set; }
        }

        #endregion Products

        #region PriceList

        public class CreatePriceListResponse
        {
            public List<Vt_PriceList> PriceList { get; set; }
        }

        public class GetCustomerProductPriceRespone
        {
            public ApiResponseHeader Header { get; set; }
            public Vt_Customers Customer { get; set; }
            public List<Vt_Products> Products { get; set; }
            public List<Vt_PriceList> PriceList { get; set; }
        }

        #endregion PriceList

        #region Estimation

        public class GetEstimationResponse
        {
            public List<Vt_Products> Products { get; set; }
            public List<Vt_JobEstimationDetails> JobEstimationDetails { get; set; }
        }

        public class GetEstimationData
        {
            public Vt_JobEstimation JobEstimation { get; set; }
            public List<GetEstimation> Estimation { get; set; }
            public int StageID { get; set; }

            public int MarkupValue { get; set; }

            public string Notes { get; set; }
        }

        public class GetEstimation
        {
            public string ProductID { get; set; }
            public string Title { get; set; }
            public string Unit { get; set; }
            public string Qty { get; set; }
            public string UnitPrice { get; set; }
            public string Price { get; set; }
            public int Markup { get; set; }
        }

        public class CreateEstimation
        {
            public Vt_JobEstimation JobEstimation { get; set; }
            public List<Vt_JobEstimationDetails> JobEstimationDetails { get; set; }
        }

        #endregion Estimation

        #region Drafting


        public class Update_JobDetails_Drafting
        {
            public int JobID { get; set; }
            public int CustomerID { get; set; }
            public int TabID { get; set; }
            public int CheckerID { get; set; }
            public int DrafterID { get; set; }
            public DateTime CheckerDueDate { get; set; }
            public DateTime DraftingDueDate { get; set; }
            public bool IsApproved { get; set; }
        }

        public class Drafter_Response
        {
            public List<Vt_JobDrafting_Drafter_Questions> Questions { get; set; }
            public List<Vt_JobDrafting_Drafter_SubQuestions> SubQuestions { get; set; }

            public List<Vt_JobDrafting_Drafter_Checklist> CheckList { get; set; }
        }

        public class Checker_Response
        {
            public List<Vt_JobDrafting_Checker_Questions> Questions { get; set; }
            public List<Vt_JobDrafting_Checker_Checklist> CheckList { get; set; }
        }


        #endregion Drafting

        #region Manufacturing

        public class ManufacturingOrderDetails
        {
            public int ID { get; set; }
            public string JobTitle { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public string Class { get; set; }
            public int Priority { get; set; }
        }

        #endregion

        #region JobOverview

        public class Get_JobOverview_Response
        {
            public JobOverview ModelObject { get; set; }
        }

        public class JobOverview
        {
            public int ID { get; set; }
            public string CustomerName { get; set; }
            public string CustomerContactDetails { get; set; }
            public Nullable<DateTime> DateRecieved { get; set; }
            public string QuoteNumber { get; set; }
            public string RevisionNumber { get; set; }
            public string Stage { get; set; }
            public bool Installation { get; set; }
            public decimal EstimateAmount { get; set; }
            public Nullable<DateTime> DraftingCompleteDate { get; set; }
            public string Payment { get; set; }


        }

        public class Get_JobsOverview_Response
        {
            public List<JobsOverview> ModelObject { get; set; }
        }

        public class JobsOverview
        {
            public int ID { get; set; }
            public string JobNumber { get; set; }
            public string JobTitle { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Phase { get; set; }

        }

        #endregion

        public class JobModel
        {
            public Vt_JobDetails JobDetails { get; set; }
            public List<Vt_JobDetails> ListofJobDetails { get; set; }

            public Vt_JobDrafting JobDrafting { get; set; }
            public List<Vt_JobDrafting> ListofJobDrafting { get; set; }
        }
        public class GetTabsResponse
        {
            public string UserName { get; set; }
            public string UserEmail { get; set; }
            public string Password { get; set; }
            public bool isavtive { get; set; }
            public ApiResponseHeader Header { get; set; }
            public List<vt_Tabs> ModelObject { get; set; }
            public bool Status { get; set; }
        }

        public class GetResponse
        {
            public ApiResponseHeader Header { get; set; }
            public List<Vt_Users> ModelObject { get; set; }
            public bool Status { get; set; }
        }

        public class GetAccessControlResponse
        {
            public ApiResponseHeader Header { get; set; }
            public Vt_Users ModelObject { get; set; }
            public bool Status { get; set; }
        }

        public class GetEditRecord
        {
            public ApiResponseHeader Header { get; set; }
            public AddTabsDataNew ModelObject { get; set; }
            public bool Status { get; set; }
        }

        public class UserCreateResponse
        {
            public ApiResponseHeader Header { get; set; }
            public Vt_Users ModelObject { get; set; }
            public bool Status { get; set; }
        }

        #endregion Responses

        public class UserRolesViewModel
        {
            public List<vt_Tabs> TabName { get; set; }
            public List<vt_Roles> Roles { get; set; }

            public List<UserRole> UserRoles { get; set; }
            public bool SuperAdmin { get; set; }
            public bool Admin { get; set; }
            public bool Contributor { get; set; }
            public bool ContributorChecker { get; set; }
            public bool ContributorDrafter { get; set; }
        }

        public class UsersTabs
        {
            public List<vt_Tabs> TabName { get; set; }

            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public bool IsActive { get; set; }
        }

        //Need to be changed
        public class Tabs
        {
            public string TabName { get; set; }
            public string tabid { get; set; }
            public bool Admin { get; set; }
            public bool superadmin { get; set; }
            public bool Coordinator { get; set; }
        }

        public class TabsNew
        {
            public string TabName { get; set; }
            public string tabid { get; set; }
            public bool Admin { get; set; }
            public bool Contributor { get; set; }
            public bool ContributorChecker { get; set; }
            public bool ContributorDrafter { get; set; }
        }

        public class user
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
            public string UserEmail { get; set; }
            public string Password { get; set; }
            public Nullable<DateTime> CreatedDate { get; set; }
            public Nullable<DateTime> ModifiedDate { get; set; }
            public Nullable<bool> IsDeleted { get; set; }
            public Nullable<bool> IsActive { get; set; }
        }        
        //Needs to be changed
        public class AddTabsData : ApiResponseHeader
        {
            public user TabUser { get; set; }
            public List<Tabs> Tabs { get; set; }
        }

        public class AddTabsDataNew : ApiResponseHeader
        {
            public user TabUser { get; set; }
            public List<TabsNew> Tabs { get; set; }
        }

        public class ApiResponseHeader
        {
            public bool Status { get; set; }
            public string Message { get; set; }
        }
    }
}