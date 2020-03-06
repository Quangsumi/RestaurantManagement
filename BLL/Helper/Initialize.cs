using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL.Helper
{
    public static class Initialize
    {
        #region For checkout funtion
        public static Button NewBtnTable(tblTable table)
        {
            Button btnTable = new Button();
            btnTable.Text = table.Name;
            btnTable.Width = 110;
            btnTable.Height = 110;
            btnTable.BackColor = System.Drawing.Color.DodgerBlue;
            btnTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTable.Font = new System.Drawing.Font("Segoe UI Light", 14.25F);
            btnTable.ForeColor = System.Drawing.Color.White;
            
            btnTable.Tag = new Order();
            (btnTable.Tag as Order).Table = table;

            return btnTable;
        }

        public static string NewCheckoutInfo(Order checkoutOrder, TextBox txtMainDiscount)
        {
            string msg = "";

            msg += "[" + checkoutOrder.Table.ID.ToString().ToUpper() + "] " + checkoutOrder.Table.Name.ToUpper();
            msg += "\r\n\n1/ Checkin Date: " + checkoutOrder.CheckInDate.ToString();
            msg += "\r\n2/ Checkout Date: " + DateTime.Now.ToString();
            msg += "\r\n3/ Orders: ";
            foreach (var food in checkoutOrder.Foods)
            {
                msg += "\r\n + " + food.Key.Name + " - x" + food.Value;
            }
            checkoutOrder.Discount = Int32.Parse(txtMainDiscount.Text);
            msg += "\r\n4/ Discount: " + checkoutOrder.Discount + "%";
            msg += "\r\n5/ Totol Price: $" + checkoutOrder.TotalPrice;

            return msg;
        }

        public static tblBill NewCheckoutBill(Order checkoutOrder, TextBox txtMainDiscount)
        {
            tblBill newBill = new tblBill();
            newBill.CheckInDate = checkoutOrder.CheckInDate;
            newBill.CheckOutDate = DateTime.Now;
            newBill.TableID = checkoutOrder.Table.ID;
            newBill.Discount = Int32.Parse(txtMainDiscount.Text);
            newBill.TotalPrice = checkoutOrder.TotalPrice;
            newBill.Status = 1;

            return newBill;
        }

        public static tblBillInfo NewCheckoutBillInfo(int lastBillID, int foodID, int count)
        {
            tblBillInfo newBillInfo = new tblBillInfo();
            newBillInfo.BillID = lastBillID;
            newBillInfo.FoodID = foodID;
            newBillInfo.Count = count;

            return newBillInfo;
        }
        #endregion


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
