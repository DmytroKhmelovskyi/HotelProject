using Hotel.ConsoleApp.Presenters;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.ConsoleApp.Menus
{
    public class PaymentMenu
    {
        private readonly IPaymentRepository paymentService;
        public PaymentMenu(IRepositoryFactory service)
        {
            paymentService = service.GetPaymentRepository();
        }
        public void Show()
        {
            while (true)
            {
                Console.WriteLine("1. Add payment ");
                Console.WriteLine("2. Read payments ");
                Console.WriteLine("3. Update payment ");
                Console.WriteLine("4. Delete payment ");
                Console.WriteLine("x. Main menu ");
                string c = Console.ReadLine();
                Console.WriteLine("------------------------------------------------------------------------" +
                  "-----------------------------------------------------------------------------------");

                switch (c)
                {
                    case "1":
                        AddPayment();
                        break;
                    case "2":
                        ReadPayment();
                        break;
                    case "3":
                        UpdatePayment();
                        break;
                    case "4":
                        DeletePayment();
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
        public void AddPayment()
        {
            Payment payment = new Payment();
            try
            {
                Console.WriteLine("Print GuestId: ");
                payment.GuestId = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Print ReservationId: ");
                payment.ReservationId = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Print Amount: ");
                payment.Amount = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Print PayTime: ");
                payment.PayTime = DateTime.Parse(Console.ReadLine());
                paymentService.AddPayment(payment);
                Console.WriteLine("Object successful Added");
                ConsolePaymentPresenter.Present(paymentService.ReadPayments());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AddPayment();
            }
        }
  
    public void ReadPayment()
    {
        ConsolePaymentPresenter.Present(paymentService.ReadPayments());
    }

    public void UpdatePayment()
    {

        Payment payment = new Payment();
        try
        {
            Console.WriteLine("Print Id: ");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Print GuestId: ");
            payment.GuestId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Print ReservationId: ");
            payment.ReservationId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Print Amount: ");
            payment.Amount = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Print PayTime: ");
            payment.PayTime = DateTime.Parse(Console.ReadLine());
            paymentService.UpdatePayment(id, payment);
            Console.WriteLine("Object successful updated");
            ConsolePaymentPresenter.Present(paymentService.ReadPayments());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            UpdatePayment();

        }

    }
    public void DeletePayment()
    {
        try
        {
            Console.WriteLine("Print Id: ");
            int id = Int32.Parse(Console.ReadLine());
            paymentService.DeletePayment(id);
            Console.WriteLine("Object successful deleted");
            ConsolePaymentPresenter.Present(paymentService.ReadPayments());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            DeletePayment();

        }
    }
}
}
