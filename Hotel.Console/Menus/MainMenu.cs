using System;

namespace Hotel.ConsoleApp.Menus
{
    public class MainMenu
    {
        private readonly GuestMenu guestMenu;
        private readonly ReservationMenu reservationMenu;
        private readonly PaymentMenu paymentMenu;
        private readonly RoomMenu roomMenu;
        private readonly RoomStatusMenu roomStatusMenu;
        private readonly RoomTypeMenu roomTypeMenu;

        public MainMenu(GuestMenu guestMenu, ReservationMenu reservationMenu, PaymentMenu paymentMenu, RoomMenu roomMenu,
            RoomStatusMenu roomStatusMenu, RoomTypeMenu roomTypeMenu)
        {
            this.guestMenu = guestMenu;
            this.reservationMenu = reservationMenu;
            this.paymentMenu = paymentMenu;
            this.roomMenu = roomMenu;
            this.roomStatusMenu = roomStatusMenu;
            this.roomTypeMenu = roomTypeMenu;
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1. Guests Operstions ");
                Console.WriteLine("2. Reservations Operstions ");
                Console.WriteLine("3. Payments Operstions ");
                Console.WriteLine("4. Rooms Operstions ");
                Console.WriteLine("5. RoomsStatuses Operstions ");
                Console.WriteLine("6. RoomsTypes Operstions ");
                Console.WriteLine("x. Quit ");

                var key = Console.ReadLine();
                Console.WriteLine("------------------------------------------------------------------------" +
              "-----------------------------------------------------------------------------------");
                switch (key)
                {
                    case "1":
                        guestMenu.Show();
                        break;
                    case "2":
                        reservationMenu.Show();
                        break;
                    case "3":
                        paymentMenu.Show();
                        break;
                    case "4":
                        roomMenu.Show();
                        break;
                    case "5":
                        roomStatusMenu.Show();
                        break;
                    case "6":
                        roomTypeMenu.Show();
                        break;

                    case "x":
                        return;
                }
                Console.WriteLine("------------------------------------------------------------------------" +
              "-----------------------------------------------------------------------------------");
            }
        }
    }
}
