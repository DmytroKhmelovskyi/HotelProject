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
            services.AddTransient<IGuestService, AdoGuestService>();
            services.AddTransient<AdoReservationService>();
            services.AddTransient<IReservationService, AdoReservationService>();
            services.AddTransient<AdoPaymentService>();
            services.AddTransient<IPaymentService, AdoPaymentService>();
            services.AddTransient<AdoRoomService>();
            services.AddTransient<IRoomService, AdoRoomService>();
            services.AddTransient<AdoRoomStatusService>();
            services.AddTransient<IRoomStatusService, AdoRoomStatusService>();
            services.AddTransient<AdoRoomTypeService>();
            services.AddTransient<IRoomTypeService, AdoRoomTypeService>();
            return services;
        }
    }
}
