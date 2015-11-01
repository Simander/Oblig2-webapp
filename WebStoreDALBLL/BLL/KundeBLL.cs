using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDALBLL.DAL;
using WebStoreDALBLL.Models;

namespace WebStoreDALBLL.BLL
{
    public class KundeBLL
    {
        public List<Kunde> getAll()
        {
           var KundeDAL = new  KundeDAL();
            List<Kunde> allCustomers = KundeDAL.getAll();
            return allCustomers;
        }
        public bool insertCustomer(Kunde innKunde)
        {
            var KundeDAL = new KundeDAL();
            return KundeDAL.insertCustomer(innKunde);
        }

        public bool changeCustomer(int id, Kunde innKunde)
        {
            var KundeDAL = new KundeDAL();
            return KundeDAL.changeCustomer(id, innKunde);
        }

        public bool deleteCustomer(int slettId)
        {
            var KundeDAL = new KundeDAL();
            return KundeDAL.deleteCustomer(slettId);
        }

        public  Kunde getSingleCustomer(int id)
        {
            var KundeDAL = new KundeDAL();
            return KundeDAL.getSingleCustomer(id);
        }
    }
}
