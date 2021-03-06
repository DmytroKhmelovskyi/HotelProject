﻿using Hotel.BL.Interfaces;
using Hotel.Shared.FilterModels;
using Microsoft.AspNetCore.Mvc;
using Hotel.BL.Models;
using System;
using Hotel.BL.Services;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hotel.Web.Controllers
{
    public class RoomController : Controller
    {
        private IRoomService roomService;
        private IRoomStatusService roomStatusService;
        private IRoomTypeService roomTypeService;
        public RoomController(IRoomService roomService, IRoomStatusService roomStatusService, IRoomTypeService roomTypeService)
        {
            this.roomService = roomService;
            this.roomStatusService = roomStatusService;
            this.roomTypeService = roomTypeService;
        }
        public IActionResult Index(int? pageNumber, RoomFilter roomFilter)
        {
            try
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
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            var roomStatuses = roomStatusService.ReadRoomStatuses();
            var roomStatusItems = roomStatuses.Select(rs => new SelectListItem { Value = $"{rs.Id}", Text = $"{rs.Status}" });
            ViewBag.RoomStatuses = roomStatusItems;

            var roomTypes = roomTypeService.ReadRoomTypes();
            var roomTypeItems = roomTypes.Select(rt => new SelectListItem { Value = $"{rt.Id}", Text = $"{rt.Type}" });
            ViewBag.RoomTypes = roomTypeItems;

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
            try
            {
                var room = roomService.ReadSingle(id);
                return View(room);
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
                var roomStatuses = roomStatusService.ReadRoomStatuses();
                var roomStatusItems = roomStatuses.Select(rs => new SelectListItem { Value = $"{rs.Id}", Text = $"{rs.Status}" });
                ViewBag.RoomStatuses = roomStatusItems;

                var roomTypes = roomTypeService.ReadRoomTypes();
                var roomTypeItems = roomTypes.Select(rt => new SelectListItem { Value = $"{rt.Id}", Text = $"{rt.Type}" });
                ViewBag.RoomTypes = roomTypeItems;

                var room = roomService.ReadSingle(id);
                return View(room);
            }
            catch (Exception)
            {
                return BadRequest();
            }
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
            try
            {
                var room = roomService.ReadSingle(id);
                return View(room);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            roomService.DeleteRoom(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
