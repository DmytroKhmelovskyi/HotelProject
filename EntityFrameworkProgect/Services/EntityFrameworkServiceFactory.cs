using Hotel.Shared.Interfaces;

namespace EntityFrameworkProgect.Services
{
    public class EntityFrameworkServiceFactory : IServiceFactory
    {
        private readonly EfGuestService guestService;
        private readonly EfPaymentService paymentService;
        private readonly EfReservationService reservationService;
        private readonly EfRoomService roomService;
        private readonly EfRoomStatusService roomStatusService;
        private readonly EfRoomTypeService roomTypeService;
        public EntityFrameworkServiceFactory(EfGuestService guestService, EfPaymentService paymentService, EfReservationService reservationService,
            EfRoomService roomService, EfRoomStatusService roomStatusService, EfRoomTypeService roomTypeService)
        {
            this.guestService = guestService;
            this.paymentService = paymentService;
            this.reservationService = reservationService;
            this.roomService = roomService;
        }
        public IGuestService GetGuestService()
        {
            return guestService;
        }

        public IPaymentService GetPaymentService()
        {
            return paymentService;
        }
        public IReservationService GetReservationService()
        {
            return reservationService;
        }
        public IRoomService GetRoomService()
        {
            return roomService;
        }
        public IRoomStatusService GetRoomStatusService()
        {
            return roomStatusService;
        }
        public IRoomTypeService GetRoomTypeService()
        {
            return roomTypeService;
        }
    }
}
