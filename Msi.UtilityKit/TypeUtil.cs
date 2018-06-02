using System;
using System.Linq;
using System.Reflection;

namespace Msi.UtilityKit
{
    public static class TypeUtil
    {

        /// <summary>
        /// Find a type by name where reference type is located. T =  ReferenceType
        /// </summary>
        public static Type FindByName<T>(string name)
        {
            return FindByName(typeof(T).Assembly, name);
        }

        public static Type FindByName(Assembly assembly, string name)
        {
            Type[] types = assembly.GetTypes();
            int typeCount = types.Length;
            for (int i = 0; i < typeCount; i++)
            {
                if (types[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return types[i];
                }
            }
            return default(Type);
        }

        /// <summary>
        /// Determine whether a type is simple (String, Decimal, DateTime, etc) 
        /// or complex (i.e. custom class with public properties and methods).
        /// </summary>
        public static bool IsSimpleType(
            this Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                new Type[] {
                typeof(String),
                typeof(Decimal),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(TimeSpan),
                typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }

    }
}
