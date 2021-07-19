using System;
using System.Linq;
using Boss.az.ExceptionNS;
using ExtensionNS;

namespace Boss.az.CompanyNS
{
    class Company
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value.Trim().All(c => char.IsDigit(c)))
                    throw new CategoryInfoException("Company namede reqem ola bilmez!");
                else if (value.HasSpecialChar())
                    throw new CategoryInfoException("Company name --> different symbols!");
                else
                    name = value;
            }
        }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


        public void Show()
        {
            Console.WriteLine($"Company name: {Name.Trim().ToLower().FirstCharToUpper()} \n");
            if (StartTime.Year > EndTime.Year)
            {
                Console.WriteLine($"Published on: {EndTime.ToString("dd MMMM yyyy")}");
                Console.WriteLine($"Expired on: {StartTime.ToString("dd MMMM yyyy")}");
            }
            else
            {
                Console.WriteLine($"Published on: {StartTime.ToString("dd MMMM yyyy")}");
                Console.WriteLine($"Expired on: {EndTime.ToString("dd MMMM yyyy")}");
            }
        }

    }
}
