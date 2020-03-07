﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using DAL;
using DTO;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace BLL.Helper
{
    public static class Tools
    {
        public static readonly string receiptsDirPath = Directory.GetCurrentDirectory() + "\\Bills\\";
        public static readonly string excelFileDirPath = Directory.GetCurrentDirectory() + "\\Excel Files\\";
        static DataSet _dataSet = new DataSet();

        public static void SaveCheckoutInfoToFile(Order checkoutOrder, int lastBillID, TextBox txtMainDiscount)
        {
            string content = GetCheckoutContent(checkoutOrder, lastBillID, txtMainDiscount);
            string path = GetNewTextPath(lastBillID);

            SaveToFile(path, content);
        }

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
                content += "\r\n + " + food.Key.Name + " - x" + food.Value;
            }
            checkoutOrder.Discount = Int32.Parse(txtMainDiscount.Text);
            content += "\r\n\n5/ Discount: " + checkoutOrder.Discount + "%";
            content += "\r\n6/ Totol Price: $" + checkoutOrder.TotalPrice;
            content += "\r\n7/ Status: Checkouted";

            return content;
        }

        private static string GetNewTextPath(int lastBillID)
            => receiptsDirPath + "#" + lastBillID.ToString() + ".txt";

        private static string GetNewExcelPath(string name)
            => excelFileDirPath + "#" + name + ".xlsx";

        public static async Task ExportToExcel(List<tblBill> bills, List<tblBillInfo> billInfos, List<tblCategory> categories, List<tblFood> foods, List<tblTable> tables)
        {
            await Task.Run(() =>
            {
                string path = GetNewExcelPath("Excel Data");
                try
                {
                    DataTable billDataTable = ConvertToDataTable<tblBill>(bills);
                    DataTable billInfoDataTable = ConvertToDataTable<tblBillInfo>(billInfos);
                    DataTable categoryDataTable = ConvertToDataTable<tblCategory>(categories);
                    DataTable foodDataTable = ConvertToDataTable<tblFood>(foods);
                    DataTable tableDataTable = ConvertToDataTable<tblTable>(tables);

                    _dataSet.Tables.Add(billDataTable);
                    _dataSet.Tables.Add(billInfoDataTable);
                    _dataSet.Tables.Add(categoryDataTable);
                    _dataSet.Tables.Add(foodDataTable);
                    _dataSet.Tables.Add(tableDataTable);

                    GenerateExcel(path);
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }

        static DataTable ConvertToDataTable<T>(List<T> models)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            // Get all the properties
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Loop through all the properties
            // Adding column to our db
            foreach (PropertyInfo prop in props)
            {
                // Setting column name as property name
                dataTable.Columns.Add(prop.Name);
            }

            // Add row
            foreach (T item in models)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    // inserting property values to db rows
                    values[i] = props[i].GetValue(item, null);
                }
                // Finally add value to db
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static void GenerateExcel(string path)
        {
            // create a excel app along side with workbook and worksheet and give a name to it
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWorkBook = excelApp.Workbooks.Add();
            Excel._Worksheet xlWorksheet = excelWorkBook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            
            foreach (DataTable table in _dataSet.Tables)
            {
                //Add a new worksheet to workbook with the Datatable name
                Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = table.TableName;
                // add all the columns
                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }
                // add all the rows
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }
            // excelWorkBook.Save(); -> this will save to its default location c:/users/{user-name}/documents/
            excelWorkBook.SaveAs(path); // -> this will do the custom
            excelWorkBook.Close();
            excelApp.Quit();
        }
    }
}