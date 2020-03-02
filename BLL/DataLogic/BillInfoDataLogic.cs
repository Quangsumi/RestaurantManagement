﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.DataLogic
{
    public class BillInfoDataLogic
    {
        BillInfoDataAccess _billInfoDataAccess = new BillInfoDataAccess();

        public List<tblBillInfo> GetBillInfos()
        {
            try
            {
                return _billInfoDataAccess.GetBillInfos();
            }
            catch (Exception ex) { throw ex; }
        }

        public bool AddOneBillInfo(tblBillInfo newBillInfo)
        {
            try
            {
                return _billInfoDataAccess.AddOneBillInfo(newBillInfo);
            }
            catch (Exception ex) { throw ex; }
        }

        public bool DeleteOneBillInfo(int idBillInfoToDelete)
        {
            try
            {
                return _billInfoDataAccess.DeleteOneBillInfo(idBillInfoToDelete);
            }
            catch (Exception ex) { throw ex; }
        }

        public bool UpdateOneBillInfo(tblBillInfo billInfoToUpdate)
        {
            try
            {
                return _billInfoDataAccess.UpdateOneBillInfo(billInfoToUpdate);
            }
            catch (Exception ex) { throw ex; }
        }

        public List<tblBill> GetBillsOfBillInfo()
        {
            try
            {
                return _billInfoDataAccess.GetBillsOfBillInfo();
            }
            catch (Exception ex) { throw ex; }
        }

        public List<tblFood> GetFoodsOfBillInfo()
        {
            try
            {
                return _billInfoDataAccess.GetFoodsOfBillInfo();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}