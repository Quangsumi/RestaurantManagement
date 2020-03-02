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
    public class BillDisplayLogic
    {
        BillDataLogic _billDataLogic = new BillDataLogic();

        DataGridView _dgvBills;
        TextBox _txtBillID;
        DateTimePicker _dtpCheckInDate;
        DateTimePicker _dtpCheckOutDate;
        ComboBox _cboBillTableID;
        TextBox _txtBillStatus;
        TextBox _txtDiscount;
        TextBox _txtTotalPrice;

        public void TransferObject(DataGridView dgvBills, TextBox txtBillID, DateTimePicker dtpCheckInDate, DateTimePicker dtpCheckOutDate, ComboBox cboBillTableID, TextBox txtBillStatus, TextBox txtDiscount, TextBox txtTotalPrice)
        {
            _txtBillID = txtBillID;
            _dtpCheckInDate = dtpCheckInDate;
            _dtpCheckOutDate = dtpCheckOutDate;
            _cboBillTableID = cboBillTableID;
            _txtBillStatus = txtBillStatus;
            _txtDiscount = txtDiscount;
            _txtTotalPrice = txtTotalPrice;
            _dgvBills = dgvBills;
        }

        public void CellClick(object sender, DataGridViewCellEventArgs e)
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

        public void LoadBillsFromDataAccess()
        {
            try
            {
                _dgvBills.DataSource = _billDataLogic.GetBills();
                LoadTableIDToComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadTableIDToComboBox()
        {
            // TODO - Make the Bill form display Table Name instead of Table ID
            // Display on both datagridview and the combobox

            _cboBillTableID.DataSource = _billDataLogic.GetTablesOfBill();
            _cboBillTableID.DisplayMember = "ID";
            //_cboBillTableID.DisplayMember = "Name";
            //_cboBillTableID.ValueMember = "ID";
        }

        private void ClearTextBox()
        {
            _txtBillID.Text = "";
            _dtpCheckInDate.Value = DateTime.Now;
            _dtpCheckOutDate.Value = DateTime.Now;
            _cboBillTableID.Text = "";
            _txtBillStatus.Text = "";
            _txtDiscount.Text = "";
            _txtTotalPrice.Text = "";
        }

        public void ClickAddBill()
        {
            tblBill newBill = new tblBill();
            newBill.CheckInDate = _dtpCheckInDate.Value;
            newBill.CheckOutDate = _dtpCheckOutDate.Value;
            newBill.TableID = Convert.ToInt32(_cboBillTableID.Text);
            newBill.Status = Convert.ToInt32(_txtBillStatus.Text);
            newBill.Discount = Convert.ToInt32(_txtDiscount.Text);
            newBill.TotalPrice = Convert.ToDouble(_txtTotalPrice.Text);

            try
            {
                if (_billDataLogic.AddOneBill(newBill))
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
                LoadBillsFromDataAccess();
                ClearTextBox();
            }
        }

        public void ClickUpdateBill()
        {
            tblBill billToUpdate = new tblBill();
            billToUpdate.ID = Convert.ToInt32(_txtBillID.Text);
            billToUpdate.CheckInDate = _dtpCheckInDate.Value;
            billToUpdate.CheckOutDate = _dtpCheckOutDate.Value;
            billToUpdate.TableID = Convert.ToInt32(_cboBillTableID.Text);
            billToUpdate.Status = Convert.ToInt32(_txtBillStatus.Text);
            billToUpdate.Discount = Convert.ToInt32(_txtDiscount.Text);
            billToUpdate.TotalPrice = Convert.ToDouble(_txtTotalPrice.Text);

            try
            {
                if (_billDataLogic.UpdateOneBill(billToUpdate))
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
                LoadBillsFromDataAccess();
                ClearTextBox();
            }
        }

        public void ClickDeleteBill()
        {
            try
            {
                if (_billDataLogic.DeleteOneBill(Convert.ToInt32(_txtBillID.Text)))
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
                LoadBillsFromDataAccess();
                ClearTextBox();
            }
        }

        public void ClickClearBill()
        {
            ClearTextBox();
        }
    }
}
