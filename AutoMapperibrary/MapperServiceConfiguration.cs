using AutoMapper;
using Hotel.AutoMapperLibrary;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.AutoMapperLibrary
{
    public static class MapperServiceConfiguration
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
