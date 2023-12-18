using System;
using System.ComponentModel.DataAnnotations;

namespace PO_Projekt.Models
{
    public class ReklamacjaModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Tresc { get; set; }
        [Required]
        public DateTime DataWystawienia { get; set; }
        [Required]
        public int ZamowienieId { get; set; }
        public ZamowienieModel Zamowienie { get; set; }
        [Required]
        public int PracownikId { get; set; }
        public PracownikModel Pracownik { get; set; }
    }
}
