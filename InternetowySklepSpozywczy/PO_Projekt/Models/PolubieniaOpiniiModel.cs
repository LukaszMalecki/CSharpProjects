using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_Projekt.Models
{
    public class PolubieniaOpiniiModel
    {
        [Key, Column(Order = 0)]
        public int OpiniaId { get; set; }
        public OpiniaModel Opinia { get; set; }
        [Key, Column(Order = 1)]
        public int PodstawowyUzytkownikId { get; set; }
        public PodstawowyUzytkownikModel PodstawowyUzytkownik { get; set; }
    }
}
