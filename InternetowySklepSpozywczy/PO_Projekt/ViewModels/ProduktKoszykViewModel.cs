using PO_Projekt.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace PO_Projekt.ViewModels
{
    public class ProduktKoszykViewModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        [Required]
        public double CenaBazowa { get; set; }
        [Required]
        public double Opodatkowanie { get; set; }
        public double? RabatStalegoKlienta { get; set; }
        [Required]
        public double Ilosc { get; set; }
        [Required]
        public int MiaraId { get; set; }
        [Required]
        public MiaraModel Miara { get; set; }
        [Required]
        public int IloscNaStanie { get; set; }
        public double? Ocena { get; set; }

        public int IloscWKoszyku { get; set; }

        public double? SumarycznaCena { get; set; }

        public ProduktKoszykViewModel() { }

        public ProduktKoszykViewModel(ProduktModel produktModel, int iloscWKoszyku)
        {
            ID = produktModel.ID;
            Nazwa = produktModel.Nazwa;
            Opis = produktModel.Opis;
            CenaBazowa = produktModel.CenaBazowa;
            Opodatkowanie = produktModel.Opodatkowanie;
            RabatStalegoKlienta = produktModel.RabatStalegoKlienta;
            Ilosc = produktModel.Ilosc;
            MiaraId = produktModel.MiaraId;
            Miara = produktModel.Miara;
            IloscNaStanie = produktModel.IloscNaStanie;
            Ocena = produktModel.Ocena;
            IloscWKoszyku = iloscWKoszyku;

            SumarycznaCena = CenaBazowa * (1 + Opodatkowanie) * (1 - RabatStalegoKlienta) * IloscWKoszyku;
        }
    }
}
