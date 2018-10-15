using BAL.Repository;
using DAL.DbEntities;
using System.Collections.Generic;
using System.Web.Http;
using static DAL.Model.ViewModels;

namespace QHSFApi.Controllers
{
    public class DynamicFieldsController : ApiController
    {
        private DynamicFieldsRepository DynamicFieldsRepo;

        public DynamicFieldsController()
        {
            DynamicFieldsRepo = new DynamicFieldsRepository(new vt_QSFHEntities());
        }

        [HttpGet]
        [Route("Api/DynamicFields/GetDynamicFieldsWithValues")]
        public GetDynamicFields_WithValues_Response GetDynamicFieldsWithValues(int ID)
        {
            GetDynamicFields_WithValues_Response dynamicfields = DynamicFieldsRepo.GetDynamicFields_WithValues(ID);

            GetDynamicFields_WithValues_Response response = new GetDynamicFields_WithValues_Response();
            ApiResponseHeader header = new ApiResponseHeader();
            if (dynamicfields != null)
            {
                header.Status = true;
                header.Message = "success";
                response.Field = dynamicfields.Field;
                response.Values = dynamicfields.Values;
            }
            else
            {
                header.Status = false;
                header.Message = "no data found";
            }

            return response;

            //vt_dynamicfields data = new vt_dynamicfields();
            //data = dynamicfieldsrepo.getdynamicfields_withvalues(id);
            //return data;
        }

        [HttpGet]
        [Route("Api/DynamicFields/GetDynamicFields")]
        public GetDynamicFieldsResponse GetDynamicFields()
        {
            List<Vt_DynamicFields> DynamicFields = DynamicFieldsRepo.GetDynamicFields();

            GetDynamicFieldsResponse Response = new GetDynamicFieldsResponse();
            ApiResponseHeader Header = new ApiResponseHeader();
            if (DynamicFields != null)
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
            Response.ModelObject = DynamicFields;
            return Response;
        }

        //Get Single Record
        [HttpGet]
        [Route("Api/DynamicFields/GetDynamicField")]
        public GetDynamicFieldResponse GetDynamicField(int ID)
        {
            Vt_DynamicFields DynamicField = DynamicFieldsRepo.GetDynamicField(ID);

            GetDynamicFieldResponse Response = new GetDynamicFieldResponse();
            ApiResponseHeader Header = new ApiResponseHeader();
            if (DynamicField != null)
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
            Response.ModelObject = DynamicField;
            return Response;
        }

        //Create
        [HttpPost]
        [Route("Api/DynamicFields/Create")]
        public bool Create(Vt_DynamicFields Obj)
        {
            bool result = DynamicFieldsRepo.Create(Obj);
            return result;
        }

        //Edit
        [HttpPost]
        [Route("Api/DynamicFields/Edit")]
        public bool Edit(GetDynamicFieldResponse Obj)
        {
            bool result = DynamicFieldsRepo.Update(Obj);
            return result;
        }

        //Delete
        [HttpGet]
        [Route("Api/DynamicFields/Delete")]
        public GetDynamicFieldResponse Delete(int ID)
        {
            GetDynamicFieldResponse Response = new GetDynamicFieldResponse();
            ApiResponseHeader Header = new ApiResponseHeader();

            bool Result = DynamicFieldsRepo.Delete(ID);
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
        [Route("Api/DynamicFields/GetDynamicFieldsWithValuesAll")]
        public List<Vt_DynamicFields> GetDynamicFieldsWithValuesAll()
        {
            GetDynamicFieldsResponse Response = new GetDynamicFieldsResponse();
            ApiResponseHeader Header = new ApiResponseHeader();

            List<Vt_DynamicFields> Data = DynamicFieldsRepo.GetDynamicFields_WithValues_All();
            Header.Message = "Success";
            Header.Status = true;

            Response.Header = Header;
            Response.ModelObject = Data;
            return Data;
        }

        [HttpGet]
        [Route("Api/DynamicFields/GetDynamicFieldsWithValuesAllTest")]
        public List<Vt_DynamicFields> GetDynamicFieldsWithValuesAllTest()
        {
            List<Vt_DynamicFields> Data = DynamicFieldsRepo.GetDynamicFields_WithValues_AllTest();

            return Data;
        }
    }
}