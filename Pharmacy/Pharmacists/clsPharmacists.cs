using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussness;

namespace Pharmacy
{
    internal class clsPharmacists
    {

        public static short PharmacistsChoice = 0;

        public static void PrintPharmacistsList()
        {
            Console.Clear();
            Console.WriteLine("\t\t---Pharmacists Screen---");
            Console.WriteLine("1.All Pharmacists.");
            Console.WriteLine("2.Find Pharmacists.");
            Console.WriteLine("3.Add Pharmacists.");
            Console.WriteLine("4.Update Pharmacists.");
            Console.WriteLine("5.Delete Pharmacists.");
            Console.WriteLine("6.Main Menu.");
        }

        public static short Read1To6()
        {
            Console.WriteLine("[Please enter a number [1]to[6] ]:");
            short Choice = short.Parse(Console.ReadLine());
            return Choice;
        }

        public static void GoBackToPharmacistsMenuScreen()
        {
            Console.WriteLine("\nPress any key to go back to Pharmacists menu...");
            Console.ReadKey();
            MainMenu();
        }

        public static void PrintPharmacistInfo(clsPharmacistsBussness Pharmacists)
        {
              
            Console.WriteLine("PharmacistsID: " + Pharmacists.PharmacistID);
            Console.WriteLine("PersonID " + Pharmacists.PersonID);
            Console.WriteLine("IsActive: " + ((Pharmacists.IsActive) ? "Yes" : "No"));


            Console.WriteLine("\nPersonInfo");
            clsPeopleBussness Person = clsPeopleBussness.Find(Pharmacists.PersonID);
            clsPeople.PrintPersonInfo(Person);

        }

        public static void ReadPharmacistInfo(ref clsPharmacistsBussness Pharmacists)
        {
            Console.Clear();
            Console.WriteLine("PersonID: ");
            Pharmacists.PersonID = int.Parse(Console.ReadLine());
            Console.WriteLine("IsActive: ");
            Pharmacists.IsActive = bool.Parse(Console.ReadLine());
        }

        public static void _AllPharmacists()
        {
            DataTable dataTable = clsPharmacistsBussness.AllPharmacists();

            Console.Clear();
            Console.WriteLine("All Pharmacists Screen");

            if (dataTable.Rows.Count == 0)
            {
                Console.WriteLine("Oops,No Results Found !!");
                return;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine("PharmacistID: {0} PersonID {1} IsActive {2}",
                           row["PharmacistID"], row["PersonID"], row["IsActive"]);
            }

        }

        private static void _Find()
        {
            Console.Clear();
           Console.WriteLine("Enter PharmacistID : ");
            int PharmacistID = int.Parse(Console.ReadLine());

            clsPharmacistsBussness Pharmacists = clsPharmacistsBussness.Find(PharmacistID);
            if (Pharmacists == null)
            {
                Console.WriteLine("Pharmacists with ID: {0} is not Found", PharmacistID);
                return;
            }
            else
            {
                PrintPharmacistInfo(Pharmacists);
            }
        }

        private static void _AddPharmacist()
        {
            clsPharmacistsBussness Pharmacists = new clsPharmacistsBussness();

            ReadPharmacistInfo(ref Pharmacists);

            if (Pharmacists.Save())
            {
                PrintPharmacistInfo(Pharmacists);
                Console.WriteLine("Add Successfully With PharmacistsID: {0}", Pharmacists.PharmacistID);
            }
            else
            {
                Console.WriteLine("An Error occured,Add Fieled!");
            }
        }


        private static void _Delete()
        {
            Console.Clear();
            Console.WriteLine("Enter PharmacistsID u want to delete : ");
            short PharmacistsID = short.Parse(Console.ReadLine());

            clsPharmacistsBussness Pharmacists = clsPharmacistsBussness.Find(PharmacistsID);
            if (Pharmacists == null)
            {
                Console.WriteLine("Not Found with ID: {0}", PharmacistsID);
                return;
            }
            else
            {
                PrintPharmacistInfo(Pharmacists);
                Console.WriteLine("Are u sure u want to delete Pharmacists with ID: {0}", PharmacistsID);
                char Answer = char.Parse(Console.ReadLine());
                if (Answer == 'Y' || Answer == 'y')
                {
                    if (clsPharmacistsBussness.Delete(PharmacistsID))
                    {
                        Console.WriteLine("Deleted Successfully :)");
                    }
                    else
                    {
                        Console.WriteLine("Deleted Fieled ! :(");
                    }
                }
                else
                {
                    Console.WriteLine("Deletion has been cancelled !!");
                }
            }
        }

        private static void _Update()
        {
            Console.Clear();
            Console.WriteLine("Enter PharmacistsID u want to Update : ");
            short PharmacistsID = short.Parse(Console.ReadLine());

            clsPharmacistsBussness Pharmacists = clsPharmacistsBussness.Find(PharmacistsID);
            if (Pharmacists == null)
            {
                Console.WriteLine("Not Found with ID: {0}", PharmacistsID);
                return;
            }
            else
            {
                PrintPharmacistInfo(Pharmacists);
                Console.WriteLine("Are u sure u want to update Pharmacists with ID: {0}", PharmacistsID);
                char Answer = char.Parse(Console.ReadLine());

                if (Answer == 'Y' || Answer == 'y')
                {

                    ReadPharmacistInfo(ref Pharmacists);

                    if (Pharmacists.Save())
                    {
                        Console.WriteLine("Updated Successfully :)");
                    }
                    else
                    {
                        Console.WriteLine("Updated Fieled ! :(");
                    }
                }
                else
                {
                    Console.WriteLine("Updating proccess has been cancelled !!");
                }
            }
        }

        public static void MainMenu()
        {
            PrintPharmacistsList();

            PharmacistsChoice = Read1To6();

            switch (PharmacistsChoice)
            {
                case 1:
                    {
                        _AllPharmacists();
                        GoBackToPharmacistsMenuScreen();
                        break;
                    }
                case 2:
                    {
                        _Find();
                        GoBackToPharmacistsMenuScreen();
                        break;
                    }
                case 3:
                    {
                        _AddPharmacist();
                        GoBackToPharmacistsMenuScreen();
                        break;
                    }
                case 4:
                    {
                        _Update();
                        GoBackToPharmacistsMenuScreen();
                        break;
                    }
                case 5:
                    {
                        _Delete();
                        GoBackToPharmacistsMenuScreen();
                        break;
                    }
                case 6:
                    {
                        Program.Run();
                        break; ;
                    }
            }
        }

    }
}
