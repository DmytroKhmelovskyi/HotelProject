using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using EntityFrameworkProgect.Presenters;
using EntityFrameworkProgect.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkProgect.Menu
{
    public class RoomMenu
    {
        IRoomService roomService;
        ConsoleRoomPresenter presenter;
        public void Menu()
        {
            Console.WriteLine("1. Create new Room ");
            Console.WriteLine("2. Read Rooms ");
            Console.WriteLine("3. Update Room ");
            Console.WriteLine("4. Delete Room ");
            string c = Console.ReadLine();
            Console.WriteLine("------------------------------------------------------------------------" +
              "-----------------------------------------------------------------------------------");
            switch (c)
            {
                case "1":
                    AddRoom();
                    break;
                case "2":
                    ReadRoom();
                    break;
                case "3":
                    UpdateteRoom();
                    break;
                case "4":
                    DeleteRoom();
                    break;
                default:
                    throw new ArgumentException("unhendled case");

            }
            Console.WriteLine("------------------------------------------------------------------------" +
             "-----------------------------------------------------------------------------------");
           // MainMenu.Menu();
        }
        public void AddRoom()
        {
            Room room = new Room();
            Console.WriteLine("Print RoomTypeId: ");
            room.RoomTypeId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Print RoomStatusId: ");
            room.RoomStatusId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Print RoomNumber: ");
            room.RoomNumber = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Print MaxPerson: ");
            room.MaxPerson = Int32.Parse(Console.ReadLine());
            roomService.AddRoom(room);
            presenter.Presenter(roomService.ReadRooms());
        }
        public void ReadRoom()
        {
            presenter.Presenter(roomService.ReadRooms());
        }
            public  void UpdateteRoom()
        {
            Room room = new Room();
            Console.WriteLine("Print Id: ");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("1.Change RoomTypeId");
            Console.WriteLine("2.Change RoomStatusId");
            Console.WriteLine("3.Change RoomNumber");
            Console.WriteLine("4.Change MaxPerson");
            Console.WriteLine("5.Change All");

            string cs = Console.ReadLine();
            switch (cs)
            {
                case "1":
                    Console.WriteLine("Print RoomTypeId: ");
                    room.RoomTypeId = Int32.Parse(Console.ReadLine());
                    break;
                case "2":
                    Console.WriteLine("Print RoomStatusId: ");
                    room.RoomStatusId = Int32.Parse(Console.ReadLine());
                    break;
                case "3":
                    Console.WriteLine("Print RoomNumber: ");
                    room.RoomNumber = Int32.Parse(Console.ReadLine());
                    break;
                case "4":
                    Console.WriteLine("Print MaxPerson: ");
                    room.MaxPerson = Int32.Parse(Console.ReadLine());
                    break;
                case "5":
                    Console.WriteLine("Print RoomTypeId: ");
                    room.RoomTypeId = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Print RoomStatusId: ");
                    room.RoomStatusId = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Print RoomNumber: ");
                    room.RoomNumber = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Print MaxPerson: ");
                    room.MaxPerson = Int32.Parse(Console.ReadLine());
                    break;
                default:
                    throw new Exception("wrong case");

            }

            roomService.UpdateRoom(id, room);
            Console.WriteLine("Object successful updated");

        }
        public void DeleteRoom()
        {
            Console.WriteLine("Print Id: ");
            int id = Int32.Parse(Console.ReadLine());
            roomService.DeleteRoom(id);
            Console.WriteLine("Object successful deleted");
        }
    }
}
