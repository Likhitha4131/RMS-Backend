namespace rms.Models
{
    public class Job
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string[] skills { get; set; }
       public string companyName { get; set; }
        public int expirenceRequired { get; set; }
        // Add other properties as needed
    }

}
