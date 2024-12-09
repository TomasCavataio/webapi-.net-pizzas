using System.Reflection;

namespace webapi.core.ioc
{
    public static class AddInjectablesIoc
    {
        private readonly record struct Types(Type Type,
            IEnumerable<Type> Interfaces,
            InjectType? InjectType
        );
        public static IServiceCollection AddInjectables(this IServiceCollection services)
        {
            var types = AnottatedTypes;
            foreach (var type in types)
            {
                foreach (var Interface in type.Interfaces)
                {

                    if (type.InjectType == InjectType.Transient)
                    {
                        services.AddTransient(Interface, type.Type);
                    }
                    else if (type.InjectType == InjectType.Scoped)
                    {
                        services.AddScoped(Interface, type.Type);
                    }
                    else if (type.InjectType == InjectType.Singleton)
                    {
                        services.AddSingleton(Interface, type.Type);
                    }

                }

            }

            return services;
        }

        private static IEnumerable<Types> AnottatedTypes
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                return assembly.GetTypes()
                    .Where(t => t.GetCustomAttributes<Injectable>().Any())
                    .Select(t => new Types(
                        t,
                        t.GetInterfaces(),
                        GetInjectType(t)));                    
            }
        }

        private static InjectType? GetInjectType(Type type)
        {
            return type.GetCustomAttribute<Injectable>()?.InjectType;
        }
    }
}