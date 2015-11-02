using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebStoreDALBLL.Models
{
        public class AdminBruker
        {
      
            public int id { get; set; }


            [Display(Name = "Fornavn")]
            [Required(ErrorMessage = "Fornavn må oppgis")]
            public string fornavn { get; set; }

            [Display(Name = "Etternavn")]
            [Required(ErrorMessage = "Etternavn må oppgis")]
            public string etternavn { get; set; }

            [Display(Name = "Telefonnr")]
            [Required(ErrorMessage = "Telefonnr må oppgis")]
            [RegularExpression("^[0-9]{8,12}$")]
            public string telefonnr { get; set; }

            [Display(Name = "Email")]
            [Required(ErrorMessage = "Email må oppgis")]
            [RegularExpression(@"(^[a-zA-ZæÆøØåÅ][\w\.-]*[a-zA-Z0-9æÆøØåÅ]@[a-zA-ZæÆøØåÅ][\w\.-]*[a-zA-Z0-9æÆøØåÅ]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$)", ErrorMessage = "Ugyldig email")]
            public string epost { get; set; }

            [Display(Name = "Passord")]
            [RegularExpression(@"(^[a-zA-Z0-9æÆøØåÅ]{8,}$)", ErrorMessage = "Ugyldig passord")]
            [Required(ErrorMessage = "Passord må oppgis")]
            public string passord { get; set; }

            public byte[] hashPassword { get; set; }
            public bool superadmin { get; set; }
    }
    
}
