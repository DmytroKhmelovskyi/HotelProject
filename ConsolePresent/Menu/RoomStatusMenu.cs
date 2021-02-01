using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using EntityFrameworkProgect.Presenters;
using EntityFrameworkProgect.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkProgect.Menu
{
    public class RoomStatusMenu
    {
        IRoomStatusService roomStatusService;
        ConsoleRoomStatusPresenter presenter;
        public void StatusMenu()
        {
            Console.WriteLine("1. Create new RoomStatus ");
            Console.WriteLine("2. Read RoomStatuses ");
            Console.WriteLine("3. Update RoomStatus ");
            Console.WriteLine("4. Delete RoomStatus ");
            string c = Console.ReadLine();
            Console.WriteLine("------------------------------------------------------------------------" +
              "-----------------------------------------------------------------------------------");

            switch (c)
            {
                case "1":
                    AddRoomStatus();
                    break;
                case "2":
                    ReadRoomStatus();
                    break;
                case "3":
                    UpdateteRoomStatus();
                    break;
                case "4":
                    DeleteRoomStatus();
                    break;
                default:
                    throw new ArgumentException("unhendled case");

            }
            Console.WriteLine("------------------------------------------------------------------------" +
             "-----------------------------------------------------------------------------------");
            //MainMenu.Menu();
        }
        public void AddRoomStatus()
        {
            var roomStatus = new RoomStatus();
            Console.WriteLine("Print Status: ");
            roomStatus.Status = Console.ReadLine();
            roomStatusService.AddRoomStatus(roomStatus);
            presenter.Presenter(roomStatusService.ReadRoomStatuses());
        }
        public void ReadRoomStatus()
        {
            presenter.Presenter(roomStatusService.ReadRoomStatuses());
        }
        public void UpdateteRoomStatus()
        {
            var roomStatus = new RoomStatus();
            Console.WriteLine("Print Id: ");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Print Status: ");

            string cs = Console.ReadLine();
            switch (cs)
            {
                case "1":
                    Console.WriteLine("Print Status: ");
                    roomStatus.Status = Console.ReadLine();
                    break;
                default:
                    throw new Exception("wrong case");

            }
            roomStatusService.UpdateRoomStatus(id, roomStatus);
            Console.WriteLine("Object successful updated");

        }
        public void DeleteRoomStatus()
        {
            Console.WriteLine("Print Id: ");
            int id = Int32.Parse(Console.ReadLine());
            roomStatusService.DeleteRoomStatus(id);
            Console.WriteLine("Object successful deleted");
        }
    }
}
