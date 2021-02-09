using Hotel.ConsoleApp.Presenters;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.ConsoleApp.Menus
{
    public class ReservationMenu
    {
        private readonly IReservationRepository reservationService;
        public ReservationMenu(IRepositoryFactory service)
        {
            reservationService = service.GetReservationRepository();
        }
        public void Show()
        {
            while (true)
            {
                Console.WriteLine("1. Add reservation ");
                Console.WriteLine("2. Read reservation ");
                Console.WriteLine("3. Update reservation ");
                Console.WriteLine("4. Delete reservation ");
                Console.WriteLine("x. Main menu ");
                string c = Console.ReadLine();
                Console.WriteLine("------------------------------------------------------------------------" +
                  "-----------------------------------------------------------------------------------");

                switch (c)
                {
                    case "1":
                        AddReservation();
                        break;
                    case "2":
                        ReadReservation();
                        break;
                    case "3":
                        UpdateReservation();
                        break;
                    case "4":
                        DeleteReservation();
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
        public void AddReservation()
        {
            try
            {
                Reservation reservation = new Reservation();
                Console.WriteLine("Print GuestId: ");
                reservation.GuestId = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Print RoomId: ");
                reservation.RoomId = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Print ReservationDate: ");
                reservation.ReservationDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Print CheckInDate: ");
                reservation.CheckInDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Print CheckOutDate: ");
                reservation.CheckOutDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Print PersonCount: ");
                reservation.PersonCount = Int32.Parse(Console.ReadLine());
                reservationService.AddReservation(reservation);
                Console.WriteLine("Object Added updated");
                ConsoleReservationPresenter.Present(reservationService.ReadReservations());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AddReservation();
            }

        }
        public void ReadReservation()
        {
            ConsoleReservationPresenter.Present(reservationService.ReadReservations());
        }
        public void UpdateReservation()
        {
            try
            {
                Reservation reservation = new Reservation();
                Console.WriteLine("Print Id: ");
                int id = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Print GuestId: ");
                reservation.GuestId = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Print RoomId: ");
                reservation.RoomId = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Print ReservationDate: ");
                reservation.ReservationDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Print CheckInDate: ");
                reservation.CheckInDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Print CheckOutDate: ");
                reservation.CheckOutDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Print PersonCount: ");
                reservation.PersonCount = Int32.Parse(Console.ReadLine());
                reservationService.UpdateReservation(id, reservation);
                Console.WriteLine("Object successful updated");
                ConsoleReservationPresenter.Present(reservationService.ReadReservations());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                UpdateReservation();
            }

        }
        public void DeleteReservation()
        {
            try
            {
                Console.WriteLine("Print Id: ");
                int id = Int32.Parse(Console.ReadLine());
                reservationService.DeleteReservation(id);
                Console.WriteLine("Object successful deleted");
                ConsoleReservationPresenter.Present(reservationService.ReadReservations());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DeleteReservation();
            }
        }
    }
}
