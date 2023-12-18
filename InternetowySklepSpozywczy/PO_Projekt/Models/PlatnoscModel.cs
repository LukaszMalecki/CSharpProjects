using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PO_Projekt.Models
{
    public class PlatnoscModel
    {
        public int Id { get; set; }
        [Required]
        public double Kwota { get; set; }
        [Required]
        public DateTime Data { get; set; }
        public string NrKonta { get; set; }
        [Required]
        public int PodstawowyUzytkownikId { get; set; }
        public PodstawowyUzytkownikModel PodstawowyUzytkownik { get; set; }
    }
}
