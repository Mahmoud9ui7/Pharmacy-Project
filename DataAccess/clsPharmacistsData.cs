using System;
using System.Data;
using System.Data.SqlClient;
 

namespace DataAccess
{
    public  class clsPharmacistsData
    {
        public static DataTable AllPharmacists()
        {
            DataTable dataTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "select * from Pharmacists";

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

        public static int AddPharmacist(int PersonID, bool IsActive)
        {
            int PharmacistID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "insert into Pharmacists( PersonID,IsActive)\r\n" +
                           "values(@PersonID,@IsActive)\r\nSELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@IsActive", IsActive); 

            try
            {
                connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PharmacistID = insertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }


            return PharmacistID;
        }

        public static bool GetPharmacistByID(int PharmacistID, ref int PersonID,ref bool IsActive)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "select * from Pharmacists where PharmacistID = @PharmacistID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@PharmacistID", PharmacistID);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)reader["PersonID"];
                    IsActive = ((int)reader["IsActive"] == 1) ? true : false;
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

        public static bool UpdatePharmacist(int PharmacistID, int PersonID,bool IsActive)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"Update  Pharmacists  
                            set PersonID =@PersonID,
                                IsActive =@IsActive
                                where PharmacistID = @PharmacistID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PharmacistID", PharmacistID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@IsActive", IsActive); 


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

        public static bool Delete(int PharmacistID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"Delete Pharmacists 
                                where PharmacistID = @PharmacistID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PharmacistID", PharmacistID);

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
