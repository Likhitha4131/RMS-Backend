using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using rms.Models;

namespace rms.Controllers
{
        
        [ApiController]
        [Route("[controller]")]
        public class CandidateController : ControllerBase
        {
            private static readonly List<Candidate> candidates = new List<Candidate>
            {
                new Candidate
                {
                     id = 1,
                    name = "John Doe",
                    AppiledjobIds = [1,3],
                    noticeperiod=15,
                    phonenumber="1111111111",
                      education=new Education
                      {
                          DegreeName="B.Tech",
                          StartYear=2019,
                          EndYear=2023,
                          InstitueName="Harvard"

                      },
                      emailAdress="john123@gmail.com",
                },
                 new Candidate
                {
                     id = 2,
                    name = "Jane Doe",
                    AppiledjobIds = [2,3],
                    noticeperiod=45,
                    phonenumber="1111111111",
                      education=new Education
                      {
                          DegreeName="B.Tech",
                          StartYear=2019,
                          EndYear=2023,
                          InstitueName="Harvard"

                      },
                      emailAdress="janedoe@gmail.com",

                },
                 new Candidate
                 {
                       id = 3,
                       name="Emily Park",
                       AppiledjobIds=[1], 
                      noticeperiod=30,
                      phonenumber="1111111111",
                      education=new Education
                      {
                          DegreeName="B.Tech",
                          StartYear=2019,
                          EndYear=2023,
                          InstitueName="Harvard"

                      },
                      emailAdress="emilypark@gmail.com",
                   }

     
            };
            private readonly ILogger<CandidateController> _logger;

            public CandidateController(ILogger<CandidateController> logger)
            {
                _logger = logger;
            }
        [HttpGet]
        public ActionResult<IEnumerable<Candidate>> GetCandidates()
        {
            return Ok(candidates);
        }



        [HttpGet("{id}")]
        public ActionResult<Candidate> GetCandidate(int id)
        {
            var candidate = candidates.Find(c => c.id == id);

            if (candidate == null)
            {
                return NotFound();
            }

            return Ok(candidate);
        }
        [HttpPost]
        public ActionResult<Candidate> CreateCandidate(Candidate candidate)
        {
            
            candidate.id = candidates.Count + 1;

            candidates.Add(candidate);

            return CreatedAtAction(nameof(GetCandidate), new { id = candidate.id }, candidate);
        }
        [HttpGet("Jobs/{jobId}")]
        public ActionResult<IEnumerable<Candidate>> GetCandidatesByJobId(int jobId)
        {
            
            var candidatesAppliedToJob = candidates.Where(candidate => candidate.AppiledjobIds.Contains(jobId)).ToList();

            return Ok(candidatesAppliedToJob);
        }

    }
}


