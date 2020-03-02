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
        CategoryDisplayLogic _categoryDisplayLogic = new CategoryDisplayLogic();
        FoodDisplayLogic _foodDisplayLogic = new FoodDisplayLogic();
        TableDisplayLogic _tableDisplayLogic = new TableDisplayLogic();
        BillDisplayLogic _billDisplayLogic = new BillDisplayLogic();
        BillInfoDisplayLogic _billInfoDisplayLogic = new BillInfoDisplayLogic();

        public frmMain()
        {
            InitializeComponent();
            TransferObjectsToDisplayLogic();
            LoadDataByBLL();
        }

        private void TransferObjectsToDisplayLogic()
        {
            _categoryDisplayLogic.TransferObject(dgvCategories, txtCategoryID, txtCategoryName);
            
            _foodDisplayLogic.TransferObject(dgvFoods, txtFoodID, txtFoodName, cboFoodCategoryID, txtFoodPrice);

            _tableDisplayLogic.TransferObject(dgvTables, txtTableID, txtTableName, txtTableStatus);

            _billDisplayLogic.TransferObject(dgvBills, txtBillID, dtpBillCheckInDate, dtpBillCheckOutDate, cboBillTableID, txtBillStatus, txtBillDiscount, txtBillTotalPrice);

            _billInfoDisplayLogic.TransferObject(dgvBillInfos, txtBillInfoID, cboBillInfoBillID, cboBillInfoFoodID, txtBillInfoCount);
        }

        private void LoadDataByBLL()
        {
            _categoryDisplayLogic.LoadCategoriesFromDataAccess();
            _foodDisplayLogic.LoadFoodsFromDataAccess();
            _tableDisplayLogic.LoadTablesFromDataAccess();
            _billDisplayLogic.LoadBillsFromDataAccess();
            _billInfoDisplayLogic.LoadBillInfosFromDataAccess();
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
            _categoryDisplayLogic.ClickAddCategory();
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            _categoryDisplayLogic.ClickUpdateCategory();
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            _categoryDisplayLogic.ClickDeleteCategory();
        }

        private void btnClearCategory_Click(object sender, EventArgs e)
        {
            _categoryDisplayLogic.ClickClearCategory();
        }
        #endregion


        #region Foods

        private void dgvFoods_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _foodDisplayLogic.CellClick(sender, e);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            _foodDisplayLogic.ClickAddFood();
        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            _foodDisplayLogic.ClickUpdateFood();
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            _foodDisplayLogic.ClickDeleteFood();
        }

        private void btnClearFood_Click(object sender, EventArgs e)
        {
            _foodDisplayLogic.ClickClearFood();
        }
        #endregion


        #region Tables
        private void dgvTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _tableDisplayLogic.CellClick(sender, e);
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            _tableDisplayLogic.ClickAddTable();
        }

        private void btnUpdateTable_Click(object sender, EventArgs e)
        {
            _tableDisplayLogic.ClickUpdateTable();
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            _tableDisplayLogic.ClickDeleteTable();
        }

        private void btnClearTable_Click(object sender, EventArgs e)
        {
            _tableDisplayLogic.ClickClearTable();
        }


        #endregion


        #region Bills
        private void dgvBills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _billDisplayLogic.CellClick(sender, e);
        }

        private void btnAddBill_Click(object sender, EventArgs e)
        {
            _billDisplayLogic.ClickAddBill();
        }

        private void btnUpdateBill_Click(object sender, EventArgs e)
        {
            _billDisplayLogic.ClickUpdateBill();
        }

        private void btnDeleteBill_Click(object sender, EventArgs e)
        {
            _billDisplayLogic.ClickDeleteBill();
        }

        private void btnClearBill_Click(object sender, EventArgs e)
        {
            _billDisplayLogic.ClickClearBill();
        }
        #endregion


        #region BillInfos
        private void dgvBillInfos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _billInfoDisplayLogic.CellClick(sender, e);
        }

        private void btnAddBillInfo_Click(object sender, EventArgs e)
        {
            _billInfoDisplayLogic.ClickAddBillInfo();
        }

        private void btnUpdateBillInfo_Click(object sender, EventArgs e)
        {
            _billInfoDisplayLogic.ClickUpdateBillInfo();
        }

        private void btnDeleteBillInfo_Click(object sender, EventArgs e)
        {
            _billInfoDisplayLogic.ClickDeleteBillInfo();
        }

        private void btnClearBillInfo_Click(object sender, EventArgs e)
        {
            _billInfoDisplayLogic.ClickClearBillInfo();
        }
        #endregion


    }
}
