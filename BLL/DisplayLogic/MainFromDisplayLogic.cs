using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL.DataLogic;
using DTO;
using BLL.Helper;

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

        List<tblTable> _tables;

        Button _selectedBtnTable;
        Order _orderOfSelectedBtnTable;
        tblFood _selectedFoodOnCbo;

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
            LoadTableToFlp();
        }

        private void LoadCategoryToCbo()
        {
            _cboMainCategoryName.DataSource = GetCategory();
            _cboMainCategoryName.DisplayMember = "Name";
            _cboMainCategoryName.ValueMember = "ID";
        }

        private List<tblCategory> GetCategory() 
            => _categoryDataLogic.GetRecords();

        private void LoadTableToFlp()
        {
            _tables = _tableDataLogic.GetRecords();

            foreach (tblTable table in _tables)
            {
                Button btnTable = new Button();
                btnTable.Text = table.Name;
                btnTable.Width = 75;
                btnTable.Height = 55;
                btnTable.Tag = new Order();
                (btnTable.Tag as Order).Table = table;
                btnTable.Click += Table_Click;

                _flpMainTable.Controls.Add(btnTable);
            }
        }

        private void Table_Click(object sender, EventArgs e)
        {
            _selectedBtnTable = sender as Button;
            Order orderOfSelectedBtnTable = _selectedBtnTable.Tag as Order;
            DisplayOrderToLvw(orderOfSelectedBtnTable);
        }

        private void DisplayOrderToLvw(Order order)
        {
            _lvwMainOrders.Items.Clear();

            if (order.Foods == null) return;

            foreach (var food in order.Foods)
            {
                ListViewItem item = new ListViewItem(food.Key.Name);
                item.SubItems.Add(food.Value.ToString());
                item.SubItems.Add(food.Key.Price.ToString());

                _lvwMainOrders.Items.Add(item);
            }

            DisplayTotalPrice(order);
        }

        private void DisplayTotalPrice(Order order)
            => _txtMainTotalPrice.Text = order.TotalPrice.ToString();


        public void ClickAddFood()
        {
            if (!IsSelectedTable()) return;

            Assign_OrderOfSelectedBtnTable_And_SelectedFoodOnCbo();

            if (!IsSelectedTableInitialized())
                InitializeSelectedTable();

            if (IsSelectedFoodInSelectedTable())
            {
                int numberOfExistingFood = _orderOfSelectedBtnTable.Foods[_selectedFoodOnCbo];
                _orderOfSelectedBtnTable.Foods[_selectedFoodOnCbo] = ++numberOfExistingFood;
            }
            else
            {
                _orderOfSelectedBtnTable.Foods.Add(_selectedFoodOnCbo, 1);
            }

            DisplayOrderToLvw(_orderOfSelectedBtnTable);
        }

        private bool IsSelectedTable()
            => _selectedBtnTable != null;

        private bool IsSelectedTableInitialized()
            => _orderOfSelectedBtnTable.Foods != null;

        private void Assign_OrderOfSelectedBtnTable_And_SelectedFoodOnCbo()
        {
            _orderOfSelectedBtnTable = _selectedBtnTable.Tag as Order;
            _selectedFoodOnCbo = _cboMainFoodName.SelectedItem as tblFood;
        }

        private void InitializeSelectedTable()
        {
            _orderOfSelectedBtnTable.Table.Status = 1;
            _orderOfSelectedBtnTable.CheckInDate = DateTime.Now;
            _orderOfSelectedBtnTable.Discount = 0;
            _orderOfSelectedBtnTable.Foods = new Dictionary<tblFood, int>();
        }

        private bool IsSelectedFoodInSelectedTable()
            => _orderOfSelectedBtnTable.Foods != null && _orderOfSelectedBtnTable.Foods.ContainsKey(_selectedFoodOnCbo);

        public void ClickClearFood()
        {
            if (!IsSelectedTable()) return;

            Assign_OrderOfSelectedBtnTable_And_SelectedFoodOnCbo();

            if (IsSelectedFoodInSelectedTable())
            {
                int numberOfExistingFood = _orderOfSelectedBtnTable.Foods[_selectedFoodOnCbo];
                if (numberOfExistingFood > 1)
                    _orderOfSelectedBtnTable.Foods[_selectedFoodOnCbo] = --numberOfExistingFood;
                else
                    _orderOfSelectedBtnTable.Foods.Remove(_selectedFoodOnCbo);
            }
            else
            {
                MessageBox.Show($"Table haven't ordered this food yet!");
            }

            DisplayOrderToLvw(_orderOfSelectedBtnTable);
        }

        private bool IsCheckoutValid() 
         => Validate.IsDigit(_txtMainDiscount) && _orderOfSelectedBtnTable.Foods != null;

        public void ClickCheckout()
        {
            if (!IsSelectedTable()) return;

            Assign_OrderOfSelectedBtnTable_And_SelectedFoodOnCbo();

            if (!IsCheckoutValid()) return;

            ShowCheckoutInfomation(_orderOfSelectedBtnTable);
        }

        private void ShowCheckoutInfomation(Order checkoutOrder)
        {
            string msg = "";

            msg += checkoutOrder.Table.ID.ToString() + " - " + checkoutOrder.Table.Name + "\r\n\n";
            msg += "- Checkin Date: " + checkoutOrder.CheckInDate.ToString() + "\r\n";
            msg += "- Checkout Date: " + DateTime.Now.ToString() + "\r\n";
            msg += "- Status: " + checkoutOrder.Status + "\r\n";
            msg += "- Orders: " + "\r\n";
            foreach (var food in checkoutOrder.Foods)
            {
                msg += "\t+ Food name: " + food.Key.Name + " - x" + food.Value + "\r\n";
            }
            checkoutOrder.Discount = Int32.Parse(_txtMainDiscount.Text);
            msg += "\r\n" + "- Discount: " + checkoutOrder.Discount + "\r\n";
            msg += "- Totol Price: " + checkoutOrder.TotalPrice;

            DialogResult dialogResult = MessageBox.Show(msg, checkoutOrder.Table.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
                AddOrderToBill(_orderOfSelectedBtnTable);
        }

        private void AddOrderToBill(Order checkoutOrder)
        {
            tblBill newBill = new tblBill();
            newBill.CheckInDate = checkoutOrder.CheckInDate;
            newBill.CheckOutDate = DateTime.Now;
            newBill.TableID = checkoutOrder.Table.ID;
            newBill.Discount = Int32.Parse(_txtMainDiscount.Text);
            newBill.TotalPrice = checkoutOrder.TotalPrice;
            newBill.Status = 1;

            try
            {
                int lastBillID = _billDataLogic.AddBillUsingSP(newBill);
                AddOrderDetailsToBillInfo(checkoutOrder, lastBillID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddOrderDetailsToBillInfo(Order checkoutOrder, int lastBillID)
        {
            foreach (var food in checkoutOrder.Foods)
            {
                tblBillInfo newBillInfo = new tblBillInfo();
                newBillInfo.BillID = lastBillID;
                newBillInfo.FoodID = food.Key.ID;
                newBillInfo.Count = food.Value;

                try
                {
                    _billInfoDataLogic.AddRecord(newBillInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            ClearCheckedOutTable();
        }

        private void ClearCheckedOutTable()
        {
            _orderOfSelectedBtnTable.Table.Status = 0;
            _orderOfSelectedBtnTable.CheckInDate = DateTime.Now;
            _orderOfSelectedBtnTable.Discount = 0;
            _orderOfSelectedBtnTable.Foods = null;

            DisplayOrderToLvw(_orderOfSelectedBtnTable);
            _selectedBtnTable = null;
        }
    }
}
