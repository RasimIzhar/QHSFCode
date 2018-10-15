using DAL.DbEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using static DAL.Model.HttpApi;
using static DAL.Model.ViewModels;

namespace QHSFApp.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            if (TempData["OperationType"] != null)
            {
                ViewBag.OperationType = TempData["OperationType"];
                ViewBag.Success = TempData["Success"];
                ViewBag.Message = TempData["Message"];

                TempData.Remove("OperationType");
                TempData.Remove("Success");
                TempData.Remove("Message");
            }

            //string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Products/GetProductDetails");
            //GetProductsResponse apiResponse = JsonConvert.DeserializeObject<GetProductsResponse>(strReponse);

            //return View(apiResponse);
            return View();

        }

        public JsonResult GetPropductsList()
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Products/GetProductDetails");
            GetProductsResponse apiResponse = JsonConvert.DeserializeObject<GetProductsResponse>(strReponse);
            List<Vt_Products> productslist = apiResponse.ModelObject;

            return Json(new { productslist }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductDetail(int ID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Products/GetProductDetail?ID=" + ID);
            GetProductResponse apiResponse = JsonConvert.DeserializeObject<GetProductResponse>(strReponse);
            return View(apiResponse);
        }

        //Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Vt_Products Data)
        {
            APIResponse Response = new APIResponse();
            bool Success = false;
            string Message = "";
            try
            {
                string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Products/Create", Data);
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

            return RedirectToAction("Index", "Products");

            //return Json(new { success = apiResponse.Success, Message = apiResponse.Message }, JsonRequestBehavior.AllowGet);
        }

        //Update
        public ActionResult Edit(int? ID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Products/GetProductDetail?ID=" + ID);
            GetProductResponse apiResponse = JsonConvert.DeserializeObject<GetProductResponse>(strReponse);

            return View(apiResponse);
        }

        [HttpPost]
        public ActionResult Edit(GetProductResponse Obj)
        {
            APIResponse Response = new APIResponse();
            bool Success = false;
            string Message = "";
            try
            {
                string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Products/Edit", Obj);
                bool apiResponse = JsonConvert.DeserializeObject<bool>(strReponses);
                if (apiResponse)
                {
                    Success = true;
                    Message = "Record Update Successfully";
                }
                else
                {
                    Success = false;
                    Message = "Error Occured! Updating Record";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            TempData["OperationType"] = "Update";
            TempData["Success"] = Success;
            TempData["Message"] = Message;

            return RedirectToAction("Index", "Products");
        }
    }
}