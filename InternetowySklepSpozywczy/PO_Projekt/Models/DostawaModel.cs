using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PO_Projekt.Models
{
    public class DostawaModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime DataDostawy { get; set; }
        [Required]
        public int ZamowienieId { get; set; }
        public ZamowienieModel Zamowienie { get; set; }
        [Required]
        public int AdresId { get; set; }
        public AdresModel Adres { get; set; }
    }
}
