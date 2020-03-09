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
using DTO;

namespace BLL.DisplayLogic
{
    public class TableDisplayLogic : DisplayLogic<tblTable>
    {
        protected override DataLogic<tblTable> _dataLogic { get; set; } = new TableDataLogic();

        DataGridView _dgvTables;
        TextBox _txtTableID;
        TextBox _txtTableName;
        TextBox _txtTableStatus;

        public TableDisplayLogic() { }

        public TableDisplayLogic(DataGridView dgvTables, TextBox txtTableID, TextBox txtTableName, TextBox txtTableStatus)
        {
            _txtTableID = txtTableID;
            _txtTableName = txtTableName;
            _txtTableStatus = txtTableStatus;
            _dgvTables = dgvTables;
        }

        public override void CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewRow row = _dgvTables.Rows[e.RowIndex];

            _txtTableID.Text = row.Cells[0].Value.ToString();
            _txtTableName.Text = row.Cells[1].Value.ToString();
            _txtTableStatus.Text = row.Cells[2].Value.ToString();
        }

        protected override bool IsInputValid()
            => Validate.IsValidID(_txtTableID)
            && Validate.IsOneAndZero(_txtTableStatus)
            && Validate.IsValidText(_txtTableName);

        public override void ClickAddRecord()
        {
            if (!IsInputValid()) return;

            tblTable newTable = new tblTable();
            newTable.Name = _txtTableName.Text;
            newTable.Status = Convert.ToInt32(_txtTableStatus.Text);

            try
            {
                if (_dataLogic.AddRecord(newTable))
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
                _dgvTables.DataSource = _dataLogic.GetRecords();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override void ClearControlsContent()
        {
            _txtTableID.Text = "0";
            _txtTableName.Text = "";
            _txtTableStatus.Text = "";
        }

        public override void ClickDeleteRecord()
        {
            if (!IsInputValid() 
                || !Validate.IsTableValidToDelte(Convert.ToInt32(_txtTableID.Text))) return;

            try
            {
                if (_dataLogic.DeleteRecord(Convert.ToInt32(_txtTableID.Text)))
                    MessageBox.Show("Deleted Successfully!");
                else
                    MessageBox.Show("Failed to deleted!");
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

            tblTable tableToUpdate = Initialize.NewTable(_txtTableID, _txtTableName, _txtTableStatus);

            try
            {
                if (_dataLogic.UpdateRecord(tableToUpdate))
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
    }
}
