using Boss.az.HumanNS;
using Boss.az.PostNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.az.Other
{
    class Notification
    {
        private static int ID { get; set; }
        public int NotificationId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string PostGuid { get; set; }
        private DateTime DateTime { get; }
        public Person FromUser { get; set; }
        public Notification(string title,string text, Person fromUser, string postGuid)
        {
            NotificationId = ++ID;
            Text = text;
            DateTime = DateTime.Now;
            FromUser = fromUser;
            PostGuid = postGuid;
            Title = title;
        }
        public Notification(string title,string text, Person fromUser)
        {
            NotificationId = ++ID;
            Text = text;
            DateTime = DateTime.Now;
            FromUser = fromUser;
            Title = title;
        }
        public override string ToString() => $"Notification id: {NotificationId}\n" +
            $"Notification text: {Text}\n" +
            $"Notification from {FromUser.GetType().Name.ToLower()}: {FromUser.Name} {FromUser.Surname}\n" +
            $"Notification datetime {DateTime}\n";
    }
}
