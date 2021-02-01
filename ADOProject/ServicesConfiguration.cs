using ADOProject.Services;
using Hotel.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ADOProject
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddAdoServices(this IServiceCollection services)
        {
            services.AddTransient<AdoGuestService>();
            services.AddTransient<AdoReservationService>();
            services.AddTransient<AdoPaymentService>();
            services.AddTransient<AdoRoomService>();
            services.AddTransient<AdoRoomStatusService>();
            services.AddTransient<AdoRoomTypeService>();
            return services;
        }
    }
}
