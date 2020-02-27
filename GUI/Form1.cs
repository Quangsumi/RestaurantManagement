using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;

namespace GUI
{
    public partial class Form1 : Form
    {
        CategoryDisplayLogic _categoryDisplayLogic = new CategoryDisplayLogic();

        public Form1()
        {
            InitializeComponent();
            TransferObjectsToDisplayLogic();
            LoadDataByBLL();
        }

        private void TransferObjectsToDisplayLogic()
        {
            _categoryDisplayLogic.TransferObject(txtCategoryID, txtCategoryName, dgvCategories);
        }

        private void LoadDataByBLL()
        {
            _categoryDisplayLogic.LoadCategories();
        }

        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _categoryDisplayLogic.CellClick(sender, e);
        }

        private void btnClearCategory_Click(object sender, EventArgs e)
        {
            _categoryDisplayLogic.ClearTextBox();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            _categoryDisplayLogic.ClickAddCategory();
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            _categoryDisplayLogic.ClickDeleteCategory();
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            _categoryDisplayLogic.ClickUpdateCategory();
        }
    }
}
