using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.DataTO
{
    public class Customer
    {
        public Customer(int id, string name, string code, string classCustomer, float point, int discount)
        {
            this.Id = id;
            this.Name = name;
            this.Code= code;
            this.ClassCustomer = classCustomer;
            this.Point = point;
            this.Discount = discount;

        }

        public Customer(DataRow r)
        {
            this.Id = (int)r["id"];
            this.Name = r["nameCustomer"].ToString();
            this.Code = r["codeCustomer"].ToString();
            this.ClassCustomer = r["class"].ToString();
            this.Point = (float)Convert.ToDouble(r["point"].ToString());
            this.Discount =(int)r["discountRate"];

        }

        private int id;

        private string name;

        private string code;

        private string classCustomer;

        private float point;

        private int discount;
        public int Id { get => id; set => id = value; }
        
        public string Name { get => name; set => name = value; }

        public string Code { get => code; set => code = value; }

        public string ClassCustomer { get => classCustomer; set => classCustomer = value; }
        
        public float Point { get => point; set => point = value; }
        
        public int Discount { get => discount; set => discount = value; }
                
    }
}
