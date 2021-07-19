using Boss.az.CompanyNS;
using Boss.az.ConsoleMenuHelper;
using Boss.az.DatabaseNS;
using Boss.az.HelpersNS;
using Boss.az.HumanNS;
using Boss.az.PostNS;
using Boss.az.ExceptionNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ExtensionNS;
using Boss.az.Other;
using Boss.az.NetworkNS;
using Boss.az.Json;

namespace Boss.az.Menu_side
{
    static class WorkerSide
    {
        public static void WorkerMenu(Database db, Worker worker)
        {
            if (worker == null) throw new WorkerInfoException("Worker is null!");
            else if (db == null) throw new DatabaseInfoException("Database is null!");
            else
            {
            Workermenu:
                int choise = ConsoleHelper.MultipleChoice(5, 1, 1, true, "Vacancies", "CV section", "Notifications", "Back to");
                switch (choise)
                {
                    case 0:
                        {
                        SubWorkerMenu:
                            choise = ConsoleHelper.MultipleChoice(5, 1, 1, true, "Search", "All vacancies", "Back to");
                            Console.Clear();
                            if (choise == 0)
                            {
                                if (db.Employers == null)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("There are currently no employers and no vacancies!");
                                    Console.Write("Click any to go back...");
                                    Console.ResetColor();
                                    Console.ReadLine();
                                    goto SubWorkerMenu;
                                }
                                else
                                {    
                                    uint filterCatId;
                                    var array = new String[db.Categories.ToList().Count];
                                    for (int i = 0; i < db.Categories.ToList().Count; i++)
                                        array[i] = db.Categories.ToArray()[i].Name.FirstCharToUpper();
                                    Console.Write("Category: ");
                                    choise = ConsoleHelper.MultipleChoice(5, 1, 3, false, array);
                                    filterCatId = db.Categories.ToList().Find(c => c.Name == array[choise]).Id;
                                    array = new String[db.SubCategories.Count(s => s.CategoryId == filterCatId)];
                                    for (int i = 0; i < db.SubCategories.Count(s => s.CategoryId == filterCatId); i++)
                                        array[i] = db.SubCategories.ToList().FindAll(s => s.CategoryId == filterCatId).ToArray()[i].Name;
                                    Console.Clear();
                                    Console.Write("Subcategory: ");
                                    choise = ConsoleHelper.MultipleChoice(5, 1, 1, false, array);
                                    uint filterSubCatId = db.SubCategories.ToList().Find(s => s.Name == array[choise]).Id;
                                    List<Vacancy> vacancies = new();
                                    List<string> arr = new();
                                    array = Array.Empty<string>();
                                    for (int i = 0; i < db.Employers.Count; i++)
                                    {
                                        for (int j = 0; j < db.Employers[i].Vacancies.Count; j++)
                                        {
                                            arr.Add(db.Employers[i].Vacancies[j].WorkCity.Trim().ToLower().FirstCharToUpper());
                                            vacancies.Add(db.Employers[i].Vacancies[j]);
                                        }
                                    }
                                    array = arr.ToArray();
                                    array = arr.Distinct<string>().ToArray();
                                    array.ToList().TrimExcess();
                                    Console.Clear();
                                    Console.Write("City: ");
                                    choise = ConsoleHelper.MultipleChoice(5, 1, 3, false, array);
                                    string filterCity = vacancies.Find(v => v.WorkCity == array[choise]).WorkCity;
                                    Console.Clear();
                                    Console.Write("Enter min salary: ");
                                    double filterMinSalary = double.Parse(Console.ReadLine());
                                    Console.Clear();
                                    Console.WriteLine("Choise experience");
                                    string[] experienceChoiseArray = { "No", "Less than 1 year", "From 1 to 3 years", "From 3 to 5 years", "More than 5 years" };
                                    choise = ConsoleHelper.MultipleChoice(26, 1, 1, false, experienceChoiseArray);
                                    string filterExperience = choise switch
                                    {
                                        0 => experienceChoiseArray[choise],
                                        1 => experienceChoiseArray[choise],
                                        2 => experienceChoiseArray[choise],
                                        3 => experienceChoiseArray[choise],
                                        4 => experienceChoiseArray[choise],
                                        _ => "None"
                                    };
                                    Console.Clear();
                                    if (vacancies.Exists(v => v.CategoryId == filterCatId && v.SubCategoryId == filterSubCatId && v.WorkCity.Trim().ToLower() == filterCity.Trim()
                                        .ToLower() && v.Salary >= filterMinSalary && v.Experience.Trim().ToLower() == filterExperience.Trim().ToLower()))
                                    {
                                        vacancies.FindAll(v => v.CategoryId == filterCatId
                                                               && v.SubCategoryId == filterSubCatId
                                                               && v.WorkCity.Trim().ToLower() == filterCity.Trim().ToLower()
                                                               && v.Salary >= filterMinSalary
                                                               && v.Experience.Trim().ToLower() == filterExperience.Trim().ToLower()).ForEach(v =>
                                                           {
                                                               Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                               Console.WriteLine($"Vacancy guid: {v.Guid}");
                                                               Console.ResetColor();
                                                               ++v.ViewCount;
                                                               db.Employers.Find(e => e.Vacancies.Exists(v => v.Guid == v.Guid)).Show();
                                                               Console.WriteLine("-------- Vacancy info --------");
                                                               v.Show(db);
                                                               Console.WriteLine("----------------------------");
                                                           });
                                        Console.BackgroundColor = ConsoleColor.Gray;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.WriteLine("(Geriye qayitmaq isteyirsinize >> back << yazin)");
                                        Console.ResetColor();
                                        Console.Write("Enter follow guid: ");
                                        string guid = Console.ReadLine();
                                        if (guid.Trim().ToLower() == "back") goto SubWorkerMenu;
                                        else if (db.Employers.ToList().Exists(e => e.Vacancies.Exists(v => v.Guid.ToString() == guid)))
                                        {
                                            Console.Clear();
                                            Employer emp = new();
                                            emp = db.Employers.ToList().Find(e => e.Vacancies.Exists(v => v.Guid.ToString() == guid));
                                            emp.Notifications = new();
                                            emp.IncomingCVs = new();
                                            worker.ShowAllCv(db);
                                            if (worker.Cvs != null)
                                            {
                                                Console.Write("Employere gondermek istediyiniz cv-in guid-ni daxil edin: ");
                                                guid = Console.ReadLine();
                                                if (worker.Cvs.Exists(c => c.Guid.ToString() == guid))
                                                {
                                                    Notification notf = new("New CV!", "Sizin teklif etdiyiniz isde islemek isteyerdim.", worker, guid);
                                                    Console.WriteLine($"Cv-niz {emp.Name} {emp.Surname}-e gonderildi!");
                                                    emp.IncomingCVs.Add(worker.Cvs.Find(c => c.Guid.ToString() == guid));
                                                    emp.Notifications.Add(notf);
                                                    Thread.Sleep(2000);
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                    Console.WriteLine("Daxil etdiyiniz guide uygun cv tapilmadi!");
                                                    Console.ResetColor();
                                                    Thread.Sleep(1000);
                                                    Console.ReadLine();
                                                }
                                            }
                                        }
                                        else Console.WriteLine("Bu guide uygun vacansiya tapilmadi!");
                                    }
                                    else
                                        Console.WriteLine("Nothing was found!");
                                    FileProcessJson.JsonFileWriteAllTextToDatabase("Database.json", db);
                                }
                            }
                            else if (choise == 1)
                            {
                                //TryView:
                                if (db.Employers == null)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Hal hazirda hec bir employer ve vakansiya yoxdur!");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    foreach (var employer in db.Employers)
                                    {
                                        if (employer.Vacancies == null)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Hal hazirda hec bir vakansiya yoxdur!");
                                            Console.ReadLine();
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("\t\t-------------- Ise goturenlerin siyahisi -------------- ");
                                            db.Employers.ForEach(e =>
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.WriteLine($"Guid: {e.Guid}");
                                                Console.ResetColor();
                                                e.Show();
                                                Console.WriteLine();
                                            });
                                        RetryGuid:
                                            Console.Write("Vakansiyasina-ne baxmaq istediyiniz employerin guidini daxil edin: ");
                                            string guid = Console.ReadLine();
                                            if (string.IsNullOrEmpty(guid))
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("Empty!!!");
                                                Console.ResetColor();
                                                goto RetryGuid;
                                            }
                                            Console.Clear();
                                            Employer tempEmp = db.Employers.Find(w => w.Guid.ToString() == guid);
                                        TryView:
                                            foreach (var vacancy in tempEmp.Vacancies)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("\n----------------------------------------------");
                                                ++vacancy.ViewCount;
                                                vacancy.ShowShort(db, tempEmp);
                                                choise = ConsoleHelper.MultipleChoice(0, 0, 3, false, "Read more", "Next vacancy", "Back to menu");
                                                switch (choise)
                                                {
                                                    case 0:
                                                        {
                                                            ++vacancy.ViewCount;
                                                            Console.Clear();
                                                            Console.WriteLine("\n----------------------------------------------------");
                                                            vacancy.Show(db);
                                                            choise = ConsoleHelper.MultipleChoice(1, 0, 2, false, "Follow", "Back to");
                                                            if (choise == 0)
                                                            {
                                                                Console.Clear();
                                                                tempEmp.Notifications = new();
                                                                tempEmp.IncomingCVs = new();
                                                                worker.ShowAllCv(db);
                                                                if (worker.Cvs != null)
                                                                {
                                                                    Console.Write("Employere gondermek istediyiniz cv-nin guid-ni daxil edin: ");
                                                                    guid = Console.ReadLine();
                                                                    if (worker.Cvs.Exists(c => c.Guid.ToString() == guid))
                                                                    {
                                                                        Notification notf = new("New CV!", "Sizin teklif etdiyiniz isde islemek isteyerdim.", worker, guid);
                                                                        Console.WriteLine($"CV-niz {tempEmp.Name} {tempEmp.Surname}-e gonderildi!");
                                                                        tempEmp.IncomingCVs.Add(worker.Cvs.Find(c => c.Guid.ToString() == guid));
                                                                        tempEmp.Notifications.Add(notf);
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.Clear();
                                                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                        Console.WriteLine("Daxil etdiyiniz guide uygun vakansiya tapilmadi!");
                                                                        Console.ResetColor();
                                                                        Thread.Sleep(1000);
                                                                        continue;
                                                                    }
                                                                }
                                                            }
                                                            else continue;
                                                            break;
                                                        }
                                                    case 1: continue;
                                                    case 2: goto Workermenu;
                                                }
                                            }
                                            Console.Clear();
                                            Console.WriteLine(">>> Butun cvlere baxdiniz <<<");
                                            choise = ConsoleHelper.MultipleChoice(1, 1, 1, false, "Yeniden baxmaq", "Back to");
                                            switch (choise)
                                            {
                                                case 0: goto TryView;
                                                case 1: goto Workermenu;
                                            }
                                            Console.ReadLine();
                                            //foreach (var vacancy in employer.Vacancies)
                                            //{
                                            //    Console.Clear();
                                            //    Console.WriteLine("\n----------------------------------------------------");
                                            //    ++vacancy.ViewCount;
                                            //    vacancy.ShowShort(db, employer);
                                            //    choise = ConsoleHelper.MultipleChoice(0, 0, 3, false, "Read more", "Next vacancy", "Back to menu");
                                            //    if (choise == 1) continue;
                                            //    else if (choise == 0)
                                            //    {
                                            //        ++vacancy.ViewCount;
                                            //        Console.Clear();
                                            //        Console.WriteLine("\n----------------------------------------------------");
                                            //        vacancy.Show(db);
                                            //        choise = ConsoleHelper.MultipleChoice(1, 0, 2, false, "Follow", "Back to");
                                            //        if (choise == 0)
                                            //        {
                                            //            Console.Clear();
                                            //            employer.Notifications = new();
                                            //            employer.IncomingCVs = new();
                                            //            worker.ShowAllCv(db);
                                            //            if (worker.Cvs != null)
                                            //            {
                                            //                Console.Write("Employere gondermek istediyiniz cv-in guid-ni daxil edin: ");
                                            //                string guid = Console.ReadLine();
                                            //                if (worker.Cvs.Exists(c => c.Guid.ToString() == guid))
                                            //                {
                                            //                    Notification notf = new("New CV!", "Sizin teklif etdiyiniz isde islemek isteyerdim.", worker, guid);
                                            //                    Console.WriteLine($"Cv-niz {employer.Name} {employer.Surname}-e gonderildi!");
                                            //                    employer.IncomingCVs.Add(worker.Cvs.Find(c => c.Guid.ToString() == guid));
                                            //                    employer.Notifications.Add(notf);
                                            //                }
                                            //                else
                                            //                {
                                            //                    Console.Clear();
                                            //                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                            //                    Console.WriteLine("Daxil etdiyiniz guide uygun cv tapilmadi!");
                                            //                    Console.ResetColor();
                                            //                    Thread.Sleep(1000);
                                            //                    continue;
                                            //                }
                                            //            }
                                            //        }
                                            //        else continue;
                                            //    }
                                            //    else goto Workermenu;
                                            //    Console.ReadLine();
                                            //}
                                        }
                                    }
                                    //Console.Clear();
                                    //Console.WriteLine(">>> Butun vakansiyalara baxdiniz <<<");
                                    //choise = ConsoleHelper.MultipleChoice(1, 1, 1, false, "Yeniden baxmaq", "Back to");
                                    //switch (choise)
                                    //{
                                    //    case 0: goto TryView;
                                    //    case 1: goto Workermenu;
                                    //}
                                }
                            }
                            else goto Workermenu;
                            break;
                        }
                    case 1:
                        {
                            while (true)
                            {
                                choise = ConsoleHelper.MultipleChoice(5, 1, 1, true, "All my CVs", "Add CV", "Update CV", "Delete CV", "Back to");
                                switch (choise)
                                {
                                    case 0:
                                        {
                                            Console.Clear();
                                            worker.ShowAllCv(db);
                                            Console.ReadLine();
                                            break;
                                        }
                                    case 1:
                                        {
                                            Console.Clear();
                                            worker.Cvs.Add(GetIncludeCvInfo(db));
                                            FileProcessJson.JsonFileWriteAllTextToDatabase("Database.json", db);
                                            Console.WriteLine("Cv add olunur...");
                                            Thread.Sleep(1500);
                                            Console.WriteLine("Cv ugurla elave olundu.");
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.Clear();
                                            if (worker.Cvs == null)
                                            {
                                                Console.WriteLine("Hal hazirda hec bir cv yoxdur!");
                                                Thread.Sleep(1500);
                                            }
                                            else
                                            {
                                                int i = 0;
                                                worker.Cvs.ForEach(cv =>
                                                {
                                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                    Console.WriteLine($"{++i}) Cv guid: {cv.Guid}");
                                                    Console.ResetColor();
                                                    cv.Show(db);
                                                });
                                                Console.Write("Update etmek istediyiniz cv-nin guidini daxil edin: ");
                                                string guid = Console.ReadLine();
                                                if (worker.Cvs.Exists(g => g.Guid.ToString() == guid))
                                                {
                                                    Cv tempCv = new();
                                                    tempCv = worker.Cvs.Find(g => g.Guid.ToString() == guid);
                                                    Console.Clear();
                                                UpdateRetry:
                                                    int choiseUpdate = ConsoleHelper.MultipleChoice(5, 2, 1, true, "Category", "City", "Edugation Degree", "Experience", "Languages", "Specialty", "Skills", "Works company", "Salary", "About", "Back to");
                                                    Console.Clear();
                                                    switch (choiseUpdate)
                                                    {
                                                        case 0:
                                                            {
                                                                Console.WriteLine("Choise new category and subcategory: ");
                                                                String[] array = new String[db.Categories.ToList().Count];
                                                                for (int k = 0; k < db.Categories.ToList().Count; k++)
                                                                    array[k] = db.Categories.ToArray()[k].Name;
                                                                choise = ConsoleHelper.MultipleChoice(5, 1, 3, true, array);
                                                                uint filterCatId = db.Categories.ToList().Find(c => c.Name == array[choise]).Id;
                                                                tempCv.CategoryId = filterCatId;
                                                                array = new String[db.SubCategories.Count(s => s.CategoryId == filterCatId)];
                                                                for (int j = 0; j < db.SubCategories.Count(s => s.CategoryId == filterCatId); j++)
                                                                    array[j] = db.SubCategories.ToList().FindAll(s => s.CategoryId == filterCatId).ToArray()[j].Name;
                                                                choise = ConsoleHelper.MultipleChoice(5, 1, 1, true, array);
                                                                uint filterSubCatId = db.SubCategories.ToList().Find(s => s.Name == array[choise]).Id;
                                                                tempCv.SubCategoryId = filterSubCatId;
                                                                Console.Clear();
                                                                if (IsReplyUpdateCv()) goto UpdateRetry;
                                                                break;
                                                            }
                                                        case 1:
                                                            {
                                                                Console.Write("Enter new city: ");
                                                                tempCv.WorkCity = Console.ReadLine();
                                                                if (IsReplyUpdateCv()) goto UpdateRetry;
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                Console.WriteLine("Choise new education degree");
                                                                string[] educationChoicesArray = { "Science Degree", "Higher", "Incomplete Higher", "Secondary" };
                                                                int choice = ConsoleHelper.MultipleChoice(30, 0, 1, false, educationChoicesArray);
                                                                tempCv.EducationDegree = choice switch
                                                                {
                                                                    0 => educationChoicesArray[choice],
                                                                    1 => educationChoicesArray[choice],
                                                                    2 => educationChoicesArray[choice],
                                                                    3 => educationChoicesArray[choice],
                                                                    _ => "None"
                                                                };
                                                                Console.WriteLine();
                                                                if (IsReplyUpdateCv()) goto UpdateRetry;
                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                Console.WriteLine("Choise new experience");
                                                                string[] experienceChoiseArray = { "No", "Less than 1 year", "From 1 to 3 years", "From 3 to 5 years", "More than 5 years" };
                                                                int choice = ConsoleHelper.MultipleChoice(24, 0, 1, false, experienceChoiseArray);
                                                                tempCv.Experience = choice switch
                                                                {
                                                                    0 => experienceChoiseArray[choice],
                                                                    1 => experienceChoiseArray[choice],
                                                                    2 => experienceChoiseArray[choice],
                                                                    3 => experienceChoiseArray[choice],
                                                                    4 => experienceChoiseArray[choice],
                                                                    _ => "None"
                                                                };
                                                                Console.WriteLine();
                                                                if (IsReplyUpdateCv()) goto UpdateRetry;
                                                                break;
                                                            }
                                                        case 4:
                                                            {
                                                                List<string> languages = new();
                                                                Console.Write("Enter new language count: ");
                                                                int languageCount = int.Parse(Console.ReadLine());
                                                                if (languageCount == 0) tempCv.Languages = null;
                                                                string language;
                                                                for (int c = 0; c < languageCount; c++)
                                                                {
                                                                    Console.Write("Enter language: ");
                                                                    language = Console.ReadLine();
                                                                    languages.Add(language);
                                                                }
                                                                tempCv.Languages = languages;
                                                                if (IsReplyUpdateCv()) goto UpdateRetry;
                                                                break;
                                                            }
                                                        case 5:
                                                            {
                                                                Console.Write("Enter new specialty: ");
                                                                tempCv.Specialty = Console.ReadLine();
                                                                if (IsReplyUpdateCv()) goto UpdateRetry;
                                                                break;
                                                            }
                                                        case 6:
                                                            {
                                                                List<string> skills = new();
                                                                Console.Write("Enter new skill count: ");
                                                                int skillCount = int.Parse(Console.ReadLine());
                                                                if (skillCount == 0) tempCv.Skills = null;
                                                                string skill;
                                                                for (int c = 0; c < skillCount; c++)
                                                                {
                                                                    Console.Write("Enter skill: ");
                                                                    skill = Console.ReadLine();
                                                                    skills.Add(skill);
                                                                }
                                                                tempCv.Skills = skills;
                                                                if (IsReplyUpdateCv()) goto UpdateRetry;
                                                                break;
                                                            }
                                                        case 7:
                                                            {
                                                                List<Company> worksCompaniesFor = new();
                                                                Console.Write("Enter works company count: ");
                                                                int worksCompanyCount = int.Parse(Console.ReadLine());
                                                                if (worksCompanyCount == 0) tempCv.WorksCompaniesFor = null;
                                                                for (int c = 0; c < worksCompanyCount; c++)
                                                                {
                                                                    Company company = new();
                                                                    Console.Write("Enter works company name: ");
                                                                    company.Name = Console.ReadLine();
                                                                    int year, month, day;
                                                                Year1:
                                                                    Console.Write("Enter works company start datetime year: ");
                                                                    year = int.Parse(Console.ReadLine());
                                                                    if (!Helpers.IsValidDateYear(year)) goto Year1;
                                                                    Month1:
                                                                    Console.Write("Enter month: ");
                                                                    month = int.Parse(Console.ReadLine());
                                                                    if (!Helpers.IsValidDateMonth(month)) goto Month1;
                                                                    Day1:
                                                                    Console.Write("Enter day: ");
                                                                    day = int.Parse(Console.ReadLine());
                                                                    if (!Helpers.IsValidDateDay(year, month, day))
                                                                        goto Day1;
                                                                    company.StartTime = new DateTime(year, month, day);

                                                                Year2:
                                                                    Console.Write("Enter works company end datetime year: ");
                                                                    year = int.Parse(Console.ReadLine());
                                                                    if (!Helpers.IsValidDateYear(year)) goto Year2;
                                                                    Month2:
                                                                    Console.Write("Enter month: ");
                                                                    month = int.Parse(Console.ReadLine());
                                                                    if (!Helpers.IsValidDateMonth(month)) goto Month2;
                                                                    Day2:
                                                                    Console.Write("Enter day: ");
                                                                    day = int.Parse(Console.ReadLine());
                                                                    if (!Helpers.IsValidDateDay(year, month, day)) goto Day2;
                                                                    company.EndTime = new DateTime(year, month, day);

                                                                    worksCompaniesFor.Add(company);
                                                                }
                                                                tempCv.WorksCompaniesFor = worksCompaniesFor;
                                                                if (IsReplyUpdateCv()) goto UpdateRetry;
                                                                break;
                                                            }
                                                        case 8:
                                                            {
                                                                Console.Write("Enter new salary: ");
                                                                tempCv.Salary = double.Parse(Console.ReadLine());
                                                                if (IsReplyUpdateCv()) goto UpdateRetry;
                                                                break;
                                                            }
                                                        case 9:
                                                            {
                                                                Console.Write("Enter new about: ");
                                                                tempCv.About = Console.ReadLine();
                                                                if (IsReplyUpdateCv()) goto UpdateRetry;
                                                                break;
                                                            }
                                                        case 10: continue;
                                                    }
                                                    FileProcessJson.JsonFileWriteAllTextToDatabase("Database.json", db);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Daxil etdiyiniz idye uygun cv yoxdur!");
                                                    Thread.Sleep(1300);
                                                }
                                            }
                                            break;
                                        }
                                    case 3:
                                        {
                                            Console.Clear();
                                            if (worker.Cvs == null)
                                            {
                                                Console.WriteLine("Hal hazirda hec bir cv yoxdur!");
                                                Thread.Sleep(1500);
                                            }
                                            else
                                            {
                                                int i = 0;
                                                worker.Cvs.ForEach(cv =>
                                                {
                                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                    Console.WriteLine($"{++i}) Cv guid: {cv.Guid}");
                                                    Console.ResetColor();
                                                    cv.Show(db);
                                                });
                                                Console.Write("Silmek istediyiniz cv-nin guidini daxil edin: ");
                                                string guid = Console.ReadLine();
                                                if (worker.Cvs.Exists(g => g.Guid.ToString() == guid))
                                                {
                                                    Console.WriteLine("Secdiyiniz cv silinir...");
                                                    Thread.Sleep(1300);
                                                    Console.WriteLine("Cv ugurla silindi.");
                                                    worker.Cvs.Remove(worker.Cvs.Find(g => g.Guid.ToString() == guid));
                                                    FileProcessJson.JsonFileWriteAllTextToDatabase("Database.json", db);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Daxil etdiyiniz idye uygun cv yoxdur!");
                                                    Thread.Sleep(1300);
                                                }
                                            }
                                            break;
                                        }
                                    case 4:
                                        goto Workermenu;
                                }
                            }
                        }
                    case 2:
                        {
                            Console.Clear();
                            if (worker.Notifications == null)
                            {
                                Console.WriteLine("Hal hazirda aktiv bildiris yoxdur!");
                                Console.ReadLine();
                            }
                            else
                            {
                                worker.ShowAllNotf();
                                Console.Write("Baxmaq istediyiniz bildirsin id-ni daxil edin: ");
                                int id = int.Parse(Console.ReadLine());
                                if (worker.Notifications.Exists(n => n.NotificationId == id))
                                {
                                    Console.Clear();
                                    Console.WriteLine("\n-------------------------------------");
                                    string guid = worker.Notifications.Find(n => n.NotificationId == id).PostGuid;
                                    Employer emp = new();
                                    emp = db.Employers.Find(w => w.Vacancies.Exists(c => c.Guid.ToString() == guid));
                                    emp.Notifications = new List<Notification>();
                                    emp.Show();
                                    Console.WriteLine("--------- Vacancy info ---------");
                                    worker.IncomingVacancies.Find(i => i.Guid.ToString() == guid).Show(db);
                                    choise = ConsoleHelper.MultipleChoice(1, 0, 2, false, "Isi qebul etmek", "Redd etmek");
                                    switch (choise)
                                    {
                                        case 0:
                                            {
                                                Notification notf = new("New notf by worker", $"Sizin isi qebul edirem!", worker);
                                                emp.Notifications.Add(notf);
                                                emp.Vacancies.Remove(emp.Vacancies.Find(c => c.Guid.ToString() == guid));
                                                Console.Clear();
                                                Console.WriteLine($"Bildiris {emp.Name}e gonderilir...");
                                                Network.SendNotf(worker, emp, notf);
                                                break;
                                            }
                                        case 1:
                                            {
                                                Notification notf = new("New notf by worker", "Teklifinize gore tesekkurler, ama bu is mene uygun deyil.Ugurlar size!", emp);
                                                Console.Clear();
                                                Console.WriteLine($"Bildiris {emp.Name}e gonderilir...");
                                                emp.Notifications.Add(notf);
                                                Network.SendNotf(worker, emp, notf);
                                                break;
                                            }
                                    }
                                    worker.DeleteNotfById(id);
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Daxil etdiyiniz id-ye uygun notf tapilmadi!");
                                    Console.ResetColor();
                                    Thread.Sleep(1000);
                                    Console.ReadLine();
                                }
                            }
                            break;
                        }
                    case 3:
                        ProgramManagment.Start(db);
                        break;
                }
            }
        }
        private static Cv GetIncludeCvInfo(Database db)
        {
            List<string> skills = new();
            List<string> languages = new();
            List<Company> worksCompaniesFor = new();
            Cv cv = new();
            String[] array = new String[db.Categories.ToList().Count];
            for (int i = 0; i < db.Categories.ToList().Count; i++)
                array[i] = db.Categories.ToArray()[i].Name;
            int choise = ConsoleHelper.MultipleChoice(5, 1, 3, true, array);
            uint filterCatId = db.Categories.ToList().Find(c => c.Name == array[choise]).Id;
            cv.CategoryId = filterCatId;
            array = new String[db.SubCategories.Count(s => s.CategoryId == filterCatId)];
            for (int i = 0; i < db.SubCategories.Count(s => s.CategoryId == filterCatId); i++)
            {
                array[i] = db.SubCategories.ToList().FindAll(s => s.CategoryId == filterCatId).ToArray()[i].Name;
            }
            choise = ConsoleHelper.MultipleChoice(5, 1, 1, true, array);
            uint filterSubCatId = db.SubCategories.ToList().Find(s => s.Name == array[choise]).Id;
            cv.SubCategoryId = filterSubCatId;
            Console.Clear();
            Console.WriteLine("Choise education degree");
            string[] educationChoicesArray = { "Science Degree", "Higher", "Incomplete Higher", "Secondary" };
            int choice = ConsoleHelper.MultipleChoice(25, 1, 1, false, educationChoicesArray);
            cv.EducationDegree = choice switch
            {
                0 => educationChoicesArray[choice],
                1 => educationChoicesArray[choice],
                2 => educationChoicesArray[choice],
                3 => educationChoicesArray[choice],
                _ => "None"
            };
            Console.Write("\nEnter city: ");
            cv.WorkCity = Console.ReadLine();
            Console.WriteLine("\n\nChoise experience");
            string[] experienceChoiseArray = { "Less than 1 year", "From 1 to 3 years", "From 3 to 5 years", "More than 5 years" };
            choice = ConsoleHelper.MultipleChoice(26, 6, 1, false, experienceChoiseArray);
            cv.Experience = choice switch
            {
                0 => experienceChoiseArray[choice],
                1 => experienceChoiseArray[choice],
                2 => experienceChoiseArray[choice],
                3 => experienceChoiseArray[choice],
                _ => "None"
            };
            Console.Write("\nEnter specialty: ");
            cv.Specialty = Console.ReadLine();
            Console.Write("Enter skill count: ");
            int skillCount = int.Parse(Console.ReadLine());
            if (skillCount == 0) cv.Skills = null;
            string skill;
            for (int i = 0; i < skillCount; i++)
            {
                Console.Write("Enter skill: ");
                skill = Console.ReadLine();
                skills.Add(skill);
            }
            cv.Skills = skills;
            Console.Write("Enter language count: ");
            int languageCount = int.Parse(Console.ReadLine());
            if (languageCount == 0) cv.Languages = null;
            string language;
            for (int i = 0; i < languageCount; i++)
            {
                Console.Write("Enter language: ");
                language = Console.ReadLine();
                languages.Add(language);
            }
            cv.Languages = languages;
            Console.Write("Enter works company count: ");
            int worksCompanyCount = int.Parse(Console.ReadLine());
            if (worksCompanyCount == 0) cv.WorksCompaniesFor = null;
            for (int i = 0; i < worksCompanyCount; i++)
            {
                Company company = new();
                Console.Write("Enter works company name: ");
                company.Name = Console.ReadLine();
                int year, month, day;
            Year1:
                Console.Write("Enter works company start datetime year: ");
                year = int.Parse(Console.ReadLine());
                if (!Helpers.IsValidDateYear(year)) goto Year1;
                Month1:
                Console.Write("Enter month: ");
                month = int.Parse(Console.ReadLine());
                if (!Helpers.IsValidDateMonth(month)) goto Month1;
                Day1:
                Console.Write("Enter day: ");
                day = int.Parse(Console.ReadLine());
                if (!Helpers.IsValidDateDay(year, month, day))
                    goto Day1;
                company.StartTime = new DateTime(year, month, day);

            Year2:
                Console.Write("Enter works company end datetime year: ");
                year = int.Parse(Console.ReadLine());
                if (!Helpers.IsValidDateYear(year)) goto Year2;
                Month2:
                Console.Write("Enter month: ");
                month = int.Parse(Console.ReadLine());
                if (!Helpers.IsValidDateMonth(month)) goto Month2;
                Day2:
                Console.Write("Enter day: ");
                day = int.Parse(Console.ReadLine());
                if (!Helpers.IsValidDateDay(year, month, day)) goto Day2;
                company.EndTime = new DateTime(year, month, day);

                worksCompaniesFor.Add(company);
            }
            cv.WorksCompaniesFor = worksCompaniesFor;
            Console.Write("Enter starting salary: ");
            cv.Salary = double.Parse(Console.ReadLine());
            Console.Write("Enter additional information: ");
            cv.About = Console.ReadLine();
            return cv;
        }
        private static bool IsReplyUpdateCv()
        {
        RetryChoise:
            Console.Write("Yeniden update etmek isteyirsiz(Enter or Esc): ");
            var retry = Console.ReadKey();
            if (retry.Key == ConsoleKey.Enter)
                return true;
            else if (retry.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("\nSSecdiyiniz cv update olunur...");
                Thread.Sleep(1300);
                Console.WriteLine("Cv ugurla update olundu.");
                return false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Yanlis secim yeniden yoxlayin!");
                Console.ResetColor();
                Thread.Sleep(800);
                goto RetryChoise;
            }
        }
    }
}
