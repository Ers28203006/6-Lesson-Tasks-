using Newtonsoft.Json;
using System.Collections.Generic;

namespace RandomFamousQuotes.Models
{
    public class Country
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("capital")]
        public string Capital { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("subregion")]
        public string Subregion { get; set; }

        [JsonProperty("population")]
        public int Population { get; set; }

        [JsonProperty("area")]
        public double? Area { get; set; }

        [JsonProperty("gini")]
        public double? Gini { get; set; }

        [JsonProperty("timezones")]
        public IList<string> Timezones { get; set; }

        [JsonProperty("nativeName")]
        public string NativeName { get; set; }

        [JsonProperty("numericCode")]
        public string NumericCode { get; set; }

        [JsonProperty("currencies")]
        public IList<string> Currencies { get; set; }
    }
}
