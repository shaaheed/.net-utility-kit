using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Msi.UtilityKit.Services.Sms.Twilio
{
    public class TwilioSmsService : ISmsService
    {
        private string _sid;
        private string _token;
        private string _from;
        private const string BASE_URL = "https://api.twilio.com/2010-04-01/Accounts";

        public TwilioSmsService(string from, string sid, string token)
        {
            _from = from;
            _sid = sid;
            _token = token;
        }

        public void Send(string text, string to)
        {
            HttpClient httpClient = new HttpClient();
            var credential = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_sid}:{_token}"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credential);
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["To"] = to,
                ["From"] = _from,
                ["Body"] = text
            });
            var url = $"{BASE_URL}/{_sid}/Messages";
            var response = httpClient.PostAsync(url, content).Result;
        }
    }
}
