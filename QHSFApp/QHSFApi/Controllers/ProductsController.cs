using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static DAL.Model.ViewModels;
using BAL.Repository;
using DAL.DbEntities;
using System.Web.Http;

namespace QHSFApi.Controllers
{
    public class ProductsController : ApiController
    {
        private ProductsRepository ProductRepo;
        // GET: Products
        public ProductsController()
        {
            ProductRepo = new ProductsRepository(new vt_QSFHEntities());
        }

        [HttpGet]
        [Route("Api/Products/GetProductDetails")]
        public GetProductsResponse GetProductDetails()
        {
            List<Vt_Products> Data = ProductRepo.GetProductDetails();

            Data.ToList().ForEach(c => c.Unit = ProductRepo.GetUnit(Convert.ToInt32(c.Unit.ToString())));

            GetProductsResponse Response = new GetProductsResponse();
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
        [Route("Api/Products/GetProductDetail")]
        public GetProductResponse GetProductDetail(int ID)
        {
            Vt_Products Data = ProductRepo.GetProductDetail(ID);

            GetProductResponse Response = new GetProductResponse();
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
        [Route("Api/Products/Create")]
        public bool Create(Vt_Products Obj)
        {
            bool Result = ProductRepo.Create(Obj);
            return Result;
        }

        //Edit
        [HttpPost]
        [Route("Api/Products/Edit")]
        public bool Edit(GetProductResponse Obj)
        {
            bool Result = ProductRepo.Update(Obj);
            return Result;
        }

        [HttpGet]
        [Route("Api/Products/GetStages")]
        public List<Vt_ProductsStages> GetStages()
        {
            List<Vt_ProductsStages> Data = ProductRepo.GetStages();
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
        [Route("Api/Products/GetProductByStages")]
        public GetEstimationResponse GetProductByStages(int ID)
        {
            GetEstimationResponse Response = new GetEstimationResponse();
            List<Vt_Products> Data = ProductRepo.GetProductByStages(ID);
            if (Data != null)
            {

                Response.Products = Data;
            }
            else
            {
                Response.Products = null;
            }

            return Response;
        }

        [HttpGet]
        [Route("Api/Products/GetProductByStagesAndCustomerID")]
        public GetEstimationData GetProductByStagesAndCustomerID(int JobId,int StageID, int CustomerID)
        {
            int ProductStage = 0;
            if (StageID == 1)
            {
                ProductStage = 1;
            }
            else
            {
                ProductStage = 2;
            }
            //stage1-> 8
            List<Vt_JobEstimationDetails> EstimationDetails = ProductRepo.GetEstimationDetails(JobId, StageID, CustomerID);  //4
            
            GetEstimationData Data = ProductRepo.GetProductByStagesAndCustomerID(EstimationDetails, ProductStage, CustomerID);
            
            

            return Data;
        }

       


    }
}