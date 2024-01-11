using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UniSenderApiClient
{
    class UniSenderClient : IUniSenderClient
    {
        private readonly UniSenderApiOptions _options;

        public UniSenderClient(UniSenderApiOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        private static readonly JsonSerializer Serializer = new JsonSerializer
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public async Task<UniSenderResult> SendAsync(UniSenderRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var urlBuilder = new UriBuilder(_options.BaseUrl)
            {
                Path = "/ru/transactional/api/v1/email/send.json"
            };

            request.Username ??= _options.Username;
            request.ApiKey ??= _options.ApiKey;

            var client = new HttpClient();

            StringContent content = null;
            await using (var text = new StringWriter())
            {
                Serializer.Serialize(text, request);

                content = new StringContent(text.ToString(), Encoding.UTF8, "application/json");
            }

            var response = await client.PostAsync(urlBuilder.Uri, content, cancellationToken);

            await using var stream = await response.Content.ReadAsStreamAsync();

            using var textReader = new StreamReader(stream);
            var message = await textReader.ReadToEndAsync();
            using var messageReader = new StringReader(message);
            using var json = new JsonTextReader(messageReader);
            return Serializer.Deserialize<UniSenderResult>(json);
        }
    }
}
