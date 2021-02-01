using Hotel.ConsoleApp.Presenters;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;

namespace Hotel.ConsoleApp.Menus
{
    public class GuestMenu
    {
        private readonly IGuestService guestService;
        public GuestMenu(IServiceFactory services)
        {
            this.guestService = services.GetGuestService();
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
            Console.WriteLine("Print First_Name: ");
            string fName = Validation.isValidInput();
            Console.WriteLine("Print Last_Name: ");
            string lName = Validation.isValidInput();
            string email = Validation.ReadEmail();
            string phone = Validation.ReadPhone();
            Console.WriteLine("Print City: ");
            string city = Validation.isValidInput();
            Console.WriteLine("Print Country: ");
            string country = Validation.isValidInput();

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
        public void UpdateGuest()
        {
            Guest guest = new Guest();
            Console.WriteLine("Print Id: ");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Incorrect Id ");
            Console.WriteLine("Print First_Name: ");
            guest.FirstName = Validation.isValidInput();
            Console.WriteLine("Print Last_Name: ");
            guest.LastName = Validation.isValidInput();
            Console.WriteLine("Print Email: ");
            guest.Email = Validation.ReadEmail();
            Console.WriteLine("Print Number: ");
            guest.Phone = Validation.ReadPhone();
            Console.WriteLine("Print City: ");
            guest.City = Validation.isValidInput();
            Console.WriteLine("Print Country: ");
            guest.Country = Validation.isValidInput();
            guestService.UpdateGuests(id, guest);
            Console.WriteLine("Object successful updated");
            ConsoleGuestPresenter.Present(guestService.ReadGuests());

        }
        public void DeleteGuest()
        {
            Console.WriteLine("Print Id: ");
            int id = Int32.Parse(Console.ReadLine());
            guestService.DeleteGuests(id);
            Console.WriteLine("Object successful deleted");
            ConsoleGuestPresenter.Present(guestService.ReadGuests());
        }
    }


}

