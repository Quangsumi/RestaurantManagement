using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL.Helper.Tools
{
    public static partial class Tools
    {
        #region Blackout funtions
        public static bool HasTempData()
            => File.Exists(tempFilePath);

        public static void DeleteTemp()
            => File.Delete(tempFilePath);

        public static List<TempBtnTable> GetTempBtnTableFromFile()
            => DeserializeTempBtnTables();

        public static List<TempBtnTable> DeserializeTempBtnTables()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);
            List<TempBtnTable> tempBtnTables = (List<TempBtnTable>)formatter.Deserialize(stream);
            stream.Close();
            return tempBtnTables;
        }

        public static void SerializeBtnTables(List<Button> btnTables)
        {
            List<TempBtnTable> tempBtnTables = new List<TempBtnTable>();

            foreach (var btnTable in btnTables)
            {
                TempBtnTable tempBtnTable = new TempBtnTable();
                tempBtnTable.Text = btnTable.Text;

                Order order = btnTable.Tag as Order;
                if (order != null)
                {
                    tempBtnTable.Table.ID = order.Table.ID;
                    tempBtnTable.Table.Name = order.Table.Name;
                    tempBtnTable.Table.Status = order.Table.Status;

                    tempBtnTable.CheckInDate = order.CheckInDate;
                    tempBtnTable.CheckOutDate = order.CheckOutDate;
                    tempBtnTable.Discount = order.Discount;
                    tempBtnTable.TotalPrice = order.TotalPrice;

                    if (order.Foods != null)
                    {
                        tempBtnTable.Foods = new TempFoods[order.Foods.Count];

                        int i = 0;
                        foreach (var food in order.Foods)
                        {
                            tempBtnTable.Foods[i] = new TempFoods();
                            tempBtnTable.Foods[i].Count = food.Value;

                            tempBtnTable.Foods[i].TempFood = new TempFood();
                            tempBtnTable.Foods[i].TempFood.ID = food.Key.ID;
                            tempBtnTable.Foods[i].TempFood.Name = food.Key.Name;
                            tempBtnTable.Foods[i].TempFood.CategoryID = food.Key.CategoryID;
                            tempBtnTable.Foods[i].TempFood.Price = food.Key.Price;
                            i++;
                        }
                    }
                }

                tempBtnTables.Add(tempBtnTable);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, tempBtnTables);
            stream.Close();
        }
        #endregion
    }
}
