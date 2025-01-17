﻿using System;
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


        // GET: Bruker
       


        // GET: /Bruker/LoggUt
        public ActionResult LoggUt()
        {
            Session["LoggetInn"] = false;
            Session["Bruker"] = null;
            Handlevogn hv = (Handlevogn)Session["Handlevogn"];
            hv.kunde = null;
            return RedirectToAction("Index", "Home");
        }

        // GET: /Bruker/LoggInn
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
                var db = new KundeBLL();
                var funnetBruker = db.getSingleCustomerByEmail(brukernavn);
                
               
                    if (funnetBruker == null)
                    {
                        return View();
                    }
                    else
                    {
                        if (funnetBruker.hashPassord.SequenceEqual(passord))
                        {
                            Session["LoggetInn"] = true;
                            Session["Bruker"] = funnetBruker;
                        Handlevogn handlevogna = (Handlevogn)Session["Handlevogn"];
                        handlevogna.kunde = funnetBruker;
                            return RedirectToAction("Index", "Home");
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

    }
}