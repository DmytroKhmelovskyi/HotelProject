using Hotel.Shared.FilterModels;
using Hotel.Shared.Models;
using Hotel.Web.VIewModel;
using System.Collections.Generic;

namespace Hotel.Web.Interfaces
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
