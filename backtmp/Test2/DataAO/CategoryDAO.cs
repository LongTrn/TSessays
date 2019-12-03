using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2.DataTO;

namespace Test2.DataAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO(); return CategoryDAO.instance; }
            private set { CategoryDAO.instance = value; }

        }

        private CategoryDAO() {; }

        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();

            string qr = "SELECT * FROM dbo.PerfumeCategory";

            DataTable data = DataProvider.Instance.ExecuteQuery(qr);
            
            foreach (DataRow item in data.Rows)
            {
                Category ctgr = new Category(item);

                list.Add(ctgr);

            }

            return list;

        }

        public Category GetCategoryByID(int id)
        {
            Category category = null;

            string qr = "SELECT * FROM dbo.PerfumeCategory WHERE id = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(qr);

            foreach (DataRow item in data.Rows)
            {
                category = new Category(item);

                return category;

            }

            return category;

        }

        public List<Category> SearchCategoryByName(string name)
        {
            List<Category> list = new List<Category>();

            string qr = string.Format("SELECT * FROM dbo.PerfumeCategory WHERE dbo.fuConvertToUnsign1(NameCategory) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);

            DataTable data = DataProvider.Instance.ExecuteQuery(qr);

            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);

                list.Add(category);

            }

            return list;

        }


        public bool InsertCategory(string name)
        {
            string qr = string.Format("INSERT dbo.PerfumeCategory ( NameCategory) VALUES(N'{0}')", name);

            int result = DataProvider.Instance.ExecuteNonQuery(qr);

            return result > 0;

        }

        public bool UpdateCategory(int id, string name)
        {
            string qr = string.Format("UPDATE dbo.PerfumeCategory SET NameCategory = N'{0}' WHERE id = {1}", name, id);

            int result = DataProvider.Instance.ExecuteNonQuery(qr);

            return result > 0;

        }

        public bool DeleteCategory(int id)
        {
            BillInfoDAO.Instance.DeleteBillInfoByPerfumeID(id);

            string qr = string.Format("DELETE dbo.PerfumeCategory WHERE id = {0}", id);

            int result = DataProvider.Instance.ExecuteNonQuery(qr);

            return result > 0;

        }

    }
}
