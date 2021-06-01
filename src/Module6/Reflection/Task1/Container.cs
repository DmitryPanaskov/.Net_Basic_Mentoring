using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Task1.DoNotChange;

namespace Task1
{
    public class Container
    {
        private readonly HashSet<Type> _types = new HashSet<Type>();

        public void AddAssembly(Assembly assembly)
        {
            CheckNull(assembly);

            foreach (var item in assembly.GetTypes())
            {
                AddIfNotExist(item);
            }
        }

        public void AddType(Type type)
        {
            CheckNull(type);
            AddIfNotExist(type);
        }

        public void AddType(Type type, Type baseType)
        {
            CheckNull(type, baseType);

            AddIfNotExist(type);
            AddIfNotExist(baseType);
        }

        public T Get<T>()
        {
            if (!_types.Any(type => typeof(T) == type))
            {
                throw new IoCException("You don't registered dependency with this type");
            }

            return (T)GetInstance(typeof(T));
        }

        private object GetInstance(Type requestedType)
        {
            if (requestedType.IsInterface)
            {
                requestedType = GetTypeIfExist(item => requestedType.IsAssignableFrom(item) &&
                                                  item.GetCustomAttribute<ExportAttribute>()?.Contract == requestedType);
            }

            if (requestedType.IsClass)
            {
                return GetByImportConstructorAttributes(requestedType) ?? GetByProperyAttributes(requestedType);
            }

            return Activator.CreateInstance(requestedType);
        }

        private object GetByProperyAttributes(Type requestedType)
        {
            var resultType = GetTypeIfExist(item => item == requestedType);
            var resultInstanse = Activator.CreateInstance(resultType);

            var properties = resultType.GetProperties().Where(prop => prop.GetCustomAttributes<ImportAttribute>().Any());

            foreach (var property in properties)
            {
                var propertyDependensy = GetTypeIfExist(type => type == property.PropertyType);
                property.SetValue(resultInstanse, GetInstance(propertyDependensy), null);
            }

            return resultInstanse;
        }

        private object GetByImportConstructorAttributes(Type requestedType)
        {
            if (requestedType.GetCustomAttribute<ImportConstructorAttribute>() is null)
            {
                return null;
            }

            var constructors = requestedType.GetConstructors().ToList();

            if (!constructors.Any())
            {
                throw new IoCException($"You don't have public constructors in class {requestedType.Name}");
            }

            if (constructors.Count > 1)
            {
                throw new IoCException($"You have more than one public constructor in class {requestedType.Name}");
            }

            var constructor = constructors.First();
            var parameters = constructor.GetParameters();

            return constructor.Invoke(parameters.Select(parameter => GetInstance(GetTypeIfExist(type => type == parameter.ParameterType))).ToArray());
        }

        private Type GetTypeIfExist(Func<Type, bool> predicate = null)
        {
            var elements = _types.Where(predicate).ToList();

            if (!elements.Any())
            {
                throw new IoCException($"You can't get type from registered list of types.");
            }

            return elements.First();
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

        private void AddIfNotExist(Type type)
        {
            if (!_types.Add(type))
            {
                throw new IoCException($"You have more that one registered dependensy");
            }
        }
    }
}