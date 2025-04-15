using System;
using System.Collections.Generic;
using System.Data; 
using DataAccess;

namespace Bussness
{
    public  class clsMedicinesBussness
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public static enMode Mode = enMode.AddNew;

        public int MedecineID { get; set; }

        public string MedecineName { get; set; }

        public string Manufacturer { get; set; }

        public int Stock { get; set; }
        public int Price { get; set; }
 

        public clsMedicinesBussness()
        {
            MedecineID = -1;
            MedecineName = string.Empty;
            Manufacturer = string.Empty;
            Stock = 0;
            Price = 0; 
            Mode = enMode.AddNew;
        }

        public clsMedicinesBussness(int MedecineID, string MedecineName, string Manufacturer, int Stock, int Price )
        {
            this.MedecineID = MedecineID;
            this.MedecineName = MedecineName;
            this.Manufacturer = Manufacturer;
            this.Stock = Stock;
            this.Price = Price; 

            Mode = enMode.Update;
        }


        static public DataTable AllMedicines()
        {
            return clsMedicinesData.AllMedicines();
        }


        public static clsMedicinesBussness Find(int MedecineID)
        {
            string MedecineName = "";
            string Manufacturer = "";
            int Stock = 0;
            int Price = 0; 

            if (clsMedicinesData.GetMedicineByID(MedecineID, ref MedecineName, ref Manufacturer, ref Stock, ref Price ))
            {
                return new clsMedicinesBussness(MedecineID, MedecineName, Manufacturer, Stock, Price);
            }
            else
            {
                return null;
            }

        }

        public static bool Delete(int MedecineID)
        {
            return clsMedicinesData.Delete(MedecineID);
        }


        private bool _AddNew()
        {
            this.MedecineID = clsMedicinesData.AddMedicine(this.MedecineName, this.Manufacturer, this.Stock, this.Price);
            return (this.MedecineID != -1);
        }



        private bool _Update()
        {
            return clsMedicinesData.UpdateMedicine(this.MedecineID, this.MedecineName, this.Manufacturer, this.Stock, this.Price);
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

