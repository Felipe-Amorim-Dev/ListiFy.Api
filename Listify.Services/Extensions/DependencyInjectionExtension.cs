using Listify.Domain.Interfaces.Repositories;
using Listify.Domain.Interfaces.Services;
using Listify.Domain.Services;
using Listify.Infra.Data.Repositories;

namespace Listify.Services.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {           
            #region Domain

            services.AddScoped<IUsuarioDomainService, UsuarioDomainServices>();
            services.AddScoped<IItemDomainService, ItemDomainService>();            

            #endregion

            #region Infrastructure

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemFotoRepository, ItemFotoRepository>();

            #endregion

            return services;
        }
    }
}
