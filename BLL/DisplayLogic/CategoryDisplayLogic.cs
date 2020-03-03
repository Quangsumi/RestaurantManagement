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
    public class CategoryDisplayLogic : DisplayLogic<tblCategory>
    {
        protected override DataLogic<tblCategory> _dataLogic { get; set; } = new CategoryDataLogic();

        DataGridView _dgvCategories;
        TextBox _txtCategoryID;
        TextBox _txtCategoryName;

        public CategoryDisplayLogic() { }

        public CategoryDisplayLogic(DataGridView dgvCategories, TextBox txtCategoryID, TextBox txtCategoryName)
        {
            _txtCategoryID = txtCategoryID;
            _txtCategoryName = txtCategoryName;
            _dgvCategories = dgvCategories;
        }

        public override void CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewRow row = _dgvCategories.Rows[e.RowIndex];

            _txtCategoryID.Text = row.Cells[0].Value.ToString();
            _txtCategoryName.Text = row.Cells[1].Value.ToString();
        }

        public override void ClickAddRecord()
        {
            tblCategory newCategory = new tblCategory();
            newCategory.Name = _txtCategoryName.Text;

            try
            {
                if (_dataLogic.AddRecord(newCategory))
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
                LoadRecordsFromDataLogic();
                ClearControlsContent();
            }
        }

        public override void ClickClearControlsContent()
        {
            ClearControlsContent();
        }

        public override void ClickDeleteRecord()
        {
            try
            {
                if (_dataLogic.DeleteRecord(Convert.ToInt32(_txtCategoryID.Text)))
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
                LoadRecordsFromDataLogic();
                ClearControlsContent();
            }
        }

        public override void ClickUpdateRecord()
        {
            tblCategory categoryToUpdate = new tblCategory();
            categoryToUpdate.ID = Convert.ToInt32(_txtCategoryID.Text);
            categoryToUpdate.Name = _txtCategoryName.Text;

            try
            {
                if (_dataLogic.UpdateRecord(categoryToUpdate))
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
                LoadRecordsFromDataLogic();
                ClearControlsContent();
            }
        }

        public override void LoadRecordsFromDataLogic()
        {
            try
            {
                _dgvCategories.DataSource = _dataLogic.GetRecords();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override void ClearControlsContent()
        {
            _txtCategoryID.Text = "";
            _txtCategoryName.Text = "";
        }
    }
}
