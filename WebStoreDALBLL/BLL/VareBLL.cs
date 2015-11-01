using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDALBLL.DAL;
using WebStoreDALBLL.Models;

namespace WebStoreDALBLL.BLL
{
    public class VareBLL
    {
       
        public List<Vare> getAll()
        {
            var VareDAL = new VareDAL();
            List<Vare> allGoods = VareDAL.getAll();
            return allGoods;
        }

        public List<Vare> getAllByCategory(string kat)
        {
            var VareDAL = new VareDAL();
            List<Vare> allGoods = VareDAL.getAllByCategory(kat);
            return allGoods;
        }

        public List<Kategori> getAllCategories()
        {
            var VareDAL = new VareDAL();
            List<Kategori> allCategories = VareDAL.getAllCategories();
            return allCategories;

        }
        public List<Produsent> getAllProducers()
        {
            var VareDAL = new VareDAL();
            List<Produsent> allProducers = VareDAL.getAllProducers();
            return allProducers;
        }

        public bool insertKategori(String kategori)
        {

            var VareDAL = new VareDAL();
            return VareDAL.insertKategori(kategori);
        }
        public bool insertProdusent(String prodNavn)
        {
            var VareDAL = new VareDAL();
            return VareDAL.insertProducer(prodNavn);
        }

        public bool insertVare(Vare innVare)
        {
            var VareDAL = new VareDAL();
            return VareDAL.insertVare(innVare);
        }
        
        public bool changeGoods(int id, Vare innVare)
        {
            var VareDAL = new VareDAL();
            return VareDAL.changeGoods(id, innVare);
        }
        
        public bool deleteGoods(int slettId)
        {
            var VareDAL = new VareDAL();
            return VareDAL.deleteGoods(slettId);
        }

        public Vare getSingleGoods(int id)
        {
            var VareDAL = new VareDAL();
            return VareDAL.getSingleGoods(id);
        }


    }
}
