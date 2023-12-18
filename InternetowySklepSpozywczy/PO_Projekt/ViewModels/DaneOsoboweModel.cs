using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PO_Projekt.Models;

namespace PO_Projekt.ViewModels
{
    public class DaneOsoboweModel
    {
        //Nie dajemy id, aby uniemozliwic zmiane w querry stringu
        //public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        //public string Email { get; set; }
        public string NIP { get; set; }

        //Trzeba zadbać by użytkownik miał opcję niewprowadzania adresu - np: jesli zadne pole z adresem nie jest wypelnione, 
        //to git przechodzi walidacja etc, jak jest chociaż jedno, to musi spełnić wymagania dla AdresModel albo usuwać wszystkie
        //public int? AdresId { get; set; }
        public string? Ulica { get; set; }
        public string? NrDomu { get; set; }
        public string? NrMieszkania { get; set; }
        public string? KodPocztowy { get; set; }
        public string? Miasto { get; set; }

        public void setOsobowe(DaneUzytkownikaModel daneUzytkownikaModel)
        {
            Imie = daneUzytkownikaModel.Imie;
            Nazwisko = daneUzytkownikaModel.Nazwisko;
            //Email = daneUzytkownikaModel.Email;
            NIP = daneUzytkownikaModel.NIP;
        }
        public void setAdres( AdresModel adresModel)
        {
            //AdresId = adresModel.Id;
            Ulica = adresModel.Ulica;
            NrDomu = adresModel.NrDomu;
            NrMieszkania = adresModel.NrMieszkania;
            KodPocztowy = adresModel.KodPocztowy;
            Miasto = adresModel.Miasto;
        }
        public bool czyAdresPoprawny()
        {
            if ( String.IsNullOrEmpty(NrDomu) || String.IsNullOrEmpty(KodPocztowy) || String.IsNullOrEmpty(Miasto))
            {
                return false;
            }

            return true;
        }
        public bool czyAdresPusty()
        {
            return String.IsNullOrEmpty(NrDomu) && String.IsNullOrEmpty(KodPocztowy) && String.IsNullOrEmpty(Miasto) && 
                String.IsNullOrEmpty(NrMieszkania) && String.IsNullOrEmpty(Ulica);
        }
        /*
         * Niepotrzebne, bo moge sam sprawdzic w bazie, czy istnieje powiazany adres
         * public bool czyAdresNowy()
        {
            return AdresId == null || (int)AdresId < 0;
        }*/

        public AdresModel getAdres()
        {
            if (!czyAdresPoprawny())
            {
                return null;
            }
            var retAdres = new AdresModel();
            retAdres.Ulica = Ulica;
            retAdres.NrDomu = NrDomu;
            retAdres.NrMieszkania = NrMieszkania;
            retAdres.KodPocztowy = KodPocztowy;
            retAdres.Miasto = Miasto;

            return retAdres;
        }
        public bool setOtherAdres( AdresModel targetAdres)
        {
            if (!czyAdresPoprawny())
            {
                return false;
            }

            targetAdres.Ulica = Ulica;
            targetAdres.NrDomu = NrDomu;
            targetAdres.NrMieszkania = NrMieszkania;
            targetAdres.KodPocztowy = KodPocztowy;
            targetAdres.Miasto = Miasto;

            return true;
        }

        public bool equalsAdres(AdresModel adresModel)
        {
            if( adresModel == null)
            {
                return czyAdresPusty();
            }
            return (Ulica == adresModel.Ulica || Ulica.Equals(adresModel.Ulica)) &&
                NrDomu.Equals(adresModel.NrDomu) &&
                (NrMieszkania == adresModel.NrMieszkania || NrMieszkania.Equals(adresModel.NrMieszkania)) &&
                KodPocztowy.Equals(adresModel.KodPocztowy) &&
                Miasto.Equals(adresModel.Miasto);
        }
        public bool equalsOsobowe(DaneUzytkownikaModel daneUzytkownikaModel)
        {
            return (Imie == daneUzytkownikaModel.Imie || Imie.Equals(daneUzytkownikaModel.Imie)) &&
                (Nazwisko == daneUzytkownikaModel.Nazwisko || Nazwisko.Equals(daneUzytkownikaModel.Nazwisko)) &&
                (NIP == daneUzytkownikaModel.NIP || NIP.Equals(daneUzytkownikaModel.NIP));
        }
    }
}
