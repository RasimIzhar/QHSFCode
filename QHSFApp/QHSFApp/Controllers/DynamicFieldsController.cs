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
    [Authorize(Roles = "SuperAdmin")]
    public class DynamicFieldsController : Controller
    {
        // GET: DynamicFields
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

            //string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/DynamicFields/GetDynamicFields");
            //GetDynamicFieldsResponse apiResponse = JsonConvert.DeserializeObject<GetDynamicFieldsResponse>(strReponse);
            //return View(apiResponse);
            return View();
        }

        public JsonResult GetDynamicFieldsList()
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/DynamicFields/GetDynamicFields");
            GetDynamicFieldsResponse apiResponse = JsonConvert.DeserializeObject<GetDynamicFieldsResponse>(strReponse);

            List<Vt_DynamicFields> List = apiResponse.ModelObject;
            return Json(new { List }, JsonRequestBehavior.AllowGet);
        }

        //Get Single Record
        public ActionResult Details(int ID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/DynamicFields/GetDynamicField?ID=" + ID);
            GetDynamicFieldResponse apiResponse = JsonConvert.DeserializeObject<GetDynamicFieldResponse>(strReponse);
            return View(apiResponse);
        }

        //Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Vt_DynamicFields DynamicField)
        {
            APIResponse Response = new APIResponse();
            bool Success = false;
            string Message = "";
            try
            {
                string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/DynamicFields/Create", DynamicField);
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

            return RedirectToAction("Index", "DynamicFields");
        }

        //Update
        public ActionResult Edit(int? ID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/DynamicFields/GetDynamicField?ID=" + ID);
            GetDynamicFieldResponse apiResponse = JsonConvert.DeserializeObject<GetDynamicFieldResponse>(strReponse);
            return View(apiResponse);
        }

        [HttpPost]
        public ActionResult Edit(GetDynamicFieldResponse Obj)
        {
            APIResponse Response = new APIResponse();
            bool Success = false;
            string Message = "";

            try
            {
                string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/DynamicFields/Edit", Obj);
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

            return View(Obj);
        }

        public ActionResult FieldsValues(int ID)
        {
            string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/DynamicFieldsValues/GetDynamicFieldsValues?ID=" + ID);
            GetDynamicFieldsValuesResponse apiResponse = JsonConvert.DeserializeObject<GetDynamicFieldsValuesResponse>(strReponses);

            ViewBag.ViewID = ID;
            return View(apiResponse);
        }

        [HttpPost]
        public ActionResult FieldsValues(List<Vt_DynamicFieldsValues> Obj)
        {
            string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/DynamicFieldsValues/Create", Obj);
            GetDynamicFieldsValuesResponse apiResponse = JsonConvert.DeserializeObject<GetDynamicFieldsValuesResponse>(strReponses);

            return View(apiResponse);
        }

        public ActionResult FieldPreview(int? ID)
        {
            string strReponses = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/DynamicFields/GetDynamicFieldsWithValues?ID=" + ID);
            GetDynamicFields_WithValues_Response apiResponse = JsonConvert.DeserializeObject<GetDynamicFields_WithValues_Response>(strReponses);

            return View(apiResponse);
        }

        //Delete
        public ActionResult Delete(int? ID)
        {
            string strReponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"].ToString() + "/Api/DynamicFields/Delete?ID=" + ID);
            GetDynamicFieldResponse apiResponse = JsonConvert.DeserializeObject<GetDynamicFieldResponse>(strReponse);
            return RedirectToAction("Index", "DynamicFields");
        }
    }
}