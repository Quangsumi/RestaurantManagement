using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL.DataLogic;
using DTO;
using BLL.Helper.Initialize;
using BLL.Helper.Validate;
using BLL.Helper.Tools;

namespace BLL.DisplayLogic
{
    public class MainFromDisplayLogic
    {
        #region Variables & Constructors
        TableDataLogic _tableDataLogic = new TableDataLogic();
        CategoryDataLogic _categoryDataLogic = new CategoryDataLogic();
        FoodDataLogic _foodDataLogic = new FoodDataLogic();
        BillDataLogic _billDataLogic = new BillDataLogic();
        BillInfoDataLogic _billInfoDataLogic = new BillInfoDataLogic();

        FlowLayoutPanel _flpMainTable;
        ListView _lvwMainOrders;
        ComboBox _cboMainCategoryName;
        ComboBox _cboMainFoodName;
        TextBox _txtMainDiscount;
        TextBox _txtMainTotalPrice;

        List<Button> _btnTables = new List<Button>();

        Button _selectedBtnTable;
        Order _orderOfSelectedBtnTable;

        public MainFromDisplayLogic() { }

        public MainFromDisplayLogic(FlowLayoutPanel flpMainTable, ListView lvwMainOrders, ComboBox cboMainCategoryName, ComboBox cboMainFoodName, TextBox txtMainDiscount, TextBox txtMainTotalPrice)
        {
            _flpMainTable = flpMainTable;
            _lvwMainOrders = lvwMainOrders;
            _cboMainCategoryName = cboMainCategoryName;
            _cboMainFoodName = cboMainFoodName;
            _txtMainDiscount = txtMainDiscount;
            _txtMainTotalPrice = txtMainTotalPrice;   
        }
        #endregion

        public void cboMainCategoryNameIndexChanged()
        {
            tblCategory selectedCategory = _cboMainCategoryName.SelectedItem as tblCategory;
            LoadFoodByCategoryID(selectedCategory.ID);
        }

        private void LoadFoodByCategoryID(int categoryID)
        {
            _cboMainFoodName.DataSource = _foodDataLogic.GetFoodByCategoryID(categoryID);
            _cboMainFoodName.DisplayMember = "Name";
            _cboMainFoodName.ValueMember = "ID";
        }

        public void LoadTablesAndCategory()
        {
            LoadCategoryToCbo();
            
            if (Tools.HasTempData())
                LoadTableFromFileTempToFlp();
            else
                LoadTableFromDbToFlp();
        }

        private void LoadCategoryToCbo()
        {
            _cboMainCategoryName.DataSource = GetCategory();
            _cboMainCategoryName.DisplayMember = "Name";
            _cboMainCategoryName.ValueMember = "ID";
        }

        private List<tblCategory> GetCategory() 
            => _categoryDataLogic.GetRecords();

        private void LoadTableFromFileTempToFlp()
        {
            List<TempBtnTable> tempBtnTables = Tools.GetTempBtnTableFromFile();

            foreach (TempBtnTable tempBtnTable in tempBtnTables)
            {
                Button btnTable = Initialize.NewBtnTable(tempBtnTable);
                btnTable.Click += Table_Click;

                _flpMainTable.Controls.Add(btnTable);
                _btnTables.Add(btnTable);
            }
        }

        private void LoadTableFromDbToFlp()
        {
            List<tblTable> tables = _tableDataLogic.GetRecords();

            foreach (tblTable table in tables)
            {
                Button btnTable = Initialize.NewBtnTable(table);
                btnTable.Click += Table_Click;

                _flpMainTable.Controls.Add(btnTable);
                _btnTables.Add(btnTable);
            }
        }

        private void Table_Click(object sender, EventArgs e)
        {
            _selectedBtnTable = sender as Button;
            _orderOfSelectedBtnTable = _selectedBtnTable.Tag as Order;
            DisplayOrderOfSelectedBtnTableToLvw();
        }

        private void DisplayOrderOfSelectedBtnTableToLvw()
        {
            _lvwMainOrders.Items.Clear();

            if (Validate.IsBtnTableSelected(_selectedBtnTable)
                && _orderOfSelectedBtnTable.Foods == null) return;
                

            foreach (var food in _orderOfSelectedBtnTable.Foods)
            {
                ListViewItem item = new ListViewItem(food.Key.Name);
                item.SubItems.Add(food.Value.ToString());
                item.SubItems.Add(food.Key.Price.ToString());

                _lvwMainOrders.Items.Add(item);
            }

            DisplayTotalPriceOfSelectedBtnTable();
        }

        private void DisplayTotalPriceOfSelectedBtnTable()
            => _txtMainTotalPrice.Text = _orderOfSelectedBtnTable.TotalPrice.ToString();

