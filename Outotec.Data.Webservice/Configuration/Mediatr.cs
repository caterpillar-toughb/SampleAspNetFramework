using Autofac;
using MediatR;
using Outotec.Data.Business;
using System.Reflection;

namespace Outotec.Data.Webservice.Configuration
{
    public static class Mediatr
    {
        public static ContainerBuilder AddMediatRUsingAutoFac(this ContainerBuilder builder)
        {
            var assembly = typeof(AssemblyMarker).GetTypeInfo().Assembly;

            // Register IMediator
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            // Register Mediator derived types.
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IRequestHandler<>));
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(INotificationHandler<>));

            builder.Register<ServiceFactory>(componentContext =>
            {
                var context = componentContext.Resolve<IComponentContext>();
                return serviceType => context.Resolve(serviceType);
            });

            return builder;
        }
    }
}