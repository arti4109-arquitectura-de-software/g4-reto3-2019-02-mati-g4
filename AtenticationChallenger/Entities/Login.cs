using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtenticationChallenger.Entities
{
    public class Login
    {

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }
        [JsonProperty(PropertyName = "config")]
        public string Config { get; set; }

    }
}
