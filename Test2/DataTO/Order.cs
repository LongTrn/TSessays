using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.DataTO
{
    public class Order
    {     
        public Order (int id, string name, int has, string status)
        {
            this.ID = id;
            this.Name = name;
            this.HasBill = has;
            this.Status = status;

        }

        public Order(DataRow r)
        {
            this.ID = (int)r["id"];
            this.Name = r["nameOrder"].ToString();
            this.HasBill= (int)r["hasBill"];
            this.Status = r["status"].ToString();

        }

        private int iD;

        private string name;
        
        private int hasBill;
        
        private string status;

        public int ID { get => iD; set => iD = value; }
        
        public string Name { get => name; set => name = value; }
        
        public string Status { get => status; set => status = value; }
        
        int HasBill { get => hasBill; set => hasBill = value; }
    }
}
