using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.DataTO
{
    public class Staff
    {
        public Staff(int id, string name, string code, int type)
        {
            this.Id = id;
            this.Name = name;
            this.Code = code;
            this.Type = type;
        }

        public Staff(DataRow r)
        {
            this.Id = (int)r["id"];
            this.Name = r["nameStaff"].ToString();
            this.code = r["codeStaff"].ToString();
            this.type = (int)r["Type"];

        }

        private int id;

        private string name;

        private string code;

        private int type;
        public int Id { get => id; set => id = value; }

        public string Name { get => name; set => name = value; }
               
        public string Code { get => code; set => code = value; }
        
        public int Type { get => type; set => type = value; }
    }
}
