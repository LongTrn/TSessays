using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2.DataTO;

namespace Test2.DataAO
{
    public class PerfumeDAO
    {
        private static PerfumeDAO instance;

        public static PerfumeDAO Instance
        {
            get { if (instance == null) instance = new PerfumeDAO(); return PerfumeDAO.instance; }
            private set { PerfumeDAO.instance = value; }

        }

        private PerfumeDAO() {; }

        public List<Perfume> GetPerfumeByCategoryID(int id)
        {
            List<Perfume> list = new List<Perfume>();

            string qr = "SELECT * FROM dbo.Perfume WHERE idCategory = " + id;
             
            DataTable data = DataProvider.Instance.ExecuteQuery(qr);

            foreach (DataRow item in data.Rows)
            {
                Perfume perfume = new Perfume(item);

                list.Add(perfume);

            }

            return list;

        }

        public List<Perfume> GetListPerfume()
        {
            List<Perfume> list = new List<Perfume>();

            string qr = "SELECT * FROM dbo.Perfume";

            DataTable data = DataProvider.Instance.ExecuteQuery(qr);

            foreach (DataRow item in data.Rows)
            {
                Perfume perfume = new Perfume(item);

                list.Add(perfume);

            }

            return list;

        }

        public List<Perfume> SearchPerfumeByName(string name)
        {
            List<Perfume> list = new List<Perfume>();

            string qr = string.Format("SELECT * FROM dbo.Perfume WHERE dbo.fuConvertToUnsign1(PerfumeName) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);

            DataTable data = DataProvider.Instance.ExecuteQuery(qr);

            foreach (DataRow item in data.Rows)
            {
                Perfume perfume = new Perfume(item);

                list.Add(perfume);

            }

            return list;

        }

        public bool InsertPerfume(string name, int id, float price)
        {
            string qr = string.Format("INSERT dbo.Perfume ( PerfumeName, idCategory, price) VALUES(N'{0}',{1},{2})",name, id, price);

            int result = DataProvider.Instance.ExecuteNonQuery(qr);

            return result > 0;

        }

        public bool UpdatePerfume(int id, string name, int idCategory, float price)
        {
            string qr = string.Format("UPDATE dbo.Perfume SET PerfumeName = N'{0}',idCategory = {1}, price = {2} WHERE id = {3}", name, idCategory, price, id);

            int result = DataProvider.Instance.ExecuteNonQuery(qr);

            return result > 0;

        }

        public bool DeletePerfume(int id)
        {
            BillInfoDAO.Instance.DeleteBillInfoByPerfumeID(id);

            string qr = string.Format("DELETE dbo.Perfume WHERE id = {0}", id);

            int result = DataProvider.Instance.ExecuteNonQuery(qr);

            return result > 0;

        }

    }
}
