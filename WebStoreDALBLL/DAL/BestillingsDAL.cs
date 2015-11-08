using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using WebStoreDALBLL.Models;

using DAL;

namespace WebStoreDALBLL.DAL
{
    public class BestillingsDAL
    {
        public bool insertBestilling(Handlevogn hv)
        {
            var db = new DBContext();
            try
            {
                List<Ordrelinjer> ordrelinjer = new List<Ordrelinjer>();
                hv.calculateSumTotal();
                DateTime now = DateTime.Now;
                Kunder kunden = db.Kunder.FirstOrDefault(k => k.ID == hv.kunde.id);
                var nyBestilling = new Bestillinger()
                {
                    KundeId = kunden.ID,
                    Kunder = kunden,
                    PrisTotal = hv.prisTotal,
                    OrderDate = now,
                    Ordrelinjer = ordrelinjer
                    
                };
                db.Bestillinger.Add(nyBestilling);
                db.SaveChanges();
           //     var bestilingur = db.Bestillinger.LastOrDefault(k => k.KundeId == kunden.ID);
                //List<Ordrelinjer> nyOrdrelinjer = new List<Ordrelinjer>();
              
                foreach (HandlevognItem h in hv.varer)
                {
                    Ordrelinjer tmpOrdrelinje = new Ordrelinjer()
                    {
                       
                        ProduktId = h.Vare.id,
                        Vare = db.Varer.FirstOrDefault(k => k.ID == h.Vare.id),
                        Kvantitet = h.Antall,
                        
                        Bestilling = nyBestilling,
                        Bestillingsnr = nyBestilling.ID
                    };

                    nyBestilling.Ordrelinjer.Add(tmpOrdrelinje);
                   
                }
              
               
              
                //  nyBestilling.Ordrelinjer = nyOrdrelinjer;


                db.SaveChanges();

               
              
                return true;
            }
            catch (Exception feil)
            {
                return false;
            }
        }

        public List<Bestilling> getAll()
        {
            
            CultureInfo culture = new CultureInfo("de-DE");
            var db = new DBContext();
            List<Bestilling> allBestillinger = db.Bestillinger.Select(k => new Bestilling()
            {
                id = k.ID,
                dato = (k.OrderDate).ToString(),              
                kundeID = k.KundeId,
                prisTotal = k.PrisTotal,

            }).ToList();
            return allBestillinger;
        }

        public List<Bestilling> getAllOrders(int innKID)
        {

           
            var db = new DBContext();
            List<Bestilling> orders = getAll();
            List<Bestilling> utOrders = new List<Bestilling>();
            foreach(Bestilling best in orders)
            {
                if(best.kundeID == innKID)
                {
                    utOrders.Add(best);
                }
            }

            return utOrders;
        }

        public List<Ordrelinje> getAllOrdrelinjer()
        {

            CultureInfo culture = new CultureInfo("de-DE");
            var db = new DBContext();
            List<Ordrelinje> allOrdrelinjer = db.Ordrelinjer.Select(k => new Ordrelinje()
            {
                id = k.ID,
                bestillingsnr = k.Bestillingsnr,
                Vare = new Vare(){
                    id = k.Vare.ID,
                    navn = k.Vare.Varenavn,
                    pris = k.Vare.Pris,
                    kategori = k.Vare.Kategorier.Navn,
                    kvantitet = k.Vare.Kvantitet,
                    beskrivelse = k.Vare.Beskrivelse,
                    photoURL = k.Vare.PhotoURL,
                    produsent = k.Vare.Produsenter.Navn
                    


                },
                Antall = k.Kvantitet
            }).ToList();
            return allOrdrelinjer;
        }

        public Bestilling getSingleBestilling(int id)
        {
            var culture = new CultureInfo("de-DE");
            var db = new DBContext();
            KundeDAL kd = new KundeDAL();
           
            var enDbBestilling = db.Bestillinger.Find(id);

            if (enDbBestilling == null)
            {
                return null;
            }
            else
            {
                var utBestilling = new Bestilling()
                {
                    id = enDbBestilling.ID,
                    dato = (enDbBestilling.OrderDate).ToString(),
                    kundeID = enDbBestilling.KundeId,
                    kunde = kd.getSingleCustomer(enDbBestilling.KundeId),
                    varer = getOrdrelinjer(id),
                    prisTotal = enDbBestilling.PrisTotal,
                
                };
                return utBestilling;
            }
        }

		public List<Ordrelinje> getOrdrelinjer(int bestillingsID)
        {
            var db = new DBContext();
            var enDbBestilling = db.Bestillinger.Find(bestillingsID);
            var vareDAL = new VareDAL();
            var alleOrdrelinjer = db.Ordrelinjer;
            if (enDbBestilling == null)
            {
                return null;
            }
            else
            {
                List<Ordrelinje> getOrdrelinjer = getAllOrdrelinjer();
                List<Ordrelinje> utOrdrelinjer = new List<Ordrelinje>();

                foreach (Ordrelinje or in getOrdrelinjer)
                {
                   if(or.bestillingsnr == bestillingsID)
                        utOrdrelinjer.Add(or);
                    }

                return utOrdrelinjer;
            }
				
               
            
        }
        

    }
}