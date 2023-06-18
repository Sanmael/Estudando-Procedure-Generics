using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FrontEnd2.Models
{
    public class ApiResponse
    {
        [JsonPropertyName("sucess")]
        public bool Sucess { get; set; }
        [JsonPropertyName("data")]
        public object Data { get; set; }
        
    }
}
