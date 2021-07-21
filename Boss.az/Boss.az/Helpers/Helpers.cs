using ExtensionNS;
using System;
using System.Linq;

namespace Boss.az.HelpersNS
{
    public static class Helpers
    {
        public static bool IsValidEmpty(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (text == string.Empty)
            {
                Console.WriteLine($"Item empty!");
                Console.ResetColor();
                return false;
            }
            Console.ResetColor();
            return true;
        }
        public static bool IsValidPhone(string phone)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (phone.Trim().Replace(" ", String.Empty).Length != 10)
            {
                Console.WriteLine("User phone number must be 10 digits !");
                Console.ResetColor();
                return false;
            }
            else if (!phone.Trim().StartsWith("0"))
            {
                Console.WriteLine("User phone number must start with 0 !");
                Console.ResetColor();
                return false;
            }
            else if (!phone.Trim().Replace(" ", String.Empty).All(c => char.IsDigit(c)))
            {
                Console.WriteLine("The user's phone number should only have numbers !");
                Console.ResetColor();
                return false;
            }
            else if (phone.HasSpecialChar())
            {
                Console.WriteLine("User phone number  --> Different symbols!");
                Console.ResetColor();
                return false;
            }
            Console.ResetColor();
            return true;
        }
        public static bool IsValidUsername(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (text == string.Empty)
            {
                Console.WriteLine("Username empty!");
                Console.ResetColor();
                return false;
            }
            else if (text.Trim().Contains(' '))
            {
                Console.WriteLine("The spelling of the username is incorrect !");
                Console.ResetColor();
                return false;
            }
            else if (text.Length < 3)
            {
                Console.WriteLine("Username length should not be less than 3 !");
                Console.ResetColor();
                return false;
            }
            Console.ResetColor();
            return true;
        }
        public static bool IsValidSpecialty(string specialty)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (specialty == string.Empty)
            {
                Console.WriteLine("Specialty empty!");
                Console.ResetColor();
                return false;
            }
            else if (specialty.Trim().Contains(' '))
            {
                Console.WriteLine("The spelling of the specialty is incorrect !");
                Console.ResetColor();
                return false;
            }
            else if (specialty.HasSpecialChar())
            {
                Console.WriteLine("The specialty --> different symbols!");
                Console.ResetColor();
                return false;
            }
            else if (!specialty.All(c => char.IsLetter(c)))
            {
                Console.WriteLine("The specialty can only be a letter!");
                Console.ResetColor();
                return false;
            }
            Console.ResetColor();
            return true;
        }
        public static bool IsValidName(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (text == string.Empty)
            {
                Console.WriteLine("Name empty!");
                Console.ResetColor();
                return false;
            }
            else if (text.Length < 3)
            {
                Console.WriteLine("Name length should not be less than 3 !");
                Console.ResetColor();
                return false;
            }
            else if (text.Trim().Contains(' '))
            {
                Console.WriteLine("The spelling of the name is incorrect !");
                Console.ResetColor();
                return false;
            }
            else if (!text.All(c => char.IsLetter(c)))
            {
                Console.WriteLine("The name can only be a letter!");
                Console.ResetColor();
                return false;
            }
            Console.ResetColor();
            return true;
        }
        public static bool IsValidSurname(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (text == string.Empty)
            {
                Console.WriteLine("Surname empty!");
                Console.ResetColor();
                return false;
            }
            else if (text.Length < 5)
            {
                Console.WriteLine("Surname length should not be less than 5 !");
                Console.ResetColor();
                return false;
            }
            else if (text.Trim().Contains(' '))
            {
                Console.WriteLine("The surname is misspelled !");
                Console.ResetColor();
                return false;
            }
            else if (!text.All(c => char.IsLetter(c)))
            {
                Console.WriteLine("The surname can only be a letter !");
                Console.ResetColor();
                return false;
            }
            Console.ResetColor();
            return true;
        }
        public static bool IsValidCity(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (text == string.Empty)
            {
                Console.WriteLine("City empty!");
                Console.ResetColor();
                return false;
            }
            else if (text.Trim().Contains(' '))
            {
                Console.WriteLine("City is a misspelling !");
                Console.ResetColor();
                return false;
            }
            else if (!text.All(c => char.IsLetter(c)))
            {
                Console.WriteLine("There can only be letters in the city !");
                Console.ResetColor();
                return false;
            }
            Console.ResetColor();
            return true;
        }
        public static bool IsValidEmail(string email)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (email == string.Empty)
            {
                Console.WriteLine("Emailde empty!");
                Console.ResetColor();
                return false;
            }
            else if (email.Trim().Contains(' '))
            {
                Console.WriteLine("Email is a misspelling !");
                Console.ResetColor();
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                Console.ResetColor();
                return addr.Address == email;
            }
            catch
            {
                Console.WriteLine("Email is a misspelling !");
                Console.ResetColor();
                return false;
            }
        }
        public static bool IsValidPassword(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (text == string.Empty)
            {
                Console.WriteLine("Password empty!");
                Console.ResetColor();
                return false;
            }
            else if (text.Trim().Contains(' '))
            {
                Console.WriteLine("Password is a misspelling !");
                Console.ResetColor();
                return false;
            }
            else if (text.Length < 5)
            {
                Console.WriteLine("Password length should not be less than 5 !");
                Console.ResetColor();
                return false;
            }
            else if (!(text.Trim().Any(char.IsDigit)))
            {
                Console.WriteLine("Password must contain at least one digit !");
                Console.ResetColor();
                return false;

            }
            Console.ResetColor();
            return true;
        }
        public static bool IsValidDateYear(int year)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (year.ToString().Length != 4 || year > DateTime.Now.Year)
            {
                Console.WriteLine("Incorret year!!!");
                Console.ResetColor();
                return false;

            }
            Console.ResetColor();
            return true;
        }
        public static bool IsValidDateMonth(int month)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (month > 12)
            {
                Console.WriteLine("Incorret month!!!");
                Console.ResetColor();
                return false;

            }
            Console.ResetColor();
            return true;
        }
        public static bool IsValidDateDay(int year, int month, int day)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (day > DateTime.DaysInMonth(year, month))
            {
                Console.WriteLine("Incorret day!!!");
                Console.ResetColor();
                return false;

            }
            Console.ResetColor();
            return true;
        }
    }
}
