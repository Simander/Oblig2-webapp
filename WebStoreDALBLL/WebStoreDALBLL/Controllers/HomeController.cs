using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreDALBLL.BLL;
using WebStoreDALBLL.Models;

namespace WebStoreDALBLL.Controllers
{
    public class HomeController : Controller
    {
        // GET: Butikk
        public ActionResult Index()
        {
           string test = "test";
            return View();
        }

        public ActionResult BrowseCategories()
        {
            var vareDb = new VareBLL();
            List<Kategori> alleVarer = vareDb.getAllCategories();
            return View(alleVarer);
        }

        public ActionResult Browse(string kategori)
        {
            //return kategori;
            var vareDb = new VareBLL();
            List<Vare> alleVarer = vareDb.getAllByCategory(kategori);
            return View(alleVarer);
        }

        public ActionResult VareDetail(int vareID)
        {
            var vareDb = new VareBLL();
            Vare enVare = vareDb.getSingleGoods(vareID);
            return View(enVare);
        }
    }
}