using Boss.az.Entity;
using Boss.az.HelpersNS;
using Boss.az.HumanNS;
using Boss.az.SubCategoryNS;
using ExtensionNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace Boss.az.DatabaseNS
{
    class Database
    {
        public List<Worker> Workers { get; set; }
        public List<Employer> Employers { get; set; }
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public Person SignIn(string username, string password)
        {
            if (username == null || password == null)
                throw new ArgumentNullException($"Sign in method username or password null!\n{Environment.CurrentDirectory}");
            else if (Workers.Any(w => w.Username == username && w.Password == password))
                return Workers.ToList().Find(w => w.Username == username && w.Password == password);
            else if (Employers.Any(e => e.Username == username && e.Password == password))
                return Employers.ToList().Find(e => e.Username == username && e.Password == password);
            return default;
        }
        public void SignUp(bool isWorker)
        {
            string name, surname, email, username, password;
            uint age;
            bool gender;
        RetryName:
            Console.Write("Enter name: ");
            name = Console.ReadLine();
            if (!Helpers.IsValidName(name)) goto RetryName;
            Console.Clear();
        RetrySurname:
            Console.Write("Enter surname: ");
            surname = Console.ReadLine();
            if (!Helpers.IsValidSurname(surname)) goto RetrySurname;
            Console.Clear();
            Console.Write("Enter age: ");
            age = uint.Parse(Console.ReadLine());
            Console.Clear();
        RetryGender:
            Console.Write("Enter gender(Male or Female): ");
            string temp = Console.ReadLine();
            if (temp.Trim().ToLower() == "male")
                gender = true;
            else if (temp.Trim().ToLower() == "female")
                gender = false;
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Wrong choice, please try again!");
                Console.ResetColor();
                goto RetryGender;
            }
            Console.Clear();
        RetryEmail:
            Console.Write("Enter email: ");
            email = Console.ReadLine();
            if (!Helpers.IsValidEmail(email)) goto RetryEmail;
            Console.Clear();
        Retry:
            Console.Write("Enter the number you want to add (min. 1): ");
            int countPhones = int.Parse(Console.ReadLine());
            if (countPhones == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("At least one phone number must be entered!");
                Thread.Sleep(1500);
                Console.ResetColor();
                goto Retry;
            }
            string phone;
            List<string> phones = new();
            for (int i = 0; i < countPhones; i++)
            {
            RetryPhone:
                Console.Write("Enter phone(s): ");
                phone = Console.ReadLine();
                if (!Helpers.IsValidPhone(phone)) goto RetryPhone;
                phones.Add(phone);
            }
            Console.Clear();
        RetryUsername:
            Console.Write("Enter username: ");
            username = Console.ReadLine();
            if (!Helpers.IsValidUsername(username)) goto RetryUsername;
            if (Workers.Any(w => w.Username == username) || Employers.Any(e => e.Username == username))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The username is available under this name. Please check again!");
                Console.ResetColor();
                goto RetryUsername;
            }
            Console.Clear();
        RetryPassword:
            Console.Write("Enter password: ");
            password = Console.ReadLine();
            if (!Helpers.IsValidPassword(password)) goto RetryPassword;
            RetryTryPassword:
            Console.Write("Write the password again: ");
            string tryPassword = Console.ReadLine();
            if (password != tryPassword)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Passwords do not match!");
                Console.ResetColor();
                goto RetryTryPassword;
            }

            if (isWorker)
            {
                Worker worker = new()
                {
                    Name = name.Trim().ToLower().FirstCharToUpper(),
                    Surname = surname.Trim().ToLower().FirstCharToUpper(),
                    Age = age,
                    Email = email,
                    Gender = gender,
                    Password = password,
                    Phones = phones,
                    Username = username
                };
                Workers.Add(worker);
                var options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                var textJson = JsonSerializer.Serialize(Workers, options);
                File.WriteAllText("Workers.json", textJson);
            }
            else
            {
                Employer employer = new()
                {
                    Name = name.Trim().ToLower().FirstCharToUpper(),
                    Surname = surname.Trim().ToLower().FirstCharToUpper(),
                    Age = age,
                    Email = email,
                    Gender = gender,
                    Password = password,
                    Phones = phones,
                    Username = username
                };
                Employers.Add(employer);
                var options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                var textJson = JsonSerializer.Serialize(Employers, options);
                File.WriteAllText("Employers.json", textJson);
            }
        }

    }
}
