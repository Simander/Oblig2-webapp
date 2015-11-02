using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDALBLL.Models;
using WebStoreDALBLL.DAL;
namespace WebStoreDALBLL.BLL
{
    public class AdminBLL
    {

        public List<AdminBruker> getAll()
        {

            var AdminDAL = new AdminDAL();
            List<AdminBruker> allAdmins = AdminDAL.getAll();
            return allAdmins;
        }

        public bool insertAdmin(AdminBruker innAdmin)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.insertAdmin(innAdmin);
        }
        public bool changeAdmin(int id, AdminBruker innAdmin)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.changeAdmin(id, innAdmin);
        }

        public bool deleteAdmin(int slettId)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.deleteAdmin(slettId);
        }

        public AdminBruker getSingleAdmin(int id)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.getSingleAdmin(id);
        }

        public AdminBruker getSingleAdminByEmail(string email)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.getSingleAdminByEmail(email);
        }
        
    }
}
