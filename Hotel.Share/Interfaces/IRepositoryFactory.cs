namespace Hotel.Shared.Interfaces
{
    public interface IRepositoryFactory
    {
        IGuestRepository GetGuestRepository();

        IPaymentRepository GetPaymentRepository();
        IReservationRepository GetReservationRepository();
        IRoomRepository GetRoomRepository();
        IRoomStatusRepository GetRoomStatusRepository();
        IRoomTypeRepository GetRoomTypeRepository();

    }
}
