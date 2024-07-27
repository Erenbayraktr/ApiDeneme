using ApiDeneme.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ApiDeneme.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
               





        }

        public async Task<IActionResult> Index()
        {

            HttpClient httpClient = new HttpClient();

            getApi getapi = new ApiDeneme.getApi(httpClient);


            List<tradeModel> trade = new List<tradeModel>();
            trade = await getapi.getTrade();


            List<islem> islem = new List<islem>();



            DateTime earliestDateTime = DateTime.MaxValue;


            for (int i = 0; i < 1; i++)
            {
                string contractValue = trade[i].Conract;

                int year = int.Parse("20" + contractValue.Substring(2, 2));
                int month = int.Parse(contractValue.Substring(4, 2));
                int day = int.Parse(contractValue.Substring(6, 2));

                earliestDateTime = new DateTime(year, month, day, 00, 0, 0);
            }




            for (int i = 0; i < 24; i++)
            {

                DateTime currentDateTime = earliestDateTime.AddHours(i);
                int saat = currentDateTime.Hour;
                string saats = saat.ToString("D2");

                List<tradeModel> toplamislem = trade
                 .Where(x => x.Conract.Substring(8, 2) == saats)
                 .Select(x => new tradeModel { Conract = x.Conract, quantity = x.quantity })
                 .ToList();

                List<tradeModel> fiyat = trade
                 .Where(x => x.Conract.Substring(8, 2) == saats)
                 .Select(x => new tradeModel { Conract = x.Conract, Price = x.Price })
                 .ToList();


                decimal toplamfiyat = 0;
                for (int j = 0; j < toplamislem.Count; j++)
                {

                    toplamfiyat += (toplamislem[j].quantity * fiyat[j].Price) / 10;

                }




                islem.Add(new islem
                {
                    Tarih = currentDateTime.ToString(),
                    toplamIslem = (float)((float)toplamislem.Sum(x => x.quantity) / 10),
                    toplamIslemFiyat = toplamfiyat,
                    agirlikliOrtalama = toplamfiyat / ((decimal)toplamislem.Sum(x => x.quantity) / 10),


                });

            }





            return View(islem);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> getApi()
        {

            HttpClient httpClient = new HttpClient();

            getApi getapi = new ApiDeneme.getApi(httpClient);


            List<tradeModel> trade = new List<tradeModel>();
            trade = await getapi.getTrade();


            List<islem> islem = new List<islem>();



            DateTime earliestDateTime = DateTime.MaxValue;


            for (int i = 0; i < 1; i++)
            {
                string contractValue = trade[i].Conract;

                int year = int.Parse("20" + contractValue.Substring(2, 2)); 
                int month = int.Parse(contractValue.Substring(4, 2));       
                int day = int.Parse(contractValue.Substring(6, 2));        

                earliestDateTime = new DateTime(year, month, day, 00, 0, 0);
            }




            for (int i = 0; i < 24; i++)
            {

                DateTime currentDateTime = earliestDateTime.AddHours(i);
                int saat = currentDateTime.Hour;
                string saats = saat.ToString("D2");

                List<tradeModel> toplamislem = trade
                 .Where(x => x.Conract.Substring(8, 2) == saats)
                 .Select(x => new tradeModel { Conract = x.Conract, quantity = x.quantity })
                 .ToList();

                List<tradeModel> fiyat = trade
                 .Where(x => x.Conract.Substring(8, 2) == saats)
                 .Select(x => new tradeModel { Conract = x.Conract, Price = x.Price })
                 .ToList();


                decimal toplamfiyat = 0;
                for (int j = 0; j < toplamislem.Count; j++)
                {

                    toplamfiyat +=  (toplamislem[j].quantity * fiyat[j].Price) / 10;

                }

            


                islem.Add(new islem
                {
                    Tarih = currentDateTime.ToString(),
                    toplamIslem = (float)((float)toplamislem.Sum(x => x.quantity) / 10),
                    toplamIslemFiyat = toplamfiyat,
                    agirlikliOrtalama = toplamfiyat / ((decimal)toplamislem.Sum(x => x.quantity) / 10),


                });

            }





            return View(islem);
        }


    }
}
