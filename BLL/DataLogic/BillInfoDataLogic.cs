using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.DataAccess;

namespace BLL.DataLogic
{
    public class BillInfoDataLogic : DataLogic<tblBillInfo>
    {
        public override DataAccess<tblBillInfo> _dataAccess { get; set; } = new BillInfoDataAccess();

        public override bool AddRecord(tblBillInfo newRecord)
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

        public override List<tblBillInfo> GetRecords()
        {
            try
            {
                return _dataAccess.GetRecords();
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool UpdateRecord(tblBillInfo recordToUpdate)
        {
            try
            {
                return _dataAccess.UpdateRecord(recordToUpdate);
            }
            catch (Exception ex) { throw ex; }
        }

        public List<tblBill> GetBillsOfBillInfo()
        {
            try
            {
                return (_dataAccess as BillInfoDataAccess).GetBillsOfBillInfo();
            }
            catch (Exception ex) { throw ex; }
        }

        public List<tblFood> GetFoodsOfBillInfo()
        {
            try
            {
                return (_dataAccess as BillInfoDataAccess).GetFoodsOfBillInfo();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
