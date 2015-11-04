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
                var nyBestilling = new Bestillinger()
                {
                    KundeId = hv.kunde.id,
                    Kunder = db.Kunder.FirstOrDefault(k => k.ID == hv.kunde.id)

                };
                
                List<Ordrelinjer> nyOrdrelinjer = new List<Ordrelinjer>();
                foreach (HandlevognItem h in hv.varer)
                {
                    Ordrelinjer tmpOrdrelinje = new Ordrelinjer()
                    {
                        ID = h.id,
                        ProduktId = h.Vare.id,
                        Vare = db.Varer.FirstOrDefault(k => k.ID == h.Vare.id),
                        Kvantitet = h.Antall,
                        Bestillingsnr = nyBestilling.ID,
                        Bestilling = nyBestilling

                    };

                    nyOrdrelinjer.Add(tmpOrdrelinje);
                    db.Ordrelinjer.Add(tmpOrdrelinje);
                }
                nyBestilling.Ordrelinjer = nyOrdrelinjer;
                hv.calculateSumTotal();
                nyBestilling.PrisTotal = hv.prisTotal;
                nyBestilling.OrderDate = DateTime.Now;
                db.Bestillinger.Add(nyBestilling);
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

        public Bestilling getSingleBestilling(int id)
        {
            var culture = new CultureInfo("de-DE");
            var db = new DBContext();
           
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
                    kunde = new Kunde()
                    {
                        id = enDbBestilling.Kunder.ID,
                        fornavn = enDbBestilling.Kunder.Fornavn,
                        etternavn = enDbBestilling.Kunder.Etternavn,
                        telefonnr = enDbBestilling.Kunder.Telefonnr,
                        adresse = enDbBestilling.Kunder.Adresse,
                        postnr = enDbBestilling.Kunder.Postnr,
                        poststed = enDbBestilling.Kunder.Poststeder.Poststed,
                        epost = enDbBestilling.Kunder.Epost,
                        hashPassord = enDbBestilling.Kunder.Password
                    },
                    varer = getOrdrelinjer(id),
                    prisTotal = enDbBestilling.PrisTotal,
                
                };
                return utBestilling;
            }
        }

		public List<Ordrelinje> getOrdrelinjer(int kundeID)
        {
            var db = new DBContext();
            var enDbBestilling = db.Bestillinger.Find(kundeID);

            if (enDbBestilling == null)
            {
                return null;
            }
            else
            {
                List<Ordrelinje> utOrdrelinjer = new List<Ordrelinje>();
                foreach (Ordrelinjer or in enDbBestilling.Ordrelinjer)
                {
                    Ordrelinje tmpOrdreLinje = new Ordrelinje()
                    {
                        id = or.ID,
                        Vare = new Vare()
                        {
                            id = or.ID,
                            navn = or.Vare.Varenavn,
                            pris = or.Vare.Pris,
                            kategori = or.Vare.Kategorier.Navn,
                            produsent = or.Vare.Produsenter.Navn,
                            beskrivelse = or.Vare.Beskrivelse,
                            kvantitet = or.Vare.Kvantitet
                        },
                        Antall = or.Kvantitet
                    };
                    utOrdrelinjer.Add(tmpOrdreLinje);
                }
				
                return utOrdrelinjer;
            }
        }

    }
}