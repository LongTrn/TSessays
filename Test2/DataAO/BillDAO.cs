using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2.DataTO;

namespace Test2.DataAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }

        private BillDAO() {; }

        // bill ID suceed || -1 failed

        public int GetUnPaidedBillIDByOrderID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Bill WHERE idOrder = " + id + "AND status = 0");
            
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);

                return bill.ID;

            }

            return -1;

        }

        public DataTable GetListBillByDate(DateTime ord, DateTime paid)
        {
            return DataProvider.Instance.ExecuteQuery("EXEC USP_GetListBillByDate @dateOrder , @datePayment ", new object[] { ord , paid });

        }

        public int GetMaxIDBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(id) FROM dbo.Bill ");

            }
            catch
            {
                return 1;

            }

        }

        public void Checkout(int id, int discount, float totalPrices, int idCustomer)
        {
            if (idCustomer != -1)
            {
                string qr = String.Format("UPDATE dbo.Bill SET status = 1, DatePayment = GETDATE() , discount = {0} , totalPrices = {1} , idCustomer = {3} WHERE id = {2} ", discount, totalPrices, id, idCustomer);

                DataProvider.Instance.ExecuteNonQuery(qr);
            }

            else
            {
                string qr = String.Format("UPDATE dbo.Bill SET status = 1, DatePayment = GETDATE() , discount = {0} , totalPrices = {1}  WHERE id = {2} ", discount, totalPrices, id);

                DataProvider.Instance.ExecuteNonQuery(qr);
                
            }

        }

        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("EXEC USP_InsertBill @idOrder ", new object[] { id });

        }

        public DataTable GetBillListByDate(DateTime dateOrder, DateTime datePayment)
        {
            return DataProvider.Instance.ExecuteQuery("USP_GetNumBillByDate @dateOrder , @datePayment", new object[] { dateOrder, datePayment });
        }

        public DataTable GetBillListByDateAndPage(DateTime dateOrder, DateTime datePayment, int pageNum)
        {
            return DataProvider.Instance.ExecuteQuery("exec USP_GetListBillByDateAndPage @dateOrder , @datePayment , @page", new object[] { dateOrder, datePayment, pageNum });
        }

        public int GetNumBillListByDate(DateTime dateOrder, DateTime datePayment)
        {
            return (int)DataProvider.Instance.ExecuteScalar("exec USP_GetNumBillByDate @dateOrder , @datePayment", new object[] { dateOrder, datePayment });
        }               

    }
}
