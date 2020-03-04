using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class FoodDataAccess : DataAccess<tblFood>
    {
        public override bool AddRecord(tblFood newRecord)
        {
            try
            {
                _dataContext.tblFoods.InsertOnSubmit(newRecord);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool DeleteRecord(int idRecordToDelete)
        {
            try
            {
                tblFood foodToDelete = _dataContext.tblFoods
                    .FirstOrDefault(f => f.ID == idRecordToDelete);

                if (foodToDelete != null)
                {
                    _dataContext.tblFoods.DeleteOnSubmit(foodToDelete);
                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public override List<tblFood> GetRecords()
        {
            try
            {
                return _dataContext.tblFoods.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool UpdateRecord(tblFood pendingRecord)
        {
            try
            {
                tblFood foodToUpdate = _dataContext.tblFoods
                    .FirstOrDefault(f => f.ID == pendingRecord.ID);

                if (foodToUpdate != null)
                {
                    foodToUpdate.Name = pendingRecord.Name;
                    foodToUpdate.Price = pendingRecord.Price;
                    foodToUpdate.tblCategory = _dataContext.tblCategories.Single(t => t.ID == pendingRecord.CategoryID);

                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<tblCategory> GetCategoriesOfFood()
        {
            try
            {
                return _dataContext.tblCategories.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public string GetCatagoryNameByCategoryID(string catagoryID)
        {
            int id = Int32.TryParse(catagoryID, out int result) ? result : 1;

            try
            {
                return _dataContext.tblCategories.FirstOrDefault(c => c.ID == id)?.Name;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<tblFood> GetFoodByCategoryID(int categoryID)
        {
            try
            {
                return _dataContext.tblFoods.Where(f => f.CategoryID == categoryID).ToList();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
