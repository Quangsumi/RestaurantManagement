using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FoodDataAccess
    {
        RestaurantManagementDataContext _dataContext = new RestaurantManagementDataContext();

        public List<tblFood> GetFoods()
        {
            try
            {
                return _dataContext.tblFoods.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public bool AddOneFood(tblFood newFood)
        {
            try
            {
                _dataContext.tblFoods.InsertOnSubmit(newFood);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool DeleteOneFood(int idFoodToDelete)
        {
            try
            {
                tblFood foodToDelete = _dataContext.tblFoods
                    .FirstOrDefault(f => f.ID == idFoodToDelete);

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

        public bool UpdateOneFood(tblFood pendingFood)
        {
            try
            {
                tblFood foodToUpdate = _dataContext.tblFoods
                    .FirstOrDefault(f => f.ID == pendingFood.ID);

                if (foodToUpdate != null)
                {
                    foodToUpdate.Name = pendingFood.Name;
                    foodToUpdate.Price = pendingFood.Price;
                    foodToUpdate.tblCategory = _dataContext.tblCategories.Single(t => t.ID == pendingFood.CategoryID);

                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<tblCategory> GetCategoryOfFood()
        {
            try
            {
                return _dataContext.tblCategories.ToList();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
