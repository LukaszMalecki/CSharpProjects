using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PO_Projekt.Models;

namespace PO_Projekt.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Student>().HasData(
                new Student()
                {
                    Id = 1,
                    Index = 123456,
                    Name = "Newman",
                    Gender = Gender.Male,
                    BirthDate = new DateTime(1999, 10, 10),
                    DepartmentId = 1,
                    Active = true,
                },
                new Student()
                {
                    Id = 2,
                    Index = 222222,
                    Name = "Yasmin",
                    Gender = Gender.Female,
                    BirthDate = new DateTime(2000, 2, 2),
                    DepartmentId = 2,
                    Active = false,
                }

                );*/
            modelBuilder.Entity<MiaraModel>().HasData(
                new MiaraModel()
                {
                    ID = 1,
                    Nazwa = "kg"
                }
                ,
                new MiaraModel()
                {
                    ID = 2,
                    Nazwa = "g"
                }
                ,
                new MiaraModel()
                {
                    ID = 3,
                    Nazwa = "dag"
                }
                ,
                new MiaraModel()
                {
                    ID = 4,
                    Nazwa = "szt"
                }
                ,
                new MiaraModel()
                {
                    ID = 5,
                    Nazwa = "opak"
                }
                ,
                new MiaraModel()
                {
                    ID = 6,
                    Nazwa = "l"
                }
                ,
                new MiaraModel()
                {
                    ID = 7,
                    Nazwa = "ml"
                }
                );

            modelBuilder.Entity<AdresModel>().HasData(
                new AdresModel()
                {
                    Id = 1,
                    Ulica = "wybrzeże Stanisława Wyspiańskiego",
                    NrDomu = "27",
                    NrMieszkania = null,
                    KodPocztowy = "50-370",
                    Miasto = "Wrocław"
                }
                ,
                new AdresModel()
                {
                    Id = 2,
                    Ulica = "Białoskórnicza",
                    NrDomu = "18",
                    NrMieszkania = "17",
                    KodPocztowy = "50-134",
                    Miasto = "Wrocław"
                }
                ,
                new AdresModel()
                {
                    Id = 3,
                    Ulica = "Najświętszej Marii Panny",
                    NrDomu = "9",
                    NrMieszkania = null,
                    KodPocztowy = "59-220",
                    Miasto = "Legnica"
                }
                ,
                new AdresModel()
                {
                    Id = 4,
                    Ulica = "Najświętszej Marii Panny",
                    NrDomu = "9",
                    NrMieszkania = null,
                    KodPocztowy = "59-220",
                    Miasto = "Legnica"
                }
                ,
                new AdresModel()
                {
                    Id = 5,
                    Ulica = "Jana Kasprowicza",
                    NrDomu = "63",
                    NrMieszkania = null,
                    KodPocztowy = "59-220",
                    Miasto = "Legnica"
                }
                ,
                new AdresModel()
                {
                    Id = 6,
                    Ulica = "Warmińska",
                    NrDomu = "36",
                    NrMieszkania = "3l",
                    KodPocztowy = "59-220",
                    Miasto = "Legnica"
                } 
                );

            modelBuilder.Entity<StatusKlientaModel>().HasData(
                new StatusKlientaModel()
                {
                    ID = 1,
                    Nazwa = "standardowy"
                }
                ,
                new StatusKlientaModel()
                {
                    ID = 2,
                    Nazwa = "stały"
                }
                
                );

            modelBuilder.Entity<ProduktModel>().HasData(
                new ProduktModel()
                {
                    ID = 1,
                    Nazwa = "Agrest polski 250g",
                    Opis = "Przepyszny polski agrest w świetnej cenie z ekologicznych upraw.",
                    CenaBazowa = 4.0, //cena za 0.25kg
                    Opodatkowanie = 0.05,
                    RabatStalegoKlienta = 0.15,
                    Ilosc = 0.25,
                    MiaraId = 1, //0.25kg na paczke
                    IloscNaStanie = 30,
                    Ocena = null
                }
                ,
                new ProduktModel()
                {
                    ID = 2,
                    Nazwa = "Agrest tajlandzki 400g",
                    Opis = "Przepyszny tajlandzki agrest najwyższej jakości o docenianym przez Polaków smaku.",
                    CenaBazowa = 8.80,
                    Opodatkowanie = 0.05,
                    RabatStalegoKlienta = 0.05,
                    Ilosc = 0.4,
                    MiaraId = 1,
                    IloscNaStanie = 20,
                    Ocena = 3.0
                }
                ,
                new ProduktModel()
                {
                    ID = 3,
                    Nazwa = "Borówka amerykańska polska 200g",
                    Opis = "Najlepszej jakości borówki.",
                    CenaBazowa = 8.00,
                    Opodatkowanie = 0.1,
                    RabatStalegoKlienta = 0.20,
                    Ilosc = 0.2,
                    MiaraId = 1,
                    IloscNaStanie = 50,
                    Ocena = 4.0
                }
                ,
                new ProduktModel()
                {
                    ID = 4,
                    Nazwa = "Jabłko papierówka sztuka",
                    Opis = "Świeże papierówki prosto z sadu.",
                    CenaBazowa = 0.49,
                    Opodatkowanie = 0.05,
                    RabatStalegoKlienta = 0.10,
                    Ilosc = 1,
                    MiaraId = 4, //sztuka
                    IloscNaStanie = 300,
                    Ocena = 1.5
                }
                ,
                new ProduktModel()
                {
                    ID = 5,
                    Nazwa = "Ciastka Delicje morelowe double opak.",
                    Opis = "Ulubione przez wszystkich ciastka z galaretką.",
                    CenaBazowa = 8.99,
                    Opodatkowanie = 0.15,
                    RabatStalegoKlienta = 0.15,
                    Ilosc = 1,
                    MiaraId = 5, //opakowanie
                    IloscNaStanie = 12,
                    Ocena = null
                }
                ,
                new ProduktModel()
                {
                    ID = 6,
                    Nazwa = "Ciastka Delicje pomarańczowe double opak.",
                    Opis = "Ulubione przez wszystkich ciastka z galaretką.",
                    CenaBazowa = 4.49,
                    Opodatkowanie = 0.15,
                    RabatStalegoKlienta = 0.10,
                    Ilosc = 1,
                    MiaraId = 5, //opakowanie
                    IloscNaStanie = 20,
                    Ocena = 4.0
                }

                );

            modelBuilder.Entity<DaneUzytkownikaModel>().HasData(
                new DaneUzytkownikaModel()
                {
                    Id = 1,
                    Login = "klient1",
                    Haslo = "jawfklfklnasdsa",
                    Imie = "Jan",
                    Nazwisko = "Kowalski",
                    Email = "jankowal@wp.pl",
                    NIP = null,
                    AdresId = 1
                }
                ,
                new DaneUzytkownikaModel()
                {
                    Id = 2,
                    Login = "klient2",
                    Haslo = "hsfdjhfkewhflen",
                    Imie = "Marianna",
                    Nazwisko = "Nowak",
                    Email = "nowakannna@wp.pl",
                    NIP = "0224111111",
                    AdresId = 2
                }
                ,
                new DaneUzytkownikaModel()
                {
                    Id = 3,
                    Login = "klient3",
                    Haslo = "kegkjldklfklnasdsa",
                    Imie = "Wiktoria",
                    Nazwisko = "Kowalska",
                    Email = "wikikowal@gmail.com",
                    NIP = null,
                    AdresId = 1
                }
                ,
                new DaneUzytkownikaModel()
                {
                    Id = 4,
                    Login = "klient4",
                    Haslo = "jlkdgggadasjfknf",
                    Imie = "Larry",
                    Nazwisko = "Wirek",
                    Email = null,
                    NIP = "0226111188",
                    AdresId = null
                }
                ,
                new DaneUzytkownikaModel()
                {
                    Id = 5,
                    Login = "pracownik1",
                    Haslo = "mlgsnjlkdsjfknf",
                    Imie = "Mirek",
                    Nazwisko = "Wegiel",
                    Email = "leg@wp.pl",
                    NIP = null,
                    AdresId = 3
                }
                ,
                new DaneUzytkownikaModel()
                {
                    Id = 6,
                    Login = "pracownik2",
                    Haslo = "mlgsnjlkdsjfknf",
                    Imie = "Tymon",
                    Nazwisko = "Kiszke",
                    Email = "tymek@wp.pl",
                    NIP = null,
                    AdresId = 4
                }

                );

            modelBuilder.Entity<PodstawowyUzytkownikModel>().HasData(
                new PodstawowyUzytkownikModel()
                {
                    ID = 1,
                    SumaWydatkow = 0,
                    MozeOplacacFaktury = false,
                    DaneUzytkownikaId = 1,
                    StatusKlientaId = 1 //zwykly
                }
                ,
                new PodstawowyUzytkownikModel()
                {
                    ID = 2,
                    SumaWydatkow = 230.10,
                    MozeOplacacFaktury = true,
                    DaneUzytkownikaId = 2,
                    StatusKlientaId = 2 //staly
                }
                ,
                new PodstawowyUzytkownikModel()
                {
                    ID = 3,
                    SumaWydatkow = 95.15,
                    MozeOplacacFaktury = false,
                    DaneUzytkownikaId = 3,
                    StatusKlientaId = 1 //standarowy
                }
                ,
                new PodstawowyUzytkownikModel()
                {
                    ID = 4,
                    SumaWydatkow = 35.15,
                    MozeOplacacFaktury = true,
                    DaneUzytkownikaId = 4,
                    StatusKlientaId = 1 //standarowy
                }

                ) ;

            modelBuilder.Entity<OpiniaModel>().HasData(
                new OpiniaModel()
                {
                    ID = 1,
                    Punktacja = 3,
                    Opis = "W porządku, ale drogi",
                    LiczbaPolubien = 2,
                    CzyPrzydatna = true,
                    ProduktId = 2,
                    PodstawowyUzytkownikId = 2

                }
                ,
                new OpiniaModel()
                {
                    ID = 2,
                    Punktacja = 4,
                    Opis = "Bardzo smaczne, ale nizsze cena moze?",
                    LiczbaPolubien = 0,
                    CzyPrzydatna = false,
                    ProduktId = 3,
                    PodstawowyUzytkownikId = 2

                }
                ,
                new OpiniaModel()
                {
                    ID = 3,
                    Punktacja = 1,
                    Opis = "beznadzieja.",
                    LiczbaPolubien = 0,
                    CzyPrzydatna = false,
                    ProduktId = 4,
                    PodstawowyUzytkownikId = 2

                }
                ,
                new OpiniaModel()
                {
                    ID = 4,
                    Punktacja = 2,
                    Opis = "Znośne, ale cena zaporowa",
                    LiczbaPolubien = 0,
                    CzyPrzydatna = false,
                    ProduktId = 4,
                    PodstawowyUzytkownikId = 3

                }
                ,
                new OpiniaModel()
                {
                    ID = 5,
                    Punktacja = 4,
                    Opis = "Fajne ciastka",
                    LiczbaPolubien = 1,
                    CzyPrzydatna = true,
                    ProduktId = 6,
                    PodstawowyUzytkownikId = 2

                }


                );

            modelBuilder.Entity<PolubieniaOpiniiModel>().HasData(
                new PolubieniaOpiniiModel()
                {
                    OpiniaId = 1,
                    PodstawowyUzytkownikId = 3
                }
                ,
                new PolubieniaOpiniiModel()
                {
                    OpiniaId = 1,
                    PodstawowyUzytkownikId = 4
                }
                ,
                new PolubieniaOpiniiModel()
                {
                    OpiniaId = 5,
                    PodstawowyUzytkownikId = 2
                }

                );


        }
    }
}
