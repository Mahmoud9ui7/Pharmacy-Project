using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsMedicinesData
    {
        public static DataTable AllMedicines()
        {
            DataTable dataTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "select * from Medicines";

            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }

            return dataTable;

        }

        public static int AddMedicine(string Name, string Manufacturerrrrr, int stock, int Price)
        {
            int MedicineID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "insert into Medicines(Name, Manufacturer,stock,Price)\r\n" +
                           "values(@Name,@Manufacturer,@stock,@Price)\r\nSELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@Name", Name);
            Command.Parameters.AddWithValue("@Manufacturer", Manufacturerrrrr);
            Command.Parameters.AddWithValue("@stock", stock);
            Command.Parameters.AddWithValue("@Price", Price); 

            try
            {
                connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    MedicineID = insertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }

            return MedicineID;
        }

        public static bool GetMedicineByID(int MedicineID, ref string Name, ref string Manufacturerrrr, ref int stock, ref int Price)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "select * from Medicines where MedicineID = @MedicineID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@MedicineID", MedicineID);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    IsFound = true;
                    Name = (string)reader["Name"];
                    Manufacturerrrr = (string)reader["Manufacturer"];
                    stock = (int)reader["stock"];
                    Price = (int)reader["Price"]; 
                }

            }
            catch (Exception ex)
            {
                IsFound = false;
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }

            return IsFound;

        }

        public static bool UpdateMedicine(int MedicineID, string Name, string Manufacturer, int stock, int Price)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"Update  Medicines  
                            set Name = @Name,                           
                                Manufacturer = @Manufacturer,
                                stock = @stock,
                                Price = @Price 
                                where MedicineID = @MedicineID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MedicineID", MedicineID);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Manufacturer", Manufacturer);
            command.Parameters.AddWithValue("@stock", stock);
            command.Parameters.AddWithValue("@Price", Price);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);

        }

        public static bool Delete(int MedicineID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"Delete Medicines 
                                where MedicineID = @MedicineID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MedicineID", MedicineID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);
        }
    }
}
