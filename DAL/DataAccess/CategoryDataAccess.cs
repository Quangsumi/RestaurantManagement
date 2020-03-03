using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class CategoryDataAccess : DataAccess<tblCategory>
    {
        public override bool AddRecord(tblCategory newRecord)
        {
            try
            {
                _dataContext.tblCategories.InsertOnSubmit(newRecord);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool DeleteRecord(int idRecordToDelete)
        {
            try
            {
                tblCategory categoryToDelete = _dataContext.tblCategories
                    .FirstOrDefault(c => c.ID == idRecordToDelete);

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

        public override List<tblCategory> GetRecords()
        {
            try
            {
                return _dataContext.tblCategories.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool UpdateRecord(tblCategory pendingRecord)
        {
            try
            {
                tblCategory categoryToUpdate = _dataContext.tblCategories
                    .FirstOrDefault(c => c.ID == pendingRecord.ID);

                if (categoryToUpdate != null)
                {
                    categoryToUpdate.Name = pendingRecord.Name;

                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
