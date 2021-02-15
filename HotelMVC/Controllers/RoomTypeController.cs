using Hotel.BL.Interfaces;
using Hotel.Shared.FilterModels;
using Microsoft.AspNetCore.Mvc;
using Hotel.BL.Models;
using Hotel.BL.Services;
using System;

namespace Hotel.Web.Controllers
{
    public class RoomTypeController : Controller
    {
        private IRoomTypeService roomTypeService;
        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            this.roomTypeService = roomTypeService;
        }
        public IActionResult Index(int? pageNumber, RoomTypeFilter roomTypeFilter)
        {
            try
            {
                roomTypeFilter.Take = 5;
                pageNumber ??= 1;
                roomTypeFilter.Skip = (pageNumber.Value - 1) * roomTypeFilter.Take;
                var (roomTypes, count) = roomTypeService.ReadRoomTypes(roomTypeFilter);
                return View(PaginatedList<RoomTypeViewModel>.Create(roomTypes, count, pageNumber.Value, roomTypeFilter.Take));
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
            try
            {
                var roomType = roomTypeService.ReadSingle(id);
                return View(roomType);
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
                var roomType = roomTypeService.ReadSingle(id);
                return View(roomType);
            }
            catch (Exception)
            {
                return BadRequest();
            }
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
            try
            {
                var roomType = roomTypeService.ReadSingle(id);
                return View(roomType);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            roomTypeService.DeleteRoomType(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
