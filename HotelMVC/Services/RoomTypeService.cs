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
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository roomTypeRepository;
        private readonly IMapper mapper;
        public RoomTypeService(IRoomTypeRepository roomTypeRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.roomTypeRepository = roomTypeRepository;
        }
        public RoomTypeViewModel AddRoomType(RoomTypeViewModel model)
        {
            var roomTypeModel = mapper.Map<RoomType>(model);
            var roomType = roomTypeRepository.AddRoomType(roomTypeModel);
            return mapper.Map<RoomTypeViewModel>(roomType);
        }

        public RoomTypeViewModel DeleteRoomType(int id)
        {
            return mapper.Map<RoomTypeViewModel>(roomTypeRepository.DeleteRoomType(id));
        }

        public IEnumerable<RoomTypeViewModel> ReadRoomTypes()
        {
            return mapper.Map<IEnumerable<RoomTypeViewModel>>(roomTypeRepository.ReadRoomTypes());
        }

        public (IEnumerable<RoomTypeViewModel> roomTypes, int count) ReadRoomTypes(RoomTypeFilter filter)
        {
            var (roomTypes, count) = roomTypeRepository.ReadRoomTypes(filter);
            var roomsModel = mapper.Map<IEnumerable<RoomTypeViewModel>>(roomTypes);
            return (roomsModel, count);
        }

        public RoomTypeViewModel ReadSingle(int id)
        {
            return mapper.Map<RoomTypeViewModel>(roomTypeRepository.ReadSingle(id));
        }

        public RoomTypeViewModel UpdateRoomType(int id, RoomTypeViewModel model)
        {
            var roomTypeModel = mapper.Map<RoomType>(model);
            var roomType = roomTypeRepository.UpdateRoomType(id, roomTypeModel);
            return mapper.Map<RoomTypeViewModel>(roomType);
        }
    }
}
