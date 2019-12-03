using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Test2.DataTO;

namespace Test2.DataAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }

        public bool Validate(string username, string pw)
        {
            string hashPass = EncodedPassword(pw);
            
            string qr = "USP_Validate @userName , @Password ";

            DataTable result = DataProvider.Instance.ExecuteQuery(qr, new object[] { username, hashPass });

            return result.Rows.Count > 0;
        }

        public string EncodedPassword(string initial)
        {
            byte[] tmp = ASCIIEncoding.ASCII.GetBytes(initial);

            byte[] hashData = new MD5CryptoServiceProvider().ComputeHash(tmp);

            string hashPass = "";

            foreach (byte item in hashData)
            {
                hashPass += item;

            }

            return hashPass;

        }

        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Account WHERE userName = '" + userName + "' ");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);

            }

            return null;
        }

        public bool UpdateAccount(string userName, string display, string pass, string newpass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("EXEC USP_UpdateAccountInfo @userName , @displayName , @password , @newPassword", new object[] { userName, display, pass, newpass });

            return result > 0;

        }

        public DataTable GetListAccount()
        {

            string qr = "SELECT id , UserName, DisplayName, Type FROM dbo.Account";

            return DataProvider.Instance.ExecuteQuery(qr);

        }

        public List<Account> GetAccountList()
        {
            List<Account> list = new List<Account>();

            string qr = "SELECT * FROM dbo.Account";

            DataTable data = DataProvider.Instance.ExecuteQuery(qr);

            foreach (DataRow item in data.Rows)
            {
                Account account = new Account(item);

                list.Add(account);

            }

            return list;

        }

        public List<Account> SearchAccountByUserName(string name)
        {
            List<Account> list = new List<Account>();

            string qr = string.Format("SELECT * FROM dbo.Account WHERE dbo.fuConvertToUnsign1(UserName) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);

            DataTable data = DataProvider.Instance.ExecuteQuery(qr);

            foreach (DataRow item in data.Rows)
            {               
                Account account = new Account(item);

                list.Add(account);

            }



            return list;

        }

        public bool InsertAccount(string name, string display, int type)
        {
            string qr = string.Format("INSERT dbo.Account ( UserName, DisplayName, Type) VALUES(N'{0}',N'{1}',{2})", name, display, type);

            int result = DataProvider.Instance.ExecuteNonQuery(qr);

            return result > 0;

        }

        public bool UpdateAccount(int id, string name, string display, int type)
        {
            string qr = string.Format("UPDATE dbo.Account SET UserName = N'{0}', DisplayName = N'{1}' , Type = {2} WHERE id = {3}", name, display, type, id);

            int result = DataProvider.Instance.ExecuteNonQuery(qr);

            return result > 0;

        }

        public bool DeleteAccount(int id)
        {
            string qr = string.Format("DELETE dbo.Account WHERE id = {0}", id);

            int result = DataProvider.Instance.ExecuteNonQuery(qr);

            return result > 0;

        }

        public bool ResetPassword(int id)
        {
            string qr = string.Format("UPDATE dbo.Account SET Password = N'20720532132149213101239102231223249249135100218' WHERE id = {0}", id);

            int result = DataProvider.Instance.ExecuteNonQuery(qr);

            return result > 0;

        }

    }
}
