using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Web.Interfaces;
using Hotel.Web.VIewModel;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Controllers
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
            int pageSize = 5;
            pageNumber ??= 1;
            var filter = new RoomTypeFilter { Take = pageSize, Skip = (pageNumber.Value - 1) * pageSize };
            var (roomTypes, count) = roomTypeService.ReadRoomTypes(filter);
            return View(PaginatedList<RoomTypeViewModel>.Create(roomTypes, count, pageNumber.Value, pageSize));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoomTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                roomTypeService.AddRoomType(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {

            var roomType = roomTypeService.ReadSingle(id);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            var roomType = roomTypeService.ReadSingle(id);
            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }

        [HttpPost]
        public IActionResult Edit(int id, RoomTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                roomTypeService.UpdateRoomType(id, model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var roomType = roomTypeService.ReadSingle(id);
            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            roomTypeService.DeleteRoomType(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
