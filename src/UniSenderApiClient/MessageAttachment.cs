using Newtonsoft.Json;

namespace UniSenderApiClient
{
    public class MessageAttachment
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}