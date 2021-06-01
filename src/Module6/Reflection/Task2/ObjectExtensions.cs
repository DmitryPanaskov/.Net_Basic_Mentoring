using Fasterflect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Task2
{
    public static class ObjectExtensions
    {
        public static void SetReadOnlyProperty(this object obj, string propertyName, object newValue)
        {
            CheckNull(obj, newValue);
            CheckStringToNullOrEmpty(propertyName);

            var type = obj.GetType();
            var property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            if (property is null)
            {
                throw new ArgumentException($"Property with name {propertyName} does not exist");
            }

            obj.SetFieldValue($"<{propertyName}>k__BackingField", newValue, Flags.AllMembers);
        }

        public static void SetReadOnlyField(this object obj, string filedName, object newValue)
        {
            CheckNull(obj, newValue);
            CheckStringToNullOrEmpty(filedName);

            obj.SetFieldValue(filedName, newValue, Flags.AllMembers);
        }

        private static void CheckNull<T>(params T[] t)
        {
            foreach (var item in t)
            {
                if (EqualityComparer<T>.Default.Equals(item, default(T)))
                {
                    throw new ArgumentNullException(nameof(item));
                }
            }
        }

        private static void CheckStringToNullOrEmpty(string filedName)
        {
            if (string.IsNullOrEmpty(filedName))
            {
                throw new ArgumentException("Property name is empty");
            }
        }
    }
}
