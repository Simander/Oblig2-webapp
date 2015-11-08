using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // denne må være med!
using System.Linq;
using System.Web;

namespace WebStoreDALBLL.Models {
    public class Kunde
    {
        // dette er både en domenemodell og en view-modell
        public int id { get; set; }

        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        public string fornavn { get; set; }

        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        public string etternavn { get; set; }

        [Display(Name = "Telefonnr")]
        [Required(ErrorMessage = "Telefonnr må oppgis")]
        [RegularExpression("^[0-9]{8,12}$", ErrorMessage = "Ugyldig telefonnummer")]
        public string telefonnr { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Adressen må oppgis")]
        public string adresse { get; set; }

        [Display(Name = "Postnr")]
        [Required(ErrorMessage = "Postnr må oppgis")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Postnr må være 4 siffer")]
        public string postnr { get; set; }

        [Display(Name = "Poststed")]
        [Required(ErrorMessage = "Poststed må oppgis")]
        public string poststed { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email må oppgis")]
        [RegularExpression(@"(^[a-zA-ZæÆøØåÅ][\w\.-]*[a-zA-Z0-9æÆøØåÅ]@[a-zA-ZæÆøØåÅ][\w\.-]*[a-zA-Z0-9æÆøØåÅ]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$)", ErrorMessage = "Ugyldig email")]
        public string epost { get; set; }

        [Display(Name = "Passord")]
        [RegularExpression(@"(^[a-zA-Z0-9æÆøØåÅ]{8,}$)", ErrorMessage = "Passordet må være 8 tegn langt ")]
        [Required(ErrorMessage = "Passord må oppgis")]
        public string passord { get; set; }
        public byte[] hashPassord { get; set; }
    }
}
