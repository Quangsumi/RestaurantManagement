using DAL;
using DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DataLogic
{
    public class AccountDataLogic : DataLogic<tblAccount>
    {
        public override DataAccess<tblAccount> _dataAccess { get; set; } = new AccountDataAccess();

        public override bool AddRecord(tblAccount newRecord)
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

        public override List<tblAccount> GetRecords()
        {
            try
            {
                return _dataAccess.GetRecords();
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool UpdateRecord(tblAccount recordToUpdate)
        {
            try
            {
                return _dataAccess.UpdateRecord(recordToUpdate);
            }
            catch (Exception ex) { throw ex; }
        }

        public tblAccount FindAccount(tblAccount account)
        {
            try
            {
                return (_dataAccess as AccountDataAccess).FindAccount(account);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
