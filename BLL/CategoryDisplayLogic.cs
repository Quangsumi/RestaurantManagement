using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace BLL
{
    public class CategoryDisplayLogic
    {
        CategoryDataLogic _categoryDataLogic = new CategoryDataLogic();
        
        DataGridView _dgvCategories;
        TextBox _txtCategoryID;
        TextBox _txtCategoryName;

        public void TransferObject(TextBox txtCategoryID, TextBox txtCategoryName, DataGridView dgvCategories)
        {
            _txtCategoryID = txtCategoryID;
            _txtCategoryName = txtCategoryName;
            _dgvCategories = dgvCategories;
        }

        public void CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewRow row = _dgvCategories.Rows[e.RowIndex];

            _txtCategoryID.Text = row.Cells[0].Value.ToString();
            _txtCategoryName.Text = row.Cells[1].Value.ToString();
        }

        public void LoadCategories()
        {
            try
            {
                _dgvCategories.DataSource = _categoryDataLogic.GetCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ClearTextBox()
        {
            _txtCategoryID.Text = "";
            _txtCategoryName.Text = "";
        }

        public void ClickAddCategory()
        {
            tblCategory newCategory = new tblCategory();
            newCategory.Name = _txtCategoryName.Text;

            try
            {
                if (_categoryDataLogic.AddOneCategory(newCategory))
                    MessageBox.Show("Add Successfully!");
                else
                    MessageBox.Show("Add Failed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadCategories();
                ClearTextBox();
            }
        }

        public void ClickDeleteCategory()
        {
            try
            {
                if (_categoryDataLogic.DeleteOneCategory(Convert.ToInt32(_txtCategoryID.Text)))
                    MessageBox.Show("Add Successfully!");
                else
                    MessageBox.Show("Add Failed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadCategories();
                ClearTextBox();
            }
        }

        public void ClickUpdateCategory()
        {
            tblCategory newCategory = new tblCategory();
            newCategory.ID = Convert.ToInt32(_txtCategoryID.Text);
            newCategory.Name = _txtCategoryName.Text;

            try
            {
                if (_categoryDataLogic.UpdateACategory(newCategory))
                    MessageBox.Show("Add Successfully!");
                else
                    MessageBox.Show("Add Failed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadCategories();
                ClearTextBox();
            }
        }
    }
}
