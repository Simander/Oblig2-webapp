using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDALBLL.Models;
namespace WebStoreDALBLL.Models
{

    public class Bestilling
    {
        public int id;
        public string dato { get; set; }
        public int kundeID { get; set; }
        public Kunde kunde;
        public List<Ordrelinje> varer { get; set; }
        public decimal prisTotal { get; set; }

        
    }

    public class Ordrelinje
    {
        public int id { get; set; }
        public Vare Vare { get; set; }
        public int Antall { get; set; }
    }
}
