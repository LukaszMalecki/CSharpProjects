using System.ComponentModel.DataAnnotations;
namespace PO_Projekt.Models
{
    public class MiaraModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Nazwa { get; set; }
    }
} 
