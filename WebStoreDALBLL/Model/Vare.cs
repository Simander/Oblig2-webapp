using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Drawing;


namespace WebStoreDALBLL.Models
{
    public class Vare
    {
        public int id { get; set; }

        [Display(Name = "Varenavn")]
        [Required(ErrorMessage = "Varenavn må oppgis")]
        public string navn { get; set; }

        [Display(Name = "Varepris")]
        [Required(ErrorMessage = "Prisen må oppgis")]
        public string pris { get; set; }

        [Display(Name = "AntPaaLager")]
        [Required(ErrorMessage = "Antall på lager må oppgis ")]
        public string kvantitet { get; set; }

        [Display(Name = "Varebeskrivelse")]
        [Required(ErrorMessage = "En varebeskrivelse må oppgis")]
        public string beskrivelse { get; set; }

        [Display(Name = "Vareprodusent")]
        [Required(ErrorMessage = "Produsenten av varen må oppgis")]
        public string produsent { get; set; }

        [Display(Name = "Varekategori")]
        [Required(ErrorMessage = "Varen må tilhøre en kategori")]
        public string kategori { get; set; }

        public byte[] bilde { get; set; }
        public string photo { get; set; }
    }
}
