using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Skytecs.UniSenderApiClient
{
    class UniOneClient : IUniOneClient
    {
        private readonly UniOneOptions _options;
        private readonly ILogger<IUniOneClient> _logger;

        public UniOneClient(UniOneOptions options, ILogger<IUniOneClient> logger)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
        }

        private static JsonSerializer _serializer = new JsonSerializer
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        //private async Task<TResult> MakeCall<TResult>(string method, IDictionary<string, string[]> args, CancellationToken cancellationToken)
        //{
        //    var uriBuilder = new UriBuilder(_options.BaseUrl);
        //    uriBuilder.Path = $"{_options.Lang}/api/{method}";

        //    var argsList = new List<KeyValuePair<string, string>>();
        //    argsList.Add(new KeyValuePair<string, string>("format", _options.Format));
        //    argsList.Add(new KeyValuePair<string, string>("api_key", _options.ApiKey));

        //    if (args != null)
        //    {
        //        argsList.AddRange(args.SelectMany(x => x.Value.Select(value => new KeyValuePair<string, string>(x.Key, value))));
        //    }

        //    var client = new HttpClient();

        //    var content = new FormUrlEncodedContent(argsList);


        //    var response = await client.PostAsync(uriBuilder.Uri, content, cancellationToken);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new HttpRequestException();
        //    }

        //    using (var ms = new MemoryStream())
        //    using (var sr = new StreamReader(ms))
        //    using (var jr = new JsonTextReader(sr))
        //    {
        //        await response.Content.CopyToAsync(ms);
        //        return _serializer.Deserialize<TResult>(jr);
        //    }
        //}

        public async Task<UniOneSendResult> Send(UniOneSendRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }


            var urlBuilder = new UriBuilder(_options.BaseUrl)
            {
                Path = "/ru/transactional/api/v1/email/send.json"
            };

            request.Username = request.Username ?? _options.Username;
            request.ApiKey = request.ApiKey ?? _options.ApiKey;

            var client = new HttpClient();

            StringContent content = null;
            using (var text = new StringWriter())
            {
                _serializer.Serialize(text, request);

                _logger.LogTrace(text.ToString());

                content = new StringContent(text.ToString(), Encoding.UTF8, "application/json");
            }

            var response = await client.PostAsync(urlBuilder.Uri, content);

            if (!response.IsSuccessStatusCode)
            {
                var message = $"Error sending email: {response.StatusCode} - {response.ReasonPhrase}";

                _logger.LogError(message);
            }

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var text = new StreamReader(stream))
            {
                var message = text.ReadToEnd();
                _logger.LogTrace(message);
                using (var messageReader = new StringReader(message))
                using (var json = new JsonTextReader(messageReader))
                {
                    return _serializer.Deserialize<UniOneSendResult>(json);
                }
            }

        }
    }


    public class Body
    {

        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("plaintext")]
        public string Plaintext { get; set; }
    }

    public class Recipient
    {

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("substitutions")]
        public IDictionary<string, string> Substitutions { get; set; }

        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; }
    }

    public static class UniOneHeaders
    {
        public const string XReplyTo = "X-ReplyTo";
    }

    public class UniOneAttachment
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }

    public class UniOneInlineAttachment
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }

    public class Options
    {

        [JsonProperty("unsubscribe_url")]
        public string UnsubscribeUrl { get; set; }
    }

    public class UniOneMessage
    {

        [JsonProperty("template_engine")]
        public UniOneTemplateEngine TemplateEngine { get; set; }

        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        [JsonProperty("global_substitutions")]
        public IDictionary<string,string> GlobalSubstitutions { get; set; }

        [JsonProperty("body")]
        public Body Body { get; set; }

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
        public ICollection<UniOneAttachment> Attachments { get; set; }

        [JsonProperty("inline_attachments")]
        public ICollection<UniOneInlineAttachment> InlineAttachments { get; set; }

        [JsonProperty("options")]
        public Options Options { get; set; }
    }

    public class UniOneSendRequest
    {

        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("message")]
        public UniOneMessage Message { get; set; }
    }

    public class UniOneSendResult
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
