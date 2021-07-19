using System;
using System.Collections.Generic;
using Boss.az.ExceptionNS;
using System.Linq;
using Boss.az.UniqueIdNS;
using ExtensionNS;
using Boss.az.Other;
using System.Net.Mail;
using System.Net;

namespace Boss.az.HumanNS
{
    abstract class Person : BaseId
    {
        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                if (value.Length < 3) throw new UserInfoException("User length 3den kicik olmamalidir!");
                else if (value.Trim().Contains(' '))
                    throw new UserInfoException("User namede yazilis sehvdir!");
                else username = value;
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                if (value.Length < 5) throw new UserInfoException("User length 5den kicik olmamalidir!");
                else if (value.Trim().Contains(' '))
                    throw new UserInfoException("User namede yazilis sehvdir!");
                else password = value;
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (!value.Trim().All(c => char.IsLetter(c)))
                    throw new UserInfoException("User namede ancag herf olmalidir!");
                else if (value.Trim().Contains(' '))
                    throw new UserInfoException("User namede yazilis sehvdir!");
                else if (value.HasSpecialChar())
                    throw new UserInfoException("User name --> Different symbols!");
                else
                    name = value;
            }
        }

        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                if (!value.Trim().All(c => char.IsLetter(c)))
                    throw new UserInfoException("User surnamede ancag herf olmalidir!");
                else if (value.Trim().Contains(' '))
                    throw new UserInfoException("User surnamede yazilis sehvdir!");
                else if (value.HasSpecialChar())
                    throw new UserInfoException("User surname --> Different symbols!");
                else
                    surname = value;
            }
        }
        public bool Gender { get; set; }

        private List<string> phones;
        public List<string> Phones
        {
            get { return phones; }
            set
            {
                foreach (var phone in value)
                {
                    if (!phone.Trim().StartsWith("0"))
                        throw new("User telfon nomresi 0 ile baslamalidir!");
                    else if (phone.Trim().Replace(" ", String.Empty).Length != 10)
                        throw new PhoneFormatException("User telfon nomresi 10 reqemli olmalidir!");
                    else if (!phone.Trim().Replace(" ", String.Empty).All(c => char.IsDigit(c)))
                        throw new PhoneFormatException("User telefonda ancag nomre yazilmalidir!");
                    else if (phone.HasSpecialChar())
                        throw new PhoneFormatException("User telefon --> Different symbols!");
                }
                phones = value;
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (!(value.Contains('@') || value.Contains('.')))
                    throw new EmailFormatException("User email yazilisi sehvdir!");
                email = value;
            }
        }

        public uint Age { get; set; }
        public List<Notification> Notifications { get; set; }
        public virtual void Show()
        {
            int i = 0;
            Console.WriteLine($"Name: {Name.Trim().ToLower().FirstCharToUpper()}");
            Console.WriteLine($"Surname: {Surname.Trim().ToLower().FirstCharToUpper()}");
            if (Gender == true) Console.WriteLine("Gender: Male");
            else Console.WriteLine("Gender: Female");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Email: {Email.Trim()}");
            Phones.ForEach(p =>
            {
                p = p.Trim().Replace(" ", String.Empty);
                Console.WriteLine($"{++i})Phone: ({p[0]}{p[1]}{p[2]}) {p[3]}{p[4]}{p[5]}-{p[6]}{p[7]}-{p[8]}{p[9]}");
            });
        }
        public void ShowAllNotf()
        {
            Console.WriteLine("------- ALL NOTIFICATIONS -------");
            Console.WriteLine($"Notifications count: {Notifications.Count}");
            Console.WriteLine("-------------------------------------------");
            Notifications.ForEach(n =>
            {
                Console.WriteLine(n);
                Console.WriteLine("----------------------------------------");
            });
        }
        public void DeleteNotfById(int id)
        {
            if (id <= 0)
                throw new NotificationInfoException("Id o-dan boyuk olmalidir!");
            else
            {
                if (Notifications.Exists(n => n.NotificationId == id))
                    Notifications.Remove(Notifications.Find(n => n.NotificationId == id));
                else
                    Console.WriteLine("Bu id ye uygun notf yoxdur!");
            }
        }

        public void SendNotf(in Employer employer, in Worker worker, in Notification notification)
        {
            SmtpClient client = new("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(employer.Email, employer.Password);
            try
            {
                client.Send(employer.Email, employer.Email, "New notification!", $"{notification.Text}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //public void DeleteAllNotf()
        //{
        //    if (Notifications != null)
        //    {
        //        List<Notification> newNotifications = new();
        //        Notifications = newNotifications;
        //    }
        //    else
        //        throw new NotificationInfoException("Notifications null!");
        //}


    }
}
//User class (Base) ---> Id,username,password,name,surname,fatherName,cins,seher,phones,email,age +
