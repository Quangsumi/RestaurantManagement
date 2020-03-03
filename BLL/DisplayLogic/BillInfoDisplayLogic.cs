using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.DataLogic;
using DAL;

namespace BLL.DisplayLogic
{
    public class BillInfoDisplayLogic : DisplayLogic<tblBillInfo>
    {
        protected override DataLogic<tblBillInfo> _dataLogic { get; set; } = new BillInfoDataLogic();

        DataGridView _dgvBillInfos;
        TextBox _txtBillInfoID;
        ComboBox _cboBillInfoBillID;
        ComboBox _cboBillInfoFoodID;
        TextBox _txtBillInfoFoodName;
        TextBox _txtCount;

        public BillInfoDisplayLogic(){}

        public BillInfoDisplayLogic(DataGridView dgvBillInfos, TextBox txtBillInfoID, ComboBox cboBillInfoBillID, ComboBox cboBillInfoFoodID, TextBox txtBillInfoFoodName, TextBox txtCount)
        {
            _txtBillInfoID = txtBillInfoID;
            _cboBillInfoBillID = cboBillInfoBillID;
            _cboBillInfoFoodID = cboBillInfoFoodID;
            _txtBillInfoFoodName = txtBillInfoFoodName;
            _txtCount = txtCount;
            _dgvBillInfos = dgvBillInfos;
        }

        public override void CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewRow row = _dgvBillInfos.Rows[e.RowIndex];

            _txtBillInfoID.Text = row.Cells[0].Value.ToString();
            _cboBillInfoBillID.Text = row.Cells[1].Value.ToString();
            _cboBillInfoFoodID.Text = row.Cells[2].Value.ToString();
            _txtCount.Text = row.Cells[3].Value.ToString();
        }

        public void cboBillInfoFoodIDIndexChanged()
        {
            _txtBillInfoFoodName.Text = GetFoodNameByFoodID(_cboBillInfoFoodID.Text) ?? "NULL";
        }

        private string GetFoodNameByFoodID(string foodID)
        {
            try
            {
                return (_dataLogic as BillInfoDataLogic).GetFoodNameByFoodID(foodID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "ERROR!!!";
            }
        }

        public override void ClickAddRecord()
        {
            tblBillInfo newBillInfo = new tblBillInfo();
            newBillInfo.BillID = Convert.ToInt32(_cboBillInfoBillID.Text);
            newBillInfo.FoodID = Convert.ToInt32(_cboBillInfoFoodID.Text);
            newBillInfo.Count = Convert.ToInt32(_txtCount.Text);

            try
            {
                if (_dataLogic.AddRecord(newBillInfo))
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
                if (_dataLogic.DeleteRecord(Convert.ToInt32(_txtBillInfoID.Text)))
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
            tblBillInfo billInfoToUpdate = new tblBillInfo();
            billInfoToUpdate.ID = Convert.ToInt32(_txtBillInfoID.Text);
            billInfoToUpdate.BillID = Convert.ToInt32(_cboBillInfoBillID.Text);
            billInfoToUpdate.FoodID = Convert.ToInt32(_cboBillInfoFoodID.Text);
            billInfoToUpdate.Count = Convert.ToInt32(_txtCount.Text);

            try
            {
                if (_dataLogic.UpdateRecord(billInfoToUpdate))
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
                _dgvBillInfos.DataSource = _dataLogic.GetRecords();
                LoadBillsIDToComboBox();
                LoadFoodsIDToComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadBillsIDToComboBox()
        {
            _cboBillInfoBillID.DataSource = (_dataLogic as BillInfoDataLogic).GetBillsOfBillInfo();
            _cboBillInfoBillID.DisplayMember = "ID";
        }

        private void LoadFoodsIDToComboBox()
        {
            _cboBillInfoFoodID.DataSource = (_dataLogic as BillInfoDataLogic).GetFoodsOfBillInfo();
            _cboBillInfoFoodID.DisplayMember = "ID";
        }

        protected override void ClearControlsContent()
        {
            _txtBillInfoID.Text = "";
            _cboBillInfoBillID.Text = "";
            _cboBillInfoFoodID.Text = "";
            _txtCount.Text = "";
        }
    }
}
