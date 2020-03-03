using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class BillDataAccess : DataAccess<tblBill>
    {
        public override bool AddRecord(tblBill newRecord)
        {
            try
            {
                _dataContext.tblBills.InsertOnSubmit(newRecord);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool DeleteRecord(int idRecordToDelete)
        {
            try
            {
                tblBill billToDelete = _dataContext.tblBills
                    .FirstOrDefault(a => a.ID == idRecordToDelete);

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

        public override List<tblBill> GetRecords()
        {
            try
            {
                return _dataContext.tblBills.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool UpdateRecord(tblBill pendingRecord)
        {
            try
            {
                tblBill billToUpdate = _dataContext.tblBills
                    .FirstOrDefault(c => c.ID == pendingRecord.ID);

                if (billToUpdate != null)
                {
                    billToUpdate.CheckInDate = pendingRecord.CheckInDate;
                    billToUpdate.CheckOutDate = pendingRecord.CheckOutDate;
                    billToUpdate.TableID = pendingRecord.TableID;
                    billToUpdate.Status = pendingRecord.Status;
                    billToUpdate.Discount = pendingRecord.Discount;
                    billToUpdate.TotalPrice = pendingRecord.TotalPrice;

                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<tblTable> GetTablesOfBill()
        {
            try
            {
                return _dataContext.tblTables.ToList();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
