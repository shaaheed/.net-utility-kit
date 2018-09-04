using System;
using System.Linq;
using System.Reflection;

namespace Msi.UtilityKit
{
    public static class ObjectUtilities
    {
        /// <summary>
        /// Gets the value of an object by the property name.
        /// Property name can be dotted. eg. "Nested.Property"
        /// </summary>
        public static object GetValue(this object source, string property, object defaultValue = null)
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
                return defaultValue;
            }
            return _source;
        }

        public static T GetValue<T>(this object source, string property, T defaultValue = default(T))
        {
            var value = source.GetValue(property);
            if (value != null)
            {
                return (T)value;
            }
            return defaultValue;
        }

        /// <summary>
        /// Sets the value to an object by the property name.
        /// Property name can be dotted. eg. "Nested.Property"
        /// </summary>
        public static bool SetValue(this object source, object value, string property)
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
        public static PropertyInfo GetProperty(this object source, string name)
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
        public static object InvokeMethod(this object source, string dottedMethodName, params object[] parameters)
        {
            (object _source, MethodInfo method) = GetMethodAndSource(source, dottedMethodName);
            if (_source != null && method != null)
            {
                method.Invoke(_source, parameters);
            }
            return null;
        }

        public static T InvokeMethod<T>(this object source, string dottedMethodName, params object[] parameters)
        {
            return (T)source.InvokeMethod(dottedMethodName, parameters);
        }

        public static object InvokeGenericMethod<T>(this object source, string methodName, params object[] parameters)
        {
            return InvokeGenericMethod(source, methodName, typeof(T), parameters);
        }

        public static object InvokeGenericMethod(this object source, string methodName, Type typeArgument, params object[] parameters)
        {
            var method = source.GetType().GetMethod(methodName);
            var genericMethod = method.MakeGenericMethod(typeArgument);
            return genericMethod.Invoke(source, parameters);
        }

        public static object InvokeGenericMethod(this object source, string methodName, Type[] typeArguments, params object[] parameters)
        {
            var method = source.GetType().GetMethod(methodName);
            var genericMethod = method.MakeGenericMethod(typeArguments);
            return genericMethod.Invoke(source, parameters);
        }

        /// <summary>
        /// Gets the method of an object by the method name.
        /// </summary>
        public static MethodInfo GetMethod(this object source, string name)
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

        public static (object, MethodInfo) GetMethodAndSource(this object source, string dottedMethodName)
        {
            string[] properties = dottedMethodName.Split('.');
            int count = properties.Length;
            if (count == 1)
            {
                return (source, GetMethod(source, dottedMethodName));
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
                return (_source, GetMethod(_source, properties[count - 1]));
            }
            return (null, null);
        }

        public static void CopyProperties(this object source, object destination)
        {
            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();
            var results = from srcProp in typeSrc.GetProperties()
                          let targetProperty = typeDest.GetProperty(srcProp.Name)
                          where srcProp.CanRead
                          && targetProperty != null
                          && (targetProperty.GetSetMethod(true) != null && !targetProperty.GetSetMethod(true).IsPrivate)
                          && (targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) == 0
                          && targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType)
                          select new { sourceProperty = srcProp, targetProperty = targetProperty };
            foreach (var props in results)
            {
                props.targetProperty.SetValue(destination, props.sourceProperty.GetValue(source, null), null);
            }
        }

        public static void DynamicUsing(this object resource, Action action)
        {
            try
            {
                action();
            }
            finally
            {
                IDisposable d = resource as IDisposable;
                d?.Dispose();
            }
        }

    }
}
