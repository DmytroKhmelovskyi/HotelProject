using Hotel.BL.Interfaces;
using Hotel.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Hotel.BL.Models.Validation
{
    class IsGuestExistAttribute : ValidationAttribute
    {
        private readonly IGuestService  guestService;
        public IsGuestExistAttribute(IGuestService guestService)
        {
            this.guestService = guestService;
            ErrorMessage = "Guest with such id does not exist";
        }

        public override bool IsValid(object value)
        {
            GuestViewModel guestViewModel = value as GuestViewModel;
            if (guestService.IsGuestExist(guestViewModel.Id))
            {
                return true;
            }
            return false;
        }

    }
}
