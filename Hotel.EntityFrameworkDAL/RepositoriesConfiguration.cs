using Hotel.Shared.Models;
using Hotel.EntityFrameworkDAL.Repositories;
using Hotel.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Hotel.EntityFrameworkDAL
{
    public static class RepositoriesConfiguration
    {
        public static IServiceCollection AddEntityFrameworkRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<HotelDatabaseContext>(c=>c.UseSqlServer(connectionString));
            services.AddTransient<EfGuestRepository>();
            services.AddTransient<IGuestRepository, EfGuestRepository>();
            services.AddTransient<EfReservationRepository>();
            services.AddTransient<IReservationRepository, EfReservationRepository>();
            services.AddTransient<EfPaymentRepository>();
            services.AddTransient<IPaymentRepository, EfPaymentRepository>();
            services.AddTransient<EfRoomRepository>();
            services.AddTransient<IRoomRepository, EfRoomRepository>();
            services.AddTransient<EfRoomStatusRepository >();
            services.AddTransient<IRoomStatusRepository, EfRoomStatusRepository >();
            services.AddTransient<EfRoomTypeRepository>();
            services.AddTransient<IRoomTypeRepository, EfRoomTypeRepository>();
            return services;
        }
    }
}
