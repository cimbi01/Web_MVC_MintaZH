using MintaZH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MintaZH.Controllers
{
    public class merkozesController : Controller
    {
        private static List<MerkozesekModell> merkozesek = new List<MerkozesekModell>() {
            new MerkozesekModell() { Cs1Nev = "cs1", Cs2Nev="cs2", Cs1Gol=1, Cs2Gol=2},
            new MerkozesekModell() { Cs1Nev = "cs2", Cs2Nev="cs3", Cs1Gol=2, Cs2Gol=1},
            new MerkozesekModell() { Cs1Nev = "cs3", Cs2Nev="cs4", Cs1Gol=1, Cs2Gol=2},
            new MerkozesekModell() { Cs1Nev = "cs4", Cs2Nev="cs5", Cs1Gol=2, Cs2Gol=1},
            new MerkozesekModell() { Cs1Nev = "cs5", Cs2Nev="cs6", Cs1Gol=1, Cs2Gol=2},
            new MerkozesekModell() { Cs1Nev = "cs6", Cs2Nev="cs1", Cs1Gol=2, Cs2Gol=1},
        };
        private static Dictionary<string, int> osszpontok = Pontok(merkozesek, Pontszamitas);
        /*MEGOLDANI!!!!*/
        private static string[] CsapatokNevei()
        {
            string[] csapatoknevei = new string[16];
            return csapatoknevei;
        }
        private static Dictionary<string, int> Pontok(List<MerkozesekModell> merkozesek,
            Func<MerkozesekModell, Dictionary<string, int>> pontszamitas)
        {
            Dictionary<string, int> ossz_pontok = new Dictionary<string, int>();
            foreach (var item in merkozesek)
            {
                if (!ossz_pontok.ContainsKey(item.Cs1Nev)) { ossz_pontok.Add(item.Cs1Nev, 0); }
                if (!ossz_pontok.ContainsKey(item.Cs2Nev)) { ossz_pontok.Add(item.Cs2Nev, 0); }
                Dictionary<string, int> merkozes_pont = pontszamitas(item);
                foreach (var item2 in merkozes_pont)
                {
                    ossz_pontok[item2.Key] += item2.Value;
                }
            }
            return ossz_pontok;
        }
        private static Dictionary<string, int> Pontszamitas(MerkozesekModell merkozes)
        {
            Dictionary<string, int> pontok = new Dictionary<string, int>();
            if (merkozes.Cs1Gol > merkozes.Cs2Gol)
            {
                pontok.Add(merkozes.Cs1Nev, 2);
                pontok.Add(merkozes.Cs2Nev, 0);
            }
            else if (merkozes.Cs1Gol < merkozes.Cs2Gol)
            {
                pontok.Add(merkozes.Cs1Nev, 0);
                pontok.Add(merkozes.Cs2Nev, 2);
            }
            else
            {
                pontok.Add(merkozes.Cs1Nev, 1);
                pontok.Add(merkozes.Cs2Nev, 1);
            }
            return pontok;
        }
        // GET: merkozes
        public ActionResult Feladat3()
        {
            ViewData["merkozes"] = merkozesek[0];
            var csapatok = merkozesek.Select(m => m.Cs1Nev).Distinct();
            int[] pontok = new int[16];
            int i = 0;
            foreach (var item in osszpontok)
            {
                pontok[i] = item.Value;
                i++;
            }
            ViewBag.osszpontok = pontok;
            ViewBag.maxpont = osszpontok.Max(cs => cs.Value);
            ViewBag.maxcsapat = osszpontok.First(cs => cs.Value == ViewBag.maxpont).Key;
            List<MerkozesekModell> maxcsapatmerkozesek = merkozesek.Where(m => m.Cs1Nev == ViewBag.maxcsapat || m.Cs2Nev == ViewBag.maxcsapat).ToList();
            return View(maxcsapatmerkozesek);
        }
    }
}