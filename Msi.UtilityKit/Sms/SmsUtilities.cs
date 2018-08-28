using System;
using System.Collections.Generic;
using System.Linq;

namespace Msi.UtilityKit.Sms
{
    public static class SmsUtilities
    {

        private static Dictionary<string, ISmsService> _smsServices = new Dictionary<string, ISmsService>();
        private static ISmsService _defaultSmsService;

        public static void AddService(string name, ISmsService service)
        {
            _smsServices.Add(name, service);
        }

        public static ISmsService GetService(string name)
        {
            if (_smsServices.ContainsKey(name))
            {
                return _smsServices[name];
            }
            return null;
        }

        public static void SendAsSms(this string text, string to)
        {
            SendSms(text, to);
        }

        public static void SendSms(string text, string to)
        {
            if (_defaultSmsService == null)
            {
                var values = _smsServices.Values;
                if (values != null && values.Count() > 0)
                {
                    _defaultSmsService = values.First();
                }
            }
            if (_defaultSmsService == null)
            {
                throw new NullReferenceException("Could not found any Sms service.");
            }
            _defaultSmsService.Send(text, to);
        }

    }
}
