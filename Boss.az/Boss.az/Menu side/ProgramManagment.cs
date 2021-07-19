using Boss.az.ConsoleMenuHelper;
using Boss.az.DatabaseNS;
using Boss.az.HelpersNS;
using Boss.az.HumanNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Boss.az.Menu_side
{
    static class ProgramManagment
    {
        public static void Start(Database db)
        {
            int choise = ConsoleHelper.MultipleChoice(5, 1, 1, true, "Sign in", "Sign up", "Exit");
            while (true)
            {
                switch (choise)
                {
                    case 0:
                        {
                            Console.Clear();
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("(Geriye qayitmaq isteyirsinize >> back << yazin)");
                            Console.ResetColor();
                            SignInMenuDisplay(db);
                            break;
                        }
                    case 1:
                        {
                            Console.Clear();
                            SignUpMenuDisplay(db);
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("Gorusenedek...");
                            Console.ResetColor();
                            Environment.Exit(0);
                            break;
                        }
                }
            }
        }
        private static void SignUpMenuDisplay(Database db)
        {
            Console.WriteLine("Ne kimi qeydiyatdan kecmek isteyiriz?");
            int choise2 = ConsoleHelper.MultipleChoice(0, 1, 3, false, "Worker", "Employer", "Back to");
            switch (choise2)
            {
                case 0:
                    {
                        Console.Clear();
                        db.SignUp(true);
                        Start(db);
                        break;
                    }
                case 1:
                    {
                        Console.Clear();
                        db.SignUp(false);
                        Start(db);
                        break;
                    }
                case 2:
                    {
                        Start(db);
                        break;
                    }
            }
        }
        private static void SignInMenuDisplay(Database db)
        {
            if (db.Workers == null && db.Employers == null)
            {
                Console.WriteLine("There are currently no users!");
                Console.ReadLine();
                Start(db);
            }
            else
            {
                string username, password;
            UsernameLogin:
                Console.Write("Enter username: ");
                username = Console.ReadLine();
                if (username.Trim().ToLower() == "back")
                    Start(db);
                else if (!Helpers.IsValidUsername(username)) goto UsernameLogin;
                PasswordLogin:
                Console.Write("Enter password: ");
                password = Console.ReadLine();
                if (!Helpers.IsValidPassword(password)) goto PasswordLogin;
                if (db.SignIn(username, password) == null)
                {
                    Console.WriteLine("Username or password incorrect. Please try again.");
                    Console.ReadLine();
                }
                else if (db.SignIn(username, password).GetType().Name == "Worker")
                {
                    WorkerSide.WorkerMenu(db, db.Workers.FirstOrDefault(w => w.Username == username));
                }
                else if (db.SignIn(username, password).GetType().Name == "Employer")
                {
                    EmployerSide.EmployerMenu(db, db.Employers.Find(e => e.Username == username));
                }
            }
        }
    }
}

