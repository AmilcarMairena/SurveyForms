using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyFormWebApp.Models
{
    public class TokenDTO
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expiresAt")]
        public DateTime ExpiresAt { get; set; }
    }
}
