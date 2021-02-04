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
            services.AddTransient<IGuestService, EfGuestService>();
            services.AddTransient<EfReservationService>();
            services.AddTransient<IReservationService, EfReservationService>();
            services.AddTransient<EfPaymentService>();
            services.AddTransient<IPaymentService, EfPaymentService>();
            services.AddTransient<EfRoomService>();
            services.AddTransient<IRoomService, EfRoomService>();
            services.AddTransient<EfRoomStatusService>();
            services.AddTransient<IRoomStatusService, EfRoomStatusService>();
            services.AddTransient<EfRoomTypeService>();
            services.AddTransient<IRoomTypeService, EfRoomTypeService>();
            return services;
        }
    }
}
