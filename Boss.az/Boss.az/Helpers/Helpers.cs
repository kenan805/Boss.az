using Boss.az.ExceptionNS;
using ExtensionNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boss.az.HelpersNS
{
    public static class Helpers
    {
        public static bool IsValidPhone(string phone)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (!phone.Trim().StartsWith("0"))
            {
                Console.WriteLine("User telfon nomresi 0 ile baslamalidir!!");
                Console.ResetColor();
                return false;
            }
            else if (phone.Trim().Replace(" ", String.Empty).Length != 10)
            {
                Console.WriteLine("User telfon nomresi 10 reqemli olmalidir!");
                Console.ResetColor();
                return false;
            }
            else if (!phone.Trim().Replace(" ", String.Empty).All(c => char.IsDigit(c)))
            {
                Console.WriteLine("User telefonda ancag nomre yazilmalidir!");
                Console.ResetColor();
                return false;
            }
            else if (phone.HasSpecialChar())
            {
                Console.WriteLine("User telefon --> Different symbols!");
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
            else if (text.Length < 3)
            {
                Console.WriteLine("Usernamede lengthi 3den kicik olmamalidir!");
                Console.ResetColor();
                return false;
            }
            else if (text.Trim().Contains(' '))
            {
                Console.WriteLine("Usernamede yazilis sehvdir!");
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
                Console.WriteLine("Name lengthi 3den kicik olmamalidir!");
                Console.ResetColor();
                return false;
            }
            else if (!text.All(c => char.IsLetter(c)))
            {
                Console.WriteLine("Namede ancag herf ola biler!");
                Console.ResetColor();
                return false;
            }
            else if (text.Trim().Contains(' '))
            {
                Console.WriteLine("Name yazilis sehvdir!");
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
                Console.WriteLine("Surname lengthi 5den kicik olmamalidir!");
                Console.ResetColor();
                return false;
            }
            else if (!text.All(c => char.IsLetter(c)))
            {
                Console.WriteLine("Surnamede ancag herf ola biler!");
                Console.ResetColor();
                return false;
            }
            else if (text.Trim().Contains(' '))
            {
                Console.WriteLine("Surname yazilis sehvdir!");
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
            else if (!text.All(c => char.IsLetter(c)))
            {
                Console.WriteLine("Cityde ancag herf ola biler!");
                Console.ResetColor();
                return false;
            }
            else if (text.Trim().Contains(' '))
            {
                Console.WriteLine("City yazilis sehvdir!");
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
                Console.WriteLine("Emailde yazilis sehvdir!");
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
                Console.WriteLine("Emailde yazilis sehvdir!");
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
            if (text.Length < 5)
            {
                Console.WriteLine("Password lengthi 5den kicik olmamalidir!");
                Console.ResetColor();
                return false;
            }
            else if (text.Trim().Contains(' '))
            {
                Console.WriteLine("Passwordda yazilis sehvdir!");
                Console.ResetColor();
                return false;
            }
            //else if (!(text.Trim().Any(char.IsUpper) && text.Trim().Any(char.IsLower)))
            //{
            //    Console.WriteLine("Passwordda hem boyuk hemde kicik herf en azi birdene olmalidir!");
            //    Console.ResetColor();
            //    return false;
            //}
            else if (!(text.Trim().Any(char.IsDigit)))
            {
                Console.WriteLine("Passwordda en azi bir reqem olmalidir!");
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
