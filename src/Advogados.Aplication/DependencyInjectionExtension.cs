using Advogados.Application.AutoMapper;
using Advogados.Application.Interfaces;
using Advogados.Application.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Advogados.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperAdvogado());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IAdvogadoService, AdvogadoService>();
        }
    }
}
