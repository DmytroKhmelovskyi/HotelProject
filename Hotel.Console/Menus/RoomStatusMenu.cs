using Hotel.ConsoleApp.Presenters;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.ConsoleApp.Menus
{
    public class RoomStatusMenu
    {
        IRoomStatusService roomStatusService;
        public RoomStatusMenu(IServiceFactory service)
        {
            roomStatusService = service.GetRoomStatusService();
        }
        public void Show()
        {
            while (true)
            {
                Console.WriteLine("1. Add roomStatus ");
                Console.WriteLine("2. Read roomStatuses ");
                Console.WriteLine("3. Update roomStatus ");
                Console.WriteLine("4. Delete roomStatus ");
                Console.WriteLine("x. Main menu ");
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
                    case "x":
                        return;
                    default:
                        throw new ArgumentException("unhendled case");

                }
                Console.WriteLine("------------------------------------------------------------------------" +
                 "-----------------------------------------------------------------------------------");
            }
        }
        public void AddRoomStatus()
        {
            try
            {

                var roomStatus = new RoomStatus();
                Console.WriteLine("Print Status: ");
                roomStatus.Status = Console.ReadLine();
                if (!Validation.IsNullOrEmpty(roomStatus.Status) || !Validation.ValidateString(roomStatus.Status))
                    AddRoomStatus();
                roomStatusService.AddRoomStatus(roomStatus);
                Console.WriteLine("Object successful added");
                ConsoleRoomStatusPresenter.Present(roomStatusService.ReadRoomStatuses());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AddRoomStatus();
            }
        }
        public void ReadRoomStatus()
        {
            ConsoleRoomStatusPresenter.Present(roomStatusService.ReadRoomStatuses());
        }
        public void UpdateteRoomStatus()
        {
            try
            {
                var roomStatus = new RoomStatus();
                Console.WriteLine("Print Id: ");
                int id = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Print Status: ");
                roomStatus.Status = Console.ReadLine();
                if (!Validation.IsNullOrEmpty(roomStatus.Status) || !Validation.ValidateString(roomStatus.Status))
                    UpdateteRoomStatus();
                roomStatusService.UpdateRoomStatus(id, roomStatus);
                Console.WriteLine("Object successful updated");
                ConsoleRoomStatusPresenter.Present(roomStatusService.ReadRoomStatuses());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                UpdateteRoomStatus();
            }

        }
        public void DeleteRoomStatus()
        {
            try
            {
                Console.WriteLine("Print Id: ");
                int id = Int32.Parse(Console.ReadLine());
                roomStatusService.DeleteRoomStatus(id);
                Console.WriteLine("Object successful deleted");
                ConsoleRoomStatusPresenter.Present(roomStatusService.ReadRoomStatuses());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DeleteRoomStatus();
            }
        }
    }
}
