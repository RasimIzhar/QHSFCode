using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Repository
{
    public class DynamicFieldsValuesRepository : BaseRepository
    {
        public DynamicFieldsValuesRepository() : base()
        {
        }

        public DynamicFieldsValuesRepository(vt_QSFHEntities ContextDB) : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public List<Vt_DynamicFieldsValues> GetDynamicFieldsValues(int FieldID)
        {
            IEnumerable<Vt_DynamicFieldsValues> DynamicFieldsValues = DBContext.Vt_DynamicFieldsValues.Where(x => x.DynamicFieldsID == FieldID);

            if (DynamicFieldsValues == null)
            {
                return null;
            }
            else
            {
                return DynamicFieldsValues.ToList();
            }
        }

        public bool Update(List<Vt_DynamicFieldsValues> Obj)
        {
            var Data = Obj.LastOrDefault();
            int FieldID = Convert.ToInt32(Data.DynamicFieldsID);
            //Delete
            Delete(FieldID);
            //Create
            Create(Obj);

            return true;
        }

        public bool Delete(int FieldID)
        {
            List<Vt_DynamicFieldsValues> FieldsValues = DBContext.Vt_DynamicFieldsValues.Where(x => x.DynamicFieldsID == FieldID).ToList();
            if (FieldsValues.Count > 0)
            {
                DBContext.Vt_DynamicFieldsValues.RemoveRange(FieldsValues);
                DBContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Create(List<Vt_DynamicFieldsValues> Obj)
        {
            List<Vt_DynamicFieldsValues> FieldsValuesList = new List<Vt_DynamicFieldsValues>();
            foreach (var items in Obj)
            {
                Vt_DynamicFieldsValues FieldsValuesObj = new Vt_DynamicFieldsValues();
                FieldsValuesObj.DynamicFieldsID = items.DynamicFieldsID;
                FieldsValuesObj.FieldText = items.FieldText;
                FieldsValuesObj.FieldValue = items.FieldValue;
                FieldsValuesObj.IsActive = items.IsActive;
                FieldsValuesList.Add(FieldsValuesObj);
                DBContext.Entry(FieldsValuesObj).State = System.Data.Entity.EntityState.Added;
            }
            DBContext.SaveChanges();
            return true;
        }
    }
}