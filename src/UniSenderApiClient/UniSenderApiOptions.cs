using System;

namespace UniSenderApiClient
{
    public class UniSenderApiOptions
    {
        private const string DefaultUrl = "https://go1.unisender.ru";
        private Uri _baseUrl;

        public Uri BaseUrl
        {
            get => _baseUrl ?? new Uri(DefaultUrl);
            set => _baseUrl = value;
        }
        public string Username { get; set; }
        public string ApiKey { get; set; }
    }
}