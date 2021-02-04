using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelMVC.Controllers
{
    public class RoomTypeController : Controller
    {
        private IRoomTypeService roomTypeService;
        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            this.roomTypeService = roomTypeService;
        }
        public IActionResult Index(int? pageNumber)
        {
            var types = roomTypeService.ReadRoomTypes();
            int pageSize = 5;
            return View(PaginatedList<RoomType>.Create(types, pageNumber ?? 1, pageSize));
        }
    }
}
