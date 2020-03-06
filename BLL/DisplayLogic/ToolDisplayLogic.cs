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

        public void ClickShowReceipts(){
            if (Directory.Exists(Tools.receiptsDirPath))
                Process.Start(Tools.receiptsDirPath);
            else
                MessageBox.Show("Folder don't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ClickExtractToExcelAllTimes()
        {
            Tools.ExportBillToExcelAllTime(_billDataLogic.GetRecords());
        }

        public void ClickExtractToExcelThisMonth()
        {
            //Tools.ClickExtractToExcelThisMonth(_billDataLogic.GetRecordsByDate())
        }
    }
}
