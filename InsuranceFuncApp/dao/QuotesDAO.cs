using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace InsuranceFuncApp.dao
{
    class QuotesDAO
    {
        private static volatile SqlConnection connection;

        private static SqlConnection GetConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "fdchackathon.database.windows.net";
            builder.UserID = "hackadmin";
            builder.Password = "H@ckathon";
            builder.InitialCatalog = "carddb";
            connection = new SqlConnection(builder.ConnectionString);
            return connection;
        }


        /*
  * update the status of the card to Activated in database
  */
        public static Boolean addQuoteRequest(VehicleQuoteRequest quoteRequest)
        {
            Boolean quoteSubmitted = false;
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    String query = "insert into [dbo].[VEHICLE_QUOTE] (QuoteType, FirstName, LastName, Age, VehicleYear, VehicleMake, VehicleModel) values " +
                        "('"+ quoteRequest.QuoteType   +
                       "', '" + quoteRequest.FirstName + "', '" + quoteRequest.LastName + "', " + quoteRequest.Age +
                        ", " + quoteRequest.Vehicle.Year + ", '" + quoteRequest.Vehicle.Make + "', '" + quoteRequest.Vehicle.Model
                        + "')";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int numRows = command.ExecuteNonQuery();
                        if (numRows > 0)
                        {
                            quoteSubmitted = true;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
            return quoteSubmitted;
        }
    }

}
