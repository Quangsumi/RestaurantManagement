﻿using System;
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

        public string GetTableNameByTableID(string tableID)
        {
            int id = Int32.TryParse(tableID, out int result) ? result : 1;

            try
            {
                return _dataContext.tblTables.FirstOrDefault(t => t.ID == id)?.Name;
            }
            catch (Exception ex) { throw ex; }
        }

        public int AddBillUsingSP(tblBill newBill)
        {
            try
            {
                spAddBillResult sp = _dataContext.spAddBill(newBill.CheckOutDate, newBill.CheckInDate, newBill.TableID, newBill.Discount, newBill.TotalPrice, newBill.Status).FirstOrDefault();

                int lastID = Int32.Parse(sp.Column1.Value.ToString());
                
                return lastID;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
