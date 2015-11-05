using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebStoreDALBLL.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetImageFilePath()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetImageFilePath(FormCollection innListe)
        {
            String filePath = innListe["image"];
            byte[] image4Storing = ImageToByteArray(filePath);
            return View();
        }

       

        public static byte[] ImageToByteArray(string imagefilePath)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(imagefilePath);
            byte[] imageByte = ImageToByteArraybyMemoryStream(image);
            return imageByte;
        }

        private static byte[] ImageToByteArraybyMemoryStream(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        public static Image ByteArrayToImagebyMemoryStream(byte[] imageByte)
        {
            MemoryStream ms = new MemoryStream(imageByte);
            Image image = Image.FromStream(ms);
            return image;
        }
    }
}