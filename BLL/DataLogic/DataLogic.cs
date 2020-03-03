using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataAccess;

namespace BLL.DataLogic
{
    public abstract class DataLogic<T>
    {
        public abstract DataAccess<T> _dataAccess { get; set; }

        public abstract List<T> GetRecords();

        public abstract bool AddRecord(T newRecord);

        public abstract bool DeleteRecord(int idRecordToDelete);

        public abstract bool UpdateRecord(T recordToUpdate);
    }
}
