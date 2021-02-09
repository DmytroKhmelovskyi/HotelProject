using Hotel.Shared.Interfaces;

namespace Hotel.AdoDAL.Repositories
{
    public class AdoRepositoryFactory : IRepositoryFactory
    {
        private readonly AdoGuestRepository guestService;
        private readonly AdoReservationRepository reservationService;
        private readonly AdoPaymentRepository paymentService;
        private readonly AdoRoomRepository roomService;
        private readonly AdoRoomStatusRepository roomStatusService;
        private readonly AdoRoomTypeRepository roomTypeService;
        public AdoRepositoryFactory(AdoGuestRepository guestService, AdoReservationRepository reservationService, AdoPaymentRepository paymentService,
            AdoRoomRepository roomService, AdoRoomStatusRepository roomStatusService, AdoRoomTypeRepository roomTypeService)
        {
            this.guestService = guestService;
            this.reservationService = reservationService;
            this.paymentService = paymentService;
            this.roomService = roomService;
            this.roomStatusService = roomStatusService;
            this.roomTypeService = roomTypeService;
        }
        public IGuestRepository GetGuestRepository()
        {
            return guestService;
        }

        public IPaymentRepository GetPaymentRepository()
        {
            return paymentService;
        }

        public IReservationRepository GetReservationRepository()
        {
            return reservationService;
        }

        public IRoomRepository GetRoomRepository()
        {
            return roomService;
        }

        public IRoomStatusRepository GetRoomStatusRepository()
        {
            return roomStatusService;
        }

        public IRoomTypeRepository GetRoomTypeRepository()
        {
            return roomTypeService;
        }
    }
}
