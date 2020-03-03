using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class TableDataAccess : DataAccess<tblTable>
    {
        public override bool AddRecord(tblTable newRecord)
        {
            try
            {
                _dataContext.tblTables.InsertOnSubmit(newRecord);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool DeleteRecord(int idRecordToDelete)
        {
            try
            {
                tblTable tableToDelete = _dataContext.tblTables
                    .FirstOrDefault(c => c.ID == idRecordToDelete);

                if (tableToDelete != null)
                {
                    _dataContext.tblTables.DeleteOnSubmit(tableToDelete);
                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public override List<tblTable> GetRecords()
        {
            try
            {
                return _dataContext.tblTables.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public override bool UpdateRecord(tblTable pendingRecord)
        {
            try
            {
                tblTable tableToUpdate = _dataContext.tblTables
                    .FirstOrDefault(c => c.ID == pendingRecord.ID);

                if (tableToUpdate != null)
                {
                    tableToUpdate.Name = pendingRecord.Name;

                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
