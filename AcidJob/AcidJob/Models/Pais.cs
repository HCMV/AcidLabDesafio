using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcidJob.Models
{
    public class Myview
    {
        public Adminregion Adminregion { get; set; }
        public Datos Datos { get; set; }
        public Fechas Fechas { get; set; }
    }

    
    public class Adminregion
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string Id { get; set; }
        public string Iso2Code { get; set; }
        public string Value { get; set; }
        public IEnumerable<SelectListItem> Ids { get; set; }


    }



    public class Fechas
    {

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Fecha Inicio")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Fecha Termino")]
        public DateTime Fecha2 { get; set; }
    }




    public class Datos
    {
        [JsonProperty("countryiso3code")]
        public string Countryiso3Code { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("obs_status")]
        public string ObsStatus { get; set; }

        [JsonProperty("decimal")]
        public long Decimal { get; set; }


    }

    [Table("Paises")]
    public class BdPais
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] // Primary key
        public string ID { get; set; }
        public string Iso2Code { get; set; }
        public string Value { get; set; }
    }
}
