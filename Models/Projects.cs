using System.ComponentModel.DataAnnotations;

namespace GIFT_OF_THE_GIVERS_fOUNDATION.Models
{
    public class Projects
    {
        [Key]
      
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }

        public ICollection<Donations> Donations { get; set; }
    }
}