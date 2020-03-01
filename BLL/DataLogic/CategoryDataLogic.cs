using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DataLogic
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
            catch (Exception ex) { throw ex; }
        }

        public bool AddOneCategory(tblCategory newCategory)
        {
            try
            {
                return _categoryDataAccess.AddOneCategory(newCategory);
            }
            catch (Exception ex) { throw ex; }
        }

        public bool DeleteOneCategory(int idCategoryToDelete)
        {
            try
            {
                return _categoryDataAccess.DeleteOneCategory(idCategoryToDelete);
            }
            catch (Exception ex) { throw ex; }
        }

        public bool UpdateOneCategory(tblCategory categoryToUpdate)
        {
            try
            {
                return _categoryDataAccess.UpdateOneCategory(categoryToUpdate);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
