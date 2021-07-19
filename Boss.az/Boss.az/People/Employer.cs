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
        private string companyName;
        public string CompanyName
        {
            get { return companyName; }
            set
            {
                if (value.Trim().All(c => char.IsDigit(c)))
                    throw new EmployerInfoException("Company namede reqem ola bilmez!");
                else if (value.HasSpecialChar())
                    throw new UserInfoException("Company name --> Different symbols!");
                else
                    companyName = value;
            }
        }
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
                Console.WriteLine("Hal hazirda hec bir vacancy yoxdur!");
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
//Employer class(derived):User --->base,sirketName,Vacancies(list)
