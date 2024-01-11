using Newtonsoft.Json;

namespace UniSenderApiClient
{
    public class MessageOptions
    {

        [JsonProperty("unsubscribe_url")]
        public string UnsubscribeUrl { get; set; }
    }
}