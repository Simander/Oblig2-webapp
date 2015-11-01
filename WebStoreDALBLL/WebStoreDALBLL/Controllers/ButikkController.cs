using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreDALBLL.BLL;
using WebStoreDALBLL.Models;

namespace WebStoreDALBLL.Controllers
{
    public class ButikkController : Controller
    {
        // GET: Butikk
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BrowseCategories()
        {
            var vareDb = new VareBLL();
            List<Kategori> alleVarer = vareDb.getAllCategories();
            return View(alleVarer);
        }
    }
}