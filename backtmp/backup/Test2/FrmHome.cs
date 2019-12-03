using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Test2.DataAO;
using Test2.DataTO;
using Menu = Test2.DataTO.Menu;
using System.Globalization;

namespace Test2
{
    public partial class FrmTrangChu : Form
    {
        BindingSource perfumeList = new BindingSource();

        BindingSource categoryList = new BindingSource();

        BindingSource accountList = new BindingSource();

        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set
            {
                loginAccount = value;

                ChangeAccountType(loginAccount.Type);

                ChangeAccountInfo(loginAccount);

                this.UpdateAccountShowInfo += TTTK_UpdateAccountShowInfo;

                this.insertPerfume += f_InsertPerfume;

                this.updatePerfume += f_UpdatePerfume;

                this.deletePerfume += f_DeletePerfume;

                this.insertCategory += f_InsertCategory;

                this.updateCategory += f_UpdateCategory;

                this.deleteCategory += f_DeleteCategory;

                this.insertAccount += f_InsertAccount;

                this.updateAccount += f_UpdateAccount;

                this.deleteAccount += f_DeleteAccount;

            }
        }

        public FrmTrangChu(Account acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;

            LoadThings();

        }

        #region Method

        void LoadThings()
        {

            dtgvSanPham.DataSource = perfumeList;

            dtgvDanhMuc.DataSource = categoryList;

            dtgvTaiKhoan.DataSource = accountList;

            LoadOrderList();

            LoadCategory();

            LoadAccount();

            //LoadStaff();

            LoadDateTimePicker();

            LoadListBillByDate(dpkFrom.Value, dpkTo.Value);

            LoadListPerfume();

            LoadListCategory();

            LoadListAccount();

            AddPerfumeBinding();

            AddCategpryBinding();

            AddAccountBinding();

            LoadCategoryIntoComboBox(cbDanhMucSP);

        }

        void ChangeAccountType(int Type)
        {
            ((Control)tpAdmin).Enabled = (Type == 1);

            tpTTTaiKhoan.Text += " (" + LoginAccount.DisplayName + ")";

            txtbTenNV.Text = LoginAccount.DisplayName;

        }

