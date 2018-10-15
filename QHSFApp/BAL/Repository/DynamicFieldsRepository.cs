using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using static DAL.Model.ViewModels;

namespace BAL.Repository
{
    public class DynamicFieldsRepository : BaseRepository
    {
        public DynamicFieldsRepository() : base()
        {
        }

        public DynamicFieldsRepository(vt_QSFHEntities ContextDB) : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public List<Vt_DynamicFields> GetDynamicFields()
        {
            IEnumerable<Vt_DynamicFields> DynamicFields = DBContext.Vt_DynamicFields.OrderByDescending(x => x.ID);

            if (DynamicFields == null)
            {
                return null;
            }
            else
            {
                return DynamicFields.ToList();
            }
        }

        //Get Single Record
        public Vt_DynamicFields GetDynamicField(int ID)
        {
            Vt_DynamicFields DynamicFields = DBContext.Vt_DynamicFields.Where(x => x.ID == ID).FirstOrDefault();

            if (DynamicFields == null)
            {
                return null;
            }
            else
            {
                return DynamicFields;
            }
        }

        //Create Record
        public bool Create(Vt_DynamicFields Obj)
        {
            Obj.CreatedDate = DateTime.Now;
            Obj.ModifiedDate = DateTime.Now;

            DBContext.Entry(Obj).State = System.Data.Entity.EntityState.Added;
            if (DBContext.SaveChanges() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Update Record
        public bool Update(GetDynamicFieldResponse Obj)
        {
            Obj.ModelObject.ModifiedDate = DateTime.Now;

            DBContext.Entry(Obj.ModelObject).State = System.Data.Entity.EntityState.Modified;
            if (DBContext.SaveChanges() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Get Dynamic Fields With Values
        public GetDynamicFields_WithValues_Response GetDynamicFields_WithValues(int ID)
        {
            GetDynamicFields_WithValues_Response Obj = new GetDynamicFields_WithValues_Response();
            List<Vt_DynamicFieldsValues> Values = new List<Vt_DynamicFieldsValues>();

            var Field = DBContext.Vt_DynamicFields.Where(x => x.ID == ID).SingleOrDefault();
            if (Field != null)
            {
                Obj.Field = Field;
                Values = DBContext.Vt_DynamicFieldsValues.Where(x => x.DynamicFieldsID == Field.ID).ToList();
                if (Values != null)
                {
                    Obj.Values = Values;
                }
                else
                {
                    Obj.Values = null;
                }
            }
            else
            {
                Obj.Field = null;
            }

            return Obj;
        }

        //public Vt_DynamicFields GetDynamicFields_WithValues(int ID)
        //{
        //    Vt_DynamicFields Data = DBContext.Vt_DynamicFields.Include("Vt_DynamicFieldsValues").Where(x => x.ID == ID).SingleOrDefault();
        //    if (Data != null)
        //    {
        //        return Data;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public GetDynamicFieldsValuesResponse GetDynamicFields_WithValues(int ID)
        //{
        //    GetDynamicFieldsValuesResponse Response = new GetDynamicFieldsValuesResponse();
        //    ApiResponseHeader Header = new ApiResponseHeader();
        //    Vt_DynamicFields Data = DBContext.Vt_DynamicFields.Include("Vt_DynamicFieldsValues").Where(x => x.ID == ID).SingleOrDefault();
        //    if (Data != null)
        //    {
        //        Response.Header.Message = "Success";
        //        Response.Header.Status = true;
        //        Response.ModelObject = Data;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //Delete Record
        public bool Delete(int? ID)
        {
            Vt_DynamicFields Data = DBContext.Vt_DynamicFields.Where(x => x.ID == ID).FirstOrDefault();
            List<Vt_DynamicFieldsValues> FieldValues = DBContext.Vt_DynamicFieldsValues.Where(x => x.DynamicFieldsID == Data.ID).ToList();

            DBContext.Entry(Data).State = System.Data.Entity.EntityState.Deleted;
            DBContext.Vt_DynamicFieldsValues.RemoveRange(FieldValues);

            if (DBContext.SaveChanges() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Vt_DynamicFields> GetDynamicFields_WithValues_All()
        {
            List<Vt_DynamicFields> Field = DBContext.Vt_DynamicFields.ToList();
            if (Field != null)
            {
                int FieldID;
                foreach (var item in Field)
                {
                    FieldID = item.ID;
                    List<Vt_DynamicFieldsValues> Values = DBContext.Vt_DynamicFieldsValues.Where(x => x.DynamicFieldsID == FieldID).ToList();
                }
            }

            return Field;
        }

        public List<Vt_DynamicFields> GetDynamicFields_WithValues_AllTest()
        {
            List<Vt_DynamicFields> Field = DBContext.Vt_DynamicFields.Include("Vt_DynamicFieldsValues").ToList();
            return Field;
        }
    }
}