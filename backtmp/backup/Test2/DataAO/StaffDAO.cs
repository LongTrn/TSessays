using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2.DataTO;

namespace Test2.DataAO
{
    public class StaffDAO
    {
        private static StaffDAO instance;

        public static StaffDAO Instance
        {
            get { if (instance == null) instance = new StaffDAO(); return StaffDAO.instance; }
            private set { StaffDAO.instance = value; }

        }

        private StaffDAO() {; }

        public Staff GetStaff()
        {
            string hashPass = AccountDAO.Instance.EncodedPassword(FrmDangNhap.savedPassword);
            
            string qr = string.Format("SELECT id FROM dbo.Account WHERE UserName = N'{0}' AND Password = N'{1}' ", FrmDangNhap.savedUserName, hashPass);

            int data = DataProvider.Instance.ExecuteNonQuery(qr);

            Staff staff = StaffDAO.Instance.GetStaffByID(data);
            
            return staff;
        }

        public Staff GetStaffByID(int id)
        {
            string qr = string.Format("SELECT s.id, nameStaff, codeStaff, UserName, DisplayName, a.Type FROM dbo.staff AS s, dbo.Account AS a WHERE s.id = {0} AND a.id = {0} ", id);

            DataTable data = DataProvider.Instance.ExecuteQuery(qr);

            foreach (DataRow item in data.Rows)
            {
                Staff staff = new Staff(item);

                return staff;

            }

            return null;

        }
    }
}
