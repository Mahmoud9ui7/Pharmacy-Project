using System;
using System.Data;
using Bussness;

namespace Pharmacy
{
    public class clsPeople
    {

        public static short PeopleChoice = 0;

        public static void PrintPeopleList()
        {
            Console.Clear();
            Console.WriteLine("\t\t---People Screen---");
            Console.WriteLine("1.All People.");
            Console.WriteLine("2.Find Person.");
            Console.WriteLine("3.Add Person.");
            Console.WriteLine("4.Update Person.");
            Console.WriteLine("5.Delete Person.");
            Console.WriteLine("6.Main Menu.");
        }

        public static short Read1To6()
        {
            Console.WriteLine("[Please enter a number [1]to[6] ]:");
            short Choice = short.Parse(Console.ReadLine());
            return Choice;
        }

        public static void GoBackToPeopleMenuScreen()
        {
            Console.WriteLine("\nPress any key to go back to people menu...");
            Console.ReadKey();
            MainMenu();
        }

        public static void PrintPersonInfo(clsPeopleBussness Person)
        {
            if (Person != null)
            {
                Console.WriteLine("PersonID: " + Person.PersonID);
                Console.WriteLine("FirstName " + Person.FirstName);
                Console.WriteLine("LastName: " + Person.LastName);
                Console.WriteLine("Email: " + Person.Email);
                Console.WriteLine("Phone: " + Person.Phone);
                Console.WriteLine("Address: " + Person.Address);
            }
        }

        public static void ReadPersonInfo( ref clsPeopleBussness Person)
        {
            Console.Clear();
            Console.WriteLine("FirstName: ");
            Person.FirstName = Console.ReadLine();
            Console.WriteLine("LastName: ");
            Person.LastName = Console.ReadLine();
            Console.WriteLine("Email: ");
            Person.Email = Console.ReadLine();
            Console.WriteLine("Phone: ");
            Person.Phone = Console.ReadLine();
            Console.WriteLine("Address: ");
            Person.Address = Console.ReadLine();
        }

        public static void _AllPeople()
        {
            DataTable dataTable = clsPeopleBussness.AllPeople();

            Console.Clear();
            Console.WriteLine("All People Screen");

            if (dataTable.Rows.Count == 0)
            {
                Console.WriteLine("Oops,No Results Found !!");
                return;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine("PersonID: {0} FirstName {1} LastName {2} Email: {3} Phone: {4} Address: {5}",
                           row["PersonID"], row["FirstName"], row["LastName"], row["Email"], row["Phone"], row["Address"]);
            }

        }

        private static void _Find()
        {
            Console.Clear();
            Console.WriteLine("Enter PersonID : ");
            int PersonID = int.Parse(Console.ReadLine());
            clsPeopleBussness Person = clsPeopleBussness.Find(PersonID);
            if (Person == null) 
            {
                Console.WriteLine("Person with ID: {0} is not Found", Person.PersonID);
                return; 
            }
            else
            {
                PrintPersonInfo(Person);
            }
        }

        private static void _AddPerson()
        {
            clsPeopleBussness Person = new clsPeopleBussness();

            ReadPersonInfo(ref Person);

            if (Person.Save())
            {
                PrintPersonInfo(Person);
                Console.WriteLine("Add Successfully With PersonID: {0}", Person.PersonID);
            }
            else
            {
                Console.WriteLine("An Error occured,Add Fieled!");
            }
        }


        private static void _Delete()
        {
            Console.Clear();
            Console.WriteLine("Enter PersonID u want to delete : ");
            short PersonID = short.Parse(Console.ReadLine());

            clsPeopleBussness Person = clsPeopleBussness.Find(PersonID);
            if (Person == null)
            {
                Console.WriteLine("Not Found with ID: {0}", PersonID);
                return;
            }
            else
            {
                PrintPersonInfo(Person);
                Console.WriteLine("Are u sure u want to delete Person with ID: {0}", PersonID);
                char Answer = char.Parse(Console.ReadLine());
                if (Answer == 'Y' || Answer == 'y')
                {
                    if (clsPeopleBussness.Delete(PersonID))
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
            Console.WriteLine("Enter PersonID u want to Update : ");
            short PersonID = short.Parse(Console.ReadLine());

            clsPeopleBussness Person = clsPeopleBussness.Find(PersonID);
            if (Person == null)
            {
                Console.WriteLine("Not Found with ID: {0}", PersonID);
                return;
            }
            else
            {
                PrintPersonInfo(Person);
                Console.WriteLine("Are u sure u want to update Person with ID: {0}", PersonID);
                char Answer = char.Parse(Console.ReadLine());

                if (Answer == 'Y' || Answer == 'y')
                {

                    ReadPersonInfo(ref Person);

                    if (Person.Save())
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
            PrintPeopleList();

            PeopleChoice=Read1To6();

            switch (PeopleChoice)
            {
                case 1:
                    {
                        _AllPeople();
                        GoBackToPeopleMenuScreen();
                        break;
                    }
                case 2:
                    {
                        _Find();
                        GoBackToPeopleMenuScreen();
                        break;      
                    }
                    case 3:
                    {
                        _AddPerson();
                        GoBackToPeopleMenuScreen();
                        break ;
                    }
                    case 4:
                    {
                        _Update();
                        GoBackToPeopleMenuScreen();
                        break ;    
                    }
                    case 5:
                    {
                        _Delete();
                        GoBackToPeopleMenuScreen();
                        break;
                    }
                    case 6:
                    {
                        Program.Run();
                        break;;
                    }
            }
        }
    }
}
