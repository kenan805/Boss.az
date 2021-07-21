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
        private DateTime DateTime { get; init; }
        public string FromUser { get; set; }
        public Notification()
        {
            NotificationId = ++ID;
            DateTime = DateTime.Now;
        }
        public override string ToString() => $"Notification id: {NotificationId}\n" +
            $"Notification text: {Text}\n" +
            $"Notification from user: {FromUser}\n" +
            $"Notification datetime {DateTime}\n";
    }
}
