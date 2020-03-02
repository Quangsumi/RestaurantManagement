using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL.DataLogic;

namespace BLL.DisplayLogic
{
    public class CategoryDisplayLogic
    {
        CategoryDataLogic _categoryDataLogic = new CategoryDataLogic();
        
        DataGridView _dgvCategories;
        TextBox _txtCategoryID;
        TextBox _txtCategoryName;

        public void TransferObject(DataGridView dgvCategories, TextBox txtCategoryID, TextBox txtCategoryName)
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

        public void LoadCategoriesFromDataAccess()
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

        private void ClearTextBox()
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
                    MessageBox.Show("Added Successfully!");
                else
                    MessageBox.Show("Failed to add!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadCategoriesFromDataAccess();
                ClearTextBox();
            }
        }
        
        public void ClickUpdateCategory()
        {
            tblCategory categoryToUpdate = new tblCategory();
            categoryToUpdate.ID = Convert.ToInt32(_txtCategoryID.Text);
            categoryToUpdate.Name = _txtCategoryName.Text;

            try
            {
                if (_categoryDataLogic.UpdateOneCategory(categoryToUpdate))
                    MessageBox.Show("Updated successfully!");
                else
                    MessageBox.Show("Failed to update!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadCategoriesFromDataAccess();
                ClearTextBox();
            }
        }

        public void ClickDeleteCategory()
        {
            try
            {
                if (_categoryDataLogic.DeleteOneCategory(Convert.ToInt32(_txtCategoryID.Text)))
                    MessageBox.Show("Deleted successfully!");
                else
                    MessageBox.Show("Failed to delted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadCategoriesFromDataAccess();
                ClearTextBox();
            }
        }


        public void ClickClearCategory()
        {
            ClearTextBox();
        }
    }
}
