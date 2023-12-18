using Microsoft.EntityFrameworkCore;
using PO_Projekt.Models;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PO_Projekt.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PolubieniaOpiniiModel>().HasKey(vf => new { vf.OpiniaId, vf.PodstawowyUzytkownikId });
            modelBuilder.Entity<SzczegolyZamowieniaModel>().HasKey(vf => new { vf.ZamowienieID, vf.ProduktID });

            modelBuilder.Seed();
        }
        public DbSet<AdresModel> Adresy { get; set; }
        public DbSet<DaneUzytkownikaModel> DaneUzytkownikow { get; set; }
        public DbSet<DostawaModel> Dostawy { get; set; }
        public DbSet<FakturaModel> Faktury { get; set; }
        public DbSet<MiaraModel> Miary { get; set; }
        public DbSet<OpiniaModel> Opinie { get; set; }
        public DbSet<PlatnoscModel> Platnosci { get; set; }
        public DbSet<PodstawowyUzytkownikModel> PodstawowiUzytkownicy { get; set; }
        public DbSet<PolubieniaOpiniiModel> PolubieniaOpinii { get; set; }
        public DbSet<PracownikModel> Pracownicy { get; set; }
        public DbSet<ProduktModel> Produkty { get; set; }
        public DbSet<ReklamacjaModel> Reklamacje { get; set; }
        public DbSet<StatusKlientaModel> StatusyKlientow { get; set; }
        public DbSet<StatusZamowieniaModel> StatusyZamowien { get; set; }
        public DbSet<SzczegolyZamowieniaModel> SzczegolyZamowien { get; set; }
        public DbSet<ZamowienieModel> Zamowienia { get; set; }
        
    }

}