        public void ClickAddFood()
        {
            if (!Validate.IsBtnTableSelected(_selectedBtnTable)) return;

            if (!Validate.IsOrderOfSelectedTableInitialized(_orderOfSelectedBtnTable))
                InitializeOrderOfSelectedTable();

            tblFood selectedFood = _cboMainFoodName.SelectedItem as tblFood;

            if (Validate.IsSelectedFoodInSelectedTable(_orderOfSelectedBtnTable, selectedFood))
            {
                int numberOfExistingFood = _orderOfSelectedBtnTable.Foods[selectedFood];
                _orderOfSelectedBtnTable.Foods[selectedFood] = ++numberOfExistingFood;
            }
            else
            {
                _orderOfSelectedBtnTable.Foods.Add(selectedFood, 1);
            }

            Tools.SerializeBtnTables(_btnTables);
            DisplayOrderOfSelectedBtnTableToLvw();
        }

        private void InitializeOrderOfSelectedTable()
        {
            TurnTableOn(true);
            _orderOfSelectedBtnTable.CheckInDate = DateTime.Now;
            _orderOfSelectedBtnTable.Foods = new Dictionary<tblFood, int>();
        }

        public void ClickClearFood()
        {
            if (!Validate.IsBtnTableSelected(_selectedBtnTable)) return;

            tblFood selectedFood = _cboMainFoodName.SelectedItem as tblFood;

            if (Validate.IsSelectedFoodInSelectedTable(_orderOfSelectedBtnTable, selectedFood))
            {
                int numberOfExistingFood = _orderOfSelectedBtnTable.Foods[selectedFood];
                if (numberOfExistingFood > 1)
                    _orderOfSelectedBtnTable.Foods[selectedFood] = --numberOfExistingFood;
                else
                    _orderOfSelectedBtnTable.Foods.Remove(selectedFood);
            }
            else
            {
                MessageBox.Show($"This table haven't ordered this food yet!");
            }

            Tools.SerializeBtnTables(_btnTables);
            DisplayOrderOfSelectedBtnTableToLvw();
        }

        private bool IsCheckoutValid()
            => Validate.IsDigit(_txtMainDiscount) 
            && Validate.IsOrderOfSelectedTableInitialized(_orderOfSelectedBtnTable);

        public void ClickCheckout()
        {
            if (!Validate.IsBtnTableSelected(_selectedBtnTable)) return;

            if (!IsCheckoutValid()) return;

            ShowCheckoutInfomation();
        }

        private void ShowCheckoutInfomation()
        {
            string msg = Initialize.NewCheckoutInfo(_orderOfSelectedBtnTable, _txtMainDiscount);

            DialogResult dialogResult = MessageBox.Show(msg, _orderOfSelectedBtnTable.Table.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
                AddOrderToBill();
        }

        private void AddOrderToBill()
        {
            tblBill newBill = Initialize.NewCheckoutBill(_orderOfSelectedBtnTable, _txtMainDiscount);

            try
            {
                int lastBillID = _billDataLogic.AddBillUsingSP(newBill);
                AddOrderDetailsToBillInfo(_orderOfSelectedBtnTable, lastBillID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                newBill.Status = 0;
            }
        }

        private void AddOrderDetailsToBillInfo(Order checkoutOrder, int lastBillID)
        {
            foreach (var food in checkoutOrder.Foods)
            {
                tblBillInfo newBillInfo = Initialize.NewCheckoutBillInfo(lastBillID, food.Key.ID, food.Value);

                try
                {
                    _billInfoDataLogic.AddRecord(newBillInfo);
                }
                catch (Exception ex) { throw ex;}
            }

            Tools.SaveCheckoutInfoToFile(checkoutOrder, lastBillID, _txtMainDiscount);
            ClearCheckedOutTable();
        }

        private void ClearCheckedOutTable()
        {
            TurnTableOn(false);

            _orderOfSelectedBtnTable.Discount = 0;
            _orderOfSelectedBtnTable.Foods = null;
            _txtMainDiscount.Text = "0";
            _txtMainTotalPrice.Text = "0";

            if (AnyFullTable())
                Tools.SerializeBtnTables(_btnTables);
            else
                Tools.DeleteTemp();

            DisplayOrderOfSelectedBtnTableToLvw();
            
            _orderOfSelectedBtnTable = null;
            _selectedBtnTable = null;
        }

        private void TurnTableOn(bool flag)
        {
            foreach (Button button in _btnTables)
            {
                Order order = button.Tag as Order;
                
                if(_orderOfSelectedBtnTable == order)
                {
                    if(flag)
                        DecorateBtnTable(button, "Full", System.Drawing.Color.Chocolate);
                    else
                        DecorateBtnTable(button, "None", System.Drawing.Color.DodgerBlue);
                }
            }
        }

        private void DecorateBtnTable(Button btnTable, string text, System.Drawing.Color color)
        {
            btnTable.Text = _orderOfSelectedBtnTable.Table.Name + "\r\n(" + text + ")";
            btnTable.BackColor = color;
        }

        private bool AnyFullTable()
        {
            foreach (var btnTable in _btnTables)
            {
                Order orderOfBtnTablt = btnTable.Tag as Order;
                
                if (orderOfBtnTablt.Foods != null)
                    return true;
            }

            return false;
        }
    }
}
