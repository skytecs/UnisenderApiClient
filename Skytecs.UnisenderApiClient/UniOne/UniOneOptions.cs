using Microsoft.AspNetCore.Http;
using System;

namespace Skytecs.UniSenderApiClient
{
    public class UniOneOptions
    {
        private const string DefaultUrl = "https://one.unisender.com";
        private Uri _baseUrl;
        private string _lang;
        private string _format;

        public Uri BaseUrl
        {
            get => _baseUrl ?? new Uri(DefaultUrl);
            set => _baseUrl = value;
        }
        public string Username { get; set; }
        public string ApiKey { get; set; }
    }
}