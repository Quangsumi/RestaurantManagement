using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TableDataAccess
    {
        RestaurantManagementDataContext _dataContext = new RestaurantManagementDataContext();

        public List<tblTable> GetTables()
        {
            try
            {
                return _dataContext.tblTables.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public bool AddOneTable(tblTable newTable)
        {
            try
            {
                _dataContext.tblTables.InsertOnSubmit(newTable);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool DeleteOneTable(int idTableToDelete)
        {
            try
            {
                tblTable tableToDelete = _dataContext.tblTables
                    .FirstOrDefault(c => c.ID == idTableToDelete);

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

        public bool UpdateOneTable(tblTable pendingTable)
        {
            try
            {
                tblTable tableToUpdate = _dataContext.tblTables
                    .FirstOrDefault(c => c.ID == pendingTable.ID);

                if (tableToUpdate != null)
                {
                    tableToUpdate.Name = pendingTable.Name;

                    _dataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
