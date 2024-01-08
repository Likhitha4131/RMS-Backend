
namespace rms.Models
{
    public class Candidate
    {
        public int id { get; set; }
        public string name { get; set; }
      
        public int[] AppiledjobIds {  get; set; }
       
        public Education education {  get; set; }
        public int noticeperiod { get; set; }
        public string phonenumber { get; set; }
        public string emailAdress { get; set; }
      
    }

}
