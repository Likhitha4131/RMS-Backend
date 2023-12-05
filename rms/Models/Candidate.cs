namespace rms.Models
{
    public class Candidate
    {
        public int id { get; set; }
        public string name { get; set; }
        public int[] jobsAppiledIds {  get; set; }
        public string[] jobAppiled {  get; set; }
        public string expressedIntrest { get; set; }
        public int expeirence { get; set; }
    }

}
