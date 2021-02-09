using Hotel.ConsoleApp.Presenters;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;

namespace Hotel.ConsoleApp.Menus
{
    public class GuestMenu
    {
        private readonly IGuestRepository guestService;
        public GuestMenu(IRepositoryFactory services)
        {
            this.guestService = services.GetGuestRepository();
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("1. Show all guests ");
                Console.WriteLine("2. Add guest ");
                Console.WriteLine("3. Update guest ");
                Console.WriteLine("4. Delete guest ");
                Console.WriteLine("x. Main menu");
                var key = Console.ReadLine();
                Console.WriteLine("------------------------------------------------------------------------" +
              "-----------------------------------------------------------------------------------");
                switch (key)
                {
                    case "1":
                        ShowAllGuests();
                        break;
                    case "2":
                        AddGuest();
                        break;
                    case "3":
                        UpdateGuest();
                        break;
                    case "4":
                        DeleteGuest();
                        break;
                    case "x":
                        return;
                    default:
                        throw new ArgumentException("unhendled case");
                }
                Console.WriteLine("------------------------------------------------------------------------" +
              "-----------------------------------------------------------------------------------");
            }
        }

        private void ShowAllGuests()
        {
            var guests = guestService.ReadGuests();
            ConsoleGuestPresenter.Present(guests);
        }

        private void AddGuest()
        {
            try
            {
                Console.WriteLine("Print First_Name: ");
                string fName = Console.ReadLine();
                if (!Validation.IsNullOrEmpty(fName) || !Validation.ValidateString(fName))
                { AddGuest(); }
                Console.WriteLine("Print Last_Name: ");
                string lName = Console.ReadLine();
                if (!Validation.IsNullOrEmpty(lName) || !Validation.ValidateString(lName))
                    AddGuest();
                string email = Validation.ReadEmail();
                string phone = Validation.ReadPhone();
                Console.WriteLine("Print City: ");
                string city = Console.ReadLine();
                if (!Validation.IsNullOrEmpty(city) || !Validation.ValidateString(city))
                    AddGuest();
                Console.WriteLine("Print Country: ");
                string country = Console.ReadLine();
                if (!Validation.IsNullOrEmpty(country) || !Validation.ValidateString(country))
                    AddGuest();

                var guest = new Guest
                {
                    FirstName = fName,
                    LastName = lName,
                    Email = email,
                    Phone = phone,
                    City = city,
                    Country = country,
                };

                guestService.AddGuest(guest);
                Console.WriteLine("Guest added successful");
                ConsoleGuestPresenter.Present(guestService.ReadGuests());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AddGuest();
            }


        }
        public void UpdateGuest()
        {
            try
            {
                Guest guest = new Guest();
                Console.WriteLine("Print Id: ");
                int id = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Print First_Name: ");
                guest.FirstName = Console.ReadLine();
                if (!Validation.IsNullOrEmpty(guest.FirstName) || !Validation.ValidateString(guest.FirstName))
                    UpdateGuest();
                Console.WriteLine("Print Last_Name: ");
                guest.LastName = Console.ReadLine();
                if (!Validation.IsNullOrEmpty(guest.LastName) || !Validation.ValidateString(guest.LastName))
                    UpdateGuest();
                guest.Email = Validation.ReadEmail();
                guest.Phone = Validation.ReadPhone();
                Console.WriteLine("Print City: ");
                guest.City = Console.ReadLine();
                if (!Validation.IsNullOrEmpty(guest.City) || !Validation.ValidateString(guest.City))
                    UpdateGuest();
                Console.WriteLine("Print Country: ");
                guest.Country = Console.ReadLine();
                if (!Validation.IsNullOrEmpty(guest.Country) || !Validation.ValidateString(guest.Country))
                    UpdateGuest();
                guestService.UpdateGuests(id, guest);
                Console.WriteLine("Object successful updated");
                ConsoleGuestPresenter.Present(guestService.ReadGuests());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                UpdateGuest();
            }

        }
        public void DeleteGuest()
        {
            try
            {
                Console.WriteLine("Print Id: ");
                int id = Int32.Parse(Console.ReadLine());
                guestService.DeleteGuests(id);
                Console.WriteLine("Object successful deleted");
                ConsoleGuestPresenter.Present(guestService.ReadGuests());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DeleteGuest();
            }
        }
    }


}

