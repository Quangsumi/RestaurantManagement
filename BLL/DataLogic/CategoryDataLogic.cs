using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.DataAccess;

namespace BLL.DataLogic
{
    public class CategoryDataLogic : DataLogic<tblCategory>
    {
        public override DataAccess<tblCategory> _dataAccess { get; set; } = new CategoryDataAccess();

        public override bool AddRecord(tblCategory newRecord)
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

        public override List<tblCategory> GetRecords()
        {
            try
            {
                return _dataAccess.GetRecords();
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool UpdateRecord(tblCategory recordToUpdate)
        {
            try
            {
                return _dataAccess.UpdateRecord(recordToUpdate);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
