using System.Collections.Generic;
using Newtonsoft.Json;

namespace UniSenderApiClient
{
    public class UniSenderMessage
    {

        [JsonProperty("template_engine")]
        public TemplateEngine TemplateEngine { get; set; }

        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        [JsonProperty("global_substitutions")]
        public IDictionary<string, string> GlobalSubstitutions { get; set; }

        [JsonProperty("body")]
        public MessageBody Body { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("from_email")]
        public string FromEmail { get; set; }

        [JsonProperty("from_name")]
        public string FromName { get; set; }

        [JsonProperty("track_links")]
        public int TrackLinks { get; set; }

        [JsonProperty("track_read")]
        public int TrackRead { get; set; }

        [JsonProperty("recipients")]
        public ICollection<Recipient> Recipients { get; set; }

        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; }

        [JsonProperty("headers")]
        public IDictionary<string,string> Headers { get; set; }

        [JsonProperty("attachments")]
        public ICollection<MessageAttachment> Attachments { get; set; }

        [JsonProperty("inline_attachments")]
        public ICollection<InlineAttachment> InlineAttachments { get; set; }

        [JsonProperty("options")]
        public MessageOptions Options { get; set; }
    }
}