using Hotel.BL.Interfaces;
using Hotel.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.BL
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomStatusService, RoomStatusService>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();
            return services;
        }
    }
}
