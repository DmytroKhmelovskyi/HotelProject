using Hotel.Shared.Interfaces;
using EntityFrameworkProgect.Presenters;
using EntityFrameworkProgect.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkProgect.Menu
{
  public  class RoomTypeMenu
    {
        IRoomTypeService roomTypeService;
        ConsoleRoomTypePresenter presenter;

        public static void TypeMenu()
        {
            Console.WriteLine("1. Create new RoomType ");
            Console.WriteLine("2. Read RoomTypes ");
            Console.WriteLine("3. Update RoomType ");
            Console.WriteLine("4. Delete RoomType ");
            string c = Console.ReadLine();
            Console.WriteLine("------------------------------------------------------------------------" +
              "-----------------------------------------------------------------------------------");

            switch (c)
            {
                case "1":
                    EfRoomTypeService.InsertRoomType();
                    break;
                case "2":
                    EfRoomTypeService.ReadRoomTypes();
                    break;
                case "3":
                    EfRoomTypeService.UpdateRoomType();
                    break;
                case "4":
                    EfRoomTypeService.DeleteRoomType();
                    break;
                default:
                    throw new ArgumentException("unhendled case");

            }
            Console.WriteLine("------------------------------------------------------------------------" +
            "-----------------------------------------------------------------------------------");
            MainMenu.Menu();
        }
        public  void AddRoomType()
        {

            Console.WriteLine("Print Status: ");
            type.Type = Console.ReadLine();

           

        }
        public  void UpdateRoomType()
        {
            Console.WriteLine("Print Id: ");
            int id = Int32.Parse(Console.ReadLine());
            RoomType type = db.RoomTypes.Find(id);
            Console.WriteLine("Print Type: ");

            string cs = Console.ReadLine();
            switch (cs)
            {
                case "1":
                    Console.WriteLine("Print Type: ");
                    type.Type = Console.ReadLine();
                    break;
                default:
                    throw new Exception("wrong case");

            }
        }
    }
}
