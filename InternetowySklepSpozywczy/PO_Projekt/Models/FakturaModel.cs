using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PO_Projekt.Models
{
    public class FakturaModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime DataWystawienia { get; set; }
        [Required]
        public string NazwaNabywcy { get; set; }
        [Required]
        public string NazwaSprzedawcy { get; set; }
        [Required]
        public string NIP { get; set; }
        [Required]
        public int PracownikId { get; set; }
        public PracownikModel Pracownik { get; set; }
        [Required]
        public int ZamowienieId { get; set; }
        public ZamowienieModel Zamowienie { get; set; }
        [Required]
        public int AdresNabywcyId { get; set; }
        public AdresModel AdresNabywcy { get; set; }
        [Required]
        public int AdresSprzedawcyId { get; set; }
        public AdresModel AdresSprzedawcy { get; set; }
    }
}
