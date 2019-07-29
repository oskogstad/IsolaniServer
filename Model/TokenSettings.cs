using Newtonsoft.Json;

namespace Isolani.Model
{
    [JsonObject("tokenSettings")]
    public class TokenSettings
    {
        [JsonProperty("secret")]
        public string Secret { get; set; }
        
        [JsonProperty("issuer")]
        public string Issuer { get; set; }
        
        [JsonProperty("expirationTimeInMinutes")]
        public int ExpirationTimeInMinutes { get; set; }
    }
}