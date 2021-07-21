using Boss.az.DatabaseNS;
using Boss.az.ExceptionNS;
using Boss.az.HumanNS;
using ExtensionNS;
using System;
using System.Linq;

namespace Boss.az.PostNS
{
    class Vacancy : Post
    {
        public string WorkName { get; set; }
        public uint AgeMin { get; set; }
        public uint AgeMax { get; set; }
        public double SalaryMax { get; set; }
        public string Requirements { get; set; }
        public string JobDescription { get; set; }
        public override void Show(Database db)
        {
            Console.WriteLine($"Work name: {WorkName.Trim().ToUpper()}");
            Console.WriteLine($"Work city: {WorkCity.Trim().ToLower().FirstCharToUpper()}");
            base.Show(db);
            if (Salary > SalaryMax)
                Console.WriteLine($"\nSalary: {SalaryMax}-{Salary} AZN");
            else if (Salary < SalaryMax)
                Console.WriteLine($"\nSalary: {Salary}-{SalaryMax} AZN");
            else
                Console.WriteLine($"\nSalary: {Salary} AZN");
            if (AgeMin > AgeMax)
                Console.WriteLine($"Age: {AgeMax}-{AgeMin}");
            else if (AgeMin < AgeMax)
                Console.WriteLine($"Age: {AgeMin}-{AgeMax}");
            else
                Console.WriteLine($"Age: {AgeMin}");
            Console.WriteLine($"\u2193 Job description \u2193\n{JobDescription}");
            Console.WriteLine($"\u2193 Requirements \u2193\n{Requirements}");
            Console.WriteLine($"Published on: {StartTimePost.ToString("dd MMMM yyyy")}");
            Console.WriteLine($"Expired on: {EndTimePost.ToString("dd MMMM yyyy")}");
            Console.WriteLine($"View: {ViewCount}");
        }
        public void ShowShort(Database db,Employer emp)
        {
            Console.WriteLine($"Work name: {WorkName.Trim().ToUpper()}");
            Console.WriteLine($"Category: {db.Categories.ToList().Find(c => c.Id == CategoryId).Name}/{db.SubCategories.ToList().Find(s => s.Id == SubCategoryId).Name}");
            Console.WriteLine($"Salary: {Salary} AZN");
            Console.WriteLine($"Company name: {emp.CompanyName}");
            Console.WriteLine($"View: {ViewCount}");
        }

    }
}
