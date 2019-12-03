using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test2.DataAO;
using Test2.DataTO;

namespace Test2
{
    public partial class FrmDangNhap : Form
    {
        public static string savedUserName;
        
        public static string savedPassword;

        public FrmDangNhap()
        {
            InitializeComponent();

            LoadAccount();

        }

        void LoadAccount()
        {
            if (Properties.Settings.Default.savedUserName != string.Empty)
            {                
                txtbUser.Text = Properties.Settings.Default.savedUserName;

                txtbPw.Text = Properties.Settings.Default.savedPassword;

                ckbSaveAccount.Checked = Properties.Settings.Default.SavedAccount;

            }
        }
              
        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }
             
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }
        
        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
            string userName = txtbUser.Text;

            string password = txtbPw.Text;
            
            if (Validate(userName, password))
            {
                SaveAccount();

                Account loginAccount = AccountDAO.Instance.GetAccountByUserName(userName);
                
                FrmTrangChu f = new FrmTrangChu(loginAccount);

                this.Hide();

                f.ShowDialog();

                this.Show();

            }

            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!!!");
            }
        }

        bool Validate(string userName, string password)
        {
            return AccountDAO.Instance.Validate(userName, password);

        }
        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
            
            this.Close();
        }

        private void btnDangNhap_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void FrmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK) 
            {
                e.Cancel = true;
            }
        }

        void SaveAccount()
        {
            string userName = txtbUser.Text;

            string password = txtbPw.Text;

            if (userName != string.Empty && password != string.Empty && ckbSaveAccount.Checked && Validate(userName, password))
            {
                Properties.Settings.Default.savedUserName = userName;

                Properties.Settings.Default.savedPassword = password;
                
                savedUserName = userName;

                savedPassword = password;

                Properties.Settings.Default.SavedAccount = ckbSaveAccount.Checked;

                Properties.Settings.Default.Save();

            }

        }
    }
}
