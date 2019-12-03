using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.DataTO
{
    public class Bill
    {
        public Bill(int id, DateTime? dateOrder, DateTime? datePayment, int idCustomer, int idOrder, int discount, float total, int status)
        {
            this.ID = id;
            this.DateOrder = dateOrder;
            this.DatePayment = datePayment;
            this.IdCustomer = idCustomer;
            this.IdOrder = idOrder;
            this.Discount = discount;
            this.Total = total;
            this.Status = status;
            
        }

        public Bill(DataRow r)
        {
            this.ID = (int)r["id"];
            this.DateOrder = (DateTime?)r["DateOrder"];
            var DatePaymentTmp = r["DatePayment"];
            if (DatePaymentTmp.ToString() != "")
            {
                this.DatePayment = (DateTime?)DatePaymentTmp;

            }
            //this.IdCustomer = (int)r["idCustomer"];
            this.IdOrder = (int)r["idOrder"];
            if ((int)r["discount"] != 0)
            {
                this.Discount = (int)r["discount"];

            }
            this.Status = (int) r["status"];

        }

        private int iD;

        private DateTime? dateOrder;

        private DateTime? datePayment;

        private int idCustomer;

        private int idOrder;
        
        private int discount;

        private float total;

        private int status;
        
        public int ID { get => iD; set => iD = value; }
              
        public DateTime? DateOrder { get => dateOrder; set => dateOrder = value; }
        
        public DateTime? DatePayment { get => datePayment; set => datePayment = value; }
                
        public int IdCustomer { get => idCustomer; set => idCustomer = value; }
        
        public int IdOrder { get => idOrder; set => idOrder = value; }
        
        public float Total { get => total; set => total = value; }

        public int Discount { get => discount; set => discount = value; }
        
        public int Status { get => status; set => status = value; }
        
    }
}
