using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTravels.NET_2017.Models
{
    public class TravelModel
    {
        [Editable(false)]
        public int Id{ get; set; }

        [Editable(false)]
        public int AgencyID { get; set; }

        [Display(Name = "Nombre del Titular")]
        [Required(ErrorMessage ="Por favor ingresa el nombre del titular")]
        public string Titular { get; set; }

        [Display(Name="Fecha Inicial del Periodo Vacacional")]
        [Required(ErrorMessage = "Por favor ingresa la Fecha Inicial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Fecha Final del Periodo Vacacional")]
        [Required(ErrorMessage = "Por favor ingresa la Fecha Final")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Costo Total de la Reservación")]
        [Required(ErrorMessage = "Por favor ingresa la Fecha Final")]
        public decimal TotalCost { get; set; }

        [Display(Name = "Descripción del Viaje")]
        public string Description { get; set; }

        [Display(Name = "Notas del Viaje")]
        public string Notes { get; set; }
    }
}