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
        public static Button BtnTable(tblTable table)
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

        public static string CheckoutInfo(Order checkoutOrder, TextBox txtMainDiscount)
        {
            string msg = "";

            msg += "[" + checkoutOrder.Table.ID.ToString().ToUpper() + "] " + checkoutOrder.Table.Name.ToUpper() + "\r\n\n";
            msg += "1/ Checkin Date: " + checkoutOrder.CheckInDate.ToString() + "\r\n";
            msg += "2/ Checkout Date: " + DateTime.Now.ToString() + "\r\n";
            msg += "3/ Status: " + checkoutOrder.Status + "\r\n";
            msg += "4/ Orders: " + "\r\n";
            foreach (var food in checkoutOrder.Foods)
            {
                msg += "+ Food name: " + food.Key.Name + " - x" + food.Value + "\r\n";
            }
            checkoutOrder.Discount = Int32.Parse(txtMainDiscount.Text);
            msg += "\r\n5/ Discount: " + checkoutOrder.Discount + "%\r\n";
            msg += "6/ Totol Price: $" + checkoutOrder.TotalPrice;

            return msg;
        }

        public static tblBill Bill(Order checkoutOrder, TextBox txtMainDiscount)
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

        public static tblBillInfo BillInfo(int lastBillID, int foodID, int count)
        {
            tblBillInfo newBillInfo = new tblBillInfo();
            newBillInfo.BillID = lastBillID;
            newBillInfo.FoodID = foodID;
            newBillInfo.Count = count;

            return newBillInfo;
        }
    }
}
