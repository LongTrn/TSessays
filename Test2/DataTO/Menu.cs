using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.DataTO
{
    public class Menu
    {
        public Menu(string name, int count, float price, float total = 0)
        {
            this.PerfumeName = name;
            this.Count = count;
            this.Price = price;
            this.TotalPrices = total;

        }

        public Menu(DataRow r)
        {
            this.PerfumeName = r["PerfumeName"].ToString();
            this.Count = (int)r["count"];
            this.Price = (float)Convert.ToDouble(r["price"].ToString());
            this.TotalPrices = (float)Convert.ToDouble(r["totalPrices"].ToString());
            
        }

        private string perfumeName;

        private int count;

        private float price;

        private float totalPrices;

        public string PerfumeName { get => perfumeName; set => perfumeName = value; }
        
        public int Count { get => count; set => count = value; }
        
        public float Price { get => price; set => price = value; }
        
        public float TotalPrices { get => totalPrices; set => totalPrices = value; }
    }
}
