using System;
	using Microsoft.Azure.WebJobs.Host;
	using System.Configuration;
	using System.Data.SqlClient;
	using System.Net;
	using System.Net.Http;
	using System.Threading.Tasks;
	


public static async Task Run(string myQueueItem, ILogger log)
{
    log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

    var cnnString = Environment.GetEnvironmentVariable("SqlConnection");

using (SqlConnection connection = new SqlConnection(cnnString))
	                {
	

 connection.Open();

 var date = DateTime.Now;
 var day = date.Day;

	                 string query = "INSERT INTO [dbo].[Zad6] (ID, Queue)  VALUES (@day ,@myQueueItem)";   
	                  
	                 
	                


                     using (SqlCommand cmd = new SqlCommand(query, connection))
	                    {
                            cmd.Parameters.AddWithValue("@day", date.Day);
	                        cmd.Parameters.AddWithValue("@myQueueItem", myQueueItem);
	                        var rows = await cmd.ExecuteNonQueryAsync();
	                        log.LogInformation($"{rows} rows were updated"); 
	                        connection.Close();
	                    }	
                    }

}
