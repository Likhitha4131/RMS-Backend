using Microsoft.AspNetCore.Mvc;
using rms.Models;

namespace rms.Controllers
{
    
        [ApiController]
        [Route("[controller]")]
        public class CandidateController : ControllerBase
        {
            private static readonly List<Candidate> Candidates = new List<Candidate>
            {
                new Candidate
                {
                     id = 1,
                    name = "John Doe",
                    jobsAppiledIds = [1,3],
                    jobAppiled = ["Front-end Developer","Software Engineer"],
                    expressedIntrest = "",
                    expeirence = 1,
                },
                 new Candidate
                {
                     id = 2,
                    name = "Jane Doe",
                    jobsAppiledIds = [2,3],
                    jobAppiled = ["Back-end Developer","Software Engineer"],
                    expressedIntrest = "",
                    expeirence = 2,
                }
            };
            private readonly ILogger<CandidateController> _logger;

            public CandidateController(ILogger<CandidateController> logger)
            {
                _logger = logger;
            }

            [HttpGet(Name = "GetCandidates")]
            public IEnumerable<Candidate> Get()
            {
                return Candidates.Select(candidate => new Candidate
                {
                    id = candidate.id,
                    name = candidate.name,
                    jobsAppiledIds = candidate.jobsAppiledIds,
                    jobAppiled = candidate.jobAppiled,
                    expressedIntrest = candidate.expressedIntrest
                });
            }

            [HttpGet("{jobId}", Name = "GetJobCandidates")]
            public IActionResult Get(int jobId)
            {
                var jobCandidates = Candidates.Where(c => c.jobsAppiledIds.Contains(jobId));
                return Ok(jobCandidates.Select(candidate => new Candidate
                {
                    id = candidate.id,
                    name = candidate.name,
                    jobsAppiledIds = candidate.jobsAppiledIds,
                    jobAppiled=candidate.jobAppiled,
                    expressedIntrest=candidate.expressedIntrest
                }));
            }

            [HttpPost(Name = "CreateCandidate")]
            public IActionResult Post([FromBody] Candidate candidate)
            {
                // Assume you have logic to validate and associate candidate with a job
                Candidates.Add(candidate);
                return CreatedAtRoute("GetCandidates", null, candidate);
            }
        }
}


