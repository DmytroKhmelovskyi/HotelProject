using AutoMapper;
using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using Hotel.Web.Interfaces;
using Hotel.Web.VIewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository roomRepository;
        private readonly IMapper mapper;
        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.roomRepository = roomRepository;
        }
        public RoomViewModel AddRoom(RoomViewModel model)
        {
            var roomModel = mapper.Map<Room>(model);
            var room = roomRepository.AddRoom(roomModel);
            return mapper.Map<RoomViewModel>(room);
        }

        public RoomViewModel DeleteRoom(int id)
        {
            return mapper.Map<RoomViewModel>(roomRepository.DeleteRoom(id));
        }

        public IEnumerable<RoomViewModel> ReadRooms()
        {
            return mapper.Map<IEnumerable<RoomViewModel>>(roomRepository.ReadRooms());
        }

        public (IEnumerable<RoomViewModel> model, int count) ReadRooms(RoomFilter filter)
        {
            var (rooms, count) = roomRepository.ReadRooms(filter);
            var roomsModel = mapper.Map<IEnumerable<RoomViewModel>>(rooms);
            return (roomsModel, count);
        }

        public RoomViewModel ReadSingle(int id)
        {
            return mapper.Map<RoomViewModel>(roomRepository.ReadSingle(id));
        }

        public RoomViewModel UpdateRoom(int id, RoomViewModel model)
        {

            var roomModel = mapper.Map<Room>(model);
            var room = roomRepository.UpdateRoom(id, roomModel);
            return mapper.Map<RoomViewModel>(room);
        }
    }
}
