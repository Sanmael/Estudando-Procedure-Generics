using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FrontEnd2.Models
{
    public class PersonViewModel
    {
        [JsonPropertyName("personId")]
        public long PersonId { get; set; }

        [JsonPropertyName("personName")]
        public string PersonName { get; set; }

        [JsonPropertyName("taxNumber")]
        public string TaxNumber { get; set; }
    }
}
