using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.DataTO
{
    public class BillInfo
    {
        public BillInfo(int id, int idBill, int idPerfume, int count)
        {
            this.ID = id;
            this.IdBill= idBill;
            this.IdPerfume= idPerfume;
            this.Count= count;

        }

        public BillInfo(DataRow r)
        {
            this.ID = (int)r["id"];
            this.IdBill = (int)r["idBill"];
            this.IdPerfume= (int)r["idPerfume"];
            this.Count= (int)r["count"];

        }

        private int iD;

        private int idBill;

        private int idPerfume;

        private int count;

        public int ID { get => iD; set => iD = value; }
        
        public int IdBill { get => idBill; set => idBill = value; }
        
        public int IdPerfume { get => idPerfume; set => idPerfume = value; }

        public int Count { get => count; set => count = value; }

    }
}
