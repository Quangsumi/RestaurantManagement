using DAL;
using DTO;
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
        #region For checkout funtion
        public static Button NewBtnTable(tblTable table)
        {
            Button btnTable = new Button();
            btnTable.Text = table.Name + "\r\n(" + (table.Status == 0 ? "None" : "Full") + ")";
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
                msg += "\r\n + " + food.Key.Name + " - $" + food.Key.Price + " - x" + food.Value;
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
    }
}
