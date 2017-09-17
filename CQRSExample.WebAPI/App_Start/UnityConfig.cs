using System;
using Microsoft.Practices.Unity;
using CQRSExample.Data.Sql.StarterDb;
using MediatR;
using System.Reflection;
using System.Linq;

namespace CQRSExample.WebAPI.App_Start
{
    internal static class IUnityContainerExtensionsMediatR
    {
        public static IUnityContainer RegisterMediator(this IUnityContainer container, LifetimeManager lifetimeManager)
        {
            return container.RegisterType<IMediator, Mediator>(lifetimeManager)
                            .RegisterInstance<SingleInstanceFactory>(t => container.IsRegistered(t) ? container.Resolve(t) : null)
                            .RegisterInstance<MultiInstanceFactory>(t => container.IsRegistered(t) ? container.ResolveAll(t) : new object[0]);
        }

        public static IUnityContainer RegisterMediatorHandlers(this IUnityContainer container, Assembly assembly)
        {
            return container.RegisterTypesImplementingType(assembly, typeof(IRequestHandler<>))
                            .RegisterTypesImplementingType(assembly, typeof(IRequestHandler<,>))
                            .RegisterTypesImplementingType(assembly, typeof(IAsyncRequestHandler<>))
                            .RegisterTypesImplementingType(assembly, typeof(IAsyncRequestHandler<,>))
                            .RegisterTypesImplementingType(assembly, typeof(INotificationHandler<>))
                            .RegisterTypesImplementingType(assembly, typeof(IAsyncNotificationHandler<>));
        }

        /// <summary>
        ///     Register all implementations of a given type for provided assembly.
        /// </summary>
        public static IUnityContainer RegisterTypesImplementingType(this IUnityContainer container, Assembly assembly, Type type)
        {
            foreach (var implementation in assembly.GetTypes().Where(t => t.GetInterfaces().Any(implementation => IsSubclassOfRawGeneric(type, implementation))))
            {
                var interfaces = implementation.GetInterfaces();
                foreach (var @interface in interfaces)
                    container.RegisterType(@interface, implementation);
            }

            return container;
        }

        private static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var currentType = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == currentType)
                    return true;

                toCheck = toCheck.BaseType;
            }

            return false;
        }
    }

    internal static class IocExtensions
    {
        public static void BindInRequestScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new PerRequestLifetimeManager());
        }

        public static void BindInRequestScope<T1, T2>(this IUnityContainer container, InjectionConstructor constructor) where T2 : T1
        {
            container.RegisterType<T1, T2>(new PerRequestLifetimeManager(), constructor);
        }

        public static void BindInSingletonScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new ContainerControlledLifetimeManager());
        }
    }

    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            return RegisterTypes(container)
            .RegisterMediator(new HierarchicalLifetimeManager())
            .RegisterMediatorHandlers(Assembly.Load("CQRSExample.Domain.Plants"))
            .RegisterMediatorHandlers(Assembly.Load("CQRSExample.Domain.Workcenters"))
            .RegisterMediatorHandlers(Assembly.Load("CQRSExample.Domain.MaterialNumbers"));
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static IUnityContainer RegisterTypes(IUnityContainer container)
        {
            // EF context
            container.RegisterType<StarterDbEntities>(new PerRequestLifetimeManager());

            // AutoMapper instance, one per application.
            var mapperConfig = AutoMapperConfig.GetMapperConfiguration();
            var mapper = mapperConfig.CreateMapper();
            container.RegisterInstance(mapper)
                .RegisterInstance(mapperConfig);
            return container;
        }
    }
}