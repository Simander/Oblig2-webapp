using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDALBLL.Models;

namespace WebStoreDALBLL.DAL
{
    public class KundeDAL
    {
        public static byte[] hashPword(string innPword)
        {
            var algoritme = System.Security.Cryptography.SHA512.Create();
            byte[] inndata, utdata;
            inndata = System.Text.Encoding.ASCII.GetBytes(innPword);
            utdata = algoritme.ComputeHash(inndata);
            return utdata;


        }
        public List<Kunde> getAll()
        {
            var db = new DBContext();
            List<Kunde> allCustomers = db.Kunder.Select(k => new Kunde()
            {
                id = k.ID,
                fornavn = k.Fornavn,
                etternavn = k.Etternavn,
                adresse = k.Adresse,
                postnr = k.Postnr,
                poststed = k.Poststeder.Poststed,
                telefonnr = k.Telefonnr,
                epost = k.Epost

            }).ToList();
            return allCustomers;
        }

        public bool insertCustomer(Kunde innKunde)
        {
            var nyKunde = new Kunder()
            {
                Fornavn = innKunde.fornavn,
                Etternavn = innKunde.etternavn,
                Adresse = innKunde.adresse,
                Postnr = innKunde.postnr,
                Telefonnr = innKunde.telefonnr,
                Epost = innKunde.epost,
                Password = hashPword(innKunde.passord)
            };

            var db = new DBContext();
            try
            {
                var postnrExists = db.Poststeder.Find(innKunde.postnr);
                if(postnrExists == null)
                {
                    var nyttPoststed = new Poststeder()
                    {
                        Postnr = innKunde.postnr,
                        Poststed = innKunde.poststed
                    };
                    nyKunde.Poststeder = nyttPoststed;
                }
                else
                {
                    nyKunde.Poststeder = postnrExists;
                }
                db.Kunder.Add(nyKunde);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                return false;
            }
        }
        public bool changeCustomer(int id, Kunde innKunde)
        {
            var db = new DBContext();
            try
            {
                Kunder endreKunde = db.Kunder.Find(id);
                endreKunde.Fornavn = innKunde.fornavn;
                endreKunde.Etternavn = innKunde.etternavn;
                endreKunde.Telefonnr = innKunde.telefonnr;
                endreKunde.Adresse = innKunde.adresse;
                if(endreKunde.Postnr!= innKunde.postnr)
                {
                    //Postnummer er endret, må sjekke om det nye eksisterer
                    Poststeder existPoststed = db.Poststeder.FirstOrDefault(p => p.Postnr == innKunde.postnr);
                    if(existPoststed == null)
                    {
                        //poststedet eksisterer ikke
                        var nyttPoststed = new Poststeder()
                        {
                            Postnr = innKunde.postnr,
                            Poststed = innKunde.poststed
                        };
                        db.Poststeder.Add(nyttPoststed);
                        endreKunde.Poststeder = nyttPoststed;
                    }
                    else
                    {
                        //poststedet med det nye postnr existerer, endre bare postnr.
                        endreKunde.Poststeder = existPoststed;
                    }
                };
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool deleteCustomer(int slettId)
        {
            var db = new DBContext();
            try
            {
                Kunder slettKunde = db.Kunder.Find(slettId);
                db.Kunder.Remove(slettKunde);
                db.SaveChanges();
                return true;
            }
            catch(Exception feil)
            {
                return false;
            }
        }

        public Kunde getSingleCustomer(int id)
        {
            var db = new DBContext();
            var enDbKunde = db.Kunder.Find(id);
            
            if(enDbKunde == null)
            {
                return null;
            }
            else
            {
                var utKunde = new Kunde()
                {
                    id = enDbKunde.ID,
                    fornavn = enDbKunde.Fornavn,
                    etternavn = enDbKunde.Etternavn,
                    telefonnr = enDbKunde.Telefonnr,
                    adresse = enDbKunde.Adresse,
                    postnr = enDbKunde.Postnr,
                    poststed = enDbKunde.Poststeder.Poststed,
                    epost = enDbKunde.Epost,
                    hashPassord = enDbKunde.Password
                };
                return utKunde;
            }
        }

        public Kunde getSingleCustomerByEmail(string email)
        {
            var db = new DBContext();
            var enDbKunde = db.Kunder.FirstOrDefault(k => k.Epost.Equals(email));

            if (enDbKunde == null)
            {
                return null;
            }
            else
            {
                var utKunde = new Kunde()
                {
                    id = enDbKunde.ID,
                    fornavn = enDbKunde.Fornavn,
                    etternavn = enDbKunde.Etternavn,
                    telefonnr = enDbKunde.Telefonnr,
                    adresse = enDbKunde.Adresse,
                    postnr = enDbKunde.Postnr,
                    poststed = enDbKunde.Poststeder.Poststed,
                    epost = enDbKunde.Epost,
                    hashPassord = enDbKunde.Password
                };
                return utKunde;
            }
        }

    }
}
