using Boss.az.DatabaseNS;
using Boss.az.ExceptionNS;
using Boss.az.PostNS;
using System;
using System.Collections.Generic;

namespace Boss.az.HumanNS
{
    class Worker : Person
    {
        public List<Cv> Cvs { get; set; }
        public List<Vacancy> IncomingVacancies { get; set; }
        public void ShowAllCv(Database db)
        {
            int i = 0;
            if (Cvs == null)
                Console.WriteLine("Hal hazirda hec bir cv yoxdur!");
            else
            {
                Console.WriteLine($"\t\t\t----------- {Name} {Surname} all Cv -----------");
                this.Show();
                Cvs.ForEach(cv =>
                {
                    Console.WriteLine($"----> Cv {++i}");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Cv guid: {cv.Guid}");
                    Console.ResetColor();
                    cv.Show(db);
                    Console.WriteLine("-----------------------------------------------");
                });
            }
        }
    }
}
//Worker class(derived):User ---> base,Cv's(list)
