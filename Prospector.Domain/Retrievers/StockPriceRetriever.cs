using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Prospector.Domain.Contracts.Retrievers;
using Prospector.Domain.Entities;

namespace Prospector.Domain.Retrievers
{
    public class StockPriceRetriever : IStockPriceRetriever
    {
        public IList<StockPriceData> GetPrices(IList<TransactionData> transactions)
        {
            var codes = new List<String>();
            foreach (var item in transactions)
            {
                codes.Add(String.Concat("\"", item.Code, ".L", "\""));
            }

            var requestUrl = String.Format("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20({0})&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=",
               HttpUtility.UrlEncode(String.Join(",", codes)));

            var httpClientWrapper = new HttpClient();
            var result = httpClientWrapper.GetAsync(requestUrl).Result.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<StockPriceDataContainer>(result.Result);

            return results.Query.Results.Quote;
        }
    }
}
