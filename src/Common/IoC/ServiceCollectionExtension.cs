using Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Common.IoC
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEnsekCommon(this IServiceCollection services)
        {
            services.AddSingleton<IRequestHandlerFactory, RequestHandlerFactory>();
            services.AddSingleton<IRequestHandlerMediator, RequestHandlerMediator>();

            return services;
        }
    }
}
