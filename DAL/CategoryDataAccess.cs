using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoryDataAccess
    {
        RestaurantManagementDataContext _dataContext = new RestaurantManagementDataContext();

        public List<tblCategory> GetCategories()
        {
            try
            {
                List<tblCategory> categories = _dataContext.tblCategories.ToList();
                return categories;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddOneCategory(tblCategory newCategory)
        {
            try
            {
                _dataContext.tblCategories.InsertOnSubmit(newCategory);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteOneCategory(int idCategoryToDelete)
        {
            try
            {
                tblCategory categoryToDelete = _dataContext.tblCategories
                .FirstOrDefault(c => c.ID == idCategoryToDelete);

                if (categoryToDelete != null)
                {
                    _dataContext.tblCategories.DeleteOnSubmit(categoryToDelete);
                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool UpdateACategory(tblCategory newCategory)
        {
            try
            {
                tblCategory oldCategory = _dataContext.tblCategories
                .FirstOrDefault(c => c.ID == newCategory.ID);

                if (oldCategory != null)
                {
                    oldCategory.Name = newCategory.Name;
                    _dataContext.SubmitChanges();

                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
