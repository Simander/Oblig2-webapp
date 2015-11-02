using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDALBLL.Models;
using WebStoreDALBLL.DAL;

namespace WebStoreDALBLL.BLL
{
    public class PoststedBLL
    {

        public List<Poststed> getAll()
        {
            var PoststedDAL = new PoststedDAL();
            List<Poststed> allPoststeder = PoststedDAL.getAll();
            return allPoststeder;
        }

        public bool insertPoststed(Poststed innPoststed)
        {
            var PoststedDAL = new PoststedDAL();

            return PoststedDAL.insertPoststed(innPoststed);
        }


        public bool deletePoststed(string slettId)
        {
            var PoststedDAL = new PoststedDAL();
            return PoststedDAL.deletePoststed(slettId);
        }

        public Poststed getSinglePoststed(string id)
        {
            var PoststedDAL = new PoststedDAL();
            return PoststedDAL.getSinglePoststed(id);

        }
    }
   }

