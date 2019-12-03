using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.DataTO
{
    public class Category
    {
        public Category(int id, string name)
        {
            this.ID = id;
            this.NameCategory = name;

        }

        public Category(DataRow r)
        {
            this.ID = (int)r["id"];
            this.NameCategory = r["nameCategory"].ToString();

        }

        private int iD;

        private string nameCategory;
                
        public int ID { get => iD; set => iD = value; }
        
        public string NameCategory { get => nameCategory; set => nameCategory = value; }
    }
}
