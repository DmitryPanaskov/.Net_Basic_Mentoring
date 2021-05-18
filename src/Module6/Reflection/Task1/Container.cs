using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Task1.DoNotChange;

namespace Task1
{
    public class Container
    {
        private readonly List<Type> _types = new List<Type>();

        public void AddAssembly(Assembly assembly)
        {
            CheckNull(assembly);

            _types.AddRange(assembly.GetTypes().ToList());
        }

        public void AddType(Type type)
        {
            CheckNull(type);

            _types.Add(type);
        }

        public void AddType(Type type, Type baseType)
        {
            CheckNull(type, baseType);

            _types.Add(type);
            _types.Add(baseType);
        }

        public T Get<T>()
        {
            if (!_types.Any() || !_types.Any(type => typeof(T) == type))
            {
                throw new IoCException("You don't registered dependency with this type");
            }

            return (T)GetInstance(typeof(T));
        }

        private object GetInstance(Type type)
        {
            if (type.IsInterface)
            {
                type = GetInterface(type);
            }

            if (type.IsClass)
            {
                if (type.GetCustomAttribute<ImportConstructorAttribute>() != null)
                {
                    var ctors = type.GetConstructors().ToList();

                    if (!ctors.Any())
                    {
                        throw new IoCException($"You don't have public constructors in class {type.Name}");
                    }

                    if (ctors.Count > 1)
                    {
                        throw new IoCException($"You have more than one public constructor in class {type.Name}");
                    }

                    var ctor = ctors.First();
                    var parameters = ctor.GetParameters();

                    var parameterInstanses = new List<object>();

                    if (parameters.Any())
                    {
                        foreach (var parameter in parameters)
                        {
                            var propDependensy = GetTypeIfExist(type => type == parameter.ParameterType);

                            if (propDependensy.IsInterface)
                            {
                                propDependensy = GetInterface(propDependensy);
                            }

                            if (propDependensy.IsClass)
                            {
                                var propInstanse = GetInstance(propDependensy);
                                parameterInstanses.Add(propInstanse);
                            }
                        }
                    }

                    return ctor.Invoke(parameterInstanses.ToArray());
                }

                var tClass = GetTypeIfExist(item => item == type);
                var tClassInstanse = Activator.CreateInstance(tClass);

                var properties = tClass.GetProperties().Where(prop => prop.GetCustomAttributes<ImportAttribute>().Any());

                if (properties.Any())
                {
                    foreach (var property in properties)
                    {
                        var propDependensy = GetTypeIfExist(type => type == property.PropertyType);

                        if (propDependensy.IsInterface)
                        {
                            propDependensy = GetInterface(propDependensy);
                        }

                        if (propDependensy.IsClass)
                        {
                            var propInstanse = GetInstance(propDependensy);
                            property.SetValue(tClassInstanse, propInstanse, null);
                        }
                    }
                }

                return tClassInstanse;
            }

            return Activator.CreateInstance(type);
        }

        private Type GetInterface(Type type)
        {
            if (_types.Any(item => type.IsAssignableFrom(item)))
            {
                return GetTypeIfExist(item => type.IsAssignableFrom(item) &&
                                                   item.GetCustomAttribute<ExportAttribute>()?.Contract == type);
            }

            throw new IoCException($"You haven't assignable registered class for this interface: {type.Name}");
        }

        private Type GetTypeIfExist(Func<Type, bool> predicate = null)
        {
            var elements = _types.Where(predicate).ToList();

            if (!elements.Any())
            {
                throw new IoCException($"You can't get type from registered list of types.");
            }

            if (elements.Count > 1)
            {
                throw new IoCException($"You have more that one registered dependensy");
            }

            return elements.First();

        }

        private void CheckNull<T>(params T[] t)
        {
            foreach (var item in t)
            {
                if (item is null)
                {
                    throw new ArgumentNullException(nameof(item));
                }
            }
        }
    }
}