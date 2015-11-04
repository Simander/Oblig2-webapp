using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreDALBLL.Models;
using WebStoreDALBLL.BLL;

namespace WebStoreDALBLL.Controllers
{
    public class BestillingController : Controller
    {
        // GET: Bestilling
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListAllOrders()
        {
            
            var bestillingsDb = new BestillingsBLL();
            List<Bestilling> alleBestillinger = bestillingsDb.getAll();
            return View(alleBestillinger);
          
        }

        public ActionResult DetailsOrder(int id)
        {
            var BestillingsDb = new BestillingsBLL();
            Bestilling enBestilling = BestillingsDb.getSingleBestilling(id);
         
            return View(enBestilling);

        }

        public ActionResult DetailsOrderLines()//int id)
        {
            return View();

        }

        public ActionResult DetailsOrdreSum(int id)
        {
            return View();
        }

        public ActionResult OrderConfirmation()
        {
            Handlevogn hv = (Handlevogn)Session["Handlevogn"];
            var BestillingsDb = new BestillingsBLL();
            BestillingsDb.insertBestilling(hv);

            return View();
        }

        public ActionResult RegisterOrder()
        {
            if(((bool)Session["LoggetInn"])!= true)
            {
                return RedirectToAction("LoggInn", "Kunde");
            }
            Handlevogn hv = (Handlevogn)Session["Handlevogn"];
           
            return View(hv);
            
        }
    }
}