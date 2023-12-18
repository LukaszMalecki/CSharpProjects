using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PO_Projekt.ViewModels;

namespace PO_Projekt.Models
{
    public class DaneUzytkownikaModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Login { get; set; }
        [Required]
        [MinLength(1)]
        public string Haslo { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string NIP { get; set; }
        public int? AdresId { get; set; }
        public AdresModel Adres { get; set; }

        public bool sprawdzHaslo(string podane)
        {
            //w zalozeniu tu ma byc poprawnie sprawdzane haslo
            //dla uproszczenia w testowaniu wykorzystany zostanie jednak equals
            return Haslo.Equals(podane);
        }
        
    }
}
