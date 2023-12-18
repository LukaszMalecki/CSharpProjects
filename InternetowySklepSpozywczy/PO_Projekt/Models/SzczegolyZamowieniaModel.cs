using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PO_Projekt.Models
{
    public class SzczegolyZamowieniaModel
    {
        [Key]
        public int ZamowienieID { get; set; }
        public ZamowienieModel Zamowienie { get; set; }
        [Key]
        public int ProduktID { get; set; }
        public ProduktModel Produkt { get; set; }
        [Required]
        public int IloscProd { get; set; }
    }
}
