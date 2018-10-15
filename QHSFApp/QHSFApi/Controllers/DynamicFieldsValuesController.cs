using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAL.Repository;
using DAL.DbEntities;
using static DAL.Model.ViewModels;


namespace QHSFApi.Controllers
{
    public class DynamicFieldsValuesController : ApiController
    {
        private DynamicFieldsValuesRepository DynamicFieldsValuesRepo;

        public DynamicFieldsValuesController()
        {
            DynamicFieldsValuesRepo = new DynamicFieldsValuesRepository(new vt_QSFHEntities());
        }

        [HttpGet]
        [Route("Api/DynamicFieldsValues/GetDynamicFieldsValues")]
        public GetDynamicFieldsValuesResponse GetDynamicFieldsValues(int ID)
        {
            List<Vt_DynamicFieldsValues> DynamicFieldsValues = DynamicFieldsValuesRepo.GetDynamicFieldsValues(ID);

            GetDynamicFieldsValuesResponse Response = new GetDynamicFieldsValuesResponse();
            ApiResponseHeader Header = new ApiResponseHeader();
            if (DynamicFieldsValues != null)
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
            Response.ModelObject = DynamicFieldsValues;
            return Response;
        }

        [HttpPost]
        [Route("Api/DynamicFieldsValues/Create")]
        public GetDynamicFieldsValuesResponse Create(List<Vt_DynamicFieldsValues> Obj)
        {
            ApiResponseHeader Header = new ApiResponseHeader();
            GetDynamicFieldsValuesResponse Response = new GetDynamicFieldsValuesResponse();


            List<Vt_DynamicFieldsValues> ListObj = new List<Vt_DynamicFieldsValues>();

            ListObj = Obj;

            bool Result = DynamicFieldsValuesRepo.Update(ListObj);
            if (Result)
            {
                Header.Status = true;
                Header.Message = "Success";
                Response.Header = Header;
                //Response.ModelObject = TabsData;
                return Response;
            }
            else
            {
                return null;
            }

        }
    }
}
