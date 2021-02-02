using Hotel.ConsoleApp.Presenters;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.ConsoleApp.Menus
{
    public class RoomTypeMenu
    {
        IRoomTypeService roomTypeService;
        public RoomTypeMenu(IServiceFactory service)
        {
            roomTypeService = service.GetRoomTypeService();
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("1. Create new RoomType ");
                Console.WriteLine("2. Read RoomTypes ");
                Console.WriteLine("3. Update RoomType ");
                Console.WriteLine("4. Delete RoomType ");
                Console.WriteLine("x. Main menu ");
                string c = Console.ReadLine();
                Console.WriteLine("------------------------------------------------------------------------" +
                  "-----------------------------------------------------------------------------------");

                switch (c)
                {
                    case "1":
                        AddRoomType();
                        break;
                    case "2":
                        ReadRoomType();
                        break;
                    case "3":
                        UpdateRoomType();
                        break;
                    case "4":
                        DeleteRoomType();
                        break;
                    default:
                        throw new ArgumentException("unhendled case");

                }
                Console.WriteLine("------------------------------------------------------------------------" +
                "-----------------------------------------------------------------------------------");
            }
        }
        public void AddRoomType()
        {
            try
            {
                var roomType = new RoomType();
                Console.WriteLine("Print Status: ");
                roomType.Type = Console.ReadLine();
                if (!Validation.IsNullOrEmpty(roomType.Type) || !Validation.ValidateString(roomType.Type))
                    AddRoomType();
                roomTypeService.AddRoomType(roomType);
                Console.WriteLine("Object successful added");
                ConsoleRoomTypePresenter.Present(roomTypeService.ReadRoomTypes());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AddRoomType();
            }
        }
        public void ReadRoomType()
        {
            ConsoleRoomTypePresenter.Present(roomTypeService.ReadRoomTypes());
        }

        public void UpdateRoomType()
        {
            try
            {
                var roomType = new RoomType();
                Console.WriteLine("Print Id: ");
                int id = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Print Type: ");
                roomType.Type = Console.ReadLine();
                if (!Validation.IsNullOrEmpty(roomType.Type) || !Validation.ValidateString(roomType.Type))
                    UpdateRoomType();
                roomTypeService.UpdateRoomType(id, roomType);
                Console.WriteLine("Object successful updated");
                ConsoleRoomTypePresenter.Present(roomTypeService.ReadRoomTypes());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                UpdateRoomType();
            }

        }
        public void DeleteRoomType()
        {
            try
            {
                Console.WriteLine("Print Id: ");
                int id = Int32.Parse(Console.ReadLine());
                roomTypeService.DeleteRoomType(id);
                Console.WriteLine("Object successful updated");
                ConsoleRoomTypePresenter.Present(roomTypeService.ReadRoomTypes());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DeleteRoomType();
            }
        }
    }
}
