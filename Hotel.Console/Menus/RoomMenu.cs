﻿using Hotel.ConsoleApp.Presenters;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.ConsoleApp.Menus
{
    public class RoomMenu
    {
        IRoomService roomService;
        public RoomMenu(IServiceFactory service)
        {
            roomService = service.GetRoomService();
        }
        public void Show()
        {
            while (true)
            {
                Console.WriteLine("1. Create new Room ");
            Console.WriteLine("2. Read Rooms ");
            Console.WriteLine("3. Update Room ");
            Console.WriteLine("4. Delete Room ");
            Console.WriteLine("x. Main menu ");
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
            }
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
            Console.WriteLine("Object successful added");
            ConsoleRoomPresenter.Present(roomService.ReadRooms());
        }
        public void ReadRoom()
        {
            ConsoleRoomPresenter.Present(roomService.ReadRooms());
        }
        public void UpdateteRoom()
        {
            Room room = new Room();
            Console.WriteLine("Print Id: ");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Print RoomTypeId: ");
            room.RoomTypeId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Print RoomStatusId: ");
            room.RoomStatusId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Print RoomNumber: ");
            room.RoomNumber = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Print MaxPerson: ");
            room.MaxPerson = Int32.Parse(Console.ReadLine());

            roomService.UpdateRoom(id, room);
            Console.WriteLine("Object successful updated");
            ConsoleRoomPresenter.Present(roomService.ReadRooms());

        }
        public void DeleteRoom()
        {
            Console.WriteLine("Print Id: ");
            int id = Int32.Parse(Console.ReadLine());
            roomService.DeleteRoom(id);
            Console.WriteLine("Object successful deleted");
            ConsoleRoomPresenter.Present(roomService.ReadRooms());

        }
    }
}
