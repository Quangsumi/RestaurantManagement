using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BillDataAccess
    {
        RestaurantManagementDataContext _dataContext = new RestaurantManagementDataContext();

        public List<tblBill> GetBills()
        {
            try
            {
                return _dataContext.tblBills.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public bool AddOneBill(tblBill newBill)
        {
            try
            {
                _dataContext.tblBills.InsertOnSubmit(newBill);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool DeleteOneBill(int idBillToDelete)
        {
            try
            {
                tblBill billToDelete = _dataContext.tblBills
                    .FirstOrDefault(a => a.ID == idBillToDelete);

                if (billToDelete != null)
                {
                    _dataContext.tblBills.DeleteOnSubmit(billToDelete);
                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool UpdateOneBill(tblBill pendingBill)
        {
            try
            {
                tblBill billToUpdate = _dataContext.tblBills
                    .FirstOrDefault(c => c.ID == pendingBill.ID);

                if (billToUpdate != null)
                {
                    billToUpdate.CheckInDate = pendingBill.CheckInDate;
                    billToUpdate.CheckOutDate = pendingBill.CheckOutDate;
                    billToUpdate.TableID = pendingBill.TableID;
                    billToUpdate.Status = pendingBill.Status;
                    billToUpdate.Discount = pendingBill.Discount;
                    billToUpdate.TotalPrice = pendingBill.TotalPrice;

                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
