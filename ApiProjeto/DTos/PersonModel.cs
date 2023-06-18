using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiProjeto.DTos
{
    public class PersonModel 
    {
        [JsonPropertyName("personId")]
        public long PersonId { get; set; }
        [JsonPropertyName("personName")]
        public string PersonName { get; set; }
        [JsonPropertyName("taxNumber")]
        public string TaxNumber { get; set; }        
    }
}
