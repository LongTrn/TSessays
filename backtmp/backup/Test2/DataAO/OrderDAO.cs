using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2.DataTO;

namespace Test2.DataAO
{
    public class OrderDAO
    {
        private static OrderDAO instance;

        public static OrderDAO Instance
        {
            get { if (instance == null) instance = new OrderDAO(); return OrderDAO.instance; }
            private set { OrderDAO.instance = value; }
        
        }

        public static int PerfumeListWidth = 160;
        public static int PerfumeListHeight = 100;

        private OrderDAO() {; }

        public List<Order> LoadOrderList()
        {
            List<Order> orderList = new List<Order>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetOrderList");

            foreach (DataRow item in data.Rows)
            {
                Order order = new Order(item);
                orderList.Add(order);

            }
            return orderList;

        }

        public void InsertOrder()
        {
            int idMax = OrderDAO.Instance.GetMaxOrder();

            int idHuy = OrderDAO.Instance.GetDeletedOrders();
                    
            if (idHuy > 0)
            {
                DataProvider.Instance.ExecuteNonQuery("EXEC USP_InsertDeletedOrder @idDeletedOrder ", new object[] { idHuy });

            }

            else 
            {
                DataProvider.Instance.ExecuteNonQuery("EXEC USP_InsertOrder @nOrder ", new object[] { idMax });

            }
        }

        public int GetDeletedOrders()
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT TOP 1 * FROM dbo.Orders WHERE status = N'Ẩn '");

            if (data.Rows.Count > 0)
            {
                Order order = new Order(data.Rows[0]);

                return order.ID;

            }

            return -1;

        }

        public void PaidedOrder(int id)
        {
            DataProvider.Instance.ExecuteQuery(string.Format("UPDATE dbo.Orders SET status = N'Đã thanh toán '  WHERE id = {0} ", new object[] { id }));

        }

        public void DeleteOrder(int id)
        {   
            DataProvider.Instance.ExecuteNonQuery("EXEC USP_DeleteOrder @idOrder ", new object[] { id });

        }

        public int GetMaxOrder()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT Max(id) FROM dbo.Orders ");

            }
            catch
            {
                return 1;

            }

        }

    }
}
