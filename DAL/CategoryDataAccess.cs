﻿using System;
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
                return _dataContext.tblCategories.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public bool AddOneCategory(tblCategory newCategory)
        {
            try
            {
                _dataContext.tblCategories.InsertOnSubmit(newCategory);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex) { throw ex; }
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

        public bool UpdateOneCategory(tblCategory pendingCategory)
        {
            try
            {
                tblCategory categoryToUpdate = _dataContext.tblCategories
                    .FirstOrDefault(c => c.ID == pendingCategory.ID);

                if (categoryToUpdate != null)
                {
                    categoryToUpdate.Name = pendingCategory.Name;
                    
                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
