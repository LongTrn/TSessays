using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test2.DataTO;
using Menu = Test2.DataTO.Menu;

namespace Test2.DataAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return MenuDAO.instance; }
            private set { MenuDAO.instance = value; }
        
        }

        private MenuDAO() {; }

        public List<Menu> GetListMenuByOrder(int id )
        {
            List<Menu> listMenu = new List<Menu>();

            string qr = "SELECT p.PerfumeName , bi.count, p.price, (p.price * bi.count) AS totalPrices FROM dbo.BillInfo AS bi, dbo.Bill as b, dbo.Perfume AS p WHERE bi.idBill = b.id AND bi.idPerfume = p.id AND b.idOrder = " + id + " AND b.status = 0 ";

            DataTable data = DataProvider.Instance.ExecuteQuery(qr);

            foreach(DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);

                listMenu.Add(menu);

            }

            return listMenu;

        }
        public int GetMaxIDMenu()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(id) FROM dbo.Orders");

            }
            catch
            {
                return 1;

            }

        }                            

    }
}
