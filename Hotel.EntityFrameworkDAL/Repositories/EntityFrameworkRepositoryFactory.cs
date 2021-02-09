using Hotel.Shared.Interfaces;

namespace Hotel.EntityFrameworkDAL.Repositories
{
    public class EntityFrameworkRepositoryFactory : IRepositoryFactory
    {
        private readonly EfGuestRepository guestRepository;
        private readonly EfPaymentRepository paymentRepository;
        private readonly EfReservationRepository reservationRepository;
        private readonly EfRoomRepository roomRepository;
        private readonly EfRoomStatusRepository  roomStatusRepository;
        private readonly EfRoomTypeRepository roomTypeRepository;
        public EntityFrameworkRepositoryFactory(EfGuestRepository guestRepository, EfPaymentRepository paymentRepository, EfReservationRepository reservationRepository,
            EfRoomRepository roomRepository, EfRoomStatusRepository roomStatusRepository, EfRoomTypeRepository roomTypeRepository)
        {
            this.guestRepository = guestRepository;
            this.paymentRepository = paymentRepository;
            this.reservationRepository = reservationRepository;
            this.roomRepository = roomRepository;
            this.roomStatusRepository = roomStatusRepository;
            this.roomTypeRepository = roomTypeRepository;
        }
        public IGuestRepository GetGuestRepository()
        {
            return guestRepository;
        }

        public IPaymentRepository GetPaymentRepository()
        {
            return paymentRepository;
        }
        public IReservationRepository GetReservationRepository()
        {
            return reservationRepository;
        }
        public IRoomRepository GetRoomRepository()
        {
            return roomRepository;
        }
        public IRoomStatusRepository GetRoomStatusRepository()
        {
            return roomStatusRepository;
        }
        public IRoomTypeRepository GetRoomTypeRepository()
        {
            return roomTypeRepository;
        }
    }
}
