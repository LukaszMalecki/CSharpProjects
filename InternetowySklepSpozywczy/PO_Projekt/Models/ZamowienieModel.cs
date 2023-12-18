using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PO_Projekt.Models
{
    public class ZamowienieModel
    {
        public int Id { get; set; }
        [Required]
        public double Cena { get; set; }
        [Required]
        public int StatusZamowieniaId { get; set; }
        public StatusZamowieniaModel StatusZamowienia { get; set; }
        [Required]
        public int PodstawowyUzytkownikId { get; set; }
        public PodstawowyUzytkownikModel PodstawowyUzytkownik { get; set; }
        public int? PracownikId { get; set; }
        public PracownikModel Pracownik { get; set; }
    }
}
