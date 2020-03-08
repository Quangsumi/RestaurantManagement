using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL.DataLogic;
using BLL.Helper.Validate;
using BLL.Helper.Initialize;

namespace BLL.DisplayLogic
{
    public class BillDisplayLogic : DisplayLogic<tblBill>
    {
        protected override DataLogic<tblBill> _dataLogic { get; set; } = new BillDataLogic();

        DataGridView _dgvBills;
        TextBox _txtBillID;
        DateTimePicker _dtpCheckInDate;
        DateTimePicker _dtpCheckOutDate;
        ComboBox _cboBillTableID;
        TextBox _txtBillTableName;
        TextBox _txtBillStatus;
        TextBox _txtDiscount;
        TextBox _txtTotalPrice;

        public BillDisplayLogic(){}

        public BillDisplayLogic(DataGridView dgvBills, TextBox txtBillID, DateTimePicker dtpCheckInDate, DateTimePicker dtpCheckOutDate, ComboBox cboBillTableID, TextBox txtBillTableName, TextBox txtBillStatus, TextBox txtDiscount, TextBox txtTotalPrice)
        {
            _txtBillID = txtBillID;
            _dtpCheckInDate = dtpCheckInDate;
            _dtpCheckOutDate = dtpCheckOutDate;
            _cboBillTableID = cboBillTableID;
            _txtBillTableName = txtBillTableName;
            _txtBillStatus = txtBillStatus;
            _txtDiscount = txtDiscount;
            _txtTotalPrice = txtTotalPrice;
            _dgvBills = dgvBills;
        }

        public override void CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewRow row = _dgvBills.Rows[e.RowIndex];

            _txtBillID.Text = row.Cells[0].Value.ToString();
            _dtpCheckInDate.Value = Convert.ToDateTime(row.Cells[1].Value.ToString());
            _dtpCheckOutDate.Value = Convert.ToDateTime(row.Cells[2].Value.ToString());
            _cboBillTableID.Text = row.Cells[3].Value.ToString();
            _txtDiscount.Text = row.Cells[4].Value.ToString();
            _txtTotalPrice.Text = row.Cells[5].Value.ToString();
            _txtBillStatus.Text = row.Cells[6].Value.ToString();
        }

        protected override bool IsInputValid()
            => Validate.IsValidID(_txtBillID) 
            && Validate.IsNumber(_txtTotalPrice) 
            && Validate.IsDigit(_txtDiscount) 
            && Validate.IsOneAndZero(_txtBillStatus);

        public override void ClickAddRecord()
        {
            if (!IsInputValid()) return;

            tblBill newBill = Initialize.NewBill(_txtBillID, _dtpCheckInDate, _dtpCheckOutDate, _cboBillTableID, _txtBillStatus, _txtDiscount, _txtTotalPrice);

            try
            {
                if (_dataLogic.AddRecord(newBill))
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

        public override void LoadRecordsFromDataLogic()
        {
            try
            {
                _dgvBills.DataSource = _dataLogic.GetRecords();
                LoadTableIDToComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadTableIDToComboBox()
        {
            _cboBillTableID.DataSource = (_dataLogic as BillDataLogic).GetTablesOfBill();
            _cboBillTableID.DisplayMember = "ID";
        }

        protected override void ClearControlsContent()
        {
            _txtBillID.Text = "0";
            _dtpCheckInDate.Value = DateTime.Now;
            _dtpCheckOutDate.Value = DateTime.Now;
            _cboBillTableID.Text = "";
            _txtBillStatus.Text = "";
            _txtDiscount.Text = "";
            _txtTotalPrice.Text = "";
        }

        public override void ClickDeleteRecord()
        {
            if (!IsInputValid()) return;

            try
            {
                if (_dataLogic.DeleteRecord(Convert.ToInt32(_txtBillID.Text)))
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
            if (!IsInputValid()) return;

            tblBill billToUpdate = Initialize.NewBill(_txtBillID, _dtpCheckInDate, _dtpCheckOutDate, _cboBillTableID, _txtBillStatus, _txtDiscount, _txtTotalPrice);

            try
            {
                if (_dataLogic.UpdateRecord(billToUpdate))
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

        public override void ClickClearControlsContent()
        {
            ClearControlsContent();
        }

        public void cboBillTableIDIndexChanged()
        {
            _txtBillTableName.Text = GetTableNameByTableID(_cboBillTableID.Text) ?? "NULL";
        }

        private string GetTableNameByTableID(string tableID)
        {
            try
            {
                return (_dataLogic as BillDataLogic).GetTableNameByTableID(tableID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "ERROR!!!";
            }
        }
    }
}
