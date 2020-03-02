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
    public class TableDisplayLogic
    {
        TableDataLogic _tableDataLogic = new TableDataLogic();

        DataGridView _dgvTables;
        TextBox _txtTableID;
        TextBox _txtTableName;
        TextBox _txtTableStatus;

        public void TransferObject(DataGridView dgvTables, TextBox txtTableID, TextBox txtTableName, TextBox txtTableStatus)
        {
            _txtTableID = txtTableID;
            _txtTableName = txtTableName;
            _txtTableStatus = txtTableStatus;
            _dgvTables = dgvTables;
        }

        public void CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewRow row = _dgvTables.Rows[e.RowIndex];

            _txtTableID.Text = row.Cells[0].Value.ToString();
            _txtTableName.Text = row.Cells[1].Value.ToString();
            _txtTableStatus.Text = row.Cells[2].Value.ToString();
        }

        public void LoadTablesFromDataAccess()
        {
            try
            {
                _dgvTables.DataSource = _tableDataLogic.GetTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearTextBox()
        {
            _txtTableID.Text = "";
            _txtTableName.Text = "";
            _txtTableStatus.Text = "";
        }

        public void ClickAddTable()
        {
            tblTable newTable = new tblTable();
            newTable.Name = _txtTableName.Text;
            newTable.Status = Convert.ToInt32(_txtTableStatus.Text);

            try
            {
                if (_tableDataLogic.AddOneTable(newTable))
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
                LoadTablesFromDataAccess();
                ClearTextBox();
            }
        }

        public void ClickUpdateTable()
        {
            tblTable tableToUpdate = new tblTable();
            tableToUpdate.ID = Convert.ToInt32(_txtTableID.Text);
            tableToUpdate.Name = _txtTableName.Text;
            tableToUpdate.Status = Convert.ToInt32(_txtTableStatus.Text);

            try
            {
                if (_tableDataLogic.UpdateOneTable(tableToUpdate))
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
                LoadTablesFromDataAccess();
                ClearTextBox();
            }
        }

        public void ClickDeleteTable()
        {
            try
            {
                if (_tableDataLogic.DeleteOneTable(Convert.ToInt32(_txtTableID.Text)))
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
                LoadTablesFromDataAccess();
                ClearTextBox();
            }
        }

        
        public void ClickClearTable()
        {
            ClearTextBox();
        }
    }
}
