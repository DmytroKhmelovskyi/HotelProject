using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelMVC.Controllers
{
    public class RoomController : Controller
    {
        private IRoomService roomService;
        public RoomController(IRoomService roomService)
        {
            this.roomService = roomService;
        }
        public IActionResult Index(int? pageNumber)
        {
            var rooms = roomService.ReadRooms();
            int pageSize = 5;
            return View(PaginatedList<Room>.Create(rooms, pageNumber ?? 1, pageSize));
        }
    }
}
