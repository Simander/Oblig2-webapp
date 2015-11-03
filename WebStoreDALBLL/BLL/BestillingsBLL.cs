using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDALBLL.DAL;
using WebStoreDALBLL.Models;

namespace WebStoreDALBLL.BLL
{
    public class BestillingsBLL
    {
        public bool insertBestilling(Handlevogn hv)
        {
            var BestillingsDAL = new BestillingsDAL();
            return BestillingsDAL.insertBestilling(hv);
        }

        public List<Bestilling> getAll()
        {
            var BestillingsDAL = new BestillingsDAL();
            List<Bestilling> allBestillinger = BestillingsDAL.getAll();
            return allBestillinger;
        }

        public Bestilling getSingleBestilling(int id)
        {
            var BestillingsDAL = new BestillingsDAL();
            return BestillingsDAL.getSingleBestilling(id);
        }

        public List<Ordrelinje> getOrdrelinjer(int kundeID)
        {
            var BestillingsDAL = new BestillingsDAL();
            List<Ordrelinje> allOrdrelinjer = BestillingsDAL.getOrdrelinjer(kundeID);
            return allOrdrelinjer;
        
        }
    }
}
