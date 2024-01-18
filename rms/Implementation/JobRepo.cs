using Microsoft.AspNetCore.Mvc;
using rms.Interface;
using Microsoft.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using rms.Models;

namespace rms.Implementation
{
   
    public class JobRepo: Interface.IJobRepo
    {
        static List<JObject> Connection(string query)
        {
            string connectionString = @"Data Source=DESKTOP-3DE0O25;Initial Catalog=rms;Integrated Security=True;trusted_connection=true;encrypt=false;";
         
           // List<object[]> results = new List<object[]>();
            List<JObject> results = new List<JObject>();
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataReader dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    JObject rowObject = new JObject();

                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string columnName = dr.GetName(i);
                        rowObject.Add(columnName, JToken.FromObject(dr.GetValue(i)));
                    }

                    results.Add(rowObject);
                }


            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            sqlConnection.Close();
            return results;
           
        }
        public async Task<List<Job>> GetJobs()
        {
           
            List<Job> jobs = new List<Job>();
            try
            {
                string queryForJobTable = "SELECT * from Job";
                List<JObject> responses = Connection(queryForJobTable);
                foreach(JObject response in responses)
                {
                    Job job = new Job();
                    if (response.ContainsKey("id")){
                        job.id=(int)response.GetValue("id");
                    }
                    if (response.ContainsKey("title"))
                    {
                        job.title = (string)response.GetValue("title");
                    }
                    if (response.ContainsKey("descrip"))
                    {
                        job.description = (string)response.GetValue("descrip");
                    }
                    if (response.ContainsKey("experienceRequired"))
                    {
                        job.expirenceRequired = (int)response.GetValue("experienceRequired");
                    }
                    if (response.ContainsKey("companyName"))
                    {
                        job.companyName = (string)response.GetValue("companyName");
                    }
                    string querytogetSkills = " SELECT js.skill FROM job_skills js JOIN Job j ON js.job_id = j.id WHERE j.id ="+ job.id +"";
                    List<JObject> skills = new List<JObject>();
                       skills= Connection(querytogetSkills);
                    List<string> skillNames = new List<string>();
                    foreach(JObject skill in skills)
                    {
                        string sk = (string)skill.GetValue("skill");
                        skillNames.Add(sk);
                        
                    }
                    job.skills = skillNames;
                    jobs.Add(job);
                }
                
               
            }
            catch(Exception ex) { 
                Console.WriteLine(ex.Message);
            }

            return  await Task.FromResult(jobs);
        }
        public async Task<Job> GetJobById(int id)
        {

            Job job = new Job();
            try
            {
                string queryForJobTable = "SELECT * from Job where id ="+id;
                List<JObject> responses = Connection(queryForJobTable);
                foreach (JObject response in responses)
                {
                   
                    if (response.ContainsKey("id"))
                    {
                        job.id = (int)response.GetValue("id");
                    }
                    if (response.ContainsKey("title"))
                    {
                        job.title = (string)response.GetValue("title");
                    }
                    if (response.ContainsKey("descrip"))
                    {
                        job.description = (string)response.GetValue("descrip");
                    }
                    if (response.ContainsKey("experienceRequired"))
                    {
                        job.expirenceRequired = (int)response.GetValue("experienceRequired");
                    }
                    if (response.ContainsKey("companyName"))
                    {
                        job.companyName = (string)response.GetValue("companyName");
                    }
                    string querytogetSkills = " SELECT js.skill FROM job_skills js JOIN Job j ON js.job_id = j.id WHERE j.id =" + id + "";
                    List<JObject> skills = new List<JObject>();
                    skills = Connection(querytogetSkills);
                    List<string> skillNames = new List<string>();
                    foreach (JObject skill in skills)
                    {
                        string sk = (string)skill.GetValue("skill");
                        skillNames.Add(sk);

                    }
                    job.skills = skillNames;
                    
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return await Task.FromResult(job);
        }
    }
}
