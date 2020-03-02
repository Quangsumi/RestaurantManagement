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
    public class FoodDisplayLogic
    {
        FoodDataLogic _foodDataLogic = new FoodDataLogic();

        DataGridView _dgvFoods;
        TextBox _txtFoodID;
        TextBox _txtFoodName;
        ComboBox _cboFoodCategoryID;
        TextBox _txtFoodPrice;

        public void TransferObject(DataGridView dgvFoods, TextBox txtFoodID, TextBox txtFoodName, ComboBox cboFoodCategoryID, TextBox txtFoodPrice)
        {
            _dgvFoods = dgvFoods;
            _txtFoodID = txtFoodID;
            _txtFoodName = txtFoodName;
            _cboFoodCategoryID = cboFoodCategoryID;
            _txtFoodPrice = txtFoodPrice;
        }

        public void CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewRow row = _dgvFoods.Rows[e.RowIndex];

            _txtFoodID.Text = row.Cells[0].Value.ToString();
            _txtFoodName.Text = row.Cells[1].Value.ToString();
            _cboFoodCategoryID.Text = row.Cells[2].Value.ToString();
            _txtFoodPrice.Text = row.Cells[3].Value.ToString();
        }

        public void LoadFoodsFromDataAccess()
        {
            try
            {
                _dgvFoods.DataSource = _foodDataLogic.GetFoods();
                LoadCategoryIDToComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadCategoryIDToComboBox()
        {
            // TODO - Make the Food form display Category Name instead of Category ID
            // Display on both datagridview and the combobox

            _cboFoodCategoryID.DataSource = _foodDataLogic.GetCategoriesOfFood();
            _cboFoodCategoryID.DisplayMember = "ID";
            //_cboFoodCategoryID.DisplayMember = "Name";
            //_cboFoodCategoryID.ValueMember = "ID";
        }

        private void ClearTextBox()
        {
            _txtFoodID.Text = "";
            _txtFoodName.Text = "";
            _cboFoodCategoryID.Text = "";
            _txtFoodPrice.Text = "";
        }

        public void ClickAddFood()
        {
            tblFood newFood = new tblFood();
            newFood.Name = _txtFoodName.Text;
            newFood.CategoryID = Convert.ToInt32(_cboFoodCategoryID.Text);
            newFood.Price = Convert.ToDouble(_txtFoodPrice.Text);

            try
            {
                if (_foodDataLogic.AddOneFood(newFood))
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
                LoadFoodsFromDataAccess();
                ClearTextBox();
            }
        }

        public void ClickUpdateFood()
        {
            tblFood foodToUpdate = new tblFood();
            foodToUpdate.ID = Convert.ToInt32(_txtFoodID.Text);
            foodToUpdate.Name = _txtFoodName.Text;
            foodToUpdate.CategoryID = Convert.ToInt32(_cboFoodCategoryID.Text);
            foodToUpdate.Price = Convert.ToDouble(_txtFoodPrice.Text);

            try
            {
                if (_foodDataLogic.UpdateOneFood(foodToUpdate))
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
                LoadFoodsFromDataAccess();
                ClearTextBox();
            }
        }

        public void ClickDeleteFood()
        {
            try
            {
                if (_foodDataLogic.DeleteOneFood(Convert.ToInt32(_txtFoodID.Text)))
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
                LoadFoodsFromDataAccess();
                ClearTextBox();
            }
        }

        public void ClickClearFood()
        {
            ClearTextBox();
        }
    }
}
