using Boss.az.DatabaseNS;
using Boss.az.ExceptionNS;
using Boss.az.PostNS;
using ExtensionNS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boss.az.HumanNS
{
    class Employer : Person
    {
        public string CompanyName { get; set; }
        public List<Vacancy> Vacancies { get; set; }
        public List<Cv> IncomingCVs { get; set; }
        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Company name: {CompanyName.Trim().ToUpper()}");
        }
        public void ShowAllVacancy(Database db)
        {
            int i = 0;
            if (Vacancies == null)
                Console.WriteLine("There are currently no vacancies!");
            else
            {
                Console.WriteLine($"\t\t\t----------- {Name} {Surname} all Vacancy -----------");
                Vacancies.ForEach(v =>
                {
                    Console.WriteLine($"----> Vacancy {++i}");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Vacanncy guid: {v.Guid}");
                    Console.ResetColor();
                    v.Show(db);
                    Console.WriteLine("-----------------------------------------------");
                });
            }
        }

    }
}
