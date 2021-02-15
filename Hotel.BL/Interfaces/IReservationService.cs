using Hotel.Shared.FilterModels;
using Hotel.BL.Models;
using System.Collections.Generic;

namespace Hotel.BL.Interfaces
{
    public interface IReservationService
    {
        IEnumerable<ReservationViewModel> ReadReservations();

        ReservationViewModel AddReservation(ReservationViewModel model);
        ReservationViewModel UpdateReservation(int id, ReservationViewModel model);
        ReservationViewModel DeleteReservation(int id);
        ReservationViewModel ReadSingle(int? id);
        (IEnumerable<ReservationViewModel> models, int count) ReadReservations(ReservationFilter filter);
    }
}
