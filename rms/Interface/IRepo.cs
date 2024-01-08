namespace rms.Interface
{
    public interface IRepo
    {
       Task<List<int>> GetSkills(int jobid);
    }
}
