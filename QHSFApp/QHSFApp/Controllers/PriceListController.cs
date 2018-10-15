using DAL.DbEntities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System;
using static DAL.Model.HttpApi;
using static DAL.Model.ViewModels;

namespace QHSFApp.Controllers
{
    public class PriceListController : Controller
    {
        // GET: PriceList
        public ActionResult Index()
        {
            //string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Customers/GetCustomers");
            //GetCustomersResponse apiResponse = JsonConvert.DeserializeObject<GetCustomersResponse>(strReponse);
            //return View(apiResponse);

            return View();
        }

        public JsonResult GetCustomersList()
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Customers/GetCustomers");
            GetCustomersResponse apiResponse = JsonConvert.DeserializeObject<GetCustomersResponse>(strReponse);
            List<Vt_Customers> List = apiResponse.ModelObject;

            return Json(new { List }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetPriceList(int? ID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/PriceList/GetProductsPrices?CustomerID=" + ID);
            GetCustomerProductPriceRespone apiResponse = JsonConvert.DeserializeObject<GetCustomerProductPriceRespone>(strReponse);
            return View(apiResponse);
        }

        
        [HttpPost]
        public ActionResult Create(CreatePriceListResponse Obj)
        {
            APIResponse Response = new APIResponse();
            bool Success = false;
            string Message = "";

            try
            {
                string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/PriceList/Create", Obj);
                bool apiResponse = JsonConvert.DeserializeObject<bool>(strReponses);

                if (apiResponse)
                {
                    Success = true;
                    Message = "Record Inserted Successfully";
                }
                else
                {
                    Success = false;
                    Message = "Error Occured ! While Inserting Record";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            TempData["OperationType"] = "Insert";
            TempData["Success"] = Success;
            TempData["Message"] = Message;

            return RedirectToAction("Index", "PriceList");
        }
    }
}