using DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDALBLL.Models;

namespace WebStoreDALBLL.DAL
{
    public class VareDAL
    {
        public static byte[] hashPword(string innPword)
        {
            var algoritme = System.Security.Cryptography.SHA512.Create();
            byte[] inndata, utdata;
            inndata = System.Text.Encoding.ASCII.GetBytes(innPword);
            utdata = algoritme.ComputeHash(inndata);
            return utdata;


        }
        public List<Vare> getAll()
        {
            var db = new DBContext();
            List<Vare> allGoods = db.Varer.Select(k => new Vare()
            {
                id = k.ID,
                navn = k.Varenavn,
                pris = k.Pris,
                kvantitet = k.Kvantitet,
                beskrivelse = k.Beskrivelse,
                produsent = k.Produsenter.Navn,
                kategori = k.Kategorier.Navn

            }).ToList();
            return allGoods;
        }

        public List<Vare> getAllByCategory(string kat)
        {
            var db = new DBContext();
            List<Vare> allGoods = db.Varer.Select(k => new Vare()
            {
                id = k.ID,
                navn = k.Varenavn,
                pris = k.Pris,
                kvantitet = k.Kvantitet,
                beskrivelse = k.Beskrivelse,
                produsent = k.Produsenter.Navn,
                kategori = k.Kategorier.Navn

            }).Where(k => k.kategori.ToUpper().Equals(kat.ToUpper())).ToList();
            return allGoods;
        }

        public List<Kategori> getAllCategories()
        {
            try
            {
                var db = new DBContext();
                List<Kategori> categories = db.Kategorier.Select(item => new Kategori()
                    {
                        id = item.ID,
                        navn = item.Navn
                    }).ToList();
                    return categories;
            }
            catch(Exception feil)
            {
                writeToFile(feil);
                return null;
            }

        }
        public List<Produsent> getAllProducers()
        {
            try
            {
                var db = new DBContext();
                List<Produsent> produsenter = db.Produsenter.Select(item => new Produsent()
                {
                    id = item.ID,
                    navn = item.Navn
                }).ToList();
                return produsenter;
            }
            catch(Exception feil)
            {
                writeToFile(feil);
                return null;
            }
        }

        public bool insertKategori(String kategori)
        {
           
            var nyKategori = new Kategorier()
            {
                Navn = kategori
            };
            try
            {
                var db = new DBContext();
                var funnetKategori = db.Kategorier.FirstOrDefault(k => k.Navn == kategori);
                if(funnetKategori == null)
                {
                    db.Kategorier.Add(nyKategori);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception feil)
            {
                writeToFile(feil);
                return false;
            }
        }
        public bool insertProducer(String prodNavn)
        {

            var nyProdusent = new Produsenter()
            {
                Navn = prodNavn
            };
            try
            {
                var db = new DBContext();
                var funnetProdusent = db.Produsenter.FirstOrDefault(k => k.Navn == prodNavn);
                if (funnetProdusent == null)
                {
                    db.Produsenter.Add(nyProdusent);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception feil)
            {
                writeToFile(feil);
                return false;
            }
        }

        public bool insertVare(Vare innVare)
        {
            var nyVare = new Varer()
            {
                Varenavn = innVare.navn,
                Pris = innVare.pris,
                Kvantitet = innVare.kvantitet,
                Beskrivelse = innVare.beskrivelse
    
            };


            var db = new DBContext();
            try
            {
                var kategoriExists = db.Kategorier.FirstOrDefault(k => k.Navn == innVare.kategori);
                if(kategoriExists == null)
                {
                    Kategorier nyKat = new Kategorier()
                    {
                        Navn = innVare.kategori
                    };
                    nyVare.Kategorier = nyKat;
                    nyVare.KategoriId = nyKat.ID;
                    db.Kategorier.Add(nyKat);

                }
                else
                {
                    nyVare.Kategorier = kategoriExists;
                    nyVare.ProdusentId = kategoriExists.ID;
                }
                var produsentExists = db.Produsenter.FirstOrDefault(k => k.Navn == innVare.produsent);
                if (produsentExists == null)
                {
                    Produsenter nyProd = new Produsenter()
                    {
                        Navn = innVare.produsent
                    };
                    nyVare.Produsenter = nyProd;
                    nyVare.ProdusentId = nyProd.ID;
                    db.Produsenter.Add(nyProd);
                }
                else
                {
                    nyVare.Produsenter = produsentExists;
                    nyVare.ProdusentId = produsentExists.ID;
                }

                
                db.Varer.Add(nyVare);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                writeToFile(feil);
                return false;
            }
        }
        public bool changeGoods(int id, Vare innVare)
        {
            var db = new DBContext();
            try
            {
                Varer endreVare = db.Varer.Find(id);
                endreVare.Varenavn = innVare.navn;
                endreVare.Pris = innVare.pris;
                endreVare.Kvantitet = innVare.kvantitet;
                endreVare.Beskrivelse = innVare.beskrivelse;
                db.SaveChanges();
                return true;
            }
            catch(Exception feil)
            {
                writeToFile(feil);
                return false;
            }
        }
        
        public bool deleteGoods(int slettId)
        {
            var db = new DBContext();
            try
            {
                Varer slettVare = db.Varer.Find(slettId);
                db.Varer.Remove(slettVare);
                db.SaveChanges();
                return true;
            }
            catch(Exception feil)
            {
                writeToFile(feil);
                return false;
            }
        }

        public Vare getSingleGoods(int id)
        {
            var db = new DBContext();
            var enDbVare = db.Varer.Find(id);
            
            if(enDbVare == null)
            {
                return null;
            }
            else
            {
                var utVare = new Vare()
                {
                    id = enDbVare.ID,
                    navn = enDbVare.Varenavn,
                    pris = enDbVare.Pris,
                    kategori = enDbVare.Kategorier.Navn,
                    produsent = enDbVare.Produsenter.Navn,
                    beskrivelse = enDbVare.Beskrivelse,
                    kvantitet = enDbVare.Kvantitet
                   
                };
                return utVare;
            }
        }

        private void writeToFile(Exception e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"WebStoreDALBLL_errorlog.txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine("////////////////// " + DateTime.Now.ToString() + " //////////////////");               
                    writer.WriteLine("Message: " + e.Message + Environment.NewLine
                        + "Stacktrace: " + e.StackTrace + Environment.NewLine);
                }
            }
            catch (IOException ioe)
            {
                Debug.WriteLine(ioe.Message);
            }
            catch (UnauthorizedAccessException uae)
            {
                Debug.WriteLine(uae.Message);
            }
        }

    }

}
