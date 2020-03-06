using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class AccountDataAccess : DataAccess<tblAccount>
    {
        public override bool AddRecord(tblAccount newRecord)
        {
            try
            {
                _dataContext.tblAccounts.InsertOnSubmit(newRecord);
                _dataContext.SubmitChanges();
                return true;

            }
            catch (Exception ex) { throw ex; }
        }

        public override bool DeleteRecord(int idRecordToDelete)
        {
            tblAccount accToDelete = _dataContext.tblAccounts
                    .FirstOrDefault(a => a.ID == idRecordToDelete);

            if (accToDelete != null)
            {
                _dataContext.tblAccounts.DeleteOnSubmit(accToDelete);
                _dataContext.SubmitChanges();
                return true;
            }
            return false;
        }

        public override List<tblAccount> GetRecords()
        {
            try
            {
                return _dataContext.tblAccounts.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool UpdateRecord(tblAccount pendingRecord)
        {
            try
            {
                tblAccount accToUpdate = _dataContext.tblAccounts
                    .FirstOrDefault(c => c.ID == pendingRecord.ID);

                if (accToUpdate != null)
                {
                    accToUpdate.Username = pendingRecord.Username;
                    accToUpdate.DisplayName = pendingRecord.DisplayName;
                    accToUpdate.Password = pendingRecord.Password;
                    accToUpdate.Type = pendingRecord.Type;

                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public tblAccount FindAccount(tblAccount account)
        {
            try
            {
                return _dataContext.tblAccounts
                    .FirstOrDefault(a => a.Username == account.Username && a.Password == account.Password);
            }
            catch (Exception ex){ throw ex; }
        }
    }
}
