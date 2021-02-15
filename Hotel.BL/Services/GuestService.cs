using AutoMapper;
using Hotel.BL.Interfaces;
using Hotel.BL.Models;
using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;

namespace Hotel.BL.Services
{
    public class GuestService : IGuestService
    {
        private readonly IMapper mapper;
        private IGuestRepository guestRepository;
        public GuestService(IMapper mapper, IGuestRepository guestRepository)
        {
            this.mapper = mapper;
            this.guestRepository = guestRepository;
        }

        public GuestViewModel AddGuest(GuestViewModel model)
        {
            var guestModel = mapper.Map<Guest>(model);

            var guest = guestRepository.AddGuest(guestModel);

            return mapper.Map<GuestViewModel>(guest);

        }

        public GuestViewModel DeleteGuests(int id)
        {

                var guest = guestRepository.DeleteGuests(id);
                return mapper.Map<GuestViewModel>(guest);

        }

        public IEnumerable<GuestViewModel> ReadGuests()
        {

            var guests = guestRepository.ReadGuests();
            var guestModel = mapper.Map<IEnumerable<GuestViewModel>>(guests);
            return guestModel;


        }

        public (IEnumerable<GuestViewModel> models, int count) ReadGuests(GuestFilter filter)
        {
            var (guests, count) = guestRepository.ReadGuests(filter);
            var guestModel = mapper.Map<IEnumerable<GuestViewModel>>(guests);
            return (guestModel, count);

        }

        public GuestViewModel ReadSingle(int? id)
        {

            var model = mapper.Map<GuestViewModel>(guestRepository.ReadSingle(id));
            return model;


        }

        public GuestViewModel UpdateGuests(int id, GuestViewModel model)
        {

            var guestModel = mapper.Map<Guest>(model);

            var guest = guestRepository.UpdateGuests(id, guestModel);

            return mapper.Map<GuestViewModel>(guest);


        }

    }
}
