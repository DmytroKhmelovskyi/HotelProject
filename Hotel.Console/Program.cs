using ADOProject;
using ADOProject.Services;
using EntityFrameworkProgect;
using EntityFrameworkProgect.Services;
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
            var services = new ServiceCollection();
            services.AddEntityFrameworkServices();
            services.AddAdoServices();
            services.AddScoped<AdoServiceFactory>();
            services.AddScoped<EntityFrameworkServiceFactory>();

            var provider = services.BuildServiceProvider();
            var menu = new ServiceProviderMenu();
            menu.Show();
            var response = menu.ReadResponse();

            services.AddScoped<IServiceFactory>(s =>
            {
                switch (response)
                {
                    case "1": return provider.GetRequiredService<AdoServiceFactory>();
                    case "2": return provider.GetRequiredService<EntityFrameworkServiceFactory>();
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
