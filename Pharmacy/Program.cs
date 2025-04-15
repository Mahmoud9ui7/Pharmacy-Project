using System;
using Pharmacy.Medicines;
using Pharmacy.Prescription;

namespace Pharmacy
{
    public  class Program
    {

        public static short MainChoice;

        public static void GoBackToMainMenuScreen()
        {

            Console.WriteLine("\nPress any key to go back to main menu...");
            Console.ReadKey();
            Run();
        }

        public static short Read1To5()
        {

            Console.WriteLine("[Please enter a number [1]to[5] ]:");
            short Choice = short.Parse(Console.ReadLine());
            return Choice;
        }

        public static void PrintList()
        {
            Console.Clear();
            Console.WriteLine("\t\t---Pharmacy App---");
            Console.WriteLine("1.People");
            Console.WriteLine("2.Pharmacists");
            Console.WriteLine("3.Medicine");
            Console.WriteLine("4.Prescription");
            Console.WriteLine("5.Close");
        }

        public static void Run()
        {
            PrintList();

            MainChoice = Read1To5();

            switch (MainChoice)
            {

                case 1:
                    {
                        clsPeople.MainMenu();
                        GoBackToMainMenuScreen();
                        break;
                    }
                case 2:
                    {
                        clsPharmacists.MainMenu();
                        GoBackToMainMenuScreen();
                        break;
                    }
                case 3:
                    {
                        clsMedicines.MainMenu();
                        GoBackToMainMenuScreen();
                        break;
                    }
                case 4:
                    {
                        clsPrescription.MainMenu();
                        GoBackToMainMenuScreen();
                        break;
                    }
                case 5:
                    {
                        Environment.Exit(0);
                        break;
                    }

            }
        }

        static void Note()
        {
            Console.WriteLine("Note: this app without exception handling !! ex: if u enter string value in int place , it will not work");
            Console.WriteLine("This app using 3-Tier!!");
            Console.WriteLine("\nPress anything to start app :)");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Note();
            Run();
        }

    }
}
