using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace PO_Projekt.Models
{
    public class ProduktModel
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
        public MiaraModel Miara { get; set; }
        [Required]
        public int IloscNaStanie { get; set; }
        public double? Ocena { get; set; }

        

        public double CenaPodatekFun()
        {
            return CenaBazowa * (1 + Opodatkowanie);
        }

        public string CenaPodatekDisplayFun()
        {
            //return CenaPodatekFun().ToString("0.00") + "zł";
            return WalutaDisplay(CenaPodatekFun());
        }
        public double CenaPodatekZa1Fun()
        {
            return CenaPodatekFun() / Ilosc;
        }
        public string CenaPodatekZa1DisplayFun()
        {
            //return CenaPodatekZa1Fun().ToString("0.00") + "zł/" + Miara.Nazwa;
            return WalutaMiaraDisplay(CenaPodatekZa1Fun());
        }
        public double CenaRabatFun()
        {
            if( RabatStalegoKlienta == null)
            {
                return CenaPodatekFun();
            }
            double wynik = CenaBazowa * (1 + Opodatkowanie - (double)RabatStalegoKlienta);
            if( wynik < 0)
            {
                return 0;
            }
            else
            {
                return wynik;
            }
        }

        public string CenaRabatDisplayFun()
        {
            //return CenaRabatFun().ToString("0.00") + "zł";
            return WalutaDisplay(CenaRabatFun());
        }
        public double CenaRabatZa1Fun()
        {
            return CenaRabatFun() / Ilosc;
        }
        public string CenaRabatZa1DisplayFun()
        {
            //return CenaRabatZa1Fun().ToString("0.00") + "zł/" + Miara.Nazwa;
            return WalutaMiaraDisplay(CenaRabatZa1Fun());
        }
        public string RabatDisplayFun()
        {
            return RabatStalegoKlienta is { } x ? "-" + (x * 100.0).ToString("0") + "%" : "brak";
        }

        public static string WalutaDisplay( double cena)
        {
            return cena.ToString("0.00") + "zł";
        }
        public string WalutaMiaraDisplay(double cena)
        {
            return cena.ToString("0.00") + "zł/" + Miara.Nazwa;
        }

        public string OcenaDisplayFun()
        {
            return Ocena is { } x ? (x).ToString("0.00") + "/5" : "?/5";
        }
        /*
        [NotMapped]
        public double CenaPodatek => CenaBazowa * (1 + Opodatkowanie);

        [NotMapped]
        public string CenaPodatekDisplay => CenaPodatek.ToString("0.00")+"zł";

        [NotMapped]
        public double CenaZa1 => CenaPodatek/Ilosc;
        [NotMapped]
        public string CenaZa1Display => CenaZa1.ToString("0.00") + "zł/" + Miara.Nazwa;
        [NotMapped]
        public string RabatDisplay => RabatStalegoKlienta is { } x ? "-" + (x*100.0).ToString("0") + "%" : "brak";
        [NotMapped]
        public string OcenaDisplay => Ocena is { } x ? (x).ToString("0.00") + "/5" : "?/5";
        */
        [NotMapped]
        public string ImageView =>
            "/images/" + ID.ToString() + ".jpg";

        /*
         * public string ImageView =>
            File.Exists("/images/" + Nazwa + "_" + ID.ToString() + ".jpg") ? "/images/"+Nazwa+"_"+ID.ToString()+".jpg" : "/images/defaultImage.jpg";
         */
        [NotMapped]
        public string RelativePath => "~/wwwroot/images/" + Nazwa + "_" + ID.ToString() + ".jpg";


    }
}
