using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL.DataLogic;
using BLL.Helper;

namespace BLL.DisplayLogic
{
    public class FoodDisplayLogic : DisplayLogic<tblFood>
    {
        protected override DataLogic<tblFood> _dataLogic { get; set; } = new FoodDataLogic();

        DataGridView _dgvFoods;
        TextBox _txtFoodID;
        TextBox _txtFoodName;
        ComboBox _cboFoodCategoryID;
        TextBox _txtFoodCategoryName;
        TextBox _txtFoodPrice;

        public FoodDisplayLogic() { }

        public FoodDisplayLogic(DataGridView dgvFoods, TextBox txtFoodID, TextBox txtFoodName, ComboBox cboFoodCategoryID, TextBox txtCategoryName, TextBox txtFoodPrice)
        {
            _dgvFoods = dgvFoods;
            _txtFoodID = txtFoodID;
            _txtFoodName = txtFoodName;
            _cboFoodCategoryID = cboFoodCategoryID;
            _txtFoodCategoryName = txtCategoryName;
            _txtFoodPrice = txtFoodPrice;
        }

        public override void CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewRow row = _dgvFoods.Rows[e.RowIndex];

            _txtFoodID.Text = row.Cells[0].Value.ToString();
            _txtFoodName.Text = row.Cells[1].Value.ToString();
            _cboFoodCategoryID.Text = row.Cells[2].Value.ToString();
            _txtFoodPrice.Text = row.Cells[3].Value.ToString();
        }

        protected override bool IsInputValid()
            => Validate.IsValidID(_txtFoodID)
            && Validate.IsValidText(_txtFoodName) 
            && Validate.IsNumber(_txtFoodPrice);

        public override void ClickAddRecord()
        {
            if (!IsInputValid()) return;

            tblFood newFood = new tblFood();
            newFood.Name = _txtFoodName.Text;
            newFood.CategoryID = Convert.ToInt32(_cboFoodCategoryID.Text);
            newFood.Price = Convert.ToDouble(_txtFoodPrice.Text);

            try
            {
                if (_dataLogic.AddRecord(newFood))
                    MessageBox.Show("Added successfully!");
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

        public override void LoadRecordsFromDataLogic()
        {
            try
            {
                _dgvFoods.DataSource = _dataLogic.GetRecords();
                LoadCategoryIDToComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadCategoryIDToComboBox()
        {
            _cboFoodCategoryID.DataSource = (_dataLogic as FoodDataLogic).GetCategoriesOfFood();
            _cboFoodCategoryID.DisplayMember = "ID";
        }

        protected override void ClearControlsContent()
        {
            _txtFoodID.Text = "";
            _txtFoodName.Text = "";
            _cboFoodCategoryID.Text = "";
            _txtFoodPrice.Text = "";
        }

        public override void ClickDeleteRecord()
        {
            if (!IsInputValid()) return;

            try
            {
                if (_dataLogic.DeleteRecord(Convert.ToInt32(_txtFoodID.Text)))
                    MessageBox.Show("Added successfully!");
                else
                    MessageBox.Show("Failed to delete!");
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
            if (!IsInputValid()) return;

            tblFood foodToUpdate = new tblFood();
            foodToUpdate.ID = Convert.ToInt32(_txtFoodID.Text);
            foodToUpdate.Name = _txtFoodName.Text;
            foodToUpdate.CategoryID = Convert.ToInt32(_cboFoodCategoryID.Text);
            foodToUpdate.Price = Convert.ToDouble(_txtFoodPrice.Text);

            try
            {
                if (_dataLogic.UpdateRecord(foodToUpdate))
                    MessageBox.Show("Updated successfully!");
                else
                    MessageBox.Show("Failed to updated!");
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

        public void cboFoodCategoryIDIndexChanged()
        {
            _txtFoodCategoryName.Text = GetCatagoryNameByCategoryID(_cboFoodCategoryID.Text) ?? "NULL";
        }

        private string GetCatagoryNameByCategoryID(string catagoryID)
        {
            try
            {
                return (_dataLogic as FoodDataLogic).GetCatagoryNameByCategoryID(catagoryID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "ERROR!!!";
            }
        }
    }
}
