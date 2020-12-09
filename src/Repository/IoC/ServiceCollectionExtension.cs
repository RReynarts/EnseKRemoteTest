using Common.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Repository.IoC
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEnsekRepository(this IServiceCollection services)
        {
            services.AddScoped<EnsekDbContext>();
            services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<EnsekDbContext>());
            services.AddScoped<DbContext>(x => x.GetRequiredService<EnsekDbContext>());
            services.AddScoped(typeof(IDataSet<>), typeof(DataSet<>));
            services.AddScoped<IDataSetFactory, DataSetFactory>();

            return services;
        }
    }
}
