using rms.Models;

namespace rms.Interface
{
    public interface IJobRepo
    {
       Task<List<Job>> GetJobs();
        Task<Job> GetJobById(int id);
    }
}
