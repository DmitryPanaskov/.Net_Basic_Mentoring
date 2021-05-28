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

            var resultFields = new List<FieldInfo>();
            GetAllHiddenFieldsRecursive(type, resultFields, property, obj, newValue);
        }

        public static void SetReadOnlyField(this object obj, string filedName, object newValue)
        {
            CheckNull(obj, newValue);
            CheckStringToNullOrEmpty(filedName);

            obj.SetFieldValue(filedName, newValue, Flags.AllMembers);
        }

        private static void GetAllHiddenFieldsRecursive(Type type, List<FieldInfo> fieldInfos, PropertyInfo property, object obj, object newValue)
        {
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            if (fields.Any())
            {
                fieldInfos.AddRange(fields);
            }

            var hiddenField = fields.FirstOrDefault(f => f.FieldType == property.PropertyType &&
                                                              f.Name.Contains($"<{property.Name}>"));

            if (hiddenField != null)
            {
                obj.SetFieldValue(hiddenField.Name, newValue, Flags.AllMembers);
            }

            if (type.BaseType != null)
            {
                GetAllHiddenFieldsRecursive(type.BaseType, fieldInfos, property, obj, newValue);
            }
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
