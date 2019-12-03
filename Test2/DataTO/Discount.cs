using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.DataTO
{
    public class Discount
    {
        public Discount(int id, string name, string classCustomer, float point, int discount)
        {
            this.Id = id;
            this.NameDiscount = name;
            this.CodeDiscount = classCustomer;
            this.DiscountRate = discount;

        }

        public Discount(DataRow r)
        {
            this.Id = (int)r["id"];
            this.NameDiscount = r["nameDiscount"].ToString();
            this.CodeDiscount = r["codeDiscount"].ToString();
            this.DiscountRate = (int)r["discountRate"];

        }

        private int id;

        private string nameDiscount;

        private string codeDiscount;

        private int discountRate;

        public int Id { get => id; set => id = value; }
        
        public string NameDiscount { get => nameDiscount; set => nameDiscount = value; }
        
        public string CodeDiscount { get => codeDiscount; set => codeDiscount = value; }
        
        public int DiscountRate { get => discountRate; set => discountRate = value; }
    }
}
