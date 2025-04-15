using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Bussness
{
    public  class clsPeopleBussness
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public static  enMode Mode = enMode.AddNew;

        public   int PersonID {  get; set; }

        public   string FirstName {  get; set; }

        public   string LastName { get; set; }

        public   string Email {  get; set; }
        public   string Phone { get; set; }
        public   string Address { get; set; }


        public clsPeopleBussness()
        {
            PersonID = -1;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Address = string.Empty;
            Mode = enMode.AddNew;
        }

        public clsPeopleBussness(int PersonID, string FirstName, string LastName, string Email, string Phone,string Address)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;

            Mode = enMode.Update;
        }


        static public DataTable AllPeople()
        {
            return clsPeopleData.AllPeople();
        }
        static public List<object> AllPeople1()
        {
            return clsPeopleData.AllPeople1();
        }


        public static clsPeopleBussness Find(int PersonID)
        {
            string FirstName = "";
            string LastName = "";
            string Email = "";
            string Phone = "";
            string Address = "";

            if (clsPeopleData.GetPersonByID(PersonID,ref FirstName,ref LastName,ref Email,ref Phone,ref Address))
            {
                return new clsPeopleBussness(PersonID,FirstName,LastName,Email,Phone,Address);
            }
            else
            {
                return null;
            }

        }

        public static bool Delete(int PersonID)
        {
            return clsPeopleData.Delete(PersonID);
        }


        private bool _AddNew()
        {
            this.PersonID = clsPeopleData.AddPerson(this.FirstName, this.LastName, this.Email, this.Phone, this.Address);
            return (this.PersonID != -1);
        }



        private bool _Update()
        {
            return clsPeopleData.UpdatePerson(this.PersonID, this.FirstName, this.LastName, this.Email, this.Phone, this.Address);
        }

        public  bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    case enMode.Update:
                    return _Update();
            }
            return false;
        }

    }
}
