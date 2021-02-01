using Hotel.Shared.Models;
using EntityFrameworkProgect.Services;
using Hotel.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFrameworkProgect
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddEntityFrameworkServices(this IServiceCollection services)
        {
            services.AddDbContext<HotelDatabaseContext>();
            services.AddTransient<EfGuestService>();
            services.AddTransient<EfReservationService>();
            services.AddTransient<EfPaymentService>();
            services.AddTransient<EfRoomService>();
            services.AddTransient<EfRoomStatusService>();
            services.AddTransient<EfRoomTypeService>();
            return services;
        }
    }
}
