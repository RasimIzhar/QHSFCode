using BAL.Repository;
using DAL.DbEntities;
using System.Collections.Generic;
using System.Web.Http;
using static DAL.Model.ViewModels;

namespace QHSFApi.Controllers
{
    public class PriceListController : ApiController
    {
        private PriceListRepository PriceListRepo;
        private ProductsRepository ProductRepo;
        private CustomersRepository CustomerRepo;

        private PriceListController()
        {
            PriceListRepo = new PriceListRepository(new vt_QSFHEntities());
            ProductRepo = new ProductsRepository(new vt_QSFHEntities());
            CustomerRepo = new CustomersRepository(new vt_QSFHEntities());
        }

        [HttpPost]
        [Route("Api/PriceList/Create")]
        public bool Create(CreatePriceListResponse Obj)
        {
            bool result = PriceListRepo.Create(Obj.PriceList);
            return result; 
        }

        [HttpGet]
        [Route("Api/PriceList/GetProductsPrices")]
        public GetCustomerProductPriceRespone GetProductsPrices(int CustomerID)
        {
            GetCustomerProductPriceRespone Response = new GetCustomerProductPriceRespone();
            ApiResponseHeader Header = new ApiResponseHeader();

            Vt_Customers Customer = CustomerRepo.Details(CustomerID);
            List<Vt_Products> Products = ProductRepo.GetProductDetails();
            List<Vt_PriceList> PriceList = PriceListRepo.GetPriceLists(CustomerID);

            Header.Status = true;
            Header.Message = "Success";
            Response.Header = Header;

            Response.Customer = Customer;
            Response.PriceList = PriceList;
            Response.Products = Products;

            return Response;
        }
    }
}