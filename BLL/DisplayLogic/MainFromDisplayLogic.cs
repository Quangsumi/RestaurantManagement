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
        TableDataLogic _tableDataLogic = new TableDataLogic();
        CategoryDataLogic _categoryDataLogic = new CategoryDataLogic();
        FoodDataLogic _foodDataLogic = new FoodDataLogic();

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

        public void InitializeData()
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

        private List<tblCategory> GetCategory() => _categoryDataLogic.GetRecords();

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
            Assign_OrderOfSelectedBtnTable_SelectedFoodOnCbo();

            if (IsSelectedFoodInOrderedFoods())
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

        private void Assign_OrderOfSelectedBtnTable_SelectedFoodOnCbo()
        {
            _orderOfSelectedBtnTable = _selectedBtnTable.Tag as Order;
            _selectedFoodOnCbo = _cboMainFoodName.SelectedItem as tblFood;
        }

        private bool IsSelectedFoodInOrderedFoods()
            => _orderOfSelectedBtnTable.Foods.ContainsKey(_selectedFoodOnCbo);

        public void ClickClearFood()
        {
            Assign_OrderOfSelectedBtnTable_SelectedFoodOnCbo();

            if (IsSelectedFoodInOrderedFoods())
            {
                int numberOfExistingFood = _orderOfSelectedBtnTable.Foods[_selectedFoodOnCbo];
                if (numberOfExistingFood > 1)
                    _orderOfSelectedBtnTable.Foods[_selectedFoodOnCbo] = --numberOfExistingFood;
                else
                    _orderOfSelectedBtnTable.Foods.Remove(_selectedFoodOnCbo);
            }
            else
            {
                MessageBox.Show($"{_orderOfSelectedBtnTable.Table.Name} haven't ordered this food yet!");
            }

            DisplayOrderToLvw(_orderOfSelectedBtnTable);
        }

        private bool IsInputValid() 
            => Validate.IsDigit(_txtMainDiscount);

        public void ClickCheckout()
        {
            if (!IsInputValid()) return;
        }
    }
}
