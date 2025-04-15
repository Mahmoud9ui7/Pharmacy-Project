using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Bussness
{
    public class clsPrescriptionBussness
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public static enMode Mode = enMode.AddNew;

        public int PrescriptionID{ get; set; }

        public string PataintName { get; set; }

        public int Quantity { get; set; }

        public int Stock { get; set; }
        public DateTime IssueDate { get; set; }
     


        public clsPrescriptionBussness()
        {
            PrescriptionID= -1;
            PataintName = string.Empty;
            Quantity = 0;
            Stock = 0;
            IssueDate = DateTime.Now; 

            Mode = enMode.AddNew;
        }

        public clsPrescriptionBussness(int PrescriptionID, string PataintName, int Quantity, int Stock, DateTime IssueDate )
        {
            this.PrescriptionID= PrescriptionID;
            this.PataintName = PataintName;
            this.Quantity = Quantity;
            this.Stock = Stock;
            this.IssueDate = IssueDate; 

            Mode = enMode.Update;
        }


        static public DataTable AllPrescription()
        {
            return clsPrescriptionData.AllPrescription();
        }


        public static clsPrescriptionBussness Find(int PrescriptionID)
        {
            string PataintName = "";
            int Quantity = 0;
            int Stock = 0;
            DateTime  IssueDate = DateTime.Now;
        

            if (clsPrescriptionData.GetPrescriptionByID(PrescriptionID, ref PataintName, ref Quantity, ref Stock, ref IssueDate)) 
            {
                return new clsPrescriptionBussness(PrescriptionID, PataintName, Quantity, Stock, IssueDate);
            }
            else
            {
                return null;
            }

        }

        public static bool Delete(int PrescriptionID)
        {
            return clsPrescriptionData.Delete(PrescriptionID);
        }


        private bool _AddNew()
        {
            this.PrescriptionID= clsPrescriptionData.AddPrescription(this.PataintName, this.Quantity, this.Stock, this.IssueDate);
            return (this.PrescriptionID!= -1);
        }



        private bool _Update()
        {
            return clsPrescriptionData.UpdatePrescription(this.PrescriptionID, this.PataintName, this.Quantity, this.Stock, this.IssueDate);
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
