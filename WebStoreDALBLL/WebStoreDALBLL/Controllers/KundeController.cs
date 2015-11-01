using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreDALBLL.BLL;
using WebStoreDALBLL.Models;

namespace WebStoreDALBLL.Controllers
{
    public class KundeController : Controller
    {
        // GET: Kunde
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisterNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterNew(Kunde innKunde)
        {
            if (ModelState.IsValid)
            {
                var kundeDb = new KundeBLL();
                bool insertOK = kundeDb.insertCustomer(innKunde);
                if (insertOK)
                {
                    return RedirectToAction("Liste");
                }
            }
            return View();
        }

        


    }
}