using AutoMapper;
using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using Hotel.Web.Interfaces;
using Hotel.Web.VIewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IMapper mapper;
        public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
        {
            this.reservationRepository = reservationRepository;
            this.mapper = mapper;
        }
        public ReservationViewModel AddReservation(ReservationViewModel model)
        {
            var reservationModel = mapper.Map<Reservation>(model);
            var reservation = reservationRepository.AddReservation(reservationModel);
            return mapper.Map<ReservationViewModel>(reservation);
        }

        public ReservationViewModel DeleteReservation(int id)
        {
            return mapper.Map<ReservationViewModel>(reservationRepository.DeleteReservation(id));
        }

        public IEnumerable<ReservationViewModel> ReadReservations()
        {
            return mapper.Map<IEnumerable<ReservationViewModel>>(reservationRepository.ReadReservations());
        }

        public (IEnumerable<ReservationViewModel> models, int count) ReadReservations(ReservationFilter filter)
        {
            var (reservations, count) = reservationRepository.ReadReservations(filter);
            var reservationModel = mapper.Map<IEnumerable<ReservationViewModel>>(reservations);
            return (reservationModel, count);
        }

        public ReservationViewModel ReadSingle(int? id)
        {
            return mapper.Map<ReservationViewModel>(reservationRepository.ReadSingle(id));
        }

        public ReservationViewModel UpdateReservation(int id, ReservationViewModel model)
        {
            var reservationModel = mapper.Map<Reservation>(model);
            var reservation = reservationRepository.UpdateReservation(id, reservationModel);
            return mapper.Map<ReservationViewModel>(reservation);
        }
    }
}
