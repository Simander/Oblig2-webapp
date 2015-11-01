using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebStoreDALBLL.Models
{
    public class Handlevogn
    {
        public int id;
        public Kunde kunde;
        public List<HandlevognItem> varer { get; set; }
        public decimal prisTotal { get; set; }

        public void calculateSumTotal()
        {
            prisTotal = 0;
           foreach(HandlevognItem v in varer){
                prisTotal += (decimal.Parse(v.Vare.pris))*v.Antall;
            }
        }
    }

    public class HandlevognItem
    {
        public Vare Vare { get; set; }
        public int Antall { get; set; }
    }
}
