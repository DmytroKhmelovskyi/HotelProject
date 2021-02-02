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
            Regex emailPattern = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
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
        public static bool IsNullOrEmpty(string nameInput)
        {
            if (string.IsNullOrEmpty(nameInput))
            {
                Console.WriteLine("Field cant be empty");
                return false;
            }

            return true;
        }
        public static bool ValidateString(string string1)
        {
            List<string> invalidChars = new List<string>() { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", };
            if (string1.Length > 50)
            {
                Console.WriteLine("String too Long");
                return false;
            }
            else if (!(!string1.Equals(string1.ToLower())))
            {
                Console.WriteLine("Requres at least one uppercase");
                return false;
            }
            else
            {
                foreach (string s in invalidChars)
                {
                    if (string1.Contains(s))
                    {
                        Console.WriteLine("String contains invalid character: " + s);
                        return false;
                    }
                }
                return true;
            }
        }

    }
}
