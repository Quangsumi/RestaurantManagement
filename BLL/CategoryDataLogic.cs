using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CategoryDataLogic
    {
        CategoryDataAccess _categoryDataAccess = new CategoryDataAccess();

        public List<tblCategory> GetCategories()
        {
            try
            {
                return _categoryDataAccess.GetCategories();
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
                _categoryDataAccess.AddOneCategory(newCategory);
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
                return _categoryDataAccess.DeleteOneCategory(idCategoryToDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateACategory(tblCategory newCategory)
        {
            try
            {
                return _categoryDataAccess.UpdateACategory(newCategory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
