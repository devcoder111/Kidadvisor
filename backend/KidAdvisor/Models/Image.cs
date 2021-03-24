using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KidAdvisor.Models
{
    public class Image
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        public string URL { get; set; }
    }
}
