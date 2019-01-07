#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
	using Microsoft.Azure.WebJobs.Host;
	using System.Configuration;
	using System.Data.SqlClient;
	using System.Net.Http;
	using System.Threading.Tasks;
    using System.Web;
    using Newtonsoft.Json.Linq;





public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{

    var values = new List<Dictionary<string, object>>();


    log.LogInformation("C# HTTP trigger function processed a request.");

    string name = req.Query["name"];

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    name = name ?? data?.name;

    
    




	

        var cnnString = Environment.GetEnvironmentVariable("SqlConnection");

        using (SqlConnection connection = new SqlConnection(cnnString))
	    {
	
        string query = "SELECT * FROM Zad6 WHERE ID = (@nameID)"; 
        connection.Open();

	                  
                     using (SqlCommand cmd = new SqlCommand(query, connection))
	                    {
                            cmd.Parameters.AddWithValue("@nameID", $"{name}");
                            
                            var reader =  cmd.ExecuteReader();

                            

                            if (reader.HasRows)
                            {
                              

                                while (reader.Read())
                                {
                                   

                                    log.LogInformation($"{reader.GetString(1)}");

                                   
                
                 
                 var fieldValues = new Dictionary<string, object>(); 
                              
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    fieldValues.Add(reader.GetName(i), reader[i]);
                }

                
                values.Add(fieldValues);

                string result = JsonConvert.SerializeObject(values, Formatting.None);
                log.LogInformation($"{result}");


                                }

                                //JavaScriptSerializer jss = new JavaScriptSerializer();
                                //jsonString = jss.Serialize(_JLog);

                            }

	                       
	                       // connection.Close();
	                    }

                       
                    }

                    
return name != null
        ? (ActionResult)new OkObjectResult($"Hello, {name}")
        : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
    

}
