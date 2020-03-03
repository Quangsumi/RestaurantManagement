using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class BillInfoDataAccess : DataAccess<tblBillInfo>
    {
        public override bool AddRecord(tblBillInfo newRecord)
        {
            try
            {
                _dataContext.tblBillInfos.InsertOnSubmit(newRecord);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool DeleteRecord(int idRecordToDelete)
        {
            try
            {
                tblBillInfo billInfoToDelete = _dataContext.tblBillInfos
                    .FirstOrDefault(b => b.ID == idRecordToDelete);

                if (billInfoToDelete != null)
                {
                    _dataContext.tblBillInfos.DeleteOnSubmit(billInfoToDelete);
                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public override List<tblBillInfo> GetRecords()
        {
            try
            {
                return _dataContext.tblBillInfos.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool UpdateRecord(tblBillInfo pendingRecord)
        {
            try
            {
                tblBillInfo billInfoToUpdate = _dataContext.tblBillInfos
                    .FirstOrDefault(c => c.ID == pendingRecord.ID);

                if (billInfoToUpdate != null)
                {
                    billInfoToUpdate.tblBill = _dataContext.tblBills.Single(b => b.ID == pendingRecord.BillID);
                    billInfoToUpdate.tblFood = _dataContext.tblFoods.Single(f => f.ID == pendingRecord.FoodID);
                    billInfoToUpdate.Count = pendingRecord.Count;

                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<tblBill> GetBillsOfBillInfo()
        {
            try
            {
                return _dataContext.tblBills.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public List<tblFood> GetFoodsOfBillInfo()
        {
            try
            {
                return _dataContext.tblFoods.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public string GetFoodNameByFoodID(string foodID)
        {
            int id = Int32.TryParse(foodID, out int result) ? result : 1;

            try
            {
                return _dataContext.tblFoods.FirstOrDefault(f => f.ID == id)?.Name;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
