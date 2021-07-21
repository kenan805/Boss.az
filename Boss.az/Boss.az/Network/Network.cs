using Boss.az.HumanNS;
using Boss.az.Other;
using System;
using System.Net;
using System.Net.Mail;

namespace Boss.az.NetworkNS
{
    static class Network 
    {
        public static void SendNotf(in Person sender, in Person receives, in Notification notification)
        {
            SmtpClient client = new("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(sender.Email, sender.Password);
            try
            {
                client.Send(sender.Email, receives.Email, "New notification!", $"{notification.Text}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
