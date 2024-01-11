using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UniSenderApiClient
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TemplateEngine
    {
        [EnumMember(Value = "simple")]
        Simple,
        [EnumMember(Value = "velocity")]
        Velocity
    }
}
