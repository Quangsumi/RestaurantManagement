using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL.Helper.Initialize
{
    public static partial class Initialize
    {
        #region For CRUD funtions in all forms
        public static tblAccount NewAccount(TextBox txtAccountID, TextBox txtAccountUsername, TextBox txtAccountDisplayName, TextBox txtAccountPassword, ComboBox cboAccountType)
        {
            tblAccount newAcc = new tblAccount();
            newAcc.ID = Convert.ToInt32(txtAccountID.Text);
            newAcc.Username = txtAccountUsername.Text;
            newAcc.DisplayName = txtAccountDisplayName.Text;
            newAcc.Password = txtAccountPassword.Text;
            newAcc.Type = Convert.ToInt32(cboAccountType.Text);

            return newAcc;
        }

        public static tblBill NewBill(TextBox txtBillID, DateTimePicker dtpCheckInDate, DateTimePicker dtpCheckOutDate, ComboBox cboBillTableID, TextBox txtBillStatus, TextBox txtDiscount, TextBox txtTotalPrice)
        {
            tblBill newBill = new tblBill();
            newBill.ID = Convert.ToInt32(txtBillID.Text);
            newBill.CheckInDate = dtpCheckInDate.Value;
            newBill.CheckOutDate = dtpCheckOutDate.Value;
            newBill.TableID = Convert.ToInt32(cboBillTableID.Text);
            newBill.Status = Convert.ToInt32(txtBillStatus.Text);
            newBill.Discount = Convert.ToInt32(txtDiscount.Text);
            newBill.TotalPrice = Convert.ToDouble(txtTotalPrice.Text);

            return newBill;
        }

        public static tblBillInfo NewBillInfo(TextBox txtBillInfoID, ComboBox cboBillInfoBillID, ComboBox cboBillInfoFoodID, TextBox txtCount)
        {
            tblBillInfo newBillInfo = new tblBillInfo();
            newBillInfo.ID = Convert.ToInt32(txtBillInfoID.Text);
            newBillInfo.BillID = Convert.ToInt32(cboBillInfoBillID.Text);
            newBillInfo.FoodID = Convert.ToInt32(cboBillInfoFoodID.Text);
            newBillInfo.Count = Convert.ToInt32(txtCount.Text);

            return newBillInfo;
        }

        public static tblCategory NewCategory(TextBox txtCategoryID, TextBox txtCategoryName)
        {
            tblCategory newCategory = new tblCategory();
            newCategory.ID = Convert.ToInt32(txtCategoryID.Text);
            newCategory.Name = txtCategoryName.Text;

            return newCategory;
        }

        public static tblFood NewFood(TextBox txtFoodID, TextBox txtFoodName, ComboBox cboFoodCategoryID, TextBox txtFoodPrice)
        {
            tblFood newFood = new tblFood();
            newFood.ID = Convert.ToInt32(txtFoodID.Text);
            newFood.Name = txtFoodName.Text;
            newFood.CategoryID = Convert.ToInt32(cboFoodCategoryID.Text);
            newFood.Price = Convert.ToDouble(txtFoodPrice.Text);

            return newFood;
        }

        public static tblTable NewTable(TextBox txtTableID, TextBox txtTableName, TextBox txtTableStatus)
        {
            tblTable newTable = new tblTable();
            newTable.ID = Convert.ToInt32(txtTableID.Text);
            newTable.Name = txtTableName.Text;
            newTable.Status = Convert.ToInt32(txtTableStatus.Text);

            return newTable;
        }
        #endregion
    }
}
