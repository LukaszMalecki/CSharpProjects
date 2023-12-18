using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PO_Projekt.Models
{
    public class StatusZamowieniaModel
    {
        public int Id { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
