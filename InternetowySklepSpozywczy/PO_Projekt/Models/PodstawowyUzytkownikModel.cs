using System.ComponentModel.DataAnnotations;

namespace PO_Projekt.Models
{
    public class PodstawowyUzytkownikModel
    {
        [Key]
        public int ID { get; set; }
        public double SumaWydatkow { get; set; }
        [Required]
        public bool MozeOplacacFaktury { get; set; }
        [Required]
        public int DaneUzytkownikaId { get; set; }
        public DaneUzytkownikaModel DaneUzytkownika { get; set; }
        [Required]
        public int StatusKlientaId { get; set; }
        public StatusKlientaModel StatusKlienta { get; set; }
    }
}
