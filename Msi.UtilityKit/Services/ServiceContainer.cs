using System;
using System.Collections.Generic;
using System.Linq;

namespace Msi.UtilityKit.Services
{
    public class ServiceContainer<TService>
    {

        private Dictionary<string, TService> _services = new Dictionary<string, TService>();

        private TService _defaultService;

        public void Add(string name, TService service)
        {
            _services.Add(name, service);
        }

        public TService Get(string name)
        {
            if (_services.ContainsKey(name))
            {
                return _services[name];
            }
            return default(TService);
        }

        public TService GetDefault()
        {
            if (_defaultService == null)
            {
                var values = _services.Values;
                if (values != null && values.Count() > 0)
                {
                    _defaultService = values.First();
                }
            }
            if (_defaultService == null)
            {
                throw new NullReferenceException("Could not found any Sms service.");
            }
            return _defaultService;
        }

    }
}
