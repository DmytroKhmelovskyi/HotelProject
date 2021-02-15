using Hotel.BL.Interfaces;
using Hotel.BL.Models;
using Hotel.BL.Services;
using Hotel.Shared.FilterModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Hotel.Web.Controllers
{
    public class RoomStatusController : Controller
    {
        private IRoomStatusService roomStatusService;
        public RoomStatusController(IRoomStatusService roomStatusService)
        {
            this.roomStatusService = roomStatusService;
        }
        public IActionResult Index(int? pageNumber, RoomStatusFilter roomStatusFilter)
        {
            try
            {
                roomStatusFilter.Take = 5;
                pageNumber ??= 1;
                roomStatusFilter.Skip = (pageNumber.Value - 1) * roomStatusFilter.Take;
                var (roomStatuses, count) = roomStatusService.ReadRoomStatuses(roomStatusFilter);
                return View(PaginatedList<RoomStatusViewModel>.Create(roomStatuses, count, pageNumber.Value, roomStatusFilter.Take));
            }
            catch (Exception)
            {
                return BadRequest();
            }
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
            try
            {
                var roomStatus = roomStatusService.ReadSingle(id);
                return View(roomStatus);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var roomStatus = roomStatusService.ReadSingle(id);
                return View(roomStatus);
            }
            catch (Exception)
            {
                return BadRequest();
            }
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
            try
            {
                var roomStatus = roomStatusService.ReadSingle(id);
                return View(roomStatus);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            roomStatusService.DeleteRoomStatus(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
