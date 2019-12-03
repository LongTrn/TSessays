using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.DataTO
{
    public class Account
    {
        public Account(int id, string name, string display, string pw, int type)
        {
            this.ID = id;
            this.UserName = name;
            this.DisplayName = display;
            this.Password = pw;
            this.Type = type;

        }

        public Account(DataRow r)
        {
            this.ID = (int)r["id"];
            this.UserName = r["UserName"].ToString();
            this.DisplayName = r["DisplayName"].ToString();          
            this.Password = r["Password"].ToString();
            this.Type = (int)r["Type"];

        }

        private int iD;

        private string userName;

        private string displayName;

        private string password;

        private int type;
        
        public int ID { get => iD; set => iD = value; }

        public string UserName { get => userName; set => userName = value; }
        
        public string DisplayName { get => displayName; set => displayName = value; }
        
        public string Password { get => password; set => password = value; }
        
        public int Type { get => type; set => type = value; }
    }
}
