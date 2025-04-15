using System;
using System.Collections.Specialized;
using System.Data;
using DataAccess;

namespace Bussness
{
    public  class clsPharmacistsBussness
    { 



        public enum enMode { AddNew = 0, Update = 1 };
        public static enMode Mode = enMode.AddNew;


        public int PharmacistID {  get; set; }

        public int PersonID { get; set; }

        public bool IsActive {  get; set; }
 

        public clsPharmacistsBussness()
        {
            PharmacistID = -1;
            PersonID = -1;
            IsActive = false;
            Mode = enMode.AddNew;
        }

        public clsPharmacistsBussness(int PharmacistID, int PersonID, bool IsActive)
        {
            this.PharmacistID = PharmacistID;
            this.PersonID = PersonID;
            this.IsActive = IsActive;

            Mode = enMode.Update;
        }


        public static DataTable AllPharmacists()
        {
            return clsPharmacistsData.AllPharmacists();
        }


        public static clsPharmacistsBussness Find(int PharmacistID)
        {
            
            int PersonID = -1;
            bool IsActive = false;

            if (clsPharmacistsData.GetPharmacistByID(PharmacistID, ref PersonID, ref IsActive)) 
            {
                return new clsPharmacistsBussness(PharmacistID, PersonID,IsActive);
            }
            else
            {
                return null;
            }

        }

        public static bool Delete(int PharmacistID)
        {
            return clsPharmacistsData.Delete(PharmacistID);
        }


        private bool _AddNew()
        {
            this.PharmacistID = clsPharmacistsData.AddPharmacist(this.PersonID, this.IsActive);
            return (this.PharmacistID != -1);
        }



        private bool _Update()
        {
            return clsPharmacistsData.UpdatePharmacist(this.PharmacistID,this.PersonID,this.IsActive);
        }

        public bool Save()
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
