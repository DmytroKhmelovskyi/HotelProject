using EntityFrameworkProgect.Services;
using System;

namespace EntityFrameworkProgect.Menu
{
    public class StoredProcedureMenu
    {
        public static void StoredProcedures()
        {
            Console.WriteLine("1. FreeRooms ");
            Console.WriteLine("2. Payments for 2021");
            Console.WriteLine("3. Get Guest Reservation");
            Console.WriteLine("4. Get Room By Person Count");
            Console.WriteLine("5. Get Guest Payment With  Room");
            string c = Console.ReadLine();
            Console.WriteLine("------------------------------------------------------------------------" +
              "-----------------------------------------------------------------------------------");
            switch (c)
            {
                case "1":
                    StoredProcedureService.FreeRoom();
                    break;
                case "2":
                    StoredProcedureService.Payments();
                    break;
                case "3":
                    StoredProcedureService.GetGuestReservation();
                    break;
                case "4":
                    StoredProcedureService.GetRoomByPersonCount();
                    break;
                case "5":
                    StoredProcedureService.GetGuestPaymentWithRoom();
                    break;
                default:
                    throw new ArgumentException("unhendled case");

            }
            Console.WriteLine("------------------------------------------------------------------------" +
            "-----------------------------------------------------------------------------------");
            //MainMenu.Menu();
        }
    }
}
