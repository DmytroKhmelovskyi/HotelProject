using Hotel.AdoDAL.Repositories;
using Hotel.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.AdoDAL
{
    public static class RepositoriesConfiguration
    {
        public static IServiceCollection AddAdoRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton(_ => new DbConfig { ConnectionString = connectionString });
            services.AddTransient<AdoGuestRepository>();
            services.AddTransient<IGuestRepository, AdoGuestRepository>();
            services.AddTransient<AdoReservationRepository>();
            services.AddTransient<IReservationRepository, AdoReservationRepository>();
            services.AddTransient<AdoPaymentRepository>();
            services.AddTransient<IPaymentRepository, AdoPaymentRepository>();
            services.AddTransient<AdoRoomRepository>();
            services.AddTransient<IRoomRepository, AdoRoomRepository>();
            services.AddTransient<AdoRoomStatusRepository>();
            services.AddTransient<IRoomStatusRepository, AdoRoomStatusRepository>();
            services.AddTransient<AdoRoomTypeRepository>();
            services.AddTransient<IRoomTypeRepository, AdoRoomTypeRepository>();
            return services;
        }
    }
}
