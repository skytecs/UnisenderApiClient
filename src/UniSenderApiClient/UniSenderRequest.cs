using Newtonsoft.Json;

namespace UniSenderApiClient
{
    public class UniSenderRequest
    {

        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("message")]
        public UniSenderMessage Message { get; set; }
    }
}