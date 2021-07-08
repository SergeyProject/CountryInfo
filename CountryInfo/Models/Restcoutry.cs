using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CountryInfo.Models
{
  public  class Restcoutry
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }     

        [JsonPropertyName("capital")]
        public string Capital { get; set; }


        [JsonPropertyName("alpha2Code")]
        public string Alpha2Code { get; set; }


        [JsonPropertyName("region")]
        public string Region { get; set; }


        [JsonPropertyName("population")]
        public int Population { get; set; }


        [JsonPropertyName("area")]
        public float Area { get; set; }

    }
}
