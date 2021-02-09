using Hotel.ConsoleApp.Presenters;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;

namespace Hotel.ConsoleApp.Menus
{
    public class RoomMenu
    {
        IRoomRepository roomService;
        public RoomMenu(IRepositoryFactory service)
        {
            roomService = service.GetRoomRepository();
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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AddRoom();
            }
        }
        public void ReadRoom()
        {
            ConsoleRoomPresenter.Present(roomService.ReadRooms());
        }
        public void UpdateteRoom()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                UpdateteRoom();
            }

        }
        public void DeleteRoom()
        {
            try
            {
                Console.WriteLine("Print Id: ");
                int id = Int32.Parse(Console.ReadLine());
                roomService.DeleteRoom(id);
                Console.WriteLine("Object successful deleted");
                ConsoleRoomPresenter.Present(roomService.ReadRooms());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DeleteRoom();
            }

        }
    }
}
