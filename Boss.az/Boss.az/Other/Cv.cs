using System;
using System.Collections.Generic;
using System.Linq;
using Boss.az.CompanyNS;
using Boss.az.DatabaseNS;
using Boss.az.ExceptionNS;
using Boss.az.HumanNS;
using ExtensionNS;

namespace Boss.az.PostNS
{
    class Cv : Post
    {
        public string Specialty { get; set; }
        public string About { get; set; }
        public List<string> Skills { get; set; }
        public List<Company> WorksCompaniesFor { get; set; }
        public override void Show(Database db)
        {
            base.Show(db);
            Console.WriteLine($"\nSpecialty: {Specialty.Trim().ToLower().FirstCharToUpper()}");
            Console.Write("Skills: ");
            if (Skills == null) Console.WriteLine("No skills!");
            else Skills.ForEach(s => Console.Write($"{s.Trim().ToLower().FirstCharToUpper()} "));
            Console.WriteLine("\n\u2193 Works Company \u2193");
            if (WorksCompaniesFor == null) Console.WriteLine("There is no place he wants before!");
            else WorksCompaniesFor.ForEach(w =>
            {
                w.Show();
                Console.WriteLine("------------------------");
            });
            Console.WriteLine($"\u2193 About \u2193\n{About}");
            Console.WriteLine($"Salary: {Salary} AZN");
            Console.WriteLine($"Published on: {StartTimePost.ToString("dd MMMM yyyy")}");
            Console.WriteLine($"Expired on: {EndTimePost.ToString("dd MMMM yyyy")}");
            Console.WriteLine($"View: {ViewCount}");
        }
        public void ShowShort(Database db, Worker worker)
        {
            Console.WriteLine($"Category: {db.Categories.ToList().Find(c => c.Id == CategoryId).Name}/{db.SubCategories.ToList().Find(s => s.Id == SubCategoryId).Name}");
            Console.WriteLine($"Salary: {Salary} AZN");
            Console.WriteLine(worker.Name + " " + worker.Surname);
            Console.WriteLine($"View: {ViewCount}");
        }
    }
}
