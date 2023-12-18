using System.ComponentModel.DataAnnotations;

namespace PO_Projekt.Models
{
    public class PracownikModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int DaneUzytkownikaId { get; set; }
        public DaneUzytkownikaModel DaneUzytkownika { get; set; }
    }
}
