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
            if(Session["Handlevogn"] == null)
            {
                Handlevogn hv = new Handlevogn();
                hv.Varer = new List<HandlevognItem>();
                Session["Handlevogn"] = hv;
            }
            if (Session["LoggetInn"] == null)
            {
                Session["LoggetInn"] = false;
                ViewBag.Innlogget = false;
            }
            else
            {
                
                ViewBag.Innlogget = (bool)Session["LoggetInn"];
            }
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

        public ActionResult AddToCart(int vareID)
        {
            
            if(Session["Handlevogn"] == null)
            {
                Session["Handlevogn"] = new Handlevogn();
            }
            var vareDb = new VareBLL();
            Vare vare = vareDb.getSingleGoods(vareID);

            Handlevogn handlevogn = ((Handlevogn)Session["Handlevogn"]);
            if (handlevogn.varer != null)
            {
                var funnetVare = handlevogn.varer.FirstOrDefault(h => h.Vare.id == vareID);
                if (funnetVare == null)
                {
                    HandlevognItem hv1 = new HandlevognItem();
                    hv1.Vare = vare;
                    hv1.Antall = 1;
                    handlevogn.varer.Add(hv1);
                }
                else
                {
                    funnetVare.Antall++;
                }

            }
            else
            {
                HandlevognItem hv1 = new HandlevognItem();
                hv1.Vare = vare;
                hv1.Antall = 1;
                handlevogn.varer.Add(hv1);
            }
            return View(((Handlevogn)Session["Handlevogn"]).varer);
            
        }
    }
}