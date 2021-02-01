using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Hotel.ConsoleApp
{
    public static class Validation
    {
        public static string ReadEmail()
        {
            var isValid = false;
            var email = "";
            Regex emailPattern = new Regex(@"^([\w\.\-] +)@([\w\-] +)((\.(\w){ 2,3})+)$");
            do
            {
                Console.WriteLine("Print Email: ");
                email = Console.ReadLine();
                if (emailPattern.IsMatch(email))
                {
                    isValid = true;
                    break;
                }

                Console.WriteLine("Incorrect email.");
            } while (!isValid);

            return email;
        }
        public static string ReadPhone()
        {
            var phone = "";
            var isValid = false;
            Regex numberPattern = new Regex(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$");

            do
            {
                Console.WriteLine("Print Phone: ");
                phone = Console.ReadLine();
                if (numberPattern.IsMatch(phone))
                {
                    isValid = true;
                    break;
                }

                Console.WriteLine("Incorrect number.");
            } while (!isValid);

            return phone;

        }
        public static string isValidInput()
        {
            bool isValid = false;
            var nameInput = "";
            do
            {
                nameInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(nameInput))
                {
                    isValid = true;
                    break;

                }
                Console.WriteLine("Incorrect input");


            } while (!isValid);
            return nameInput;
        }

    }
}
