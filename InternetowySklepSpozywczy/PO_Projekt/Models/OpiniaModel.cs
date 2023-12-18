using System;
using System.ComponentModel.DataAnnotations;

namespace PO_Projekt.Models
{
    public class OpiniaModel
    {
        public int ID { get; set; }
        [Required]
        public double Punktacja { get; set; }
        [Required]
        public string Opis { get; set; }
        public int? LiczbaPolubien { get; set; }
        [Required]
        public bool CzyPrzydatna { get; set; }
        [Required]
        public int ProduktId { get; set; }
        public ProduktModel Produkt { get; set; }
        [Required]
        public int PodstawowyUzytkownikId { get; set; }
        public PodstawowyUzytkownikModel PodstawowyUzytkownik { get; set; }

        public string liczbaPolubienDisplay()
        {
            return LiczbaPolubien is { } x ? x.ToString() : "0";
        }
        public string PunktacjaDisplayFun()
        {
            return Punktacja.ToString("0") + "/5";
        }
        public string przydatnaDisplayFun()
        {
            if( CzyPrzydatna)
            {
                return "Przydatna opinia!";
            }
            else
            {
                return "";
            }
        }
        public static double PunktacjaRadio(string intRadio)
        {

            if (String.IsNullOrEmpty(intRadio))
            {
                return errorPunktacja;
            }
            double wartosc = (double)Int32.Parse(intRadio);
            
            if( PunktacjaPoprawna(wartosc))
            {
                return wartosc;
            }

            return -errorPunktacja;
        }
        public static readonly double errorPunktacja = -1.0;

        public static bool PunktacjaPoprawna(double wartosc)
        {
            return wartosc >= 1 && wartosc <= 5 && wartosc % 1 == 0;
        }

    }
}
