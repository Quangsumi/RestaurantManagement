using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using DTO;

namespace BLL.Helper.Tools
{
    public static partial class Tools
    {
        public static readonly string receiptsDirPath = Directory.GetCurrentDirectory() + "\\Bills\\";
        public static readonly string excelFileDirPath = Directory.GetCurrentDirectory() + "\\Excel Files\\";
        public static readonly string tempFilePath = Directory.GetCurrentDirectory() + "\\Temp\\temp.dat";
        static DataSet _dataSet = new DataSet();


        #region Save checkout info to file text
        public static void SaveCheckoutInfoToFile(Order checkoutOrder, int lastBillID, TextBox txtMainDiscount)
        {
            string content = GetCheckoutContent(checkoutOrder, lastBillID, txtMainDiscount);
            string path = GetNewTextPath(lastBillID);

            SaveToFile(path, content);
        }

        private static string GetCheckoutContent(Order checkoutOrder, int lastBillID, TextBox txtMainDiscount)
        {
            string content = "";

            content += "=================== #" + lastBillID.ToString() + " ===================";
            content += "\r\n\n1/ Table: " + checkoutOrder.Table.ID.ToString() + "-" + checkoutOrder.Table.Name;
            content += "\r\n2/ Checkin Date: " + checkoutOrder.CheckInDate.ToString();
            content += "\r\n3/ Checkout Date: " + DateTime.Now.ToString();
            content += "\r\n\n4/ Orders: ";
            foreach (var food in checkoutOrder.Foods)
            {
                content += "\r\n + " + food.Key.Name + " - $" + food.Key.Price + " - x" + food.Value;
            }
            checkoutOrder.Discount = Int32.Parse(txtMainDiscount.Text);
            content += "\r\n\n5/ Discount: " + checkoutOrder.Discount + "%";
            content += "\r\n6/ Totol Price: $" + checkoutOrder.TotalPrice;
            content += "\r\n7/ Status: Checkouted";

            return content;
        }

        private static string GetNewTextPath(int lastBillID)
            => receiptsDirPath + "#" + lastBillID.ToString() + ".txt";

        private static void SaveToFile(string path, string content)
        {
            FileStream fs = null;
            StreamWriter textOut = null;
            try
            {
                fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                textOut = new StreamWriter(fs);
                textOut.Write(content);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (fs != null)
                    textOut.Close(); // Close the StreamWriter obj and the associated FileStream obj
            }
        }
        #endregion
    }
}