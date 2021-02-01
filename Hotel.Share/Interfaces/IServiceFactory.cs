namespace Hotel.Shared.Interfaces
{
    public interface IServiceFactory
    {
        IGuestService GetGuestService();

        IPaymentService GetPaymentService();
        IReservationService GetReservationService();
        IRoomService GetRoomService();
        IRoomStatusService GetRoomStatusService();
        IRoomTypeService GetRoomTypeService();

    }
}
