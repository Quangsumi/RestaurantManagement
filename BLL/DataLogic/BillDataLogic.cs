using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.DataLogic
{
    public class BillDataLogic
    {
        BillDataAccess _billDataAccess = new BillDataAccess();

        public List<tblBill> GetBills()
        {
            try
            {
                return _billDataAccess.GetBills();
            }
            catch (Exception ex) { throw ex; }
        }

        public bool AddOneBill(tblBill newBill)
        {
            try
            {
                return _billDataAccess.AddOneBill(newBill);
            }
            catch (Exception ex) { throw ex; }
        }

        public bool DeleteOneBill(int idBillToDelete)
        {
            try
            {
                return _billDataAccess.DeleteOneBill(idBillToDelete);
            }
            catch (Exception ex) { throw ex; }
        }

        public bool UpdateOneBill(tblBill billToUpdate)
        {
            try
            {
                return _billDataAccess.UpdateOneBill(billToUpdate);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
