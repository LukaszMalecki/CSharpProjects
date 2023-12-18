using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PO_Projekt.Models
{
    public class AdresModel
    {
        public int Id { get; set; }
        public string? Ulica { get; set; }
        [Required]
        [MinLength(1)]
        public string NrDomu { get; set; }
        public string? NrMieszkania { get; set; }
        [Required]
        [MinLength(1)]
        public string KodPocztowy { get; set; }
        [Required]
        [MinLength(1)]
        public string Miasto { get; set; }

    }
}
