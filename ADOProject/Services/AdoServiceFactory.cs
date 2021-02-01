using Hotel.Shared.Interfaces;

namespace ADOProject.Services
{
    public class AdoServiceFactory : IServiceFactory
    {
        private readonly AdoGuestService guestService;
        private readonly AdoReservationService reservationService;
        private readonly AdoPaymentService paymentService;
        private readonly AdoRoomService roomService;
        private readonly AdoRoomStatusService roomStatusService;
        private readonly AdoRoomTypeService roomTypeService;
        public AdoServiceFactory(AdoGuestService guestService, AdoReservationService reservationService, AdoPaymentService paymentService,
            AdoRoomService roomService, AdoRoomStatusService roomStatusService, AdoRoomTypeService roomTypeService)
        {
            this.guestService = guestService;
            this.reservationService = reservationService;
            this.paymentService = paymentService;
            this.roomService = roomService;
            this.roomStatusService = roomStatusService;
            this.roomTypeService = roomTypeService;
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
