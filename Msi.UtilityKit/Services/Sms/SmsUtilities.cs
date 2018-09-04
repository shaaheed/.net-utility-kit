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

        /// <summary>
        /// if service is null, it will try to send sms by default sms service provider.
        /// </summary>
        public static void SendSms(string text, string to, string service = null)
        {
            ISmsService _smsService = null;
            if (service == null)
            {
                _smsService = _services.GetDefault();
            }
            else
            {
                _smsService = _services.Get(service);
                if(_smsService == null)
                {
                    throw new NullReferenceException($"Could not find the {service} sms service.");
                }
            }
            _smsService?.Send(text, to);
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
