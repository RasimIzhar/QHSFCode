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
    public class CustomersController : Controller
    {
        // GET: Customers
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
            //string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Customers/GetCustomers");
            //GetCustomersResponse apiResponse = JsonConvert.DeserializeObject<GetCustomersResponse>(strReponse);
            //return View(apiResponse);
            return View();
        }

        public JsonResult GetCustomers_List()
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Customers/GetCustomers");
            GetCustomersResponse apiResponse = JsonConvert.DeserializeObject<GetCustomersResponse>(strReponse);
            List<Vt_Customers> List = apiResponse.ModelObject;

            return Json(new { List }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Details(int ID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Customers/Details?ID=" + ID);
            GetCustomerResponse apiResponse = JsonConvert.DeserializeObject<GetCustomerResponse>(strReponse);
            return View(apiResponse);
        }

        //Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Vt_Customers Data)
        {
            APIResponse Response = new APIResponse();
            bool Success = false;
            string Message = "";
            try
            {
                string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Customers/Create", Data);
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

            return RedirectToAction("Index", "Customers");
        }

        //Update
        public ActionResult Edit(int? ID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Customers/Details?ID=" + ID);
            GetCustomerResponse apiResponse = JsonConvert.DeserializeObject<GetCustomerResponse>(strReponse);
            return View(apiResponse);
        }

        [HttpPost]
        public ActionResult Edit(GetCustomerResponse Obj)
        {
            APIResponse Response = new APIResponse();
            bool Success = false;
            string Message = "";

            try
            {
                string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Customers/Edit", Obj);
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

            return RedirectToAction("Index", "Customers");
        }

        //Delete
        public ActionResult Delete(int? ID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/Customers/Delete?ID=" + ID);
            GetCustomerResponse apiResponse = JsonConvert.DeserializeObject<GetCustomerResponse>(strReponse);
            return RedirectToAction("Index", "Customers");
        }
    }
}