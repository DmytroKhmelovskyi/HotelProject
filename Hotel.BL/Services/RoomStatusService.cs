using AutoMapper;
using Hotel.BL.Interfaces;
using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System.Collections.Generic;
using Hotel.BL.Models;
namespace Hotel.BL.Services
{
    public class RoomStatusService : IRoomStatusService
    {
        private readonly IRoomStatusRepository roomStatusRepository;
        private readonly IMapper mapper;
        public RoomStatusService(IRoomStatusRepository roomStatusRepository, IMapper mapper)
        {
            this.roomStatusRepository = roomStatusRepository;
            this.mapper = mapper;
        }
        public RoomStatusViewModel AddRoomStatus(RoomStatusViewModel model)
        {
            var roomStatuModel = mapper.Map<RoomStatus>(model);
            var roomStatus = roomStatusRepository.AddRoomStatus(roomStatuModel);
            return mapper.Map<RoomStatusViewModel>(roomStatus);
        }

        public RoomStatusViewModel DeleteRoomStatus(int id)
        {
            return mapper.Map<RoomStatusViewModel>(roomStatusRepository.DeleteRoomStatus(id));
        }

        public IEnumerable<RoomStatusViewModel> ReadRoomStatuses()
        {
            return mapper.Map<IEnumerable<RoomStatusViewModel>>(roomStatusRepository.ReadRoomStatuses());
        }

        public (IEnumerable<RoomStatusViewModel> roomStatuses, int count) ReadRoomStatuses(RoomStatusFilter filter)
        {
            var (roomStatuses, count) = roomStatusRepository.ReadRoomStatuses(filter);
            var roomsStatusModel = mapper.Map<IEnumerable<RoomStatusViewModel>>(roomStatuses);
            return (roomsStatusModel, count);
        }

        public RoomStatusViewModel ReadSingle(int id)
        {
            return mapper.Map<RoomStatusViewModel>(roomStatusRepository.ReadSingle(id));
        }

        public RoomStatusViewModel UpdateRoomStatus(int id, RoomStatusViewModel model)
        {
            var roomStatuModel = mapper.Map<RoomStatus>(model);
            var roomStatus = roomStatusRepository.UpdateRoomStatus(id, roomStatuModel);
            return mapper.Map<RoomStatusViewModel>(roomStatus);
        }
    }
}
