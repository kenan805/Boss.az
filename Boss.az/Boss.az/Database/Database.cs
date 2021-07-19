using Boss.az.CategoryNS;
using Boss.az.CompanyNS;
using Boss.az.ConsoleMenuHelper;
using Boss.az.ExceptionNS;
using Boss.az.HelpersNS;
using Boss.az.HumanNS;
using Boss.az.PostNS;
using Boss.az.SubCategoryNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

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
            RetrySurname:
            Console.Write("Enter surname: ");
            surname = Console.ReadLine();
            if (!Helpers.IsValidSurname(surname)) goto RetrySurname;
            Console.Write("Enter age: ");
            age = uint.Parse(Console.ReadLine());
        RetryGender:
            Console.Write("Enter gender(Male or Female): ");
            string temp = Console.ReadLine();
            if (temp.Trim().ToLower() == "male")
                gender = true;
            else if (temp.Trim().ToLower() == "female")
                gender = false;
            else
            {
                Console.WriteLine("Yanlis secim, try again!");
                goto RetryGender;
            }
        RetryEmail:
            Console.Write("Enter email: ");
            email = Console.ReadLine();
            if (!Helpers.IsValidEmail(email)) goto RetryEmail;
            Retry:
            Console.Write("Elave etmek isdeyiniz nomre sayini daxil edin(min 1): ");
            int countPhones = int.Parse(Console.ReadLine());
            if (countPhones == 0)
                goto Retry;
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
        RetryUsername:
            Console.Write("Enter username: ");
            username = Console.ReadLine();
            if (!Helpers.IsValidUsername(username)) goto RetryUsername;
            else if (Workers.Any(w => w.Username == username) || Employers.Any(e => e.Username == username))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Bu adda username artig movcuddur.Yeniden yoxlayin!");
                Console.ResetColor();
                goto RetryUsername;
            }
        RetryPassword:
            Console.Write("Enter password: ");
            password = Console.ReadLine();
            if (!Helpers.IsValidPassword(password)) goto RetryPassword;

            if (isWorker)
            {
                Worker worker = new()
                {
                    Name = name,
                    Surname = surname,
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
                var textJson = JsonSerializer.Serialize(Workers, options);
                File.WriteAllText("Workers.json", textJson);
            }
            else
            {
                Employer employer = new()
                {
                    Name = name,
                    Surname = surname,
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
                var textJson = JsonSerializer.Serialize(Employers, options);
                File.WriteAllText("Employers.json", textJson);
            }
        }

    }
}
