using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.DataLogic;

namespace BLL.DisplayLogic
{
    public abstract class DisplayLogic<T>
    {
        protected abstract DataLogic<T> _dataLogic { get; set; }

        public abstract void CellClick(object sender, DataGridViewCellEventArgs e);

        public abstract void LoadRecordsFromDataLogic();

        protected abstract void ClearControlsContent();

        public abstract void ClickAddRecord();

        public abstract void ClickUpdateRecord();

        public abstract void ClickDeleteRecord();

        public abstract void ClickClearControlsContent();
    }
}
