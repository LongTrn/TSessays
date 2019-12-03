using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.DataTO
{
    public class Perfume
    {
        public Perfume (int id, string name, int idctgr, float price)
        {
            this.ID = id;
            this.PerfumeName = name;
            this.IdCategory = idctgr;
            this.Price = price;
            
        }

        public Perfume(DataRow r)
        {
            this.ID = (int)r["id"];
            this.PerfumeName = r["PerfumeName"].ToString();
            this.IdCategory = (int)r["idCategory"];
            this.Price = (float)Convert.ToDouble(r["price"].ToString());

        }

        private int iD;

        private string perfumeName;

        private int idCategory;

        private float price;

        public int ID { get => iD; set => iD = value; }
        
        public string PerfumeName { get => perfumeName; set => perfumeName = value; }
        
        public int IdCategory { get => idCategory; set => idCategory = value; }
        
        public float Price { get => price; set => price = value; }
    }
}
