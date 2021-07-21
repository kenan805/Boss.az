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
                                RetryVacancyChoise:
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
                                        Console.WriteLine("(If you want to go back, type >> back <<)");
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
                                                Console.BackgroundColor = ConsoleColor.Gray;
                                                Console.ForegroundColor = ConsoleColor.Black;
                                                Console.WriteLine("(If you want to go back, type >> back <<)");
                                                Console.ResetColor();
                                                Console.Write("Enter the CV guid you want to send to employer: ");
                                                guid = Console.ReadLine();
                                                if (guid.Trim().ToLower() == "back") goto RetryVacancyChoise;
                                                else if (worker.Cvs.Exists(c => c.Guid.ToString() == guid))
                                                {
                                                    Notification notf = new() { Title = "New CV!", Text = "I would like to work on the job you offer.", FromUser = worker.Name + " " + worker.Surname, PostGuid = guid };
                                                    Console.WriteLine($"Your CV has been sent to {emp.Name} {emp.Surname}!");
                                                    emp.IncomingCVs.Add(worker.Cvs.Find(c => c.Guid.ToString() == guid));
                                                    emp.Notifications.Add(notf);
                                                    FileProcessJson.JsonFileWriteAllText("Employers.json", db.Employers);
                                                    Thread.Sleep(2000);
                                                    goto SubWorkerMenu;
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                    Console.WriteLine("No matching CV found in guid!");
                                                    Console.ResetColor();
                                                    Thread.Sleep(1500);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("No matching vacancy found in guid!");
                                            Thread.Sleep(1500);
                                            goto SubWorkerMenu;
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("Nothing was found!");
                                        Thread.Sleep(1500);
                                        goto SubWorkerMenu;
                                    }
                                }
                            }
                            else if (choise == 1)
                            {
                                if (db.Employers == null)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.WriteLine("Hal hazirda hec bir employer ve vakansiya yoxdur!");
                                    Console.ResetColor();
                                    Thread.Sleep(1500);
                                    goto SubWorkerMenu;
                                }
                                else
                                {
                                    foreach (var employer in db.Employers)
                                    {
                                        if (employer.Vacancies == null)
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Hal hazirda hec bir vakansiya yoxdur!");
                                            Console.ResetColor();
                                            Thread.Sleep(1500);
                                            goto SubWorkerMenu;
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("\t\t-------------- List of employers -------------- ");
                                            db.Employers.ForEach(e =>
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.WriteLine($"Guid: {e.Guid}");
                                                Console.ResetColor();
                                                e.Show();
                                                Console.WriteLine();
                                            });
                                        RetryGuid:
                                            Console.BackgroundColor = ConsoleColor.Gray;
                                            Console.ForegroundColor = ConsoleColor.Black;
                                            Console.WriteLine("(If you want to go back, type >> back <<)");
                                            Console.ResetColor();
                                            Console.Write("Enter the guid of the employer you want to view the vacancy: ");
                                            string guid = Console.ReadLine();
                                            if (guid.Trim().ToLower() == "back")
                                                goto SubWorkerMenu;
                                            if (!Helpers.IsValidEmpty(guid))
                                                goto RetryGuid;
                                            if (db.Employers.Exists(e => e.Guid.ToString() == guid))
                                            {
                                                Console.Clear();
                                                Employer tempEmp = db.Employers.Find(w => w.Guid.ToString() == guid);
                                            TryView:
                                                foreach (var vacancy in tempEmp.Vacancies)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\n----------------------------------------------");
                                                    ++vacancy.ViewCount;
                                                    FileProcessJson.JsonFileWriteAllText("Employers.json", db.Employers);
                                                    vacancy.ShowShort(db, tempEmp);
                                                    choise = ConsoleHelper.MultipleChoice(0, 0, 3, false, "Read more", "Next vacancy", "Back to menu");
                                                    switch (choise)
                                                    {
                                                        case 0:
                                                            {
                                                            TryView2:
                                                                ++vacancy.ViewCount;
                                                                FileProcessJson.JsonFileWriteAllText("Employers.json", db.Employers);
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
                                                                    RetrySendCv:
                                                                        Console.BackgroundColor = ConsoleColor.Gray;
                                                                        Console.ForegroundColor = ConsoleColor.Black;
                                                                        Console.WriteLine("(If you want to go back, type >> back <<)");
                                                                        Console.ResetColor();
                                                                        Console.Write("Enter the CV guid you want to send to employer: ");
                                                                        guid = Console.ReadLine();
                                                                        if (guid.Trim().ToLower() == "back")
                                                                            goto TryView2;
                                                                        if (!Helpers.IsValidEmpty(guid))
                                                                            goto RetrySendCv;
                                                                        if (worker.Cvs.Exists(c => c.Guid.ToString() == guid))
                                                                        {
                                                                            Notification notf = new() { Title = "New CV!", Text = "I would like to work on the job you offer.", FromUser = worker.Name + " " + worker.Surname, PostGuid = guid };
                                                                            Console.WriteLine($"Your CV has been sent to {tempEmp.Name} {tempEmp.Surname}!");
                                                                            tempEmp.IncomingCVs.Add(worker.Cvs.Find(c => c.Guid.ToString() == guid));
                                                                            tempEmp.Notifications.Add(notf);
                                                                            FileProcessJson.JsonFileWriteAllText("Employers.json", db.Employers);
                                                                            Thread.Sleep(1500);
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                            Console.WriteLine("No vacancies were found matching the guid you entered!");
                                                                            Console.ResetColor();
                                                                            Thread.Sleep(1500);
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
                                                Console.WriteLine(">>> You looked at all the CV's <<<");
                                                choise = ConsoleHelper.MultipleChoice(1, 1, 1, false, "Look again", "Back to");
                                                FileProcessJson.JsonFileWriteAllText("Employers.json", db.Employers);
                                                switch (choise)
                                                {
                                                    case 0: goto TryView;
                                                    case 1: goto Workermenu;
                                                }
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("No employer matching the guid you entered!");
                                                goto RetryGuid;

                                            }
                                            Console.ReadLine();

                                        }
                                    }
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
                                            Console.Write("Click any to go back... ");
                                            Console.ReadLine();
                                            break;
                                        }
                                    case 1:
                                        {
                                            Console.Clear();
                                            worker.Cvs.Add(GetIncludeCvInfo(db));
                                            FileProcessJson.JsonFileWriteAllText("Workers.json", db.Workers);
                                            Console.WriteLine("CV is added...");
                                            Thread.Sleep(1500);
                                            Console.WriteLine("CV was successfully added.");
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.Clear();
                                            if (worker.Cvs == null)
                                            {
                                                Console.WriteLine("There are currently no CV's!");
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
                                                Console.Write("Enter the CV guid you want to update: ");
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
                                                                Console.WriteLine($"Current category: {db.Categories.Find(c => c.Id == tempCv.CategoryId).Name}");
                                                                Console.WriteLine("Choise new category and subcategory: ");
                                                                String[] array = new String[db.Categories.ToList().Count];
                                                                for (int k = 0; k < db.Categories.ToList().Count; k++)
                                                                    array[k] = db.Categories.ToArray()[k].Name;
                                                                choise = ConsoleHelper.MultipleChoice(5, 3, 3, false, array);
                                                                uint filterCatId = db.Categories.ToList().Find(c => c.Name == array[choise]).Id;
                                                                tempCv.CategoryId = filterCatId;
                                                                array = new String[db.SubCategories.Count(s => s.CategoryId == filterCatId)];
                                                                for (int j = 0; j < db.SubCategories.Count(s => s.CategoryId == filterCatId); j++)
                                                                    array[j] = db.SubCategories.ToList().FindAll(s => s.CategoryId == filterCatId).ToArray()[j].Name;
                                                                Console.Clear();
                                                                Console.WriteLine($"Current subcategory: {db.SubCategories.Find(s => s.Id == tempCv.SubCategoryId).Name}");
                                                                choise = ConsoleHelper.MultipleChoice(5, 1, 1, false, array);
                                                                uint filterSubCatId = db.SubCategories.ToList().Find(s => s.Name == array[choise]).Id;
                                                                tempCv.SubCategoryId = filterSubCatId;
                                                                Console.Clear();
                                                                if (IsReplyUpdateCv()) goto UpdateRetry;
                                                                break;
                                                            }
                                                        case 1:
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine($"Current city: {tempCv.WorkCity}");
                                                            RetryCity:
                                                                Console.Write("Enter new city: ");
                                                                tempCv.WorkCity = Console.ReadLine();
                                                                if (!Helpers.IsValidCity(tempCv.WorkCity))
                                                                    goto RetryCity;
                                                                else if (IsReplyUpdateCv()) goto UpdateRetry;
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine($"Current education degree: {tempCv.EducationDegree}");
                                                                Console.WriteLine("Choise new education degree");
                                                                string[] educationChoicesArray = { "Science Degree", "Higher", "Incomplete Higher", "Secondary" };
                                                                int choice = ConsoleHelper.MultipleChoice(30, 2, 1, false, educationChoicesArray);
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
                                                                Console.Clear();
                                                                Console.WriteLine($"Current experience: {tempCv.Experience}");
                                                                Console.WriteLine("Choise new experience");
                                                                string[] experienceChoiseArray = { "No", "Less than 1 year", "From 1 to 3 years", "From 3 to 5 years", "More than 5 years" };
                                                                int choice = ConsoleHelper.MultipleChoice(24, 2, 1, false, experienceChoiseArray);
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
                                                                Console.Clear();
                                                                Console.Write($"Current language(s): ");
                                                                tempCv.Languages.ForEach(v => Console.Write(v + " "));
                                                                List<string> languages = new();
                                                                Console.Write("\nEnter new language count: ");
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
                                                                Console.Clear();
                                                                Console.WriteLine($"Current specialty: {tempCv.EducationDegree}");
                                                            RetrySpec:
                                                                Console.Write("Enter new specialty: ");
                                                                tempCv.Specialty = Console.ReadLine();
                                                                if (!Helpers.IsValidSpecialty(tempCv.Specialty))
                                                                    goto RetrySpec;
                                                                else if (IsReplyUpdateCv()) goto UpdateRetry;
                                                                break;
                                                            }
                                                        case 6:
                                                            {
                                                                Console.Clear();
                                                                Console.Write($"Current skills: ");
                                                                tempCv.Skills.ForEach(v => Console.Write(v));
                                                                List<string> skills = new();
                                                                Console.Write("\nEnter new skill count: ");
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
                                                                Console.Clear();
                                                                Console.WriteLine($"Current works company: ");
                                                                tempCv.WorksCompaniesFor.ForEach(v => v.Show());
                                                                List<Company> worksCompaniesFor = new();
                                                                Console.Write("Enter works company count: ");
                                                                int worksCompanyCount = int.Parse(Console.ReadLine());
                                                                if (worksCompanyCount == 0) tempCv.WorksCompaniesFor = null;
                                                                for (int c = 0; c < worksCompanyCount; c++)
                                                                {
                                                                    RetryCompanyName:
                                                                    Company company = new();
                                                                    Console.Write("Enter works company name: ");
                                                                    company.Name = Console.ReadLine();
                                                                    if (!Helpers.IsValidName(company.Name)) goto RetryCompanyName;
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
                                                                Console.Clear();
                                                                Console.WriteLine($"Current salary: {tempCv.Salary} Azn");
                                                                Console.Write("Enter new salary: ");
                                                                tempCv.Salary = double.Parse(Console.ReadLine());
                                                                if (IsReplyUpdateCv()) goto UpdateRetry;
                                                                break;
                                                            }
                                                        case 9:
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine($"Current about: {tempCv.About}");
                                                                Console.Write("Enter new about: ");
                                                                tempCv.About = Console.ReadLine();
                                                                if (IsReplyUpdateCv()) goto UpdateRetry;
                                                                break;
                                                            }
                                                        case 10: continue;
                                                    }
                                                    FileProcessJson.JsonFileWriteAllText("Workers.json", db.Workers);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("There is no CV matching the id you entered!");
                                                    Thread.Sleep(1300);
                                                }
                                            }
                                            break;
                                        }
                                    case 3:
                                        {
                                            if (worker.Cvs == null)
                                            {
                                                Console.WriteLine("There are currently no CV's!");
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
                                            RetryCvguid:
                                                Console.BackgroundColor = ConsoleColor.Gray;
                                                Console.ForegroundColor = ConsoleColor.Black;
                                                Console.WriteLine("(If you want to go back, type >> back <<)");
                                                Console.ResetColor();
                                                Console.Write("Enter the guide of the CV you want to delete: ");
                                                string guid = Console.ReadLine();
                                                if (!Helpers.IsValidEmpty(guid)) goto RetryCvguid;
                                                if (worker.Cvs.Exists(g => g.Guid.ToString() == guid))
                                                {
                                                    Console.WriteLine("The selected CV is deleted...");
                                                    Thread.Sleep(1300);
                                                    Console.WriteLine("Cv was successfully deleted.");
                                                    worker.Cvs.Remove(worker.Cvs.Find(g => g.Guid.ToString() == guid));
                                                    FileProcessJson.JsonFileWriteAllText("Workers.json", db.Workers);
                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                    Console.WriteLine("There is no CV matching the id you entered!");
                                                    Console.ResetColor();
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
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("There are currently no active notifications!");
                                Console.ResetColor();
                                Thread.Sleep(1500);
                            }
                            else
                            {
                            RetryNotf:
                                Console.Clear();
                                worker.ShowAllNotf();
                                Console.Write("Enter the id you want to view: ");
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
                                    choise = ConsoleHelper.MultipleChoice(1, 0, 2, false, "To accept the job", "Reject");
                                    switch (choise)
                                    {
                                        case 0:
                                            {
                                                Notification notf = new() { Title = "New notf by worker!", Text = "I accept your work.", FromUser = worker.Name + " " + worker.Surname };
                                                emp.Notifications.Add(notf);
                                                emp.Vacancies.Remove(emp.Vacancies.Find(c => c.Guid.ToString() == guid));
                                                Console.Clear();
                                                Console.WriteLine($"The message is sent to {emp.Name}...");
                                                Network.SendNotf(worker, emp, notf);
                                                break;
                                            }
                                        case 1:
                                            {
                                                Notification notf = new() { Title = "New notf by worker!", Text = "Thanks for your suggestion, but it's not for me. Good luck to you!", FromUser = emp.Name + " " + emp.Surname };
                                                emp.Notifications.Add(notf);
                                                Console.Clear();
                                                Console.WriteLine($"The notification is sent to {emp.Name}...");
                                                Network.SendNotf(worker, emp, notf);
                                                break;
                                            }
                                    }
                                    worker.DeleteNotfById(id);
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("No matching notification was found for the id you entered!");
                                    Console.ResetColor();
                                    Thread.Sleep(1400);
                                    goto RetryNotf;
                                }
                            }
                            goto Workermenu;
                        }
                    case 3:
                        ProgramManagment.Start();
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
                array[i] = db.SubCategories.ToList().FindAll(s => s.CategoryId == filterCatId).ToArray()[i].Name;
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
            Console.Clear();
        RetryCity:
            Console.Write("Enter city: ");
            cv.WorkCity = Console.ReadLine();
            if (!Helpers.IsValidCity(cv.WorkCity)) goto RetryCity;
            Console.Clear();
            Console.WriteLine("Choise experience");
            string[] experienceChoiseArray = { "Less than 1 year", "From 1 to 3 years", "From 3 to 5 years", "More than 5 years" };
            choice = ConsoleHelper.MultipleChoice(26, 1, 1, false, experienceChoiseArray);
            cv.Experience = choice switch
            {
                0 => experienceChoiseArray[choice],
                1 => experienceChoiseArray[choice],
                2 => experienceChoiseArray[choice],
                3 => experienceChoiseArray[choice],
                _ => "None"
            };
            Console.Clear();
        RetrySpec:
            Console.Write("Enter specialty: ");
            cv.Specialty = Console.ReadLine();
            if (!Helpers.IsValidSpecialty(cv.Specialty)) goto RetrySpec;
            Console.Clear();
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
            Console.Clear();
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
            Console.Clear();
            Console.Write("Enter works company count: ");
            int worksCompanyCount = int.Parse(Console.ReadLine());
            if (worksCompanyCount == 0) cv.WorksCompaniesFor = null;
            for (int i = 0; i < worksCompanyCount; i++)
            {
            RetryCompanyName:
                Company company = new();
                Console.Write("Enter works company name: ");
                company.Name = Console.ReadLine();
                if (!Helpers.IsValidName(company.Name)) goto RetryCompanyName;
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
            Console.Clear();
            Console.Write("Enter starting salary: ");
            cv.Salary = double.Parse(Console.ReadLine());
            Console.Clear();
            Console.Write("Enter additional information: ");
            cv.About = Console.ReadLine();
            return cv;
        }
        private static bool IsReplyUpdateCv()
        {
        RetryChoise:
            Console.Write("You want to update again?(Enter or Esc): ");
            var retry = Console.ReadKey();
            if (retry.Key == ConsoleKey.Enter)
                return true;
            else if (retry.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("\nTThe selected resume is update...");
                Thread.Sleep(1300);
                Console.WriteLine("CV was successfully updated.");
                return false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Wrong choice, please try again!");
                Console.ResetColor();
                Thread.Sleep(800);
                goto RetryChoise;
            }
        }
    }
}
