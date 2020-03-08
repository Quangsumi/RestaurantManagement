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

        public void SynccboCategoryAndcboFood()
        {
            if (_lvwMainOrders.SelectedItems.Count > 0)
            {
                ListViewItem selectedRow = _lvwMainOrders.SelectedItems[0];
                int foodID = Convert.ToInt32(selectedRow.SubItems[0].Text);
                tblFood food = _foodDataLogic.GetFoodByID(foodID);

                _cboMainFoodName.Text = food.Name;
                _cboMainCategoryName.Text = _foodDataLogic.GetCatagoryNameByCategoryID(food.CategoryID.ToString());
            }
        }

        public void LoadFoodByCategoryID()
        {
            tblCategory selectedCategory = _cboMainCategoryName.SelectedItem as tblCategory;

            _cboMainFoodName.DataSource = _foodDataLogic.GetFoodByCategoryID(selectedCategory.ID);
            _cboMainFoodName.DisplayMember = "Name";
            _cboMainFoodName.ValueMember = "ID";
        }

        public void LoadTablesAndCategory()
        {
            LoadCategoryToCbo();
            
            if (Tools.HasTempData())
                LoadTableFromTempToFlp();
            else
                LoadTableFromDbToFlp();
        }

        private void LoadCategoryToCbo()
        {
            _cboMainCategoryName.DataSource = _categoryDataLogic.GetRecords();
            _cboMainCategoryName.DisplayMember = "Name";
            _cboMainCategoryName.ValueMember = "ID";
        }

        private void LoadTableFromTempToFlp()
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
            
            if (_orderOfSelectedBtnTable?.Foods == null) return;
                
            foreach (var food in _orderOfSelectedBtnTable.Foods)
            {
                ListViewItem item = new ListViewItem(food.Key.ID.ToString());
                item.SubItems.Add(food.Key.Name);
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

            tblFood selectedFoodOnCbo = _cboMainFoodName.SelectedItem as tblFood;

            if (Validate.IsSelectedFoodInSelectedTable(_orderOfSelectedBtnTable, selectedFoodOnCbo))
            {
                foreach (tblFood foodOnOrder in _orderOfSelectedBtnTable.Foods.Keys)
                {
                    if (foodOnOrder.ID == selectedFoodOnCbo.ID)
                        selectedFoodOnCbo = foodOnOrder;
                }
                int numberOfExistingFood = _orderOfSelectedBtnTable.Foods[selectedFoodOnCbo];
                _orderOfSelectedBtnTable.Foods[selectedFoodOnCbo] = ++numberOfExistingFood;
            }
            else
            {
                _orderOfSelectedBtnTable.Foods.Add(selectedFoodOnCbo, 1);
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

            tblFood selectedFoodOnCbo = _cboMainFoodName.SelectedItem as tblFood;

            if (Validate.IsSelectedFoodInSelectedTable(_orderOfSelectedBtnTable, selectedFoodOnCbo))
            {
                foreach (tblFood foodOnOrder in _orderOfSelectedBtnTable.Foods.Keys)
                {
                    if (foodOnOrder.ID == selectedFoodOnCbo.ID)
                        selectedFoodOnCbo = foodOnOrder;
                }
                int numberOfExistingFood = _orderOfSelectedBtnTable.Foods[selectedFoodOnCbo];
                if (numberOfExistingFood > 1)
                    _orderOfSelectedBtnTable.Foods[selectedFoodOnCbo] = --numberOfExistingFood;
                else
                    _orderOfSelectedBtnTable.Foods.Remove(selectedFoodOnCbo);
            }
            else
            {
                MessageBox.Show($"This table haven't ordered this food yet!");
            }

            Tools.SerializeBtnTables(_btnTables);
            DisplayOrderOfSelectedBtnTableToLvw();

            if (_orderOfSelectedBtnTable.Foods.Count == 0)
                ClearCheckedOutTable();
        }

        private bool IsCheckoutValid()
            => Validate.IsDigit(_txtMainDiscount) 
            && Validate.IsOrderOfSelectedTableInitialized(_orderOfSelectedBtnTable);

        public void ClickCheckout()
        {
            if (!Validate.IsBtnTableSelected(_selectedBtnTable) || !IsCheckoutValid()) return;

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
                AddOrderDetailsToBillInfo(lastBillID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //newBill.Status = 0;
                //rollback
            }
        }

        private void AddOrderDetailsToBillInfo(int lastBillID)
        {
            foreach (var food in _orderOfSelectedBtnTable.Foods)
            {
                tblBillInfo newBillInfo = Initialize.NewCheckoutBillInfo(lastBillID, food.Key.ID, food.Value);

                try
                {
                    _billInfoDataLogic.AddRecord(newBillInfo);
                }
                catch (Exception ex) { throw ex;}
            }

            Tools.SaveCheckoutInfoToFile(_orderOfSelectedBtnTable, lastBillID, _txtMainDiscount);
            ClearCheckedOutTable();
        }

        private void ClearCheckedOutTable()
        {
            TurnTableOn(false);

            _orderOfSelectedBtnTable.Discount = 0;
            _orderOfSelectedBtnTable.Foods = null;
            _txtMainDiscount.Text = "0";
            _txtMainTotalPrice.Text = "0";

            if (Validate.AnyFullTable(_btnTables))
                Tools.SerializeBtnTables(_btnTables);
            else
                Tools.DeleteTemp();

            DisplayOrderOfSelectedBtnTableToLvw();
            
            _orderOfSelectedBtnTable = null;
            _selectedBtnTable = null;
        }

        private void TurnTableOn(bool isFull)
        {
            foreach (Button button in _btnTables)
            {
                Order order = button.Tag as Order;
                
                if(_orderOfSelectedBtnTable == order)
                {
                    if(isFull)
                        Initialize.DecorateBtnTable(_orderOfSelectedBtnTable, button, "Full", System.Drawing.Color.Chocolate);
                    else
                        Initialize.DecorateBtnTable(_orderOfSelectedBtnTable, button, "None", System.Drawing.Color.DodgerBlue);
                }
            }
        }
    }
}
