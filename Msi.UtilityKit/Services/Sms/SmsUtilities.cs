using System;
using System.Collections.Generic;

namespace Msi.UtilityKit.Services.Sms
{
    public static class SmsUtilities
    {

        private static ServiceContainer<ISmsService> _services = new ServiceContainer<ISmsService>();

        public static void ConfigureService(Action<ServiceContainer<ISmsService>> options)
        {
            options?.Invoke(_services);
        }

        public static void SendAsSms(this string text, string to, string service = null)
        {
            SendSms(text, to, service);
        }

        public static void SendSms(string text, string to, string service = null)
        {
            var _service = _services.Get(service ?? string.Empty) ?? _services.GetDefault();
            _service.Send(text, to);
        }

        public static void SendSms(string text, IEnumerable<string> tos, string service)
        {
            foreach (var to in tos)
            {
                SendSms(text, to, service);
            }
        }

    }
}
