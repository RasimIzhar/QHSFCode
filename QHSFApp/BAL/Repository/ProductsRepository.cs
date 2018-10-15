using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using static DAL.Model.ViewModels;

namespace BAL.Repository
{
    public class ProductsRepository : BaseRepository
    {
        public ProductsRepository() : base()
        {
        }

        public ProductsRepository(vt_QSFHEntities ContextDB) : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public List<Vt_Products> GetProductDetails()
        {
            IEnumerable<Vt_Products> Data = DBContext.Vt_Products.OrderByDescending(x => x.ID);

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data.ToList();
            }
        }

        public string GetUnit(int id)
        {
            var v = DBContext.vt_ProductUnit.Where(x => x.ID == id).FirstOrDefault();
            if (v != null)
                return v.UnitName;
            else
                return null;
        }

        public List<Vt_Products> GetProductByStages(int StageID)
        {
            IEnumerable<Vt_Products> Data = DBContext.Vt_Products.Where(x => x.StageID == StageID).OrderByDescending(x => x.ID);
            //IEnumerable<Vt_Products> Data = DBContext.Vt_Products.OrderByDescending(x => x.ID);

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data.ToList();
            }
        }

        public Vt_Products GetProductDetail(int ID)
        {
            Vt_Products Data = DBContext.Vt_Products.Where(x => x.ID == ID).FirstOrDefault();

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data;
            }
        }

        public bool Create(Vt_Products obj)
        {
            obj.CreatedDate = DateTime.Now;
            obj.ModifiiedDate = DateTime.Now;

            DBContext.Entry(obj).State = System.Data.Entity.EntityState.Added;
            if (DBContext.SaveChanges() != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(GetProductResponse Obj)
        {
            Obj.ModelObject.ModifiiedDate = DateTime.Now;
            DBContext.Entry(Obj.ModelObject).State = System.Data.Entity.EntityState.Modified;
            if (DBContext.SaveChanges() != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Vt_ProductsStages> GetStages()
        {
            IEnumerable<Vt_ProductsStages> Data = DBContext.Vt_ProductsStages.ToList();

            if (Data == null)
            {
                return null;
            }
            else
            {
                return Data.ToList();
            }
        }

        public GetEstimationData GetProductByStagesAndCustomerID(List<Vt_JobEstimationDetails> EstimationDetails, int StageID, int CustomerID)
        {
            //IEnumerable<Vt_PriceList> Data = DBContext.Vt_PriceList.Include("Vt_Products").Where(x => x.CustomerID == CustomerID).OrderByDescending(x => x.ID);

            // List<Vt_PriceList> PriceListRepository = new List<Vt_PriceList>();

            GetEstimationData data = new GetEstimationData();

            List<GetEstimation> productLIst = new List<GetEstimation>();

            List<Vt_Products> Products = DBContext.Vt_Products.Where(x => x.StageID == StageID).ToList(); //12

            if (Products.Count > 0)
            {
                foreach (var item in Products)
                {
                    GetEstimation products = new GetEstimation();
                    Vt_JobEstimationDetails jobestdetails = new Vt_JobEstimationDetails();
                    if (EstimationDetails != null)
                        jobestdetails = EstimationDetails.FirstOrDefault(x => x.ProductID == item.ID && x.Vt_JobEstimation.CustomerID == CustomerID); //
                    else
                        jobestdetails = null;

                    if (jobestdetails == null)
                    {
                        Vt_PriceList PriceExist = DBContext.Vt_PriceList.Include("Vt_Products").Where(x => x.CustomerID == CustomerID && x.ProductID == item.ID).FirstOrDefault();

                        if (PriceExist != null)
                        {
                            if (PriceExist.SellPrice == null)
                                PriceExist.SellPrice = 0;

                            products.ProductID = Convert.ToInt32(PriceExist.ProductID).ToString();
                            products.Qty = "0";
                            products.Price = "0";
                            products.UnitPrice = PriceExist.SellPrice.ToString();
                            products.Title = PriceExist.Vt_Products.Title;
                            products.Unit = GetUnit(Convert.ToInt32(PriceExist.Vt_Products.Unit.ToString()));
                        }
                        else
                        {
                            if (item.UnitPrice == null)
                                item.UnitPrice = 0;

                            products.ProductID = Convert.ToInt32(item.ID).ToString();
                            products.Qty = "0";
                            products.Price = "0";
                            products.UnitPrice = item.UnitPrice.ToString();
                            products.Title = item.Title.ToString();
                            products.Unit = GetUnit(Convert.ToInt32(item.Unit.ToString()));
                        }
                    }
                    else
                    {
                        Vt_Products p = DBContext.Vt_Products.Where(x => x.ID == jobestdetails.ProductID).FirstOrDefault();
                        products.ProductID = jobestdetails.ProductID.ToString();
                        products.Qty = jobestdetails.Quantity.ToString();
                        products.Price = jobestdetails.TotalPrice.ToString();
                        products.UnitPrice = jobestdetails.UnitPrice.ToString();
                        products.Title = p.Title.ToString();
                        products.Unit = GetUnit(Convert.ToInt32(p.Unit.ToString()));
                        products.Markup = Convert.ToInt32(jobestdetails.MarkupValue.ToString());
                    }
                    productLIst.Add(products);
                }
            }

            //IEnumerable<Vt_Products> Data = DBContext.Vt_Products.OrderByDescending(x => x.ID);

            data.Estimation = productLIst;

            return data;
        }

        public List<Vt_JobEstimationDetails> GetEstimationDetails(int JobId, int StageID, int CustomerID)
        {
            Vt_JobEstimation jobEst = DBContext.Vt_JobEstimation.Where(x => x.JobID == JobId).FirstOrDefault();
            if (jobEst != null)
                return DBContext.Vt_JobEstimationDetails.Where(x => x.JobEstimationID == jobEst.ID && x.JobID == jobEst.JobID && x.StageID == StageID).ToList();
            else
                return null;
        }
    }
}