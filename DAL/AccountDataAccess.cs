using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AccountDataAccess
    {
        RestaurantManagementDataContext _dataContext = new RestaurantManagementDataContext();

        public List<tblAccount> GetCategories()
        {
            try
            {
                return _dataContext.tblAccounts.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public bool AddOneAccount(tblAccount newAccount)
        {
            try
            {
                _dataContext.tblAccounts.InsertOnSubmit(newAccount);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool DeleteOneAccount(int idAccountToDelete)
        {
            try
            {
                tblAccount accountToDelete = _dataContext.tblAccounts
                    .FirstOrDefault(a => a.ID == idAccountToDelete);

                if (accountToDelete != null)
                {
                    _dataContext.tblAccounts.DeleteOnSubmit(accountToDelete);
                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool UpdateOneAccount(tblAccount pendingAccount)
        {
            try
            {
                tblAccount accountToUpdate = _dataContext.tblAccounts
                    .FirstOrDefault(c => c.ID == pendingAccount.ID);

                if (accountToUpdate != null)
                {
                    accountToUpdate.Username= pendingAccount.Username;
                    accountToUpdate.DisplayName = pendingAccount.DisplayName;
                    accountToUpdate.Password = pendingAccount.Password;
                    accountToUpdate.Type = pendingAccount.Type;
                    
                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
