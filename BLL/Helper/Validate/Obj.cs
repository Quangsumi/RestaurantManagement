using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using DTO;

namespace BLL.Helper.Validate
{
    public static partial class Validate
    {
        public static bool IsAccountNull(tblAccount account)
            => account == null ? ShowNullAccountError() : false;

        private static bool ShowNullAccountError()
        {
            MessageBox.Show("Account is invalid. Try again", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return true;
        }

        public static bool IsBtnTableSelected(Button btnTable) 
            => btnTable != null;

        public static bool IsOrderOfSelectedTableInitialized(Order orderOfSelectedBtnTable)
            => orderOfSelectedBtnTable.Foods != null;

        public static bool IsSelectedFoodInSelectedTable(Order orderOfSelectedBtnTable, tblFood selectedFoodOnCbo)
            => orderOfSelectedBtnTable != null
            && orderOfSelectedBtnTable.Foods != null
            && ContainsKey(orderOfSelectedBtnTable.Foods, selectedFoodOnCbo);

        private static bool ContainsKey(Dictionary<tblFood, int> foods, tblFood selectedFoodOnCbo)
        {
            foreach (tblFood food in foods.Keys)
            {
                if (food.ID == selectedFoodOnCbo.ID)
                    return true;
            }
            return false;
        }

        public static bool AnyFullTable(List<Button> btnTables)
        {
            foreach (var btnTable in btnTables)
            {
                Order orderOfBtnTablt = btnTable.Tag as Order;

                if (orderOfBtnTablt.Foods != null)
                    return true;
            }

            return false;
        }
    }
}
