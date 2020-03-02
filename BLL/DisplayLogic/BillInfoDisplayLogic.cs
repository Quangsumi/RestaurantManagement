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
    public class BillInfoDisplayLogic
    {
        BillInfoDataLogic _billInfoDataLogic = new BillInfoDataLogic();

        DataGridView _dgvBillInfos;
        TextBox _txtBillInfoID;
        ComboBox _cboBillInfoBillID;
        ComboBox _cboBillInfoFoodID;
        TextBox _txtCount;

        public void TransferObject(DataGridView dgvBillInfos, TextBox txtBillInfoID, ComboBox cboBillInfoBillID, ComboBox cboBillInfoFoodID, TextBox txtCount)
        {
            _txtBillInfoID = txtBillInfoID;
            _cboBillInfoBillID = cboBillInfoBillID;
            _cboBillInfoFoodID = cboBillInfoFoodID;
            _txtCount = txtCount;
            _dgvBillInfos = dgvBillInfos;
        }

        public void CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewRow row = _dgvBillInfos.Rows[e.RowIndex];

            _txtBillInfoID.Text = row.Cells[0].Value.ToString();
            _cboBillInfoBillID.Text = row.Cells[1].Value.ToString();
            _cboBillInfoFoodID.Text = row.Cells[2].Value.ToString();
            _txtCount.Text = row.Cells[3].Value.ToString();
        }

        public void LoadBillInfosFromDataAccess()
        {
            try
            {
                _dgvBillInfos.DataSource = _billInfoDataLogic.GetBillInfos();
                LoadBillsIDToComboBox();
                LoadFoodsIDToComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearTextBox()
        {
            _txtBillInfoID.Text = "";
            _cboBillInfoBillID.Text = "";
            _cboBillInfoFoodID.Text = "";
            _txtCount.Text = "";
        }

        private void LoadBillsIDToComboBox()
        {
            _cboBillInfoBillID.DataSource = _billInfoDataLogic.GetBillsOfBillInfo();
            _cboBillInfoBillID.DisplayMember = "ID";
        }

        private void LoadFoodsIDToComboBox()
        {
            // TODO - Make the BillInfo form display Food Name instead of Food ID
            // Display on both datagridview and the combobox

            _cboBillInfoFoodID.DataSource = _billInfoDataLogic.GetFoodsOfBillInfo();
            _cboBillInfoFoodID.DisplayMember = "ID";
            //_cboBillInfoFoodID.DisplayMember = "Name";
            //_cboBillInfoFoodID.ValueMember = "ID";
        }

        public void ClickAddBillInfo()
        {
            tblBillInfo newBillInfo = new tblBillInfo();
            newBillInfo.BillID = Convert.ToInt32(_cboBillInfoBillID.Text);
            newBillInfo.FoodID = Convert.ToInt32(_cboBillInfoFoodID.Text);
            newBillInfo.Count = Convert.ToInt32(_txtCount.Text);

            try
            {
                if (_billInfoDataLogic.AddOneBillInfo(newBillInfo))
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
                LoadBillInfosFromDataAccess();
                ClearTextBox();
            }
        }

        public void ClickUpdateBillInfo()
        {
            tblBillInfo billInfoToUpdate = new tblBillInfo();
            billInfoToUpdate.ID = Convert.ToInt32(_txtBillInfoID.Text);
            billInfoToUpdate.BillID = Convert.ToInt32(_cboBillInfoBillID.Text);
            billInfoToUpdate.FoodID = Convert.ToInt32(_cboBillInfoFoodID.Text);
            billInfoToUpdate.Count = Convert.ToInt32(_txtCount.Text);

            try
            {
                if (_billInfoDataLogic.UpdateOneBillInfo(billInfoToUpdate))
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
                LoadBillInfosFromDataAccess();
                ClearTextBox();
            }
        }

        public void ClickDeleteBillInfo()
        {
            try
            {
                if (_billInfoDataLogic.DeleteOneBillInfo(Convert.ToInt32(_txtBillInfoID.Text)))
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
                LoadBillInfosFromDataAccess();
                ClearTextBox();
            }
        }


        public void ClickClearBillInfo()
        {
            ClearTextBox();
        }
    }
}
