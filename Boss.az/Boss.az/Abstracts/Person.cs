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
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public uint Age { get; set; }
        public List<string> Phones { get; set; }
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
                throw new NotificationInfoException("Id must be greater than 0!");
            else
            {
                if (Notifications.Exists(n => n.NotificationId == id))
                    Notifications.Remove(Notifications.Find(n => n.NotificationId == id));
                else
                    Console.WriteLine("There is no notification to match this id!");
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
    }
}
