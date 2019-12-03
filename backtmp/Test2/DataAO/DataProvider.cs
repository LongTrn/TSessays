using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.DataAO
{
    class DataProvider
    {
        private static DataProvider instance; //packed with Ctrl + R + E

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() {;}

        private string cnstr = @"Data Source=XPS2284\TRNLONGJ;Initial Catalog=QuanLyCuaHangMyPham;Integrated Security=True";
        
        public DataTable ExecuteQuery(string query,object[] para = null) 
        {
            DataTable data = new DataTable();

            using (SqlConnection cn = new SqlConnection(cnstr))
            {
                cn.Open();

                SqlCommand cm = new SqlCommand(query, cn);

                if (para != null)
                {
                    string[] listPara = query.Split(' ');

                    int i = 0;

                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        { 
                            cm.Parameters.AddWithValue(item, para[i]);
                            i++;
                        }
                    }

                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(cm))
                {
                    adapter.Fill(data);

                }
                cn.Close();
            }

            return data;
        }

        public int ExecuteNonQuery(string query, object[] para = null)
        {
            int data = 0;

            using (SqlConnection cn = new SqlConnection(cnstr))
            {
                cn.Open();

                SqlCommand cm = new SqlCommand(query, cn);

                if (para != null)
                {
                    string[] listPara = query.Split(' ');

                    int i = 0;

                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            cm.Parameters.AddWithValue(item, para[i]);
                            i++;
                        }
                    }

                }
                data = cm.ExecuteNonQuery();

                cn.Close();
            }

            return data;
        }

        public object ExecuteScalar(string query, object[] para = null)
        {
            object data = 0;

            using (SqlConnection cn = new SqlConnection(cnstr))
            {
                cn.Open();

                SqlCommand cm = new SqlCommand(query, cn);

                if (para != null)
                {
                    string[] listPara = query.Split(' ');

                    int i = 0;

                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            cm.Parameters.AddWithValue(item, para[i]);
                            i++;
                        }
                    }

                }
                data = cm.ExecuteScalar();

                cn.Close();
            }

            return data;
        }


    }
}
