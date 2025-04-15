using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussness;

namespace Pharmacy.Prescription
{
    public  class clsPrescription
    {
        public static short PrescriptionChoice = 0;

        public static void PrintPrescriptionList()
        {
            Console.Clear();
            Console.WriteLine("\t\t---Prescriptions Screen---");
            Console.WriteLine("1.All Prescriptions.");
            Console.WriteLine("2.Find Prescription.");
            Console.WriteLine("3.Add Prescription.");
            Console.WriteLine("4.Update Prescription.");
            Console.WriteLine("5.Delete Prescription.");
            Console.WriteLine("6.Main Menu.");
        }

        public static short Read1To6()
        {
            Console.WriteLine("[Please enter a number [1]to[6] ]:");
            short Choice = short.Parse(Console.ReadLine());
            return Choice;
        }

        public static void GoBackToPrescriptionMenuScreen()
        {
            Console.WriteLine("\nPress any key to go back to Prescription menu...");
            Console.ReadKey();
            MainMenu();
        }

        public static void PrintPrescriptionInfo(clsPrescriptionBussness Prescription)
        {

            Console.WriteLine("PrescriptionID: " + Prescription.PrescriptionID);
            Console.WriteLine("PataintName " + Prescription.PataintName);
            Console.WriteLine("Quantity: " + Prescription.Quantity);
            Console.WriteLine("Stock: " + Prescription.Stock);
            Console.WriteLine("IssueDate: " + Prescription.IssueDate); 
        }

        public static void ReadPrescriptionInfo(ref clsPrescriptionBussness Prescription)
        {
            Console.Clear();
            Console.WriteLine("PataintName: ");
            Prescription.PataintName = Console.ReadLine();
            Console.WriteLine("Quantity: ");
            Prescription.Quantity = int.Parse(Console.ReadLine());
            Console.WriteLine("Stock: ");
            Prescription.Stock = int.Parse(Console.ReadLine());
            Console.WriteLine("IssueDate: ");
            Prescription.IssueDate = DateTime.Parse(Console.ReadLine());
        }

        public static void _AllPrescription()
        {
            DataTable dataTable = clsPrescriptionBussness.AllPrescription();

            Console.Clear();
            Console.WriteLine("All Prescription Screen");

            if (dataTable.Rows.Count == 0)
            {
                Console.WriteLine("Oops,No Results Found !!");
                return;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine("PrescriptionID: {0} PataintName {1} Quantity {2} Stock: {3} IssueDate: {4}",
                           row["PrescriptionID"], row["PataintName"], row["Quantity"], row["Stock"], row["IssueDate"]);
            }

        }

        private static void _Find()
        {
            Console.Clear();
            Console.WriteLine("Enter PrescriptionID : ");
            int PrescriptionID = int.Parse(Console.ReadLine());
            clsPrescriptionBussness Prescription = clsPrescriptionBussness.Find(PrescriptionID);
            if (Prescription == null)
            {
                Console.WriteLine("Prescription with ID: {0} is not Found", Prescription.PrescriptionID);
                return;
            }
            else
            {
                PrintPrescriptionInfo(Prescription);
            }
        }

        private static void _AddPrescription()
        {
            clsPrescriptionBussness Prescription = new clsPrescriptionBussness();

            ReadPrescriptionInfo(ref Prescription);

            if (Prescription.Save())
            {
                PrintPrescriptionInfo(Prescription);
                Console.WriteLine("Add Successfully With PrescriptionID: {0}", Prescription.PrescriptionID);
            }
            else
            {
                Console.WriteLine("An Error occured,Add Fieled!");
            }
        }


        private static void _Delete()
        {
            Console.Clear();
            Console.WriteLine("Enter PrescriptionID u want to delete : ");
            short PrescriptionID = short.Parse(Console.ReadLine());

            clsPrescriptionBussness Prescription = clsPrescriptionBussness.Find(PrescriptionID);
            if (Prescription == null)
            {
                Console.WriteLine("Not Found with ID: {0}", PrescriptionID);
                return;
            }
            else
            {
                PrintPrescriptionInfo(Prescription);
                Console.WriteLine("Are u sure u want to delete Prescription with ID: {0}", PrescriptionID);
                char Answer = char.Parse(Console.ReadLine());
                if (Answer == 'Y' || Answer == 'y')
                {
                    if (clsPrescriptionBussness.Delete(PrescriptionID))
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
            Console.WriteLine("Enter PrescriptionID u want to Update : ");
            short PrescriptionID = short.Parse(Console.ReadLine());

            clsPrescriptionBussness Prescription = clsPrescriptionBussness.Find(PrescriptionID);
            if (Prescription == null)
            {
                Console.WriteLine("Not Found with ID: {0}", PrescriptionID);
                return;
            }
            else
            {
                PrintPrescriptionInfo(Prescription);
                Console.WriteLine("Are u sure u want to update Prescription with ID: {0}", PrescriptionID);
                char Answer = char.Parse(Console.ReadLine());

                if (Answer == 'Y' || Answer == 'y')
                {

                    ReadPrescriptionInfo(ref Prescription);

                    if (Prescription.Save())
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
            PrintPrescriptionList();

            PrescriptionChoice = Read1To6();

            switch (PrescriptionChoice)
            {
                case 1:
                    {
                        _AllPrescription();
                        GoBackToPrescriptionMenuScreen();
                        break;
                    }
                case 2:
                    {
                        _Find();
                        GoBackToPrescriptionMenuScreen();
                        break;
                    }
                case 3:
                    {
                        _AddPrescription();
                        GoBackToPrescriptionMenuScreen();
                        break;
                    }
                case 4:
                    {
                        _Update();
                        GoBackToPrescriptionMenuScreen();
                        break;
                    }
                case 5:
                    {
                        _Delete();
                        GoBackToPrescriptionMenuScreen();
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
