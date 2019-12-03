using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.DataTO
{
    public class ClassMember
    {
        public ClassMember(int id, string name, float point, int type)
        {
            this.Id = id;
            this.NameClass = name;
            this.Point = point;
            this.DiscountRate = type;
        }

        public ClassMember(DataRow r)
        {
            this.Id = (int)r["id"];
            this.NameClass = r["nameClass"].ToString();
            this.Point = (float)Convert.ToDouble(r["point"].ToString());
            this.DiscountRate = (int)r["discountRate"];

        }

        private int id;

        private string nameClass;

        private float point;

        private int discountRate;

        public int Id { get => id; set => id = value; }
        
        public string NameClass { get => nameClass; set => nameClass = value; }
        
        public float Point { get => point; set => point = value; }
        
        public int DiscountRate { get => discountRate; set => discountRate = value; }
    }
}
