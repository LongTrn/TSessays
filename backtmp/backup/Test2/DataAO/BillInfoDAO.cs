using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2.DataTO;

namespace Test2.DataAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return instance; }
            private set { BillInfoDAO.instance = value; }

        }

        private BillInfoDAO() {; }


        public List<BillInfo> GetListBillInfo (int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.BillInfo WHERE id = " + id );

            foreach (DataRow item in data.Rows)
            {
                BillInfo Info = new BillInfo(item);

                listBillInfo.Add(Info);

            }

            return listBillInfo;
        }

        public void InsertBillInfo(int idBill, int idPerfume, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("EXEC USP_InsertBillInfo @idBill , @idPerfume , @count", new object[] { idBill, idPerfume, count } );

        }

        public void DeleteBillInfoByPerfumeID(int id)
        { 
            DataTable data = DataProvider.Instance.ExecuteQuery("DELETE dbo.BillInfo WHERE idPerfume = " + id);

        }
        
        public void DeleteBillInfoByCategoryID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("DELETE dbo.BillInfo WHERE idCategory = " + id);

        }

    }
}
