using System;

namespace Hotel.ConsoleApp.Menus
{
    public class ServiceProviderMenu
    {
        public void Show()
        {
            Console.WriteLine("1. ADO Services ");
            Console.WriteLine("2. EntityFramework Services ");
        }

        public string ReadResponse()
        {
            var resp = Console.ReadLine();
            return resp;
        }

    }
}