using System;
using Newtonsoft.Json;

namespace Prospector.Domain.Entities
{
    public class StockPriceData
    {
        [JsonProperty("Symbol")]
        public String Code { get; set; }

        [JsonProperty("Ask")]
        public Decimal Ask { get; set; }

        [JsonProperty("Name")]
        public String Name { get; set; }
    }
    
    public class StockPriceDataContainer
    {
        public StockPriceResultContainer Query { get; set; }
    }

    public class StockPriceResultContainer
    {
        public int Count { get; set; }
        public StockPriceQuoteContainer Results { get; set; }
    }

    public class StockPriceQuoteContainer
    {
        public StockPriceData[] Quote { get; set; }
    }
}
