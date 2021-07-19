using Boss.az.CategoryNS;
using Boss.az.CompanyNS;
using Boss.az.ConsoleMenuHelper;
using Boss.az.DatabaseNS;
using Boss.az.HelpersNS;
using Boss.az.HumanNS;
using Boss.az.Json;
using Boss.az.Menu_side;
using Boss.az.NetworkNS;
using Boss.az.Other;
using Boss.az.PostNS;
using Boss.az.SubCategoryNS;
using ExtensionNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Boss.az
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            #region current
            /*Worker worker1 = new()
            {
                Name = "Mehman",
                Surname = "Bəşirov",
                Age = 33,
                Email = "ujuliano.jv@cakesrecipesbook.com",
                Gender = true,
                Phones = new List<string>
                                  {
                                      "0509876543","0551212345"
                                  },
                Username = "mehman123",
                Password = "mehman123",
                Cvs = new List<Cv>
                             {
                                 new Cv
                                 {
                                     CategoryId=3,
                                     SubCategoryId=9,
                                     Experience="More than 5 years",
                                     EducationDegree="Higher",
                                     Salary=3500,
                                     WorkCity="Bakı",
                                     Specialty="İnformasiya texnoloiyaları",
                                     Skills=new List<string>{ "Java Spring Boot", "React JS", "HTML5 ,CSS3 ,Jquery, AJAX","PHP", "MSSQL", "MySql"},
                                     Languages= new List<string>{"Azərbaycan", "İngilis", "Rus" },
                                     About=@"Hərbi xidməti keçmişəm
            -Hər cür yeniliyi öyrənməyə hazıram;
            -Texniki tapşırıq və task manager-lə sistemli işləməyi sevirəm;
            -Rəhbərliklə münasibətdə heç zaman problemim olmayıb(Görüş əsnasında qeyd etdiyim əvvəlki və indiki iş yerlərimdə olan istənilən rəhbərimin nömrəsinə zəng etməklə haqqımda maraqlanmaqda sərbəstsiniz);
            -Komanda idarəetmə qabiliyyətim və təcrübəm var;
            -Azərbaycan dilini qrammatik səviyyədə bilirəm;
            -Hal hazırda işləyirəm.
            -Xaiş olunur nisyə vədlərlə narahat etməyin.",
                                      WorksCompaniesFor= new List<Company>
                                      {
                                          new Company{ Name="Nəqliyyatı İntellektual İdarəetmə Mərkəzi", StartTime=new DateTime(2015,1,1), EndTime=new DateTime(2016,11,1)},
                                          new Company{Name="Dövlət avtomobil Nəqliyyatı xidməti", StartTime=new DateTime(2016,11,15), EndTime=new DateTime(2019,4,15)},
                                          new Company{Name="Idrak Texnoloji Transfer", StartTime=new DateTime(2019,4,22), EndTime=new DateTime(2021,2,15)}
                                      }
                                 }
                            }
            };
            Worker worker2 = new()
            {
                Name = "Vəfa",
                Surname = "Hacıyeva",
                Age = 28,
                Email = "6freeboyg@devoc.site",
                Gender = false,
                Phones = new List<string>
                                  {
                                      "0701112233"
                                  },
                Username = "vefa123",
                Password = "vefa123",
                Cvs = new List<Cv>
                             {
                                 new Cv
                                 {
                                     CategoryId=1,
                                     SubCategoryId=3,
                                     Experience="More than 5 years",
                                     EducationDegree="Secondary",
                                     Salary=400,
                                     WorkCity="Sumqayıt",
                                     Specialty="Mühasibat uçotu və audit",
                                     Skills=new List<string>{ "1C", "Word", "Excel","Akinsoft", "Bars"},
                                     Languages= new List<string>{"Azərbaycan", "Rus" },
                                     About=@"Ikinci is olaraq baxiram,bir sutka is iki gun evdeyem ve evde oldugum vaxtlar isleye bilerem.
            Zehmet olmasa cv deqiq oxuduqdan sonra elaqe saxlayin.
            İşe duzeltme sirketleri narahat etmesin!!!
            Yasamal rayonuna yaxin is yerleri ile maraqlaniram.",
                                      WorksCompaniesFor= new List<Company>
                                      {
                                          new Company{ Name="Azza", StartTime=new DateTime(2015,6,5), EndTime=new DateTime(2015,12,10)},
                                          new Company{Name="Qoç ət restoranı", StartTime=new DateTime(2016,1,15), EndTime=new DateTime(2017,1,12)},
                                          new Company{Name="Nar şərab restoranı", StartTime=new DateTime(2017,2,8), EndTime=new DateTime(2021,5,9)}
                                      }
                                 },
                                 new Cv
                                 {
                                     CategoryId=1,
                                     SubCategoryId=1,
                                     Experience="Less than 1 year",
                                     EducationDegree="Secondary",
                                     Salary=450,
                                     WorkCity="Sumqayıt",
                                     Specialty="Mühasibat uçotu və audit",
                                     Skills=new List<string>{ "1C", "Word", "Excel", " Asan imza ilə bağlı xidmətlər", "Hüquqi və fiziki şəxslərin qeydiyyatı","e-gov.az, e-taxes.gov .az və digər portallardan istifadə etmək"},
                                     Languages= new List<string>{"Azərbaycan", "Rus" },
                                     About=@"Çox xahiş edirəm şəbəkə marketinglə məşğul olan şirkətlərdən narahat etməsinlər",
                                      WorksCompaniesFor= new List<Company>
                                      {
                                          new Company{ Name="Latın nəşriyyat", StartTime=new DateTime(2014,4,13), EndTime=new DateTime(2014,11,9)}
                                      }
                                 }
                            }
            };
            Worker worker3 = new()
            {
                Name = "Emil",
                Surname = "Abbasov",
                Age = 33,
                Email = "khanasamir7226@makente.com",
                Gender = true,
                Phones = new List<string>
                                  {
                                      "0509090989","0516542387"
                                  },
                Username = "emil123",
                Password = "emil123",
                Cvs = new List<Cv>
                             {
                                 new Cv
                                 {
                                     CategoryId=7,
                                     SubCategoryId=25,
                                     Experience="From 3 to 5 years",
                                     EducationDegree="Science",
                                     Salary=1000,
                                     WorkCity="Xaçmaz",
                                     Specialty="İngilis dili filologiyası",
                                     Skills=new List<string>{ "Mətnlərin,müqavilələrin,elmi ədəbiyyatın tərcüməsi ilə məşğul oluram"},
                                     Languages= new List<string>{"Azərbaycan", "İngilis", "Rus" },
                                     About=@"Hər zaman yeni fəaliyyətlərlə qarşılaşmağa 
            və fürsətlər dünyasında başqa bir döyüşdə qalib gəlmək üçün özümü tədricən 
            aşmağın çətinliklərinə meydan oxumağa çalışıram.
            Qiymət - 300 söz 3 AZN",
                                      WorksCompaniesFor= new List<Company>
                                      {
                                          new Company{ Name="Cağrı mərkəzi nümayəndəsi", StartTime=new DateTime(2017,1,1), EndTime=new DateTime(2018,9,3)},
                                          new Company{Name="Online səyahət məsləhətçisi", StartTime=new DateTime(2018,11,15), EndTime=new DateTime(2019,12,15)},
                                          new Company{Name="Landmark Hotel", StartTime=new DateTime(2020,2,12), EndTime=new DateTime(2021,4,13)}
                                      }
                                 }
                            }
            };

            Employer emp1 = new()
            {
                Name = "Elvin",
                Surname = "Selimov",
                Age = 35,
                Gender = true,
                Phones = new List<string> { "0126782299", "0509878723" },
                CompanyName = "Web Studio \"AVANTGARDE",
                Username = "elvin123",
                Password = "elvin123",
                Email = "0ziad.zizo.39794b@soilsuperfood.com",
                Vacancies = new List<Vacancy>
                             {
                                 new Vacancy
                                 {
                                     CategoryId=7,
                                     SubCategoryId=25,
                                     WorkName="Tərcüməçi",
                                     WorkCity="Bakı",
                                     AgeMin=20,
                                     AgeMax=35,
                                     Experience="From 3 to 5 years",
                                     EducationDegree="Higher",
                                     Salary=300,
                                     SalaryMax=400,
                                     Languages=new List<string>{"Azərbaycan","Rus"},
                                     JobDescription= @"- Azərbaycan - Rus , Rus - Azərbaycan dilləri üzrə xəbər və məqalə tərcümələrinin yerinə yetirilməsi
            - Tərcümə zamanı punktuasiya, qrammatika qaydalarına dəqiqliklə riayət etmək, müvafiq üslubu gözləmək, tam peşəkar səviyyədə iş təhvil vermək
            - Ehtiyac olduqda tərcümə olunan materialın redaktəsi
            - İş qrafiki: həftədə 6 gün 9.00-dan 18.00-dək
            - Onlayn iş şərayətində
            - Peşəkar inkişaf imkanı
            - Əmək haqqı artandır, uğurlu namizədlə ayrıca danışılacaq
            - 1 ay ödənişli sınaq müddəti",
                                     Requirements=@"- İşlədiyi dillərin qrammatik qaydalarını mükəmməl səviyyədə bilmək
            - Tərcüməçi kimi iş təcrübəsi: 3-5 il
            - Müxtəlif mövzularda peşəkar səviyyədə, səhvsiz tərcümə etmək bacarığı
            - Müvafiq sahədə ali təhsil
            - Tərcüməçi kimi işləməmiş namizədlərin CV-nə baxılmayacaq
            - Xahiş edirik tələblərə cavab verməyən namizədlər müraciət etməsin"
                                 }
                             }
            };
            Employer emp2 = new()
            {
                Name = "Müsabir",
                Surname = "Musabəyli",
                Age = 42,
                Gender = true,
                Phones = new List<string> { "0125645454", "0778909080" },
                CompanyName = "Bitboxlab MMC",
                Username = "musa123",
                Password = "musa123",
                Email = "wkhaled.chouichiz@olsita.com",
                Vacancies = new List<Vacancy>
                            {
                                 new Vacancy
                                 {
                                     CategoryId=3,
                                     SubCategoryId=9,
                                     WorkName="C#/Xamarin proqramçı",
                                     WorkCity="Astara",
                                     AgeMin=18,
                                     AgeMax=65,
                                     Experience="From 3 to 5 years",
                                     EducationDegree="Higher",
                                     Salary=2300,
                                     SalaryMax=2800,
                                     Languages=new List<string>{"Azərbaycan"},
                                     JobDescription= @"İş barədə məlumat
            Bitboxlab MMC Azerbaycanda yeni qurulmuş bir şirkətdir, və şirkətin əsası Estoniyadır.

            İş şəraiti:
            - İş qrafiki: 5 günlük iş həftəsi
            - İş saatları: sərbəst (8 saat)
            - İş yeri: Bakı şəhəri
            - Əmək haqqi: 1500-3500 AZN

            Əmək Haqqı müsahibə zamanı namizədin bilik və bacarığına uyğun təyin olunacaqdır",
                                     Requirements=@"- C# -da (ən azı) 3 illik sübut olunmuş təcrübəyə malik olmalı (tercihen Xamarin Formaları ilə). Java bilməsi elavə bir üstünlük
            - Təmiz və oxunaqlı kod yazmağı bacağı
            - Komanda ilə işləmə bacarığı
            - Version Control (git) -lə sərbəst işləmə bacarığı"
                                 },
                                 new Vacancy
                                 {
                                     CategoryId=3,
                                     SubCategoryId=11,
                                     WorkName="1C ÜZRƏ ƏMƏLİYYATÇI (MÜTƏXƏSSİS)",
                                     WorkCity="Quba",
                                     AgeMin=20,
                                     AgeMax=40,
                                     Experience="From 1 to 3 years",
                                     EducationDegree="Secondary",
                                     Salary=700,
                                     SalaryMax=900,
                                     Languages=new List<string>{"Azərbaycan","Rus"},
                                     JobDescription= @"- Excel faylda əməliyyatları aparıb onları 1C proqramına yükləmək
            - 1C proqramında aparılan əməliyyatlara gündəlik nəzarət etmək
            - Problemlərin araşdırılması və vaxtında həllini təmin etmək",
                                     Requirements=@"- Ali təhsil
            - 1C proqram biliyi
            - Yüksək səviyyədə Excel biliyi
            - Oxşar sahələrdə və retail sektorda iş təcrübəsi mütləqdir
            - Azərbaycan və rus dili bilikləri"
                                 },
                             }
            };
            Employer emp3 = new()
            {
                Name = "Orxan",
                Surname = "Necefli",
                Age = 41,
                Gender = true,
                Phones = new List<string> { "0124097667", "0502907623" },
                CompanyName = "Mars Overseas LTD MMM",
                Username = "orxan123",
                Password = "orxan123",
                Email = "dtezarsingle20@idiotmails.com",
                Vacancies = new List<Vacancy>
                            {
                                 new Vacancy
                                 {
                                     CategoryId=7,
                                     SubCategoryId=22,
                                     WorkName="Sürücü",
                                     WorkCity="Gəncə",
                                     AgeMin=25,
                                     AgeMax=50,
                                     Experience="From 1 to 3 years",
                                     EducationDegree="Secondary",
                                     Salary=600,
                                     SalaryMax=800,
                                     Languages=new List<string>{"Azərbaycan"},
                                     JobDescription= @"- İş saatı 7:30-da başlayır
            - 6 günlük iş qrafiki",
                                     Requirements=@"- BC kateqoryalı sürücülük vəsiqəsi
            - Yük maşınları üzrə minimum 1 il iş təcrübəsi (Hundai HD 72, İsuzu və s.;)
            - Məsuliyyətli və dəqiq
            - Qaimə və faktura ilə işləmə bacarığı
            - 25-50 yaş aralığında
            - Fiziki cəhətdən sağlam"
                                 },
                                 new Vacancy
                                 {
                                     CategoryId=4,
                                     SubCategoryId=13,
                                     WorkName="SATIŞ MENECERİ",
                                     WorkCity="Şuşa",
                                     AgeMin=30,
                                     AgeMax=45,
                                     Experience="From 3 to 5 years",
                                     EducationDegree="Higher",
                                     Salary=1500,
                                     SalaryMax=2000,
                                     Languages=new List<string>{"Azərbaycan"},
                                     JobDescription= @"İş yeri :
            - Şuşa şəhəri

            İş günləri :
            - Həftənin 6 günü

            İş saatı :
            08:30-dan 17:30-dək
            Əmək haqqı :
            1500 Azn (net) + bonus",
                                     Requirements=@"- Distribyutor satış komandasının planların tərtib olunması
            - Satış komandasının inkişaf olunması
            - Ərazi yoxlanışı və inkişaf yolların müəyyən olunması
            - Soyuducaların yerləşdirilməsi kontrolu
            - Təmsilçilərlə birgə vizitlərin həyata keçirilməsi
            - Yeni müştərilərlə işin başlanması
            - Penetrasiya, distribyusiya,AKB göstəricilərin artırılması
            - SDO- ların həyata keçirilməsi
            - Must-list standartların ərazi tətbiqinin kontrolu
            - Planoqram standartların ərazi tətbiqinin kontrolu
            - POSM standartların ərazi tətbiqinin kontrolu"
                                 }
                            }
            };
            */
            #endregion
            try
            {
                Database db = new();
                db.Workers = new List<Worker>();
                db.Employers = new List<Employer>();
                db.Categories = new List<Category>();
                db.SubCategories = new List<SubCategory>();
                //db.Workers.Add(worker1);
                //db.Workers.Add(worker2);
                //db.Workers.Add(worker3);
                //db.Employers.Add(emp1);
                //db.Employers.Add(emp2);
                //db.Employers.Add(emp3);
                #region Category and Subcategory
                /*Category maliyye = new("Maliyyə");
                SubCategory muhasibat = new(1, "Mühasibat");
                SubCategory audit = new(1, "Audit");
                SubCategory kassir = new(1, "Kassir");
                SubCategory iqtisadci = new(1, "İqtisadçı");
                SubCategory sigorta = new(1, "Sığorta");
                Category marketing = new Category("Marketinq");
                SubCategory reklam = new SubCategory(2, "Reklam");
                SubCategory marketinq = new SubCategory(2, "Marketinq");
                Category informTechn = new Category("IT");
                SubCategory sistemIdare = new(3, "Sistem idarəetmesi");
                SubCategory prog = new(3, "Proqramlaşdırma");
                SubCategory layiheİdare = new(3, "IT layihələrin idarə edilməsi");
                SubCategory melumatBaza = new(3, "Məlumat bazasının idarə edilməsi");
                Category satis = new("Satış");
                SubCategory makler = new(4, "Daşınmaz əmlak agentliyi");
                SubCategory satisMut = new(4, "Satış üzrə mütəxəssis");
                Category dizayn = new("Dizayn");
                SubCategory vebDizayn = new(5, "Veb dizayn");
                SubCategory memar = new(5, "Memar");
                SubCategory ressam = new(5, "Rəssam");
                Category huquqsunasliq = new("Hüquqşünaslıq");
                SubCategory vekil = new(6, "Vəkil");
                SubCategory huqSunas = new(6, "Hüquqşünas");
                Category xidmet = new("Xidmət");
                SubCategory kuryer = new(7, "Kuryer");
                SubCategory xadime = new(7, "Xadimə");
                SubCategory anbardar = new(7, "Anbardar");
                SubCategory surucu = new(7, "Sürücü");
                SubCategory daye = new(7, "Dayə");
                SubCategory fehle = new(7, "Fəhlə");
                SubCategory tercumeci = new(7, "Tərcüməçi");
                SubCategory diger = new(7, "Digər");
                Category muxtelif = new("Müxtəlif");
                SubCategory telebeler = new(8, "Tələbələr üçün");
                SubCategory jurnalistika = new(8, "Jurnalistika");
                db.Categories.Add(maliyye);
                db.Categories.Add(marketing);
                db.Categories.Add(informTechn);
                db.Categories.Add(satis);
                db.Categories.Add(dizayn);
                db.Categories.Add(huquqsunasliq);
                db.Categories.Add(xidmet);
                db.Categories.Add(muxtelif);
                db.SubCategories.Add(muhasibat);
                db.SubCategories.Add(audit);
                db.SubCategories.Add(kassir);
                db.SubCategories.Add(iqtisadci);
                db.SubCategories.Add(sigorta);
                db.SubCategories.Add(reklam);
                db.SubCategories.Add(marketinq);
                db.SubCategories.Add(sistemIdare);
                db.SubCategories.Add(prog);
                db.SubCategories.Add(layiheİdare);
                db.SubCategories.Add(melumatBaza);
                db.SubCategories.Add(makler);
                db.SubCategories.Add(satisMut);
                db.SubCategories.Add(vebDizayn);
                db.SubCategories.Add(memar);
                db.SubCategories.Add(ressam);
                db.SubCategories.Add(vekil);
                db.SubCategories.Add(huqSunas);
                db.SubCategories.Add(kuryer);
                db.SubCategories.Add(xadime);
                db.SubCategories.Add(anbardar);
                db.SubCategories.Add(surucu);
                db.SubCategories.Add(daye);
                db.SubCategories.Add(fehle);
                db.SubCategories.Add(tercumeci);
                db.SubCategories.Add(diger);
                db.SubCategories.Add(telebeler);
                db.SubCategories.Add(jurnalistika);*/
                #endregion
                //worker1.Email = "idayatov256@gmail.com";
                //worker2.Email = "idayatov256@gmail.com";
                //worker1.Password = "kenan239932";
                //worker2.Password = "kenan239932";
                //emp1.Email = "idayatov256@gmail.com";
                //emp2.Email = "idayatov256@gmail.com";
                //emp1.Password = "kenan239932";
                //emp2.Password = "kenan239932";

                //var options = new JsonSerializerOptions();
                //options.WriteIndented = true;
                //options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

                //var textJson = JsonSerializer.Serialize(db.Workers, options);
                //File.WriteAllText("Workers.json", textJson);

                //textJson = JsonSerializer.Serialize(db.Employers, options);
                //File.WriteAllText("Employers.json", textJson);

                //textJson = JsonSerializer.Serialize(db.Categories, options);
                //File.WriteAllText("Categories.json", textJson);

                //textJson = JsonSerializer.Serialize(db.SubCategories, options);
                //File.WriteAllText("Subcategories.json", textJson);

                //var textJson = JsonSerializer.Serialize(db, options);
                //File.WriteAllText("Database.json", textJson);

                //var text = File.ReadAllText("Workers.json");
                //db.Workers = JsonSerializer.Deserialize<List<Worker>>(text);
                //text = File.ReadAllText("Employers.json");
                //db.Employers = JsonSerializer.Deserialize<List<Employer>>(text);
                //text = File.ReadAllText("Categories.json");
                //db.Categories = JsonSerializer.Deserialize<List<Category>>(text);
                //text = File.ReadAllText("Subcategories.json");
                //db.SubCategories = JsonSerializer.Deserialize<List<SubCategory>>(text);
                var text = File.ReadAllText("Database.json");
                db = JsonSerializer.Deserialize<Database>(text);
                ProgramManagment.Start(db); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

