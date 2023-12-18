using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PO_Projekt.Data;
using PO_Projekt.Models;
using PO_Projekt.ViewModels;

namespace PO_Projekt.Controllers
{
    public class PodstawowyUzytkownikController : Controller
    {
        private readonly MyDbContext _context;

        private string user1Cookie = "user1";
        private string user2Cookie = "user2";
        private string user3Cookie = "user3";
        private string user4Cookie = "user4";

        public PodstawowyUzytkownikController(MyDbContext context)
        {
            _context = context;
        }

        // GET: PodstawowyUzytkownik
        public async Task<IActionResult> Index()
        {
            CookieOptions cookieOptions;
            var cookie = Request.Cookies["cart"];
            if (true)//cookie == null
            {
                var cart = new Dictionary<int, int>();

                cart.Add(1, 4);
                //cart.Add(2, 2);
                //cart.Add(3, 3);

                cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart), cookieOptions);
            }
            cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Append("user", user1Cookie, cookieOptions);


            var myDbContext = _context.Produkty.Include(p => p.Miara);
            return View(await myDbContext.ToListAsync());
        }

        public async Task<IActionResult> Koszyk()
        {
            Dictionary<int, int> cart;
            var cookie = Request.Cookies["cart"];
            if (cookie == null)
            {
                TempData["message"] = "Koszyk jest pusty";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cookie);
            }

            var userCookie = Request.Cookies["user"];
            if (userCookie == null)
            {
                TempData["message"] = "Nie jesteś zalogowanym użytkownikiem";
                return RedirectToAction(nameof(Index));
            }
            if (userCookie.Substring(0, 4) != "user")
            {
                TempData["message"] = "Nie jesteś zalogowanym użytkownikiem";
                return RedirectToAction(nameof(Index));
            }
            int userId = Int32.Parse(userCookie.Substring(4));

            double totalPrice = 0.0;
            List<ProduktKoszykViewModel> articles = new List<ProduktKoszykViewModel>();
            foreach (var id in cart.Keys)
            {
                var article = _context.Produkty.Include(a => a.Miara).FirstOrDefault(a => a.ID == id);
                articles.Add(new ProduktKoszykViewModel(article, cart[id]));
                if (_context.PodstawowiUzytkownicy.Find(userId).StatusKlientaId == 2)//stały klient
                {
                    articles.Last().SumarycznaCena = Math.Round((double)(article.CenaBazowa * (1 + article.Opodatkowanie) * (1 - article.RabatStalegoKlienta) * cart[id]), 2);
                }
                else
                {
                    articles.Last().SumarycznaCena = Math.Round((double)(article.CenaBazowa * (1 + article.Opodatkowanie) * cart[id]), 2);
                }
                totalPrice += (double)articles.Last().SumarycznaCena;

            }

            if (articles.Count() == 0)
                TempData["cart_message"] = "Koszyk jest obecnie pusty";
            ViewBag.totalPrice = totalPrice;
            return View(articles);
        }

       

        // GET: PodstawowyUzytkownik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktModel = await _context.Produkty
                .Include(p => p.Miara)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produktModel == null)
            {
                return NotFound();
            }

            return View(produktModel);
        }

        // GET: PodstawowyUzytkownik/Create
        public IActionResult Create()
        {
            ViewData["MiaraId"] = new SelectList(_context.Miary, "ID", "Nazwa");
            return View();
        }

        // POST: PodstawowyUzytkownik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nazwa,Opis,CenaBazowa,Opodatkowanie,RabatStalegoKlienta,Ilosc,MiaraId,IloscNaStanie,Ocena")] ProduktModel produktModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produktModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MiaraId"] = new SelectList(_context.Miary, "ID", "Nazwa", produktModel.MiaraId);
            return View(produktModel);
        }

        // GET: PodstawowyUzytkownik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktModel = await _context.Produkty.FindAsync(id);
            if (produktModel == null)
            {
                return NotFound();
            }
            ViewData["MiaraId"] = new SelectList(_context.Miary, "ID", "Nazwa", produktModel.MiaraId);
            return View(produktModel);
        }

        // POST: PodstawowyUzytkownik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nazwa,Opis,CenaBazowa,Opodatkowanie,RabatStalegoKlienta,Ilosc,MiaraId,IloscNaStanie,Ocena")] ProduktModel produktModel)
        {
            if (id != produktModel.ID)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                
                try
                {
                    _context.Update(produktModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduktModelExists(produktModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            /*var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();
            errors[0].*/
            /*else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                ViewData["Errors"] = errors;
            }*/

            ViewData["MiaraId"] = new SelectList(_context.Miary, "ID", "Nazwa", produktModel.MiaraId);
            return View(produktModel);
        }



        // GET: PodstawowyUzytkownik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktModel = await _context.Produkty
                .Include(p => p.Miara)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produktModel == null)
            {
                return NotFound();
            }

            return View(produktModel);
        }

        // POST: PodstawowyUzytkownik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produktModel = await _context.Produkty.FindAsync(id);
            _context.Produkty.Remove(produktModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktModelExists(int id)
        {
            return _context.Produkty.Any(e => e.ID == id);
        }

        // GET: DaneUzytkownikaModels/Edit/5
        public async Task<IActionResult> DaneOsoboweEdit()
        {
            //Dane uzytkownika id z cookies
            int? uzytkownikId = idPodstawowegoUzytkownikaFromCookies();
            if( uzytkownikId == null)
            {
                return NotFound();
            }
            var danePodstawowegoUzytkownikaModel = await _context.PodstawowiUzytkownicy.FindAsync(uzytkownikId);

            if( danePodstawowegoUzytkownikaModel == null)
            {
                return NotFound();
            }

            var daneUzytkownikaModel = await _context.DaneUzytkownikow.FindAsync(danePodstawowegoUzytkownikaModel.DaneUzytkownikaId);
            if (daneUzytkownikaModel == null)
            {
                return NotFound();
            }
            var daneAdresModel = await _context.Adresy.FindAsync(daneUzytkownikaModel.AdresId);

            var daneOsoboweModel = new DaneOsoboweModel();

            if(daneAdresModel != null)
            {
                daneOsoboweModel.setAdres(daneAdresModel);
            }
            daneOsoboweModel.setOsobowe(daneUzytkownikaModel);
            @ViewBag.ErrorMessage = "";

            return View(daneOsoboweModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DaneOsoboweEdit([Bind("Imie,Nazwisko,NIP,Ulica,NrDomu,NrMieszkania,KodPocztowy,Miasto")] DaneOsoboweModel daneOsoboweModel, string haslo)
        {
            int? uzytkownikId = idPodstawowegoUzytkownikaFromCookies();
            if (uzytkownikId == null)
            {
                return NotFound();
            }
            var danePodstawowegoUzytkownikaModel = await _context.PodstawowiUzytkownicy.FindAsync(uzytkownikId);

            if (danePodstawowegoUzytkownikaModel == null)
            {
                return NotFound();
            }

            var daneUzytkownikaModel = await _context.DaneUzytkownikow.FindAsync(danePodstawowegoUzytkownikaModel.DaneUzytkownikaId);
            if (daneUzytkownikaModel == null)
            {
                return NotFound();
            }
            @ViewBag.ErrorMessage = "";
            @ViewBag.SuccessMessage = "";


            if (ModelState.IsValid)
            {
                if(!daneOsoboweModel.czyAdresPoprawny() && !daneOsoboweModel.czyAdresPusty())
                {
                    @ViewBag.ErrorMessage = "Informacje dotyczące adresu są niekompletne. Wymagane: Nr domu, Kod pocztowy i Miasto. Alternatywnie usuń wszelkie dane o adresie, aby usunąć powiązany adres";
                    return View(daneOsoboweModel);
                }

                var daneAdresModel = await _context.Adresy.FindAsync(daneUzytkownikaModel.AdresId);


                if (daneOsoboweModel.equalsAdres(daneAdresModel) && daneOsoboweModel.equalsOsobowe(daneUzytkownikaModel))
                {
                    @ViewBag.ErrorMessage = "Nie dokonano zmiany w danych - nie wykryto zmian w polach";
                    return View(daneOsoboweModel);
                }

                if(!daneUzytkownikaModel.sprawdzHaslo(haslo))
                {
                    @ViewBag.ErrorMessage = "Wpisane haslo jest niepoprawne";
                    return View(daneOsoboweModel);
                }

                //Tu zaczyna sie dodawanie do bazy danych
                daneOsoboweModel.setOsobowe(daneUzytkownikaModel);

                AdresModel nowyAdres = daneOsoboweModel.getAdres(); //jesli nie ma byc adresu to null
                if(daneUzytkownikaModel.AdresId == null)
                {
                    if(nowyAdres != null)
                    {
                        _context.Adresy.Add(nowyAdres);
                        await _context.SaveChangesAsync();
                        daneUzytkownikaModel.AdresId = nowyAdres.Id;
                        _context.DaneUzytkownikow.Update(daneUzytkownikaModel);
                        await _context.SaveChangesAsync();
                    }
                    //else nie trzeba nic zmieniac
                }
                else
                {
                    if (nowyAdres != null)
                    {
                        daneOsoboweModel.setOtherAdres(daneAdresModel);

                        _context.Adresy.Update(daneAdresModel);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        daneUzytkownikaModel.AdresId = null;
                        _context.DaneUzytkownikow.Update(daneUzytkownikaModel);
                        await _context.SaveChangesAsync();
                        _context.Adresy.Remove(daneAdresModel);
                        await _context.SaveChangesAsync();
                    }
                }

                
                
                
                @ViewBag.SuccessMessage = "Pomyślnie zmieniono twoje dane osobowe";
                
                return View(daneOsoboweModel);
            }
            
            return View(daneOsoboweModel);
        }

        private bool DaneUzytkownikaModelExists(int id)
        {
            return _context.DaneUzytkownikow.Any(e => e.Id == id);
        }


        public async Task<IActionResult> PrzegladOferty(string searchString, string sortBy, string sortType)
        {
            

            CookieOptions cookieOptions;
            var cookie = Request.Cookies["cart"];
            if (cookie == null)
            {
                //produkt 1 w ilosci 1, produk 2 w ilosc 2
                var cart = new Dictionary<int, int>();

                cart.Add(1, 1);
                cart.Add(2, 2);
                cart.Add(3, 3);

                cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart), cookieOptions);
            }
            cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Append("user", user1Cookie, cookieOptions);

            @ViewBag.searched = "";

            var myDbContext = _context.Produkty.Include(p => p.Miara).Select(m=>m);

            var myDbContext1 = _context.Produkty;
            if (!String.IsNullOrEmpty(searchString))
            {
                @ViewBag.searched = searchString;
                myDbContext = myDbContext.Where(s => s.Nazwa.Contains(searchString));
                //return View(await _context.Produkty.Include(p => p.Miara).Where(s => s.Nazwa.Contains(searchString)).ToListAsync());
            }
            bool isAscending = true;
            @ViewBag.searchType = "ascending";
            if ( sortType == "descending")
            {
                isAscending = false;
                @ViewBag.searchType = "descending";
            }
            @ViewBag.searchBy = "nazwa";
            if ( sortBy == "cena")
            {
                @ViewBag.searchBy = "cena";
                if (isAscending)
                {
                    myDbContext = myDbContext.OrderBy(m => m.CenaBazowa);
                }
                else
                {
                    myDbContext = myDbContext.OrderByDescending(m => m.CenaBazowa);
                }
            }
            else //nazwa
            {
                if (isAscending)
                {
                    myDbContext = myDbContext.OrderBy(m => m.Nazwa);
                }
                else
                {
                    myDbContext = myDbContext.OrderByDescending(m => m.Nazwa);
                }
            }
            //var myDbContext = _context.Opinie.Include(o => o.PodstawowyUzytkownik).Include(o => o.Produkt)
             //   .Where(o => o.ProduktId == id).OrderByDescending(x => x.LiczbaPolubien.HasValue).ThenByDescending(x => x.LiczbaPolubien);
            //var myDbContext = _context.Produkty.Include(p => p.Miara);


            return View(await myDbContext.ToListAsync());
        }

        public async Task<IActionResult> DaneProduktu(int? id, string errorMessage, string successMessage)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktModel = await _context.Produkty
                .Include(p => p.Miara)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produktModel == null)
            {
                return NotFound();
            }
            @ViewBag.ErrorMessage = "";
            if (!String.IsNullOrEmpty(errorMessage))
            {
                @ViewBag.ErrorMessage = errorMessage;
            }
            @ViewBag.SuccessMessage = "";
            if (!String.IsNullOrEmpty(successMessage))
            {
                @ViewBag.SuccessMessage = successMessage;
            }

            @ViewBag.liczbaOpinii = await _context.Opinie
                .CountAsync(m => m.ProduktId == id);


            return View(produktModel);
        }

        public async Task<IActionResult> DaneProduktuOpinie(int? id, string errorMessage, string successMessage)
        {
            if (id == null)
            {
                return NotFound();
            }
            //podstaowywUzytkownikId from cookies
            int? uzytkownikId = idPodstawowegoUzytkownikaFromCookies();
            if (uzytkownikId == null)
            {
                return NotFound();
            }
            var danePodstawowegoUzytkownikaModel = await _context.PodstawowiUzytkownicy.FindAsync(uzytkownikId);

            if (danePodstawowegoUzytkownikaModel == null)
            {
                return NotFound();
            }

            var produktModel = await _context.Produkty
                .Include(p => p.Miara)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produktModel == null)
            {
                return NotFound();
            }
            @ViewBag.idProd = id;
            @ViewBag.nazwaProd= produktModel.Nazwa;

            @ViewBag.ErrorMessage = "";
            if( !String.IsNullOrEmpty(errorMessage))
            {
                @ViewBag.ErrorMessage = errorMessage;
            }
            @ViewBag.SuccessMessage = "";
            if (!String.IsNullOrEmpty(successMessage))
            {
                @ViewBag.SuccessMessage = successMessage;
            }


            @ViewBag.liczbaOpinii = await _context.Opinie
                .CountAsync(m => m.ProduktId == id);

            var myDbContext = _context.Opinie.Include(o => o.PodstawowyUzytkownik).Include(o => o.Produkt)
                .Where(o => o.ProduktId == id).OrderByDescending(x => x.LiczbaPolubien.HasValue).ThenByDescending(x => x.LiczbaPolubien);
            var opiniaList = await myDbContext.ToListAsync();
            foreach (var item in opiniaList)
            {
                @ViewData[item.ID.ToString()] = czyPolubioneString(item.ProduktId, item.ID, (int)uzytkownikId);
            }
            return View(opiniaList);
        }

        public void DodajPolubienie(int idProd, int idOpinia, int uzytkownikId)
        {

            var polubienie = _context.PolubieniaOpinii.Find(idOpinia, uzytkownikId);
            if (polubienie != null)
            {
                //Usun polubienie
                _context.PolubieniaOpinii.Remove(polubienie);
            }
            else
            {
                polubienie = new PolubieniaOpiniiModel();
                polubienie.OpiniaId = idOpinia;
                polubienie.PodstawowyUzytkownikId = uzytkownikId;

                _context.PolubieniaOpinii.Add(polubienie);
            }
            _context.SaveChanges();
            
            AktualizujPolubienie(idOpinia);
        }
        public void AktualizujPolubienie(int idOpinia)
        {
            var opinia = _context.Opinie.Find(idOpinia);
            var liczbaPolubien = _context.PolubieniaOpinii.Count(m => m.OpiniaId == idOpinia);
            if(opinia.LiczbaPolubien != liczbaPolubien)
            {
                opinia.LiczbaPolubien = liczbaPolubien;
                _context.Opinie.Update(opinia);
                _context.SaveChanges();
                AktualizujPolubieniaPrzydatnosc(opinia.ProduktId);
            }
            
        }
        public void AktualizujPolubieniaPrzydatnosc(int idProd)
        {
            var opinie = _context.Opinie.Where(m => m.ProduktId == idProd).Select(m=>m).ToList();
            var ocenyCount = opinie.Count();
            if( ocenyCount == 0)
            {
                return;
            }
            double ocenyPolubieniaSrednia = opinie.Average(m=>m.LiczbaPolubien.GetValueOrDefault());

            if(ocenyPolubieniaSrednia <= 0)
            {
                ocenyPolubieniaSrednia = 0.9;
            }
            

            //var opinieList = opinie.ToList();
            foreach (var item in opinie)
            {
                if( (double)item.LiczbaPolubien > ocenyPolubieniaSrednia)
                {
                    item.CzyPrzydatna = true;
                }
                else
                {
                    item.CzyPrzydatna = false;
                }
            }
            _context.SaveChanges();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PolubienieOpinii(int ?idProd, int ?idOpinia)
        {
            int? uzytkownikId = idPodstawowegoUzytkownikaFromCookies();
            if (uzytkownikId == null)
            {
                return NotFound();
            }
            var danePodstawowegoUzytkownikaModel = await _context.PodstawowiUzytkownicy.FindAsync(uzytkownikId);

            @ViewBag.idProd = idProd;
            @ViewBag.idOpinia = idOpinia;
            if (danePodstawowegoUzytkownikaModel == null)
            {
                return NotFound();
            }
            if (idProd == null)
            {
                return NotFound();
            }
            var produktModel = await _context.Produkty
                .Include(p => p.Miara)
                .FirstOrDefaultAsync(m => m.ID == idProd);
            if (produktModel == null)
            {
                return NotFound();
            }
            if( idOpinia == null)
            {
                return NotFound();
            }
            var opiniaModel = await _context.Opinie.FindAsync(idOpinia);
            if( opiniaModel == null)
            {
                return NotFound();
            }

            var polubienie = await _context.PolubieniaOpinii.FindAsync(idOpinia, uzytkownikId);
            if( polubienie == null)
            {
                DodajPolubienie(produktModel.ID, opiniaModel.ID, (int)uzytkownikId);
                return RedirectToAction(nameof(DaneProduktuOpinie), new { id = idProd});
            }

            //anulowanie opinii
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AnulowanieOpinii(int? idProd, int? idOpinia, int czyAnulowac)
        {
            int? uzytkownikId = idPodstawowegoUzytkownikaFromCookies();
            if (uzytkownikId == null)
            {
                return NotFound();
            }
            var danePodstawowegoUzytkownikaModel = await _context.PodstawowiUzytkownicy.FindAsync(uzytkownikId);

            @ViewBag.idProd = idProd;
            @ViewBag.idOpinia = idOpinia;
            if (danePodstawowegoUzytkownikaModel == null)
            {
                return NotFound();
            }
            if (idProd == null)
            {
                return NotFound();
            }
            var produktModel = await _context.Produkty
                .Include(p => p.Miara)
                .FirstOrDefaultAsync(m => m.ID == idProd);
            if (produktModel == null)
            {
                return NotFound();
            }
            if (idOpinia == null)
            {
                return NotFound();
            }
            var opiniaModel = await _context.Opinie.FindAsync(idOpinia);
            if (opiniaModel == null)
            {
                return NotFound();
            }


            if( czyAnulowac == 0)
                return RedirectToAction(nameof(DaneProduktuOpinie), new { id = idProd });
            var polubienie = await _context.PolubieniaOpinii.FindAsync(idOpinia, uzytkownikId);
            if (polubienie == null)
            {
                return RedirectToAction(nameof(DaneProduktuOpinie), new { id = idProd });
            }
            //anulowanie polubienia
            DodajPolubienie(produktModel.ID, opiniaModel.ID, (int)uzytkownikId);
            return RedirectToAction(nameof(DaneProduktuOpinie), new { id = idProd });
        }

        public bool czyPolubione(int idProd, int idOpinia, int podstawowyUzytkownikId)
        {
            var polubienie = _context.PolubieniaOpinii.Find(idOpinia, podstawowyUzytkownikId);
            return polubienie != null;
        }

        public bool czyPolubioneZCookies(int idProd, int idOpinia)
        {
            int? uzytkownikId = idPodstawowegoUzytkownikaFromCookies();
            if (uzytkownikId == null)
            {
                return false;
            }
            return czyPolubione(idProd, idOpinia, (int)uzytkownikId);
        }

        public string czyPolubioneZCookiesString(int idProd, int idOpinia)
        {
            if (czyPolubioneZCookies(idProd, idOpinia))
                return "polubione";
            else
            {
                return "niepolubione";
            }
        }
        public string czyPolubioneString(int idProd, int idOpinia, int podstawowyUzytkownikId)
        {
            if (czyPolubione(idProd, idOpinia, podstawowyUzytkownikId))
                return "polubione";
            else
            {
                return "niepolubione";
            }
        }

        public bool czyMozeWystawicOpinie(int idProd, int podstawowyUzytkownikId)
        {
            //var polubienie = _context.PolubieniaOpinii.Find(idOpinia, podstawowyUzytkownikId);
            var zamowienia = _context.Zamowienia.Where(z => z.PodstawowyUzytkownikId == podstawowyUzytkownikId && z.StatusZamowieniaId == 2).Select(z => z.Id).ToList();


            //var myDbContext = _context.Produkty.Include(p => p.Miara).Select(m => m);
            foreach (var item in zamowienia)
            {
                var polubienie = _context.SzczegolyZamowien.Find(item, idProd);
                if (polubienie != null)
                {
                    return true;
                }
            }
            return false;
        }

        public void DodajOpinie(OpiniaModel opiniaModel)
        {
            _context.Opinie.Add(opiniaModel);
            _context.SaveChanges();
            ObliczSredniaOcen(opiniaModel.ProduktId);
        }
        public void ObliczSredniaOcen(int produktId)
        {
            var produktModel = _context.Produkty
                .FirstOrDefault(m => m.ID == produktId);
            if (produktModel == null)
            {
                return;
            }
            ObliczSredniaOcen(produktModel);
        }
        public void ObliczSredniaOcen(ProduktModel produktModel)
        {
            var opinie = _context.Opinie.Where(m => m.ProduktId == produktModel.ID);
            var opinieCount = opinie.Count();
            double srednia = 0.0;
            if( opinieCount > 0)
                srednia = opinie.Average(m => m.Punktacja);
            if( srednia >= 1 && srednia <= 5)
            {
                produktModel.Ocena = (double)srednia;
            }
            else
            {
                produktModel.Ocena = null;
            }
            _context.Update(produktModel);
            _context.SaveChangesAsync();
        }

        public async Task<IActionResult> WystawienieOpinii(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var produktModel = await _context.Produkty
                .Include(p => p.Miara)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produktModel == null)
            {
                return NotFound();
            }
            @ViewBag.produktId = produktModel.ID;
            @ViewBag.produktNazwa = produktModel.Nazwa;
            @ViewBag.opinia = "";
            @ViewBag.ErrorMessage = "";
            //tutaj z Cookies będzie ID uzytkownika
            //Na potrzeby testu, wybralem przykladowe ID uzytkownika
            int? uzytkownikId = idPodstawowegoUzytkownikaFromCookies();
            if (uzytkownikId == null)
            {
                return NotFound();
            }
            var danePodstawowegoUzytkownikaModel = _context.PodstawowiUzytkownicy
                .FirstOrDefault(m => m.ID == uzytkownikId);

            if(!czyMozeWystawicOpinie(produktModel.ID, (int)uzytkownikId))
            {
                return RedirectToAction(nameof(DaneProduktu), new { id = produktModel.ID, errorMessage = "Nie można wystawić opinii bez wcześniejszego zakupienia produktu." });
            }


            return View();
        }

        // POST: OpiniaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WystawienieOpinii(int? id, int punktacja, string opis)
        {
            int? uzytkownikId = idPodstawowegoUzytkownikaFromCookies();
            if (uzytkownikId == null)
            {
                return NotFound();
            } 
            var produktModel = await _context.Produkty
                .Include(p => p.Miara)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produktModel == null)
            {
                return NotFound();
            }
            @ViewBag.produktId = produktModel.ID;
            @ViewBag.produktNazwa = produktModel.Nazwa;
            @ViewBag.opinia = opis;
            @ViewBag.ErrorMessage = "";

            

            double punkty = (double)punktacja;
            if ( String.IsNullOrEmpty(opis) || !OpiniaModel.PunktacjaPoprawna(punkty))
            {
                @ViewBag.ErrorMessage = "Nie wybrano oceny lub nie wpisano treści opinii";
                return View();
            }
            if (!czyMozeWystawicOpinie(produktModel.ID, (int)uzytkownikId))
            {
                return RedirectToAction(nameof(DaneProduktuOpinie), new { id = produktModel.ID, errorMessage = "Nie można wystawić opinii bez wcześniejszego zakupienia produktu." });
            }
            var opiniaModel = new OpiniaModel();
            opiniaModel.Punktacja = punkty;
            opiniaModel.LiczbaPolubien = 0;
            opiniaModel.CzyPrzydatna = false;
            opiniaModel.PodstawowyUzytkownikId = (int)uzytkownikId;
            opiniaModel.ProduktId = produktModel.ID;
            opiniaModel.Opis = opis;
            DodajOpinie(opiniaModel);
            return RedirectToAction(nameof(DaneProduktuOpinie), new { id = produktModel.ID, successMessage = "Pomyślnie wystawiono opinię" });
            
        }

        public bool? czyRabat( ProduktModel produkt, PodstawowyUzytkownikModel podstawowyUzytkownik)
        {
            var statusUzytkownika =  _context.StatusyKlientow.Find(podstawowyUzytkownik.DaneUzytkownikaId);
            if( statusUzytkownika == null)
            {
                return null;
            }
            if (statusUzytkownika.ID == 2) //stały
            {
                return true;
            }
            else
                return false;
        }

        public async Task<IActionResult> DodajDoKoszyka(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktModel = await _context.Produkty
                .Include(p => p.Miara)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produktModel == null)
            {
                return NotFound();
            }
            @ViewBag.produktNazwa = produktModel.Nazwa;
            @ViewBag.produktImage = produktModel.ImageView;

            int? uzytkownikId = idPodstawowegoUzytkownikaFromCookies();
            if (uzytkownikId == null)
            {
                return NotFound();
            }

            if(czyKoszykZawiera(produktModel.ID))
            {
                return RedirectToAction(nameof(DaneProduktu), new { id = produktModel.ID, errorMessage = "Produkt już znajduje się w koszyku" });
            }
            if (produktModel.IloscNaStanie <= 0)
            {
                @ViewBag.ErrorMessage = "Niestety produkt jest obecnie niedostępny";
                return View();
            }

            var danePodstawowegoUzytkownikaModel = _context.PodstawowiUzytkownicy
                .FirstOrDefault(m => m.ID == uzytkownikId);


            var rabatBool = czyRabat(produktModel, danePodstawowegoUzytkownikaModel);
            if ( rabatBool == null)
            {
                return NotFound(); //tutaj wstawić błąd
            }
            double cena= 0;
            if( rabatBool == true)
            {
                cena = produktModel.CenaRabatFun();
                @ViewBag.cena = produktModel.CenaRabatDisplayFun();
                @ViewBag.cenaZa1 = produktModel.CenaRabatZa1DisplayFun();
            }
            else
            {
                cena = produktModel.CenaPodatekFun();
                @ViewBag.cena = produktModel.CenaPodatekDisplayFun();
                @ViewBag.cenaZa1 = produktModel.CenaPodatekZa1DisplayFun();
            }
            




            
            int liczbaProduktow = 1; //defaultowo 1 produkt
            @ViewBag.suma = ProduktModel.WalutaDisplay(cena*liczbaProduktow);
            @ViewBag.prodLiczba = liczbaProduktow;
            @ViewBag.prodId = id;

            @ViewBag.ErrorMessage = "";
            //ViewData["PodstawowyUzytkownikId"] = new SelectList(_context.PodstawowiUzytkownicy, "ID", "ID", opinia.PodstawowyUzytkownikId);
            //ViewData["ProduktId"] = new SelectList(_context.Produkty, "ID", "Nazwa", opinia.ProduktId);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DodajDoKoszyka(int id, int liczbaProduktow, int akcja) //int? id, int liczbaProduktow, int prodId
        {
            @ViewBag.ErrorMessage = "";
            //var produktModel = await _context.Produkty.FindAsync(formModel.ProdId);
            /*if( id == null)
            {
                return NotFound();
            }*/
            var produktModel = await _context.Produkty
                .Include(p => p.Miara)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produktModel == null)
            {
                return NotFound();
            }
            @ViewBag.produktNazwa = produktModel.Nazwa;
            @ViewBag.produktImage = produktModel.ImageView;
            int? uzytkownikId = idPodstawowegoUzytkownikaFromCookies();
            if (uzytkownikId == null)
            {
                return NotFound();
            }
            var danePodstawowegoUzytkownikaModel = _context.PodstawowiUzytkownicy
                .FirstOrDefault(m => m.ID == uzytkownikId);


            var rabatBool = czyRabat(produktModel, danePodstawowegoUzytkownikaModel);
            if (rabatBool == null)
            {
                return NotFound(); //tutaj wstawić błąd
            }
            double cena = 0;
            if (rabatBool == true)
            {
                cena = produktModel.CenaRabatFun();
                @ViewBag.cena = produktModel.CenaRabatDisplayFun();
                @ViewBag.cenaZa1 = produktModel.CenaRabatZa1DisplayFun();
            }
            else
            {
                cena = produktModel.CenaPodatekFun();
                @ViewBag.cena = produktModel.CenaPodatekDisplayFun();
                @ViewBag.cenaZa1 = produktModel.CenaPodatekZa1DisplayFun();
            }

            if( akcja == -1 || akcja == 1)
            {
                liczbaProduktow += akcja;
            }
            if( liczbaProduktow < 1)
            {
                liczbaProduktow = 1;
            }
            //int liczbaProduktow = 1; //defaultowo 1 produkt
            @ViewBag.suma = ProduktModel.WalutaDisplay(cena * liczbaProduktow);
            @ViewBag.prodLiczba = liczbaProduktow;
            @ViewBag.prodId = id;
            //@ViewBag.suma = "";

            if (akcja == 0)
            {
                /*_context.Add(opiniaModel);
                await _context.SaveChangesAsync();*/


                //Tutaj sprawdzic czy jest wystarczajaco na stanie
                if(produktModel.IloscNaStanie < liczbaProduktow)
                {
                    @ViewBag.ErrorMessage = "Nie udało się dodać produktu do koszyka: brak wystarczającej ilości na stanie";
                    return View();
                }

                if (dodajDoKoszyka(produktModel.ID, liczbaProduktow, (int)uzytkownikId))
                {
                    return RedirectToAction(nameof(DaneProduktu), new { id = produktModel.ID, successMessage = "Pomyślnie dodano produkt do koszyka" });
                    //return RedirectToAction(nameof(PrzegladOferty));
                }
                else
                {
                    @ViewBag.ErrorMessage = "Nie udało się dodać produktu do koszyka: masz już ten produkt w koszyku";
                    return View();
                    //redirect do bledu
                }
            }
            
            return View();
        }

        private int? idPodstawowegoUzytkownikaFromCookies()
        {
            
            var userCookie = Request.Cookies["user"];
            if (userCookie == null)
            {
                return null;
            }
            if (userCookie.Substring(0, 4) != "user")
            {
                return null;
            }
            int userId = Int32.Parse(userCookie.Substring(4));
            return userId;
        }

        private bool dodajDoKoszyka(int prodId, int liczbaProd, int podstawowyUzytkownikId)
        {
            //zaimplementowac to poprawnie

            CookieOptions cookieOptions;
            //var cookie = Request.Cookies["cart"];
            

            Dictionary<int, int> cart;
            var cookie = Request.Cookies["cart"];
            if (cookie == null)
            {
                cart = new Dictionary<int, int>();

                cart.Add(prodId, liczbaProd);

                cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart), cookieOptions);
                return true;
            }
            else
            {
                cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cookie);
                if (cart.ContainsKey(prodId))
                {
                    return false;
                }
                else
                {
                    cart.Add(prodId, liczbaProd);

                    cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart), cookieOptions);
                    return true;
                }
            }

            
        }
        private bool czyKoszykZawiera(int prodId)
        {
            Dictionary<int, int> cart;
            var cookie = Request.Cookies["cart"];
            cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cookie);
            return cart.ContainsKey(prodId);
        }



    }




}