        void LoadOrderList()
        {
            flpBanHang.Controls.Clear();

            List<Order> orderList = OrderDAO.Instance.LoadOrderList();

            foreach (Order item in orderList)
            {
                Button btn = new Button() { Width = OrderDAO.PerfumeListWidth, Height = OrderDAO.PerfumeListHeight };

                btn.Text = item.Name + Environment.NewLine + item.Status;

                btn.Click += btn_Click;

                btn.Tag = item;

                switch (item.Status)
                {
                    case "Trống ":
                        btn.BackColor = Color.Cyan;
                        break;
                    default:
                        btn.BackColor = Color.OrangeRed;
                        break;

                }

                flpBanHang.Controls.Add(btn);

            }
        }

        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbPhanLoai.DataSource = listCategory;
            cbPhanLoai.DisplayMember = "nameCategory";
        }

        void LoadPerfumeListByCategoryID(int id)
        {
            List<Perfume> listPerfume = PerfumeDAO.Instance.GetPerfumeByCategoryID(id);
            cbSanPham.DataSource = listPerfume;
            cbSanPham.DisplayMember = "PerfumeName";

        }

        void LoadAccount()
        {
            List<Account> listAccount = AccountDAO.Instance.GetAccountList();
        }

        void LoadStaff()
        {
            Staff staff = StaffDAO.Instance.GetStaff();

            txtbTenNV.Text = staff.Name;

            txtbMaHienThiNV.Text = staff.Code;
            
        }

        void LoadDateTimePicker()
        {
            DateTime today = DateTime.Now;

            dpkFrom.Value = new DateTime(today.Year, today.Month, 1);

            dpkTo.Value = dpkFrom.Value.AddMonths(1).AddDays(-1);

        }

        void LoadListBillByDate(DateTime ord, DateTime paid)
        {
            dtgvDoanhThu.DataSource = BillDAO.Instance.GetListBillByDate(ord, paid);
        }

        void TTTK_UpdateAccountShowInfo(object sender, AccountEvent e)
        {
            tpTTTaiKhoan.Text = " Thông tin tài khoản (" + e.Acc.DisplayName + ")";

        }

        void ResetPassword(int id)
        {
            if (AccountDAO.Instance.ResetPassword(id))
            {
                MessageBox.Show("Thiết lập lại mật khẩu thành công!");
                
                if (deleteAccount != null)
                {
                    deleteAccount(this, new EventArgs());

                }

            }

            else
            {
                MessageBox.Show("Có lỗi khi thiết lập lại mật khẩu");

            }

        }

        #endregion
        
        #region ThongTInTaiKhoan
        
        void ChangeAccountInfo(Account acc)
        {
            txtbUser.Text = LoginAccount.UserName;

            txtbHienThi.Text = LoginAccount.DisplayName;

        }

        void UpdateAccountInfo() 
        {
            string userName = txtbUser.Text;
            string displayName = txtbHienThi.Text;
            string password = AccountDAO.Instance.EncodedPassword(txtbPw.Text);
            string newPw = txtbPwMoi.Text;
            string rePw = txtbRePw.Text;

            if(!newPw.Equals(rePw))
            {
                MessageBox.Show("Vui lòng nhập laij5 mật khẩu đúng và mật khẩu mối. !!!");
                               
            }
            else
            {
                if(AccountDAO.Instance.UpdateAccount(userName, displayName, password, newPw))
                {
                    MessageBox.Show("Cập nhật thông tin thành công. !!!");

                    if(updateAccountShowInfo != null)
                    {
                        updateAccountShowInfo(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));

                    }
                }

                else 
                {
                    MessageBox.Show("Vui lòng điền lại đúng mật khẩu. !!!");
                }

            }
            
        }

        private event EventHandler<AccountEvent> updateAccountShowInfo;

        public event EventHandler<AccountEvent> UpdateAccountShowInfo
        {
            add { updateAccountShowInfo += value; }
            remove { updateAccountShowInfo -= value; }

        } 

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }

        #endregion

        #region BindingData
        
        void AddPerfumeBinding()
        {             
            txtbIDSP.DataBindings.Add(new Binding("Text", dtgvSanPham.DataSource, "id", true,DataSourceUpdateMode.Never));

            txtbTenSP.DataBindings.Add(new Binding("Text", dtgvSanPham.DataSource, "PerfumeName", true, DataSourceUpdateMode.Never));

            nmGiaSP.DataBindings.Add(new Binding("Value", dtgvSanPham.DataSource, "Price", true, DataSourceUpdateMode.Never));
            
        }

        void AddCategpryBinding()
        {
            txtbIdDM.DataBindings.Add(new Binding("Text", dtgvDanhMuc.DataSource, "id", true, DataSourceUpdateMode.Never));

            txtbTenDM.DataBindings.Add(new Binding("Text", dtgvDanhMuc.DataSource, "NameCategory", true, DataSourceUpdateMode.Never));

        }

        void AddAccountBinding()
        {
            txtbIDTK.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "id", true, DataSourceUpdateMode.Never));

            txtbDangNhapTK.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "UserName", true, DataSourceUpdateMode.Never));

            txtbHienThiTK.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));

            nmQuyenTK.DataBindings.Add(new Binding("Value", dtgvTaiKhoan.DataSource, "Type", true, DataSourceUpdateMode.Never));

        }
        void LoadCategoryIntoComboBox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "nameCategory";

        }

        private void txtbIDSP_TextChanged(object sender, EventArgs e)
        {
            if (dtgvSanPham.SelectedCells.Count > 0)
            {
                int id = (int)dtgvSanPham.SelectedCells[0].OwningRow.Cells["idCategory"].Value;

                Category category = CategoryDAO.Instance.GetCategoryByID(id);

                cbDanhMucSP.SelectedItem = category;

                int index = -1;
                int i = 0;

                foreach (Category item in cbDanhMucSP.Items)
                {
                    if (item.ID == category.ID)
                    {
                        index = i;

                        break;

                    }

                    i++;

                }

                cbDanhMucSP.SelectedIndex = index;

            }

        }

        private event EventHandler insertPerfume;

        public event EventHandler InsertPerfume
        {
            add { insertPerfume += value; }

            remove { insertPerfume -= value; }
        }

        private event EventHandler updatePerfume;

        public event EventHandler UpdatePerfume
        {
            add { updatePerfume += value; }

            remove { updatePerfume -= value; }
        }

        private event EventHandler deletePerfume;

        public event EventHandler DeletePerfume
        {
            add { deletePerfume += value; }

            remove { deletePerfume -= value; }
        }

        void f_InsertPerfume(object sender, EventArgs e)
        {
            LoadPerfumeListByCategoryID((cbDanhMucSP.SelectedItem as Category).ID);

            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Order).ID);
            
            }

        }

        void f_UpdatePerfume(object sender, EventArgs e)
        {
            LoadPerfumeListByCategoryID((cbDanhMucSP.SelectedItem as Category).ID);
            
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Order).ID);
            
            }

        }

        void f_DeletePerfume(object sender, EventArgs e)
        {
            LoadPerfumeListByCategoryID((cbDanhMucSP.SelectedItem as Category).ID);
            
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Order).ID);
            
            }

            LoadOrderList();

        }

        private event EventHandler insertCategory;

        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }

            remove { insertCategory -= value; }
        }

        private event EventHandler updateCategory;

        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }

            remove { updateCategory -= value; }
        }

        private event EventHandler deleteCategory;

        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }

            remove { deleteCategory -= value; }
        }

        void f_InsertCategory(object sender, EventArgs e)
        {
            LoadCategory();

            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Order).ID);

            }

        }

        void f_UpdateCategory(object sender, EventArgs e)
        {
            LoadCategory();

            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Order).ID);

            }

        }

        void f_DeleteCategory(object sender, EventArgs e)
        {
            LoadCategory();

            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Order).ID);

            }

            LoadOrderList();

        }

        private event EventHandler insertAccount;

        public event EventHandler InsertAccount
        {
            add { insertAccount += value; }

            remove { insertAccount -= value; }
        }

        private event EventHandler updateAccount;

        public event EventHandler UpdateAccount
        {
            add { updateAccount += value; }

            remove { updateAccount -= value; }
        }

        private event EventHandler deleteAccount;

        public event EventHandler DeleteAccount
        {
            add { deleteAccount += value; }

            remove { deleteAccount -= value; }
        }

        void f_InsertAccount(object sender, EventArgs e)
        {
            LoadAccount();

            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Order).ID);

            }

        }

        void f_UpdateAccount(object sender, EventArgs e)
        {
            LoadAccount();

            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Order).ID);

            }

        }

        void f_DeleteAccount(object sender, EventArgs e)
        {
            LoadAccount();

            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Order).ID);

            }

            LoadOrderList();

        }

        #endregion

        #region Events

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();

            List<Menu> listBillInfo = MenuDAO.Instance.GetListMenuByOrder(id);

            float totalPrice = 0;

            CultureInfo culture = new CultureInfo("vi-VN");

            foreach (Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.PerfumeName.ToString());

                lsvItem.SubItems.Add(item.Count.ToString());                               
                            
                lsvItem.SubItems.Add(item.Price.ToString("c",culture));
                
                lsvItem.SubItems.Add(item.TotalPrices.ToString("c", culture));

                totalPrice += item.TotalPrices;

                lsvBill.Items.Add(lsvItem);
            }

            txtbTongTien.Text = totalPrice.ToString("c", culture);

        }

        void btn_Click(object sender, EventArgs e)
        {

            int orderID = ((sender as Button).Tag as Order).ID;
            
            lsvBill.Tag = (sender as Button).Tag;

            ShowBill(orderID);

        }
                
        void LoadListPerfume()
        {
            perfumeList.DataSource = PerfumeDAO.Instance.GetListPerfume();

        }

        void LoadListCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();

        }

        void LoadListAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();

        }
        
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void FrmTrangChu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
        
        private void cbPhanLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
            {
                return;
            }

            Category selected = cb.SelectedItem as Category;

            id = selected.ID;

            LoadPerfumeListByCategoryID(id);
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            Order ord = lsvBill.Tag as Order;

            if (ord != null)
            {
                int idBill = BillDAO.Instance.GetUnPaidedBillIDByOrderID(ord.ID);

                int perfumeID = (cbSanPham.SelectedItem as Perfume).ID;

                int count = (int)nmSoLuong.Value;

                if (idBill == -1)
                {
                    BillDAO.Instance.InsertBill(ord.ID);

                    BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), perfumeID, count);

                }
                else
                {
                    BillInfoDAO.Instance.InsertBillInfo(idBill, perfumeID, count);

                }

                ShowBill(ord.ID);

                LoadOrderList();

            }

            else
            {
                MessageBox.Show("Hãy chọn Yêu Cầu!!!","Thông báo");

            }
        }

        private void btnHuyOrder_Click(object sender, EventArgs e)
        {
            Order ord = lsvBill.Tag as Order;
            
            if (ord != null)
            {
                OrderDAO.Instance.DeleteOrder(ord.ID);

                LoadOrderList();
            
            }

            else 
            {
                MessageBox.Show("Hãy chọn Yêu Cầu!!!", "Thông báo");

            }
            
        }

        private void btnTimMaKH_Click(object sender, EventArgs e)
        {
            if (txtbMaKH.Text != string.Empty)
            {
                Customer customer = SearchCustomerByCustomerID(txtbMaKH.Text);

                if (customer != null)
                {
                    LoadCustomerInfo(customer);

                }

                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng này!");
                }
                
            }
            
            else
            {
                MessageBox.Show("Hãy điền mã khách hàng!");

            }

        }

        void LoadCustomerInfo(Customer customer)
        {
            txtbCapKH.Text = customer.ClassCustomer;
            
            txtbMaKH.Text = customer.Code;

            txtbTenKH.Text = customer.Name;

            txtbMucGiamGia.Text = customer.Discount.ToString();

        }

        private void btnTaoOrder_Click(object sender, EventArgs e)
        {            
            OrderDAO.Instance.InsertOrder();

            LoadOrderList();
             
        }

        private void btnThanhToan_Click_2(object sender, EventArgs e)
        {
            Order ord = lsvBill.Tag as Order;

            if (ord != null)
            {
                int idBill = BillDAO.Instance.GetUnPaidedBillIDByOrderID(ord.ID);

                string str = txtbTongTien.Text.Split(',')[0];

                string tostr = "";

                foreach (string chr in str.Split('.'))
                {
                    tostr += chr;
                }

                double total = Convert.ToDouble(tostr);

                int discount = (int)nmGiamGia.Value;

                if (txtbMucGiamGia.Text != string.Empty)
                {                    
                    discount += Convert.ToInt32(txtbMucGiamGia.Text);

                }

                double finalTotal = total - (total / 100) * discount;
                //1 điểm = 100 000 VNĐ
                int accumulatedPoint = 100000;

                double point = finalTotal / accumulatedPoint;

                int idCustomer = 0;
                if (DataProvider.Instance.ExecuteNonQuery("SELECT id FROM dbo.Customer WHERE nameCustomer = N'" + txtbTenKH.Text + "' ") == null)
                {
                    idCustomer = -1;
                }
                else { idCustomer = DataProvider.Instance.ExecuteNonQuery("SELECT id FROM dbo.Customer WHERE nameCustomer = N'" + txtbTenKH.Text + "' "); }
                if (idBill != -1)
                {
                    if (MessageBox.Show(string.Format("Bạn có muốn thanh toán Yêu Cầu số {0} hay không?\nTổng tiền : \t {1}  \nMức giảm giá :\t {2} %\nTổng điểm tích lũy thêm : \t {3}\nTổng tiền cần thanh toán : \t {4}", ord.Name, total, discount, point, finalTotal), "Thông Báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        BillDAO.Instance.Checkout(idBill, discount, (float)finalTotal, idCustomer);

                        OrderDAO.Instance.PaidedOrder(ord.ID);

                        ShowBill(ord.ID);

                        LoadOrderList();

                    }
                }
            }

            else 
            {
                MessageBox.Show("Hãy chọn Yêu Cầu!");

            }
            
        }
                
        #endregion

        #region ETC

        private void FrmTrangChu_MdiChildActivate(object sender, EventArgs e)
        {

        }

        private void tpTaiKhoan_Click(object sender, EventArgs e)
        {

        }

        private void btnApDung_Click(object sender, EventArgs e)
        {

        }


        #endregion

        #region TPSanPham
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            string name = txtbTenSP.Text;

            int idCategory = (cbDanhMucSP.SelectedItem as Category).ID;

            float price = (float)nmGiaSP.Value;

            if (PerfumeDAO.Instance.InsertPerfume(name, idCategory, price))
            {
                MessageBox.Show("Thêm sản phẩm thành công!");

                LoadListPerfume();
                if (insertPerfume != null)
                {
                    insertPerfume(this, new EventArgs());

                }

            }

            else
            {
                MessageBox.Show("Có lỗi khi thêm sản phẩm");

            }
        }

        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtbIDSP.Text);
            
            string name = txtbTenSP.Text;

            int idCategory = (cbDanhMucSP.SelectedItem as Category).ID;

            float price = (float)nmGiaSP.Value;

            if (PerfumeDAO.Instance.UpdatePerfume(id,name, idCategory, price))
            {
                MessageBox.Show("Cập nhật sản phẩm thành công!");

                LoadListPerfume();

                if (updatePerfume != null)
                {
                    updatePerfume(this, new EventArgs());

                }

            }

            else
            {
                MessageBox.Show("Có lỗi khi cập nhật sản phẩm");

            }
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtbIDSP.Text);
            
            if (PerfumeDAO.Instance.DeletePerfume(id))
            {
                MessageBox.Show("Xóa sản phẩm thành công!");

                LoadListPerfume();

                if(deletePerfume != null)
                {
                    deletePerfume(this, new EventArgs());

                }

            }

            else
            {
                MessageBox.Show("Có lỗi khi xóa sản phẩm");

            }
        }

        List<Perfume> SearchPerfumeByName(string name)
        {
            List<Perfume> list = PerfumeDAO.Instance.SearchPerfumeByName(name);

            return list;

        }

        Customer SearchCustomerByCustomerID(string code)
        {
            Customer customer = CustomerDAO.Instance.SearchCustomerByCustomerID(code);

            return customer;

        }

        List<Category> SearchCategoryByName(string name)
        {
            List<Category> list = CategoryDAO.Instance.SearchCategoryByName(name);

            return list;

        }

        List<Account> SearchAccountByUserName(string name)
        {
            List<Account> list = AccountDAO.Instance.SearchAccountByUserName(name);
            
            return list;

        }

        private void btnTimSP_Click(object sender, EventArgs e)
        {
            perfumeList.DataSource = SearchPerfumeByName(txtbTimSP.Text);

        }

        private void btnXemSP_Click(object sender, EventArgs e)
        {
            LoadListPerfume();                       

        }
        
        #endregion

        #region TPDanhMuc
       
        private void btnThemDM_Click_1(object sender, EventArgs e)
        {
            string name = txtbTenDM.Text;

            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm danh mục thành công!");

                LoadListCategory();
                if (insertCategory != null)
                {
                    insertCategory(this, new EventArgs());

                }

            }

            else
            {
                MessageBox.Show("Có lỗi khi thêm danh mục");

            }

        }

        private void btnXoaDM_Click_1(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtbIdDM.Text);

            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa danh mục thành công!");

                LoadListCategory();

                if (deleteCategory != null)
                {
                    deleteCategory(this, new EventArgs());

                }

            }

            else
            {
                MessageBox.Show("Có lỗi khi xóa danh mục");

            }

            LoadOrderList();

        }

        private void btnSuaDM_Click_1(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtbIdDM.Text);

            string name = txtbTenDM.Text;

            if (CategoryDAO.Instance.UpdateCategory(id, name))
            {
                MessageBox.Show("Cập nhật danh mục thành công!");

                LoadListCategory();

                if (updateCategory != null)
                {
                    updateCategory(this, new EventArgs());

                }

            }

            else
            {
                MessageBox.Show("Có lỗi khi cập nhật danh mục");

            }

            LoadOrderList();

        }

        private void btnXemDM_Click_1(object sender, EventArgs e)
        {
            LoadListCategory();   

        }

        private void btnTimDM_Click_1(object sender, EventArgs e)
        {
            categoryList.DataSource = SearchCategoryByName(txtbTimDM.Text);

        }

        #endregion

        #region TPTaiKhoan

        private void btnThemTK_Click(object sender, EventArgs e)
        {
            string name = txtbDangNhapTK.Text;

            string display = txtbHienThiTK.Text;

            int type = (int)nmQuyenTK.Value;

            if (AccountDAO.Instance.InsertAccount(name, display, type))
            {
                MessageBox.Show("Thêm tài khoản thành công!");

                LoadListAccount();
                if (insertAccount != null)
                {
                    insertAccount(this, new EventArgs());

                }

            }

            else
            {
                MessageBox.Show("Có lỗi khi thêm tài khoản");

            }

        }

        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtbIDTK.Text);

            string name = txtbDangNhapTK.Text;

            string display = txtbHienThiTK.Text;

            int permit = (int)nmQuyenTK.Value;

            if (AccountDAO.Instance.UpdateAccount(id, name, display, permit))
            {
                MessageBox.Show("Cập nhật tài khoản thành công!");

                LoadListAccount();

                if (updateAccount != null)
                {
                    updateAccount(this, new EventArgs());

                }

            }

            else
            {
                MessageBox.Show("Có lỗi khi cập nhật tài khoản");

            }

            LoadOrderList();
        }

        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtbIDTK.Text);

            if (this.loginAccount.ID != id)
            {
                if (AccountDAO.Instance.DeleteAccount(id))
                {
                    MessageBox.Show("Xóa tài khoản thành công!");

                    LoadListAccount();

                    if (deleteAccount != null)
                    {
                        deleteAccount(this, new EventArgs());

                    }

                }

                else
                {
                    MessageBox.Show("Có lỗi khi xóa tài khoản");

                }

                LoadOrderList();

            }

            else
            {
                MessageBox.Show("Không thể xóa tài khoản đang hiện hành", "Lỗi");

                return;
            }

        }

        private void btnTimTK_Click(object sender, EventArgs e)
        {
            accountList.DataSource = SearchAccountByUserName(txtbTimTK.Text);

        }

        private void btnXemTK_Click(object sender, EventArgs e)
        {
            LoadListAccount();

        }

        private void btnResetTK_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtbIDTK.Text);

            ResetPassword(id);

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtbHienThiTK_TextChanged(object sender, EventArgs e)
        {

        }

        private void nmQuyenTK_ValueChanged(object sender, EventArgs e)
        {

        }

        #endregion
        
        #region TrangBill

        private void pnlTrang_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDau_Click(object sender, EventArgs e)
        {
            txtbTrang.Text = "1";

        }

        private void btnTruoc_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txtbTrang.Text);

            if (page > 1)
                page--;

            txtbTrang.Text = page.ToString();

        }

        private void btnSau_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txtbTrang.Text);

            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dpkFrom.Value, dpkTo.Value);

            if (page < sumRecord)
                page++;

            txtbTrang.Text = page.ToString();

        }

        private void btnCuoi_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dpkFrom.Value, dpkTo.Value);

            int lastPage = sumRecord / 10;

            if (sumRecord % 10 != 0)
                lastPage++;

            txtbTrang.Text = lastPage.ToString();

        }

        private void txtbTrang_TextChanged_1(object sender, EventArgs e)
        {
            dtgvDoanhThu.DataSource = BillDAO.Instance.GetBillListByDateAndPage(dpkFrom.Value, dpkTo.Value, Convert.ToInt32(txtbTrang.Text));

        }


        #endregion

        #region Report
        /*
        private void fAdmin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'QuanLyQuanCafeDataSet2.USP_GetListBillByDateForReport' table. You can move, or remove it, as needed.
            this.USP_GetListBillByDateForReportTableAdapter.Fill(this.QuanLyQuanCafeDataSet2.USP_GetListBillByDateForReport, dtpkFromDate.Value, dtpkToDate.Value);

            this.rpViewer.RefreshReport();
        }
        */
        #endregion

        private void btnThongKe_Click_1(object sender, EventArgs e)
        {
            LoadListBillByDate(dpkFrom.Value.Date, dpkTo.Value.Date);

        }
    }

    public class AccountEvent : EventArgs
    {
        private Account acc;

        public Account Acc
        {
            get { return acc; }
            set { acc = value; }
        
        }

        public AccountEvent(Account acc)
        {
            this.Acc = acc;
        }
    }
}
