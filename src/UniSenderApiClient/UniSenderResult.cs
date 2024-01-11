using System.Collections.Generic;
using Newtonsoft.Json;

namespace UniSenderApiClient
{
    public class UniSenderResult
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("job_id")]
        public string JobId { get; set; }

        [JsonProperty("emails")]
        public ICollection<string> Emails { get; set; }

        [JsonProperty("failed_emails")]
        public Dictionary<string,string> FailedEmails { get; set; }
    }
}