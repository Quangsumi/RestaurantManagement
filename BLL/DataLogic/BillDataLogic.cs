﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.DataAccess;

namespace BLL.DataLogic
{
    public class BillDataLogic : DataLogic<tblBill>
    {

        public override DataAccess<tblBill> _dataAccess { get; set; } = new BillDataAccess();

        public override bool AddRecord(tblBill newRecord)
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

        public override List<tblBill> GetRecords()
        {
            try
            {
                return _dataAccess.GetRecords();
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool UpdateRecord(tblBill recordToUpdate)
        {
            try
            {
                return _dataAccess.UpdateRecord(recordToUpdate);
            }
            catch (Exception ex) { throw ex; }
        }

        public List<tblTable> GetTablesOfBill()
        {
            try
            {
                return (_dataAccess as BillDataAccess).GetTablesOfBill();
            }
            catch (Exception ex) { throw ex; }
        }

        public string GetTableNameByTableID(string tableID)
        {
            try
            {
                return (_dataAccess as BillDataAccess).GetTableNameByTableID(tableID);
            }
            catch (Exception ex) { throw ex; }
        }

        public int AddBillUsingSP(tblBill newBill)
        {
            try
            {
                int lastID = (_dataAccess as BillDataAccess).AddBillUsingSP(newBill);
                
                return lastID; 
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
