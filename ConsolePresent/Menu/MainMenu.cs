using ADOProject.Services;
using Hotel.Shared.Interfaces;
using EntityFrameworkProgect.Services;
using Hotel.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkProgect.Menu
{
    public class MainMenu
    {
        private readonly IServiceCollection services;
        private IServiceProvider provider;
        public MainMenu(IServiceCollection services)
        {
            this.services = services;
        }
        public void Show()
        {
            Console.WriteLine("1. ADO Operstions ");
            Console.WriteLine("2. EntityFramework Operstions ");
            string c = Console.ReadLine();
            Console.WriteLine("------------------------------------------------------------------------" +
              "-----------------------------------------------------------------------------------");
            switch (c)
            {
                case "1":
                    services.AddSingleton<IServiceFactory, AdoServiceFactory>();
                    break;
                case "2":
                    services.AddSingleton<IServiceFactory, EntityFrameworkServiceFactory>();
                    break;
                default:
                    throw new ArgumentException("unhendled case");
            }
            provider = services.BuildServiceProvider();
            Menu();
        }
        public  void Menu()
        {
            
            Console.WriteLine("1. Guests Operstions ");
            Console.WriteLine("2. Reservations Operstions ");
            Console.WriteLine("3. Rooms Operstions ");
            Console.WriteLine("4. Payments Operations ");
            Console.WriteLine("5. RoomStatuses Operations ");
            Console.WriteLine("6. RoomTypes Operations ");
            Console.WriteLine("7. Stored Procedures ");
            string c = Console.ReadLine();
            Console.WriteLine("------------------------------------------------------------------------" +
              "-----------------------------------------------------------------------------------");
            switch (c)
            {
                case "1":
                    var g = new GuestMenu();
                    g.GuestsMenu();
                    break;
                case "2":
                    var rs = new ReservationMenu();
                    rs.ResesvationsMenu();
                    break;
                case "3":
                    RoomMenu.Menu();
                    break;
                case "4":

                    break;
                case "5":
                   
                    break;
                case "6":
                   
                    break;
                case "7":
                   //StoredProcedureMenu.StoredProcedures();
                    break;
                default:
                    throw new ArgumentException("unhendled case");
            }

        }

    }
}
