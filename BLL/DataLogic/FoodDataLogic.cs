using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.DataAccess;

namespace BLL.DataLogic
{
    public class FoodDataLogic : DataLogic<tblFood>
    {
        public override DataAccess<tblFood> _dataAccess { get; set; } = new FoodDataAccess();

        public override bool AddRecord(tblFood newRecord)
        {
            try
            {
                return _dataAccess.AddRecord(newRecord);
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool DeleteRecord(int idRecordToDelete)
        {
            try
            {
                return _dataAccess.DeleteRecord(idRecordToDelete);
            }
            catch (Exception ex) { throw ex; }
        }

        public override List<tblFood> GetRecords()
        {
            try
            {
                return _dataAccess.GetRecords();
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool UpdateRecord(tblFood recordToUpdate)
        {
            try
            {
                return _dataAccess.UpdateRecord(recordToUpdate);
            }
            catch (Exception ex) { throw ex; }
        }

        public List<tblCategory> GetCategoriesOfFood()
        {
            try
            {
                return (_dataAccess as FoodDataAccess).GetCategoriesOfFood();
            }
            catch (Exception ex) { throw ex; }
        }

        public string GetCatagoryNameByCategoryID(string catagoryID)
        {
            try
            {
                return (_dataAccess as FoodDataAccess).GetCatagoryNameByCategoryID(catagoryID);
            }
            catch (Exception ex) { throw ex; }
        }

        public List<tblFood> GetFoodByCategoryID(int categoryID)
        {
            try
            {
                return (_dataAccess as FoodDataAccess).GetFoodByCategoryID(categoryID);
            }
            catch (Exception ex) { throw ex; }
        }

        public tblFood GetFoodByID(int id)
        {
            try
            {
                return (_dataAccess as FoodDataAccess).GetFoodByID(id);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
