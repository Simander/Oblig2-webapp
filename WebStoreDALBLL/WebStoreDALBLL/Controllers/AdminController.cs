using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreDALBLL.BLL;
using WebStoreDALBLL.Models;

namespace WebStoreDALBLL.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                Session["Admin"] = false;
                ViewBag.Admin = false;
                return RedirectToAction("LoggInn");
            }
            else
            {
                ViewBag.Admin = (bool)Session["Admin"];
            }
            return View();     
        }

        public ActionResult LoggInn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoggInn(FormCollection innListe)
        {
            var brukernavn = innListe["Epost"];
            var passord = KundeBLL.hashPword(innListe["Passord"]);
            try
            {
                var db = new AdminBLL();
                var funnetBruker = db.getSingleAdminByEmail(brukernavn);


                if (funnetBruker == null)
                {
                    return View();
                }
                else
                {
                    if (funnetBruker.hashPassword.SequenceEqual(passord))
                    {
                        Session["Admin"] = true;
                      
                        return RedirectToAction("Index");
                        // return "Kundenr: " + ((Kunde)Session["Bruker"]).KundeNR + " | Brukernavn: " + ((Kunde)Session["Bruker"]).Epost + " er logget inn!";
                    }
                    //return "funnetBruker.Passord: " + funnetBruker.Passord + " | innskrevet hash: " + passord;
                }

                return View();
            }
            catch (Exception feil)
            {

                return View(feil);
            }
        }

        //KUNDER

        public ActionResult ListCustomers()
        {
            var kundeDb = new KundeBLL();
            List<Kunde> alleKunder = kundeDb.getAll();
            return View(alleKunder);
        }

        public ActionResult EditCustomer(int id)
        {
            var kundeDb = new KundeBLL();
            Kunde enKunde = kundeDb.getSingleCustomer(id);
            return View(enKunde);
        }

        [HttpPost]
        public ActionResult EditCustomer(int id, Kunde endreKunde)
        {

            if (ModelState.IsValid)
            {
                var kundeDb = new KundeBLL();
                bool endringOK = kundeDb.changeCustomer(id, endreKunde);
                if (endringOK)
                {
                    return RedirectToAction("ListCustomers");
                }
            }
            return View();
        }

        public ActionResult DeleteCustomer(int id)
        {
            var kundeDb = new KundeBLL();
            Kunde enKunde = kundeDb.getSingleCustomer(id);
            return View(enKunde);
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int id, Kunde slettKunde)
        {
            var kundeDb = new KundeBLL();
            bool slettOK = kundeDb.deleteCustomer(id);
            if (slettOK)
            {
                return RedirectToAction("ListCustomers");
            }
            return View();
        }

        public ActionResult DetailsCustomer(int id)
        {
            var kundeDb = new KundeBLL();
            Kunde enKunde = kundeDb.getSingleCustomer(id);
            return View(enKunde);
        }

        //VARER

        public ActionResult ListGoods()
        {
            var vareDb = new VareBLL();
            List<Vare> alleVarer = vareDb.getAll();
            return View(alleVarer);
        }

        public ActionResult AddNewGoods()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewGoods(Vare innVare)
        {
            if (ModelState.IsValid)
            {
                var vareDb = new VareBLL();
                bool insertOK = vareDb.insertVare(innVare);
                if (insertOK)
                {
                    return RedirectToAction("ListGoods");
                }
            }
            return View();
        }

        public ActionResult EditGoods(int id)
        {
            var vareDb = new VareBLL();
            Vare enVare = vareDb.getSingleGoods(id);
            return View(enVare);
        }

        [HttpPost]
        public ActionResult EditGoods(int id, Vare endreVare)
        {

            if (ModelState.IsValid)
            {
                var vareDb = new VareBLL();
                bool endringOK = vareDb.changeGoods(id, endreVare);
                if (endringOK)
                {
                    return RedirectToAction("ListGoods");
                }
            }
            return View();
        }
        public ActionResult DeleteGoods(int id)
        {
            var vareDb = new VareBLL();
            Vare enVare = vareDb.getSingleGoods(id);
            return View(enVare);
        }

        [HttpPost]
        public ActionResult DeleteGoods(int id, Vare slettVare)
        {
            var vareDb = new VareBLL();
            bool slettOK = vareDb.deleteGoods(id);
            if (slettOK)
            {
                return RedirectToAction("ListCustomers");
            }
            return View();
        }

        public ActionResult DetailsGoods(int id)
        {
            var vareDb = new VareBLL();
            Vare enVare = vareDb.getSingleGoods(id);
            return View(enVare);
        }

        public ActionResult ListCategories()
        {
            var vareDb = new VareBLL();
            List<Kategori> alleVarer = vareDb.getAllCategories();
            return View(alleVarer);
        }

        public ActionResult ListByCategory(string kat)
        {
            var vareDb = new VareBLL();
            List<Vare> alleVarer = vareDb.getAllByCategory(kat);
            return View(alleVarer);
        }


        public ActionResult ListPoststeder()
        {
            var PoststedDb = new PoststedBLL();
            List<Poststed> allePoststeder = PoststedDb.getAll();
            return View(allePoststeder);
        }

       

        public ActionResult DeletePoststed(string id)
        {
            var PoststedDb = new PoststedBLL();
            Poststed ettPoststed = PoststedDb.getSinglePoststed(id);
            return View(ettPoststed);
        }

        [HttpPost]
        public ActionResult DeletePoststed(string id, Poststed slettPoststed)
        {
            var PoststedDb = new PoststedBLL();
            bool slettOK = PoststedDb.deletePoststed(id);
            if (slettOK)
            {
                return RedirectToAction("ListPoststeder");
            }
            return View();
        }

        public ActionResult DetailsPoststeder(string id)
        {
            var PoststedDb = new PoststedBLL();
            Poststed ettPoststed = PoststedDb.getSinglePoststed(id);
            return View(ettPoststed);
        }

    }
}