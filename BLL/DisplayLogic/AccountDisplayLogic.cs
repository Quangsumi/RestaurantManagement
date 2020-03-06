using BLL.DataLogic;
using BLL.Helper;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL.DisplayLogic
{
    public class AccountDisplayLogic : DisplayLogic<tblAccount>
    {
        private DataGridView _dgvAccounts;
        private TextBox _txtAccountID;
        private TextBox _txtAccountUsername;
        private TextBox _txtAccountDisplayName;
        private TextBox _txtAccountPassword;
        private ComboBox _cboAccountType;

        protected override DataLogic<tblAccount> _dataLogic { get; set; } = new AccountDataLogic();

        public AccountDisplayLogic() { }

        public AccountDisplayLogic(DataGridView dgvAccounts, TextBox txtAccountID, TextBox txtAccountUsername, TextBox txtAccountDisplayName, TextBox txtAccountPassword, ComboBox cboAccountType)
        {
            _dgvAccounts = dgvAccounts;
            _txtAccountID = txtAccountID;
            _txtAccountUsername = txtAccountUsername;
            _txtAccountDisplayName = txtAccountDisplayName;
            _txtAccountPassword = txtAccountPassword;
            _cboAccountType = cboAccountType;
        }

        public override void CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewRow row = _dgvAccounts.Rows[e.RowIndex];

            _txtAccountID.Text = row.Cells[0].Value.ToString();
            _txtAccountUsername.Text = row.Cells[1].Value.ToString();
            _txtAccountDisplayName.Text = row.Cells[2].Value.ToString();
            _txtAccountPassword.Text = row.Cells[3].Value.ToString();
            _cboAccountType.Text = row.Cells[4].Value.ToString();
        }

        protected override bool IsInputValid()
             => ValidateInput.IsValidID(_txtAccountID)
            && ValidateInput.IsValidText(_txtAccountUsername)
            && ValidateInput.IsValidText(_txtAccountDisplayName)
            && ValidateInput.IsValidText(_txtAccountPassword);

        public override void ClickAddRecord()
        {
            if (!IsInputValid()) return;

            tblAccount newAcc = Initialize.NewAccount(_txtAccountID, _txtAccountUsername, _txtAccountDisplayName, _txtAccountPassword, _cboAccountType);

            try
            {
                if (_dataLogic.AddRecord(newAcc))
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

        public override void ClickDeleteRecord()
        {
            if (!IsInputValid()) return;

            try
            {
                if (_dataLogic.DeleteRecord(Convert.ToInt32(_txtAccountID.Text)))
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

            tblAccount accToUpdate = Initialize.NewAccount(_txtAccountID, _txtAccountUsername, _txtAccountDisplayName, _txtAccountPassword, _cboAccountType);

            try
            {
                if (_dataLogic.UpdateRecord(accToUpdate))
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
                _dgvAccounts.DataSource = _dataLogic.GetRecords();
                LoadTypeAccToCbo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadTypeAccToCbo()
        {
            _cboAccountType.Items.Add(0);
            _cboAccountType.Items.Add(1);
        }

        public override void ClickClearControlsContent()
        {
            ClearControlsContent();
        }

        protected override void ClearControlsContent()
        {
            _txtAccountID.Text = "0";
            _txtAccountUsername.Text = "";
            _txtAccountDisplayName.Text = "";
            _txtAccountPassword.Text = "";
            _cboAccountType.SelectedIndex = 1;
        }
    }
}
