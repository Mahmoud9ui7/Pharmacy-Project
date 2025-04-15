using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussness;

namespace Pharmacy.Medicines
{
    public  class clsMedicines
    {

        public static short MedicinesChoice = 0;

        public static void PrintMedicinesList()
        {
            Console.Clear();
            Console.WriteLine("\t\t---Medicines Screen---");
            Console.WriteLine("1.All Medicines.");
            Console.WriteLine("2.Find Medicine.");
            Console.WriteLine("3.Add Medicine.");
            Console.WriteLine("4.Update Medicine.");
            Console.WriteLine("5.Delete Medicine.");
            Console.WriteLine("6.Main Menu.");
        }

        public static short Read1To6()
        {
            Console.WriteLine("[Please enter a number [1]to[6] ]:");
            short Choice = short.Parse(Console.ReadLine());
            return Choice;
        }

        public static void GoBackToMedicinesMenuScreen()
        {
            Console.WriteLine("\nPress any key to go back to Medicines menu...");
            Console.ReadKey();
            MainMenu();
        }

        public static void PrintMedicineInfo(clsMedicinesBussness Medicine)
        {

            Console.WriteLine("MedicineID: " + Medicine.MedecineID);
            Console.WriteLine("MedecineName " + Medicine.MedecineName);
            Console.WriteLine("Manufacturer: " + Medicine.Manufacturer);
            Console.WriteLine("Stock: " + Medicine.Stock);
            Console.WriteLine("Price: " + Medicine.Price); 
        }

        public static void ReadMedicineInfo(ref clsMedicinesBussness Medicine)
        {
            Console.Clear();
            Console.WriteLine("MedecineName: ");
            Medicine.MedecineName = Console.ReadLine();
            Console.WriteLine("Manufacturer: ");
            Medicine.Manufacturer = Console.ReadLine();
            Console.WriteLine("Stock: ");
            Medicine.Stock = int.Parse(Console.ReadLine());
            Console.WriteLine("Price: ");
            Medicine.Price = int.Parse(Console.ReadLine());
       
        }

        public static void _AllMedicines()
        {
            DataTable dataTable = clsMedicinesBussness.AllMedicines();

            Console.Clear();
            Console.WriteLine("All Medicines Screen");

            if (dataTable.Rows.Count == 0)
            {
                Console.WriteLine("Oops,No Results Found !!");
                return;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine("MedicineID: {0} MedecineName {1} Manufacturer {2} Stock: {3} Price: {4} ",
                           row["MedicineID"], row["Name"], row["Manufacturer"], row["Stock"], row["Price"] );
            }

        }

        private static void _Find()
        {
            Console.Clear();
            Console.WriteLine("Enter MedicineID : ");
            int MedicineID = int.Parse(Console.ReadLine());
            clsMedicinesBussness Medicine = clsMedicinesBussness.Find(MedicineID);
            if (Medicine == null)
            {
                Console.WriteLine("Medicine with ID: {0} is not Found", Medicine.MedecineID);
                return;
            }
            else
            {
                PrintMedicineInfo(Medicine);
            }
        }

        private static void _AddMedicine()
        {
            clsMedicinesBussness Medicine = new clsMedicinesBussness();

            ReadMedicineInfo(ref Medicine);

            if (Medicine.Save())
            {
                PrintMedicineInfo(Medicine);
                Console.WriteLine("Add Successfully With MedicineID: {0}", Medicine.MedecineID);
            }
            else
            {
                Console.WriteLine("An Error occured,Add Fieled!");
            }
        }


        private static void _Delete()
        {
            Console.Clear();
            Console.WriteLine("Enter MedicineID u want to delete : ");
            short MedicineID = short.Parse(Console.ReadLine());

            clsMedicinesBussness Medicine = clsMedicinesBussness.Find(MedicineID);
            if (Medicine == null)
            {
                Console.WriteLine("Not Found with ID: {0}", MedicineID);
                return;
            }
            else
            {
                PrintMedicineInfo(Medicine);
                Console.WriteLine("Are u sure u want to delete Medicine with ID: {0}", MedicineID);
                char Answer = char.Parse(Console.ReadLine());
                if (Answer == 'Y' || Answer == 'y')
                {
                    if (clsMedicinesBussness.Delete(MedicineID))
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
            Console.WriteLine("Enter MedicineID u want to Update : ");
            short MedicineID = short.Parse(Console.ReadLine());

            clsMedicinesBussness Medicine = clsMedicinesBussness.Find(MedicineID);
            if (Medicine == null)
            {
                Console.WriteLine("Not Found with ID: {0}", MedicineID);
                return;
            }
            else
            {
                PrintMedicineInfo(Medicine);
                Console.WriteLine("Are u sure u want to update Medicine with ID: {0}", MedicineID);
                char Answer = char.Parse(Console.ReadLine());

                if (Answer == 'Y' || Answer == 'y')
                {

                    ReadMedicineInfo(ref Medicine);

                    if (Medicine.Save())
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
            PrintMedicinesList();

            MedicinesChoice = Read1To6();

            switch (MedicinesChoice)
            {
                case 1:
                    {
                        _AllMedicines();
                        GoBackToMedicinesMenuScreen();
                        break;
                    }
                case 2:
                    {
                        _Find();
                        GoBackToMedicinesMenuScreen();
                        break;
                    }
                case 3:
                    {
                        _AddMedicine();
                        GoBackToMedicinesMenuScreen();
                        break;
                    }
                case 4:
                    {
                        _Update();
                        GoBackToMedicinesMenuScreen();
                        break;
                    }
                case 5:
                    {
                        _Delete();
                        GoBackToMedicinesMenuScreen();
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
