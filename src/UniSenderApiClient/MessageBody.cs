using Newtonsoft.Json;

namespace UniSenderApiClient
{
    public class MessageBody
    {

        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("plaintext")]
        public string Plaintext { get; set; }
    }
}