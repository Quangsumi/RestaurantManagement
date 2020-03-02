using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BillInfoDataAccess
    {
        RestaurantManagementDataContext _dataContext = new RestaurantManagementDataContext();

        public List<tblBillInfo> GetBillInfos()
        {
            try
            {
                return _dataContext.tblBillInfos.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public bool AddOneBillInfo(tblBillInfo newBillInfo)
        {
            try
            {
                _dataContext.tblBillInfos.InsertOnSubmit(newBillInfo);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool DeleteOneBillInfo(int idBillInfoToDelete)
        {
            try
            {
                tblBillInfo billInfoToDelete = _dataContext.tblBillInfos
                    .FirstOrDefault(b => b.ID == idBillInfoToDelete);

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

        public bool UpdateOneBillInfo(tblBillInfo pendingBillInfo)
        {
            try
            {
                tblBillInfo billInfoToUpdate = _dataContext.tblBillInfos
                    .FirstOrDefault(c => c.ID == pendingBillInfo.ID);

                if (billInfoToUpdate != null)
                {
                    billInfoToUpdate.tblBill = _dataContext.tblBills.Single(b => b.ID == pendingBillInfo.BillID);
                    billInfoToUpdate.tblFood = _dataContext.tblFoods.Single(f => f.ID == pendingBillInfo.FoodID);
                    billInfoToUpdate.Count = pendingBillInfo.Count;

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
    }
}
