using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDALBLL.Models;
using DAL;

namespace WebStoreDALBLL.DAL
{
    public class AdminDAL
    {
        public static byte[] hashPword(string innPword)
        {
            var algoritme = System.Security.Cryptography.SHA512.Create();
            byte[] inndata, utdata;
            inndata = System.Text.Encoding.ASCII.GetBytes(innPword);
            utdata = algoritme.ComputeHash(inndata);
            return utdata;


        }
        public List<AdminBruker> getAll()
        {
           
            var db = new DBContext();
            List<AdminBruker> allAdmins = db.AdminBrukere.Select(k => new AdminBruker()
            {
                id = k.ID,
                fornavn = k.Fornavn,
                etternavn = k.Etternavn,
                telefonnr = k.Telefonnr,
                epost = k.Epost,     
                superadmin = k.Superadmin 
            }).ToList();
            return allAdmins;
        }

        public bool insertAdmin(AdminBruker innAdmin)
        {
            var nyAdmin = new AdminBrukere()
            {
                Fornavn = innAdmin.fornavn,
                Etternavn = innAdmin.etternavn,
                Telefonnr = innAdmin.telefonnr,
                Epost = innAdmin.epost,
                Password = hashPword(innAdmin.passord),
                Superadmin = innAdmin.superadmin
            };
            try { 
                var db = new DBContext();
                db.AdminBrukere.Add(nyAdmin);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                return false;
            }
        }
        public bool changeAdmin(int id, AdminBruker innAdmin)
        {
            var db = new DBContext();
            try
            {
                AdminBrukere endreAdmin = db.AdminBrukere.Find(id);
                endreAdmin.Fornavn = innAdmin.fornavn;
                endreAdmin.Etternavn = innAdmin.etternavn;
                endreAdmin.Telefonnr = innAdmin.telefonnr;
                endreAdmin.Epost = innAdmin.epost;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deleteAdmin(int slettId)
        {
            var db = new DBContext();
            try
            {
                AdminBrukere slettAdmin = db.AdminBrukere.Find(slettId);
                db.AdminBrukere.Remove(slettAdmin);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                return false;
            }
        }

        public AdminBruker getSingleAdmin(int id)
        {
            var db = new DBContext();
            var enDbAdmin = db.Kunder.Find(id);

            if (enDbAdmin == null)
            {
                return null;
            }
            else
            {
                var utAdmin = new AdminBruker()
                {
                    id = enDbAdmin.ID,
                    fornavn = enDbAdmin.Fornavn,
                    etternavn = enDbAdmin.Etternavn,
                    telefonnr = enDbAdmin.Telefonnr,
                    epost = enDbAdmin.Epost,
                    hashPassword = enDbAdmin.Password
                };
                return utAdmin;
            }
        }

        public AdminBruker getSingleAdminByEmail(string email)
        {
            var db = new DBContext();
            var enDbAdmin = db.AdminBrukere.FirstOrDefault();

            if (enDbAdmin == null)
            {
                return null;
            }
            else
            {
                var utAdmin = new AdminBruker()
                {
                    id = enDbAdmin.ID,
                    fornavn = enDbAdmin.Fornavn,
                    etternavn = enDbAdmin.Etternavn,
                    telefonnr = enDbAdmin.Telefonnr,
                    epost = enDbAdmin.Epost,
                    hashPassword = enDbAdmin.Password
                };
                return utAdmin;
            }
        }
    }
}
