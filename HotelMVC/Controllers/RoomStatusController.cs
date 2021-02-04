using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelMVC.Controllers
{
    public class RoomStatusController : Controller
    {
        private IRoomStatusService roomStatusService;
        public RoomStatusController(IRoomStatusService roomStatusService)
        {
            this.roomStatusService = roomStatusService;
        }
        public IActionResult Index(int? pageNumber)
        {
            var statuses = roomStatusService.ReadRoomStatuses();
            int pageSize = 5;

            return View(PaginatedList<RoomStatus>.Create(statuses, pageNumber ?? 1, pageSize));
        }
    }
}
