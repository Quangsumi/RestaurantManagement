using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public abstract class DataAccess<T>
    {
        protected RestaurantManagementDataContext _dataContext;

        public DataAccess()
        {
            _dataContext = new RestaurantManagementDataContext();
        }

        public abstract List<T> GetRecords();

        public abstract bool AddRecord(T newRecord);

        public abstract bool DeleteRecord(int idRecordToDelete);

        public abstract bool UpdateRecord(T pendingRecord);
    }
}
