﻿namespace rms.Controllers
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
            private readonly IJobRepo _repo;
            private readonly ILogger<JobController> _logger;
            
            public JobController(ILogger<JobController> logger,IJobRepo repo)
            {
                _logger = logger;
                _repo = repo;
            }
            

            [HttpGet(Name = "GetJobs")]
            public async Task<ActionResult<List<Job>>> GetJobs()
{
    List<Job> jobs = await _repo.GetJobs();

    return Ok(jobs);
}


            [HttpGet("{id}", Name = "GetJob")]
            public async Task<ActionResult<Job>> GetJob(int id)
            {
                Job job = await _repo.GetJobById(id);
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
            public ActionResult<IEnumerable<Candidate>> GetJobsCandidatesApplied(int candidateId)
            {

                var jobsAppliedbyCandidate = Jobs.Where(candidate => candidate.AppliedCandidateids.Contains(candidateId)).ToList();

                return Ok(jobsAppliedbyCandidate);
            }
        }

       
    }

}
