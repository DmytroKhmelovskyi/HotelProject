using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using Hotel.Web.Interfaces;
using Hotel.Web.VIewModel;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Controllers
{
    public class RoomController : Controller
    {
        private IRoomService roomService;
        public RoomController(IRoomService roomService)
        {
            this.roomService = roomService;
        }
        public IActionResult Index(int? pageNumber, RoomFilter roomFilter)
        {
            ViewData["CurrentSort"] = roomFilter.SortOrder;
            ViewData["RoomNumberSortParm"] = String.IsNullOrEmpty(roomFilter.SortOrder) ? "roomNumber" : "";
            ViewData["MaxPersonSortParm"] = roomFilter.SortOrder == "MaxPerson" ? "maxPerson" : "MaxPerson";
            roomFilter.Take = 5;
            pageNumber ??= 1;
            roomFilter.Skip = (pageNumber.Value - 1) * roomFilter.Take;
            var (rooms, count) = roomService.ReadRooms(roomFilter);
            return View(PaginatedList<RoomViewModel>.Create(rooms, count, pageNumber.Value, roomFilter.Take));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoomViewModel model)
        {
            if (ModelState.IsValid)
            {
                roomService.AddRoom(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {

            var room = roomService.ReadSingle(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            var room = roomService.ReadSingle(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost]
        public IActionResult Edit(int id, RoomViewModel model)
        {
            if (ModelState.IsValid)
            {
                roomService.UpdateRoom(id, model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var room = roomService.ReadSingle(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            roomService.DeleteRoom(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
