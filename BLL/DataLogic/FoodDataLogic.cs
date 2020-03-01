using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.DataLogic
{
    public class FoodDataLogic
    {
        FoodDataAccess _dataAccess = new FoodDataAccess();

        public List<tblFood> GetFoods()
        {
            try
            {
                return _dataAccess.GetFoods();
            }
            catch (Exception ex) { throw ex; }
        }

        public bool AddOneFood(tblFood newFood)
        {
            try
            {
                return _dataAccess.AddOneFood(newFood);
            }
            catch (Exception ex) { throw ex; }
        }

        public bool DeleteOneFood(int idFoodToDelete)
        {
            try
            {
                return _dataAccess.DeleteOneFood(idFoodToDelete);
            }
            catch (Exception ex) { throw ex; }
        }

        public bool UpdateOneFood(tblFood foodToUpdate)
        {
            try
            {
                return _dataAccess.UpdateOneFood(foodToUpdate);
            }
            catch (Exception ex) { throw ex; }
        }

        public List<tblCategory> GetCategoryOfFood()
        {
            try
            {
                return _dataAccess.GetCategoryOfFood();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
