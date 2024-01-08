namespace rms.Controllers
{
    using global::rms.Interface;
    using global::rms.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing.Matching;
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
                     expirenceRequired = 2,
                     description="",
                     AppliedCandidateids=[1,3]
                 },
                 new Job
                 {
                     title = "Back-end Developer",
                     skills = ["Node.js", "Express", "MongoDB"],
                     companyName = "TCS",
                     id = 2,
                     expirenceRequired = 3,
                     description="",
                     AppliedCandidateids=[2]
                 },
                  new Job
 {
     title = "Software Engineer",
     skills = ["C#", ".Net", "C++","DSA","OOPS"],
     companyName = "TCS",
     id = 3,
     description="",
    AppliedCandidateids=[1,2]
 },
   
   };
            private readonly IRepo _repo;
            private readonly ILogger<JobController> _logger;

            public JobController(ILogger<JobController> logger,IRepo repo)
            {
                _logger = logger;
                _repo = repo;
            }

            [HttpGet(Name = "GetJobs")]
            public ActionResult<IEnumerable<Job>> GetJobs()
            {
               
                return Ok(Jobs);
            }


            [HttpGet("{id}", Name = "GetJob")]
            public ActionResult<Job> GetJob(int id)
            {
                _repo.GetSkills(id);
                var job = Jobs.FirstOrDefault(j => j.id == id);

                if (job == null)
                {
                    return NotFound();
                }

                return Ok(job);
            }
            [HttpPost(Name = "CreateJob")]
         
            public ActionResult<Job> CreateJob(Job job)
            {
                
                job.id = Jobs.Count + 1;

                Jobs.Add(job);

                return CreatedAtAction(nameof(GetJob), new { id = job.id }, job);
            }
            [HttpGet("Candidates/{candidateId}", Name = "GetJobsByCandidates")]
            public ActionResult<IEnumerable<Candidate>> GetJobsByCandidates(int candidateId)
            {

                var jobsAppliedbyCandidate = Jobs.Where(candidate => candidate.AppliedCandidateids.Contains(candidateId)).ToList();

                return Ok(jobsAppliedbyCandidate);
            }
        }

       
    }

}
