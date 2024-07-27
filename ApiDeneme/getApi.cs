
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ApiDeneme.Models;
using Newtonsoft.Json;

namespace ApiDeneme
{
    public class getApi
    {

        public readonly HttpClient _httpClient;
        public const string BaseUrl = "https://seffaflik.epias.com.tr/transparency/service/market/intra-day-trade-history?endDate=2022-12-24&startDate=2022-12-24";


        public getApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<tradeModel>> getTrade()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<apiResponse>(jsonResponse);

                var filteredTradeList = apiResponse.Body.IntraDayTradeHistoryList
                                          .Where(trade => trade.Conract.StartsWith("PH"))
                                          .ToList();

                return filteredTradeList;
            }
            return null;
        }



    }
}
