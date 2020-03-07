using BLL.DataLogic;
using BLL.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL.DisplayLogic
{
    public class ToolDisplayLogic
    {
        BillDataLogic _billDataLogic = new BillDataLogic();
        BillInfoDataLogic _billInfoDataLogic = new BillInfoDataLogic();
        CategoryDataLogic _categoryDataLogic = new CategoryDataLogic();
        FoodDataLogic _foodDataLogic = new FoodDataLogic();
        TableDataLogic _tableDataLogic = new TableDataLogic();

        public void ClickShowReceipts(){
            if (Directory.Exists(Tools.receiptsDirPath))
                Process.Start(Tools.receiptsDirPath);
            else
                MessageBox.Show("Folder don't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public async void ClickExportToExcel()
        {
            await Tools.ExportToExcel(
                    _billDataLogic.GetRecords(),
                    _billInfoDataLogic.GetRecords(),
                    _categoryDataLogic.GetRecords(),
                    _foodDataLogic.GetRecords(),
                    _tableDataLogic.GetRecords()
                    );

            if (Directory.Exists(Tools.excelFileDirPath))
                Process.Start(Tools.excelFileDirPath);
            else
                MessageBox.Show("Folder don't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
