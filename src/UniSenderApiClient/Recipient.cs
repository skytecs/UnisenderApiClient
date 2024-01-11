using System.Collections.Generic;
using Newtonsoft.Json;

namespace UniSenderApiClient
{
    public class Recipient
    {

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("substitutions")]
        public IDictionary<string, string> Substitutions { get; set; }

        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; }
    }
}