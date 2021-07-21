using System;
using System.Linq;
using Boss.az.ExceptionNS;
using ExtensionNS;

namespace Boss.az.CompanyNS
{
    class Company
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


        public void Show()
        {
            Console.WriteLine($"Company name: {Name.Trim().ToLower().FirstCharToUpper()} ");
            if (StartTime.Year > EndTime.Year)
            {
                Console.WriteLine($"Published on: {EndTime.ToString("dd MMMM yyyy")}");
                Console.WriteLine($"Expired on: {StartTime.ToString("dd MMMM yyyy")}\n");
            }
            else
            {
                Console.WriteLine($"Published on: {StartTime.ToString("dd MMMM yyyy")}");
                Console.WriteLine($"Expired on: {EndTime.ToString("dd MMMM yyyy")}\n");
            }
        }

    }
}
