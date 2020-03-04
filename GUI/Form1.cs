using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.DisplayLogic;
using DAL;

namespace GUI
{
    public partial class frmMain : Form
    {
        CategoryDisplayLogic _categoryDisplayLogic;
        FoodDisplayLogic _foodDisplayLogic;
        TableDisplayLogic _tableDisplayLogic;
        BillDisplayLogic _billDisplayLogic;
        BillInfoDisplayLogic _billInfoDisplayLogic;
        MainFromDisplayLogic _mainFormDisplayLogic;

        public frmMain()
        {
            InitializeComponent();
            TransferObjectsToDisplayLogic();
            LoadDataByBLL();
        }

        private void TransferObjectsToDisplayLogic()
        {
            _categoryDisplayLogic = new CategoryDisplayLogic(dgvCategories, txtCategoryID, txtCategoryName);
            
            _foodDisplayLogic = new FoodDisplayLogic(dgvFoods, txtFoodID, txtFoodName, cboFoodCategoryID, txtFoodCategoryName, txtFoodPrice);
            
            _tableDisplayLogic = new TableDisplayLogic(dgvTables, txtTableID, txtTableName, txtTableStatus);
            
            _billDisplayLogic = new BillDisplayLogic(dgvBills, txtBillID, dtpBillCheckInDate, dtpBillCheckOutDate, cboBillTableID, txtBillTableName, txtBillStatus, txtBillDiscount, txtBillTotalPrice);
            
            _billInfoDisplayLogic = new BillInfoDisplayLogic(dgvBillInfos, txtBillInfoID, cboBillInfoBillID, cboBillInfoFoodID, txtBillInfoFoodName, txtBillInfoCount);

            _mainFormDisplayLogic = new MainFromDisplayLogic(flpMainTables, lvwMainOrders, cboMainCategoryName, cboMainFoodName, txtMainDiscount, txtMainTotalPrice);
        }

        private void LoadDataByBLL()
        {
            _categoryDisplayLogic.LoadRecordsFromDataLogic();
            _foodDisplayLogic.LoadRecordsFromDataLogic();
            _tableDisplayLogic.LoadRecordsFromDataLogic();
            _billDisplayLogic.LoadRecordsFromDataLogic();
            _billInfoDisplayLogic.LoadRecordsFromDataLogic();
            _mainFormDisplayLogic.InitializeData();

        }


        #region Menu
        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splMain.BringToFront();
        }

        private void foodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splFoods.BringToFront();
        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splCategory.BringToFront();
        }

        private void tablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splTables.BringToFront();
        }

        private void billToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splBills.BringToFront();
        }

        private void billInfosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splBillInfos.BringToFront();
        }
        #endregion


        #region Categories
        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _categoryDisplayLogic.CellClick(sender, e);
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            _categoryDisplayLogic.ClickAddRecord();
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            _categoryDisplayLogic.ClickUpdateRecord();
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            _categoryDisplayLogic.ClickDeleteRecord();
        }

        private void btnClearCategory_Click(object sender, EventArgs e)
        {
            _categoryDisplayLogic.ClickClearControlsContent();
        }
        #endregion


        #region Foods

        private void dgvFoods_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _foodDisplayLogic.CellClick(sender, e);
        }

        private void cboFoodCategoryID_SelectedIndexChanged(object sender, EventArgs e)
        {
            _foodDisplayLogic.cboFoodCategoryIDIndexChanged();
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            _foodDisplayLogic.ClickAddRecord();
        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            _foodDisplayLogic.ClickUpdateRecord();
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            _foodDisplayLogic.ClickDeleteRecord();
        }

        private void btnClearFood_Click(object sender, EventArgs e)
        {
            _foodDisplayLogic.ClickClearControlsContent();
        }
        #endregion


        #region Tables
        private void dgvTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _tableDisplayLogic.CellClick(sender, e);
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            _tableDisplayLogic.ClickAddRecord();
        }

        private void btnUpdateTable_Click(object sender, EventArgs e)
        {
            _tableDisplayLogic.ClickUpdateRecord();
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            _tableDisplayLogic.ClickDeleteRecord();
        }

        private void btnClearTable_Click(object sender, EventArgs e)
        {
            _tableDisplayLogic.ClickClearControlsContent();
        }


        #endregion


        #region Bills
        private void dgvBills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _billDisplayLogic.CellClick(sender, e);
        }

        private void cboBillTableID_SelectedIndexChanged(object sender, EventArgs e)
        {
            _billDisplayLogic.cboBillTableIDIndexChanged();
        }

        private void btnAddBill_Click(object sender, EventArgs e)
        {
            _billDisplayLogic.ClickAddRecord();
        }

        private void btnUpdateBill_Click(object sender, EventArgs e)
        {
            _billDisplayLogic.ClickUpdateRecord();
        }

        private void btnDeleteBill_Click(object sender, EventArgs e)
        {
            _billDisplayLogic.ClickDeleteRecord();
        }

        private void btnClearBill_Click(object sender, EventArgs e)
        {
            _billDisplayLogic.ClickClearControlsContent();
        }
        #endregion


        #region BillInfos
        private void dgvBillInfos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _billInfoDisplayLogic.CellClick(sender, e);
        }

        private void cboBillInfoFoodID_SelectedIndexChanged(object sender, EventArgs e)
        {
            _billInfoDisplayLogic.cboBillInfoFoodIDIndexChanged();
        }

        private void btnAddBillInfo_Click(object sender, EventArgs e)
        {
            _billInfoDisplayLogic.ClickAddRecord();
        }

        private void btnUpdateBillInfo_Click(object sender, EventArgs e)
        {
            _billInfoDisplayLogic.ClickUpdateRecord();
        }

        private void btnDeleteBillInfo_Click(object sender, EventArgs e)
        {
            _billInfoDisplayLogic.ClickDeleteRecord();
        }

        private void btnClearBillInfo_Click(object sender, EventArgs e)
        {
            _billInfoDisplayLogic.ClickClearControlsContent();
        }




        #endregion


        #region MainForm
        private void cboMainCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            _mainFormDisplayLogic.cboMainCategoryNameIndexChanged();
        }

        private void btnMainAddFood_Click(object sender, EventArgs e)
        {
            _mainFormDisplayLogic.ClickAddFood();
        }

        private void btnMainRemoveFood_Click(object sender, EventArgs e)
        {
            _mainFormDisplayLogic.ClickClearFood();
        }

        private void btnMainCheckout_Click(object sender, EventArgs e)
        {
            _mainFormDisplayLogic.ClickCheckout();
        }
        #endregion
    }
}
