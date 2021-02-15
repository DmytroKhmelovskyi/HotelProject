using Hotel.AdoDAL;
using Hotel.AdoDAL.Repositories;
using Hotel.EntityFrameworkDAL;
using Hotel.EntityFrameworkDAL.Repositories;
using Hotel.ConsoleApp.Menus;
using Hotel.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hotel.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=HotelDatabase;Trusted_Connection=True;";
            var services = new ServiceCollection();
            services.AddEntityFrameworkRepositories(connectionString);
            services.AddAdoRepositories(connectionString);
            services.AddScoped<AdoRepositoryFactory>();
            services.AddScoped<EntityFrameworkRepositoryFactory>();

            var provider = services.BuildServiceProvider();
            var menu = new ServiceProviderMenu();
            menu.Show();
            var response = menu.ReadResponse();

            services.AddScoped<IRepositoryFactory>(s =>
            {
                switch (response)
                {
                    case "1": return provider.GetRequiredService<AdoRepositoryFactory>();
                    case "2": return provider.GetRequiredService<EntityFrameworkRepositoryFactory>();
                    default: throw new Exception("wrong key");
                }
            });

            services.AddScoped<MainMenu>();
            services.AddScoped<GuestMenu>();
            services.AddScoped<PaymentMenu>();
            services.AddScoped<ReservationMenu>();
            services.AddScoped<RoomMenu>();   
            services.AddScoped<RoomStatusMenu>();
            services.AddScoped<RoomTypeMenu>();

            provider = services.BuildServiceProvider();

            var mainMenu = provider.GetRequiredService<MainMenu>();
            mainMenu.Show();
        }
    }
}
