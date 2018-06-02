using System;
using System.Reflection;

namespace Msi.UtilityKit
{
    public static class ObjectUtil
    {
        /// <summary>
        /// Gets the value of an object by the property name.
        /// Property name can be dotted. eg. "Nested.Property"
        /// </summary>
        public static object GetValue(object source, string property)
        {
            string[] properties = property.Split('.');
            object _source = source;
            PropertyInfo _property = null;
            int count = properties.Length;
            for (int i = 0; i < count; i++)
            {
                _property = GetProperty(_source, properties[i]);
                _source = _property?.GetValue(_source);
                if (_source == null || _property == null)
                {
                    break;
                }
            }
            if (_source == null || _property == null)
            {
                return null;
            }
            return _source;
        }

        /// <summary>
        /// Sets the value to an object by the property name.
        /// Property name can be dotted. eg. "Nested.Property"
        /// </summary>
        public static bool SetValue(object source, object value, string property)
        {
            string[] properties = property.Split('.');
            object _source = source;
            PropertyInfo _property = null;
            int count = properties.Length;
            for (int i = 0; i < count; i++)
            {
                _property = GetProperty(_source, properties[i]);
                if (_property == null) { return false; }
                if (i != properties.Length - 1)
                {
                    _source = _property.GetValue(_source);
                }
            }
            if (_source != null)
            {
                _property.SetValue(_source, value);
                return true;
            }
            return _source == null;
        }

        /// <summary>
        /// Gets the property of an object by the name.
        /// </summary>
        public static PropertyInfo GetProperty(object source, string name)
        {
            PropertyInfo[] properties = source.GetType().GetProperties();
            int count = properties.Length;
            for (int i = 0; i < count; i++)
            {
                if (properties[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return properties[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Invoke the method of an object by the method name. Name can be dotted.
        /// </summary>
        public static object InvokeMethod(object source, string method, params object[] parameters)
        {
            string[] properties = method.Split('.');
            int count = properties.Length;
            if (count == 1)
            {
                MethodInfo _method = GetMethod(source, method);
                return _method?.Invoke(source, parameters);
            }

            object _source = source;
            PropertyInfo _property = null;
            for (int i = 0; i < count; i++)
            {
                _property = GetProperty(_source, properties[i]);
                if (_property == null) { break; }
                if (i != properties.Length - 1)
                {
                    _source = _property.GetValue(_source);
                }
            }
            if (_source != null)
            {
                MethodInfo _method = GetMethod(_source, properties[count - 1]);
                return _method?.Invoke(_source, parameters);
            }
            return null;
        }

        /// <summary>
        /// Gets the method of an object by the method name.
        /// </summary>
        public static MethodInfo GetMethod(object source, string name)
        {
            MethodInfo[] methods = source.GetType().GetMethods();
            int count = methods.Length;
            for (int i = 0; i < count; i++)
            {
                if (methods[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return methods[i];
                }
            }
            return null;
        }

    }
}
