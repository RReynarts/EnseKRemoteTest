using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Services;
using Contracts;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Services.IoC
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEnsekServices(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollectionExtension).Assembly;
            var requestHandlers = GetAllTypesImplementingOpenGenericType(typeof(IRequestHandler<,>), assembly);
            foreach (var requestHandler in requestHandlers)
            {
                services.AddScoped(requestHandler.GetInterfaces().First(), requestHandler);
            }

            var types = assembly.DefinedTypes.ToList();
            var validators = types.Where(t => t.ImplementedInterfaces.Any(i => i == typeof(IValidator)) && !t.IsAbstract).ToList();
            foreach (var validator in validators)
            {
                services.AddScoped(validator);
            }
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IMeterService, MeterService>();

            return services;
        }

        public static IEnumerable<Type> GetAllTypesImplementingOpenGenericType(Type openGenericType, Assembly assembly)
        {
            return from x in assembly.GetTypes().Where(t => !t.IsAbstract)
                from z in x.GetInterfaces()
                let y = x.BaseType
                where
                    (y != null && y.IsGenericType &&
                     openGenericType.IsAssignableFrom(y.GetGenericTypeDefinition())) ||
                    (z.IsGenericType &&
                     openGenericType.IsAssignableFrom(z.GetGenericTypeDefinition()))
                select x;
        }
    }
}
