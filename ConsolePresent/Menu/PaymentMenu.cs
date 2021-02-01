//using Hotel.Shared.Interfaces;
//using Hotel.Shared.Models;
//using EntityFrameworkProgect.Presenters;
//using EntityFrameworkProgect.Services;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace EntityFrameworkProgect.Menu
//{
//    class PaymentMenu
//    {
//        private IPaymentService paymentService;
//        private ConsolePaymentPresenter presenter;
//        public void PaymentsMenu()
//        {
//            Console.WriteLine("1. Create new Payment ");
//            Console.WriteLine("2. Read Payments ");
//            Console.WriteLine("3. Update Payment ");
//            Console.WriteLine("4. Delete Payment ");
//            string c = Console.ReadLine();
//            Console.WriteLine("------------------------------------------------------------------------" +
//              "-----------------------------------------------------------------------------------");

//            switch (c)
//            {
//                case "1":
//                    AddPayment();
//                    break;
//                case "2":
//                    ReadPayment();
//                    break;
//                case "3":
//                    UpdatePayment();
//                    break;
//                case "4":
//                    DeletePayment();
//                    break;
//                default:
//                    throw new ArgumentException("unhendled case");

//            }
//            Console.WriteLine("------------------------------------------------------------------------" +
//              "-----------------------------------------------------------------------------------");
//            MainMenu.Menu();


//        }
//        public void AddPayment()
//        {
//            Payment payment = new Payment();
//            Console.WriteLine("Print GuestId: ");
//            payment.GuestId = Int32.Parse(Console.ReadLine());
//            Console.WriteLine("Print ReservationId: ");
//            payment.ReservationId = Int32.Parse(Console.ReadLine());
//            Console.WriteLine("Print Amount: ");
//            payment.Amount = Int32.Parse(Console.ReadLine());
//            Console.WriteLine("Print PayTime: ");
//            payment.PayTime = DateTime.Parse(Console.ReadLine());
//            paymentService.AddPayment(payment);
//            presenter.Presenter(paymentService.ReadPayments());

//        }
//        public void ReadPayment()
//        {
//            presenter.Presenter(paymentService.ReadPayments());
//        }
        
//            public void UpdatePayment()
//        {
//            Payment payment = new Payment();
//            Console.WriteLine("Print Id: ");
//            int id = Int32.Parse(Console.ReadLine());
//            Console.WriteLine("1.Change GuestId");
//            Console.WriteLine("2.Change ReservationId");
//            Console.WriteLine("3.Change Amount");
//            Console.WriteLine("4.Change PayTime");
//            Console.WriteLine("5.Change All");

//            string cs = Console.ReadLine();
//            switch (cs)
//            {
//                case "1":
//                    Console.WriteLine("Print GuestId: ");
//                    payment.GuestId = Int32.Parse(Console.ReadLine());
//                    break;
//                case "2":
//                    Console.WriteLine("Print ReservationId: ");
//                    payment.ReservationId = Int32.Parse(Console.ReadLine());
//                    break;
//                case "3":
//                    Console.WriteLine("Print Amount: ");
//                    payment.Amount = Int32.Parse(Console.ReadLine());
//                    break;
//                case "4":
//                    Console.WriteLine("Print PayTime: ");
//                    payment.PayTime = DateTime.Parse(Console.ReadLine());
//                    break;
//                case "5":
//                    Console.WriteLine("Print GuestId: ");
//                    payment.GuestId = Int32.Parse(Console.ReadLine());
//                    Console.WriteLine("Print ReservationId: ");
//                    payment.ReservationId = Int32.Parse(Console.ReadLine());
//                    Console.WriteLine("Print Amount: ");
//                    payment.Amount = Int32.Parse(Console.ReadLine());
//                    Console.WriteLine("Print PayTime: ");
//                    payment.PayTime = DateTime.Parse(Console.ReadLine());
//                    break;
//                default:
//                    throw new Exception("wrong case");

//            }
//            paymentService.UpdatePayment(id, payment);
//            Console.WriteLine("Object successful updated");

//        }
//        public void DeletePayment()
//        {
//            Console.WriteLine("Print Id: ");
//            int id = Int32.Parse(Console.ReadLine());
//            paymentService.DeletePayment(id);
//            Console.WriteLine("Object successful deleted");
//        }
//    }
//}
