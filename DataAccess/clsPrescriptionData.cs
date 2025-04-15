using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public  class clsPrescriptionData
    {
        public static DataTable AllPrescription()
        {
            DataTable dataTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "select * from Prescription";

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

        public static int AddPrescription(string PataintName, int Quantity, int Stock, DateTime  IssueDate)
        {
            int PrescriptionID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "insert into Prescription( PataintName,Quantity,Stock,IssueDate)\r\n" +
                           "values(@PataintName,@Quantity,@Stock,@IssueDate)\r\nSELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@PataintName", PataintName);
            Command.Parameters.AddWithValue("@Quantity", Quantity);
            Command.Parameters.AddWithValue("@Stock", Stock);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);

            try
            {
                connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PrescriptionID = insertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }


            return PrescriptionID;
        }

        public static bool GetPrescriptionByID(int PrescriptionID, ref string PataintName, ref int Quantity, ref int Stock, ref DateTime IssueDate)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "select * from Prescription where PrescriptionID = @PrescriptionID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    IsFound = true;
                    PataintName = (string)reader["PataintName"];
                    Quantity = (int)reader["Quantity"];
                    Stock = (int)reader["Stock"];
                    IssueDate = (DateTime )reader["IssueDate"];
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

        public static bool UpdatePrescription(int PrescriptionID, string PataintName, int Quantity, int Stock, DateTime  IssueDate)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"Update  Prescription  
                            set PataintName = @PataintName,                           
                                Quantity = @Quantity,
                                Stock = @Stock,
                                IssueDate = @IssueDate
                                where PrescriptionID = @PrescriptionID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);
            command.Parameters.AddWithValue("@PataintName", PataintName);
            command.Parameters.AddWithValue("@Quantity", Quantity);
            command.Parameters.AddWithValue("@Stock", Stock);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);


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

        public static bool Delete(int PrescriptionID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"Delete Prescription 
                                where PrescriptionID = @PrescriptionID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);

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
