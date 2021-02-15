using AutoMapper;
using Hotel.Shared.Models;
using Hotel.BL.Models;
namespace Hotel.AutoMapperLibrary

{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Guest, GuestViewModel>();
            CreateMap<GuestViewModel, Guest>();
            CreateMap<Payment, PaymentViewModel>()
                .ForMember(vm => vm.GuestName, vm => vm.MapFrom(p => $"{p.Guest.FirstName} {p.Guest.LastName}"));
            CreateMap<PaymentViewModel, Payment>();
            CreateMap<Reservation, ReservationViewModel>()
                .ForMember(vm => vm.GuestName, vm => vm.MapFrom(r => $"{r.Guest.FirstName} {r.Guest.LastName}"));
            CreateMap<ReservationViewModel, Reservation>();
            CreateMap<Room, RoomViewModel>()
                .ForMember(vm => vm.RoomStatusName, vm => vm.MapFrom(r => r.RoomStatus.Status))
                .ForMember(vm => vm.RoomTypeName, vm => vm.MapFrom(r => r.RoomType.Type));
            CreateMap<RoomViewModel, Room>();
            CreateMap<RoomStatus, RoomStatusViewModel>();
            CreateMap<RoomStatusViewModel, RoomStatus>();
            CreateMap<RoomType, RoomTypeViewModel>();
            CreateMap<RoomTypeViewModel, RoomType>();

        }
    }
}
