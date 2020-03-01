using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.DataLogic
{
    public class TableDataLogic
    {
        TableDataAccess _tableDataAccess = new TableDataAccess();

        public List<tblTable> GetTables()
        {
            try
            {
                return _tableDataAccess.GetTables();
            }
            catch (Exception ex) { throw ex; }
        }

        public bool AddOneTable(tblTable newTable)
        {
            try
            {
                return _tableDataAccess.AddOneTable(newTable);
            }
            catch (Exception ex) { throw ex; }
        }

        public bool DeleteOneTable(int idTableToDelete)
        {
            try
            {
                return _tableDataAccess.DeleteOneTable(idTableToDelete);
            }
            catch (Exception ex) { throw ex; }
        }

        public bool UpdateOneTable(tblTable tableToUpdate)
        {
            try
            {
                return _tableDataAccess.UpdateOneTable(tableToUpdate);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
