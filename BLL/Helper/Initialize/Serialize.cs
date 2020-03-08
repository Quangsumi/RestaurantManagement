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
        #region For saving when app is suddenly closed
        public static Button NewBtnTable(TempBtnTable tempBtnTable)
        {
            Button btnTable = new Button();
            btnTable.Text = tempBtnTable.Text;
            btnTable.Width = 110;
            btnTable.Height = 110;

            btnTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTable.Font = new System.Drawing.Font("Segoe UI Light", 14.25F);
            btnTable.ForeColor = System.Drawing.Color.White;

            Order orderOfBtnTable = new Order();
            orderOfBtnTable.CheckInDate = tempBtnTable.CheckInDate;
            orderOfBtnTable.CheckOutDate = tempBtnTable.CheckOutDate;
            orderOfBtnTable.Discount = tempBtnTable.Discount;
            orderOfBtnTable.TotalPrice = tempBtnTable.TotalPrice;

            if (tempBtnTable.Table != null)
            {
                orderOfBtnTable.Table = new tblTable();
                orderOfBtnTable.Table.ID = tempBtnTable.Table.ID;
                orderOfBtnTable.Table.Name = tempBtnTable.Table.Name;
                orderOfBtnTable.Table.Status = tempBtnTable.Table.Status;
            }

            if (tempBtnTable.Foods != null)
            {
                orderOfBtnTable.Foods = new Dictionary<tblFood, int>();

                for (int i = 0; i < tempBtnTable.Foods.Length; i++)
                {
                    tblFood food = new tblFood();

                    if (tempBtnTable.Foods[i].TempFood != null)
                    {
                        food.ID = tempBtnTable.Foods[i].TempFood.ID;
                        food.Name = tempBtnTable.Foods[i].TempFood.Name;
                        food.CategoryID = tempBtnTable.Foods[i].TempFood.CategoryID;
                        food.Price = tempBtnTable.Foods[i].TempFood.Price;
                    }

                    orderOfBtnTable.Foods.Add(food, tempBtnTable.Foods[i].Count);
                }
            }

            btnTable.Tag = orderOfBtnTable;

            if (orderOfBtnTable.Foods != null)
                btnTable.BackColor = System.Drawing.Color.Chocolate;
            else
                btnTable.BackColor = System.Drawing.Color.DodgerBlue;

            return btnTable;
        }
        #endregion
    }
}
