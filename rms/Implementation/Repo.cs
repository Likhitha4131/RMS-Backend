using Microsoft.AspNetCore.Mvc;
using rms.Interface;
using Microsoft.Data.SqlClient;
using System.Data;

namespace rms.Implementation
{
   
    public class Repo:IRepo
    {
        static object Connection(string query)
        {
            string connectionString = @"Data Source=DESKTOP-3DE0O25;Initial Catalog=rms;Integrated Security=True;trusted_connection=true;encrypt=false;";
            List<string> results = new List<string>();

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataReader dr = sqlCommand.ExecuteReader();
                
                    while (dr.Read())
                    {
                        var result = dr.GetString(0); 
                        results.Add(result);
                    }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            sqlConnection.Close();
            return results;
           
        }
        public async Task<List<int>> GetSkills(int jobid)
        {
            string query = "SELECT js.job_id FROM job_skills js JOIN Job j ON js.job_id = j.id WHERE j.title = 'Software Engineer';";
            object res=Connection(query);
            List<int> a=new List<int>
            {
                1, 2, 3, 4, 5, 6, 7
            };
            return  await Task.FromResult(a);
        }
    }
}
