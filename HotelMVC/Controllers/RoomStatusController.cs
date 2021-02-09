using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using Hotel.Web.Interfaces;
using Hotel.Web.VIewModel;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Controllers
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
            int pageSize = 5;
            pageNumber ??= 1;
            var filter = new RoomStatusFilter { Take = pageSize, Skip = (pageNumber.Value - 1) * pageSize};
            var (roomStatuses, count) = roomStatusService.ReadRoomStatuses(filter);
            return View(PaginatedList<RoomStatusViewModel>.Create(roomStatuses, count, pageNumber.Value, pageSize));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoomStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                roomStatusService.AddRoomStatus(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {

            var roomStatus = roomStatusService.ReadSingle(id);
            if (roomStatus == null)
            {
                return NotFound();
            }

            return View(roomStatus);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            var roomStatus = roomStatusService.ReadSingle(id);
            if (roomStatus == null)
            {
                return NotFound();
            }
            return View(roomStatus);
        }

        [HttpPost]
        public IActionResult Edit(int id, RoomStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                roomStatusService.UpdateRoomStatus(id, model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var roomStatus = roomStatusService.ReadSingle(id);
            if (roomStatus == null)
            {
                return NotFound();
            }
            return View(roomStatus);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            roomStatusService.DeleteRoomStatus(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
