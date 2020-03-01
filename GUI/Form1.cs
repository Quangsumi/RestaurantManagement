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
    public partial class Form1 : Form
    {
        CategoryDisplayLogic _categoryDisplayLogic = new CategoryDisplayLogic();
        FoodDisplayLogic _foodDisplayLogic = new FoodDisplayLogic();

        public Form1()
        {
            InitializeComponent();
            TransferObjectsToDisplayLogic();
            LoadDataByBLL();
        }

        private void TransferObjectsToDisplayLogic()
        {
            _categoryDisplayLogic.TransferObject(dgvCategories, txtCategoryID, txtCategoryName);
            
            _foodDisplayLogic.TransferObject(dgvFoods, txtFoodID, txtFoodName, cboFoodCategoryID, txtFoodPrice);
        }

        private void LoadDataByBLL()
        {
            _categoryDisplayLogic.LoadCategories();
            _foodDisplayLogic.LoadFoods();
        }

        #region Manager funtions on the Menu
        private void foodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splFoods.BringToFront();
        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splCategory.BringToFront();
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

    }
}
