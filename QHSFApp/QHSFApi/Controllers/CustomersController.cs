using BAL.Repository;
using DAL.DbEntities;
using System.Collections.Generic;
using System.Web.Http;
using static DAL.Model.ViewModels;

namespace QHSFApi.Controllers
{
    public class CustomersController : ApiController
    {
        private CustomersRepository CustomerRepo;

        public CustomersController()
        {
            CustomerRepo = new CustomersRepository(new vt_QSFHEntities());
        }

        [HttpGet]
        [Route("Api/Customers/GetCustomers")]
        public GetCustomersResponse GetCustomers()
        {
            List<Vt_Customers> Data = CustomerRepo.GetCustomers();
            GetCustomersResponse Response = new GetCustomersResponse();
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

        
        //Get Single Record
        [HttpGet]
        [Route("Api/Customers/Details")]
        public GetCustomerResponse Details(int ID)
        {
            Vt_Customers Data = CustomerRepo.Details(ID);

            GetCustomerResponse Response = new GetCustomerResponse();
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
            return Response;
        }

        [HttpPost]
        [Route("Api/Customers/Create")]
        public bool Create(Vt_Customers Obj)
        {
            bool result = CustomerRepo.Create(Obj);
            return result;
        }

        //Edit
        [HttpPost]
        [Route("Api/Customers/Edit")]
        public bool Edit(GetCustomerResponse Obj)
        {
            bool result = CustomerRepo.Update(Obj);
            return result; 
        }

        //Delete
        [HttpGet]
        [Route("Api/Customers/Delete")]
        public GetCustomerResponse Delete(int ID)
        {
            GetCustomerResponse Response = new GetCustomerResponse();
            ApiResponseHeader Header = new ApiResponseHeader();

            bool Result = CustomerRepo.Delete(ID);
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
    }
}