using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebStoreDALBLL.BLL;
using WebStoreDALBLL.Models;

namespace WebStoreDALBLL.Controllers
{
    public class AdminController : Controller
    {


        public bool loginCheck()
        {
            if (Session["Admin"] == null)
            {
                Session["Admin"] = false;
                ViewBag.Admin = false;
                return false;
            }
            else
            {
                ViewBag.Admin = (bool)Session["Admin"];
                return true;
            }
           

        }


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
         
           
            if (brukernavn.Equals("superuser@superuser.admin") && passord.SequenceEqual(KundeBLL.hashPword("superuser")))
            {
                Session["Admin"] = true;
                Session["Superuser"] = true;
  
               return RedirectToAction("Index");
            }
           
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
            if (loginCheck() == false){ return RedirectToAction("LoggInn"); }
            var kundeDb = new KundeBLL();
            List<Kunde> alleKunder = kundeDb.getAll();
            return View(alleKunder);
        }

        public ActionResult EditCustomer(int id)
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var kundeDb = new KundeBLL();
            Kunde enKunde = kundeDb.getSingleCustomer(id);
            return View(enKunde);
        }

        [HttpPost]
        public ActionResult EditCustomer(int id, Kunde endreKunde)
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
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

            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var kundeDb = new KundeBLL();
            Kunde enKunde = kundeDb.getSingleCustomer(id);
            return View(enKunde);
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int id, Kunde slettKunde)
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
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
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var kundeDb = new KundeBLL();
            Kunde enKunde = kundeDb.getSingleCustomer(id);
            return View(enKunde);
        }

        //VARER

        public ActionResult ListGoods()
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var vareDb = new VareBLL();
            List<Vare> alleVarer = vareDb.getAll();
            return View(alleVarer);
        }

        public ActionResult AddNewGoods()
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            return View();
        }

        [HttpPost]
        public ActionResult AddNewGoods(Vare innVare)
        {
            
            
         
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
    
          
            if (ModelState.IsValid)
            {
                
                
               // byte[] image2Store = BildeMetoder.ImageToByteArray(innListe["image"]);
               // innVare.bilde = image2Store;
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
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var vareDb = new VareBLL();
            Vare enVare = vareDb.getSingleGoods(id);
            return View(enVare);
        }

        [HttpPost]
        public ActionResult EditGoods(int id, Vare endreVare)
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
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
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var vareDb = new VareBLL();
            Vare enVare = vareDb.getSingleGoods(id);
            return View(enVare);
        }

        [HttpPost]
        public ActionResult DeleteGoods(int id, Vare slettVare)
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
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
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var vareDb = new VareBLL();
            Vare enVare = vareDb.getSingleGoods(id);
           
            return View(enVare);
        }

        public ActionResult ListCategories()
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var vareDb = new VareBLL();
            List<Kategori> alleVarer = vareDb.getAllCategories();
            return View(alleVarer);
        }

        public ActionResult ListByCategory(string kat)
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var vareDb = new VareBLL();
            List<Vare> alleVarer = vareDb.getAllByCategory(kat);
            return View(alleVarer);
        }

        ///POSTSTEDER////
        public ActionResult ListPoststeder()
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var PoststedDb = new PoststedBLL();
            List<Poststed> allePoststeder = PoststedDb.getAll();
            return View(allePoststeder);
        }

       

        public ActionResult DeletePoststed(string id)
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var PoststedDb = new PoststedBLL();
            Poststed ettPoststed = PoststedDb.getSinglePoststed(id);
            return View(ettPoststed);
        }

        [HttpPost]
        public ActionResult DeletePoststed(string id, Poststed slettPoststed)
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
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
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var PoststedDb = new PoststedBLL();
            Poststed ettPoststed = PoststedDb.getSinglePoststed(id);
            return View(ettPoststed);
        }
        ///ADMINBRUKERE////
        public ActionResult ListAdmins()
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var adminDb = new AdminBLL();
            List<AdminBruker> alleKunder = adminDb.getAll();
            return View(alleKunder);
        }

        public ActionResult EditAdmin(int id)
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var adminDb = new AdminBLL();
            AdminBruker enAdmin = adminDb.getSingleAdmin(id);
            return View(enAdmin);
        }

        [HttpPost]
        public ActionResult EditAdmin(int id, AdminBruker endreAdmin)
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            if (ModelState.IsValid)
            {
                var adminDb = new AdminBLL();
                bool endringOK = adminDb.changeAdmin(id, endreAdmin);
                if (endringOK)
                {
                    return RedirectToAction("ListAdmins");
                }
            }
            return View();
        }

        public ActionResult DeleteAdmin(int id)
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var adminDb = new AdminBLL();
            AdminBruker enAdmin = adminDb.getSingleAdmin(id);
            return View(enAdmin);
        }

        [HttpPost]
        public ActionResult DeleteAdmin(int id, AdminBruker slettKunde)
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var adminDb = new AdminBLL();
            bool slettOK = adminDb.deleteAdmin(id);
            if (slettOK)
            {
                return RedirectToAction("ListAdmins");
            }
            return View();
        }

        public ActionResult DetailsAdmin(int id)
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            var adminDb = new AdminBLL();
            AdminBruker enAdmin = adminDb.getSingleAdmin(id);
            return View(enAdmin);
        }

        public ActionResult RegisterNewAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterNewAdmin(AdminBruker innAdmin)
        {
            if (loginCheck() == false) { return RedirectToAction("LoggInn"); }
            if (ModelState.IsValid)
            {
                var adminDb = new AdminBLL();
                bool insertOK = adminDb.insertAdmin(innAdmin);
                if (insertOK)
                {
                    return RedirectToAction("ListAdmins");
                }
            }
            return View();
        }

        
    }
}