using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using WebStoreDALBLL.Models;

namespace WebStoreDALBLL.DAL
{
    public class PoststedDAL
    {
       
        public List<Poststed> getAll()
        {
            var db = new DBContext();
            List<Poststed> allPoststeder = db.Poststeder.Select(k => new Poststed()
            {
                postnr = k.Postnr,
                poststed = k.Poststed
              

            }).ToList();
            return allPoststeder;
        }

        public bool insertPoststed(Poststed innPoststed)
        {
            var nyPoststed = new Poststeder()
            {
                Postnr = innPoststed.postnr,
                Poststed = innPoststed.poststed,            
            };

            var db = new DBContext();
            try
            {
                var postnrExists = db.Poststeder.Find(innPoststed.postnr);
                if (postnrExists == null)
                {
                    var nyttPoststed = new Poststeder()
                    {
                        Postnr = innPoststed.postnr,
                        Poststed = innPoststed.poststed
                    };
                   
                }
                
                db.Poststeder.Add(nyPoststed);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                return false;
            }
        }
        

        public bool deletePoststed(string slettId)
        {
            var db = new DBContext();
            Kunder funnetPoststed = db.Kunder.FirstOrDefault(p => p.Postnr == slettId);
            if(funnetPoststed == null) { 
                try
                {
                    Poststeder slettPoststed = db.Poststeder.Find(slettId);
                    db.Poststeder.Remove(slettPoststed);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception feil)
                {
                    return false;
                }
            }
            return false;
        }

        public Poststed getSinglePoststed(string id)
        {
            var db = new DBContext();
            var enDbPoststed = db.Poststeder.Find(id);

            if (enDbPoststed == null)
            {
                return null;
            }
            else
            {
                var utPoststed = new Poststed()
                {
                    postnr = enDbPoststed.Postnr,
                    poststed = enDbPoststed.Poststed
                };
                return utPoststed;
            }
        }
        
    }
}
