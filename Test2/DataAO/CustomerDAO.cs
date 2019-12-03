using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2.DataTO;

namespace Test2.DataAO
{
    public class CustomerDAO
    {
        private static CustomerDAO instance;

        public static CustomerDAO Instance
        {
            get { if (instance == null) instance = new CustomerDAO(); return CustomerDAO.instance; }
            private set { CustomerDAO.instance = value; }

        }

        private CustomerDAO() {; }

        public Customer SearchCustomerByCustomerID(string code)
        {
            string qr = string.Format("SELECT * FROM dbo.Customer WHERE codeCustomer = N'{0}' ", code);

            DataTable data = DataProvider.Instance.ExecuteQuery(qr);

            foreach (DataRow item in data.Rows)
            {
                Customer customer = new Customer(item);
                
                return customer;

            }

            return null;

        }
        
        public void AccumulatePoint(float totalPrices)
        {            
            string qr = "UPDATE dbo.Customer SET point = " + totalPrices;

            DataProvider.Instance.ExecuteNonQuery(qr);

        }

    }
}
