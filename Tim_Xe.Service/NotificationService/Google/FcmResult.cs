using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Service.NotificationService.Google
{
    public class FcmResult
    {
        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        [JsonProperty("registration_id")]
        public string RegistrationId { get; set; }

        public string Error { get; set; }
    }
}
