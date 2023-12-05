namespace rms.Controllers
{
    using global::rms.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace rms.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class JobController : ControllerBase
        {
            private static readonly List<Job> Jobs = new List<Job> {
                 new Job
                 {
                     title = "Front-end Developer",
                     skills = ["HTML", "CSS", "JavaScript"],
                     companyName = "Google",
                     id = 1,
                     expirenceRequired = 2
                 },
                 new Job
                 {
                     title = "Back-end Developer",
                     skills = ["Node.js", "Express", "MongoDB"],
                     companyName = "TCS",
                     id = 2,
                     expirenceRequired = 3
                 },
                  new Job
 {
     title = "Software Engineer",
     skills = ["C#", ".Net", "C++","DSA","OOPS"],
     companyName = "TCS",
     id = 3,
     expirenceRequired = 2
 },
   
   };
            private readonly ILogger<JobController> _logger;

            public JobController(ILogger<JobController> logger)
            {
                _logger = logger;
            }

            [HttpGet(Name = "GetJobs")]
            public IEnumerable<Job> Get()
            {
                return Jobs.Select(job => new Job
                {
                    id = job.id,
                    title = job.title,
                    description = job.description,
                    companyName = job.companyName,
                    skills= job.skills,
                    expirenceRequired= job.expirenceRequired,
                });
            }

            [HttpGet("{id}", Name = "GetJob")]
            public ActionResult<Job> Get(int id)
            {
                var job = Jobs.FirstOrDefault(j => j.id == id);
                if (job == null)
                {
                    return NotFound();
                }

                return new Job
                {
                    id = job.id,
                    title = job.title,
                    description = job.description,
                    companyName = job.companyName,
                    skills = job.skills,
                    expirenceRequired = job.expirenceRequired,
                };
            }

            [HttpPost(Name = "CreateJob")]
            public IActionResult Post([FromBody] Job job)
            {
                job.id = Jobs.Count + 1;
               // job.PostedDate = DateTime.Now;
                Jobs.Add(job);
                return CreatedAtRoute("GetJob", new { id = job.id }, job);
            }

            [HttpPut("{id}", Name = "UpdateJob")]
            public IActionResult Put(int id, [FromBody] Job updatedJob)
            {
                var existingJob = Jobs.FirstOrDefault(j => j.id == id);
                if (existingJob == null)
                {
                    return NotFound();
                }

                existingJob.title = updatedJob.title;
                existingJob.description = updatedJob.description;

                return Ok(new Job
                {
                    id = existingJob.id,
                    title = existingJob.title,
                    description = existingJob.description,
                  
                    companyName = existingJob.companyName,
                    skills = existingJob.skills,
                    expirenceRequired = existingJob.expirenceRequired,
                    // PostedDate = existingJob.PostedDate
                });
            }
        }

       
    }

}
