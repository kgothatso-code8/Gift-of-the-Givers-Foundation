using System.ComponentModel.DataAnnotations;

namespace GIFT_OF_THE_GIVERS_fOUNDATION.Models
{
    public class Volunteers
    {
        [Key]
        public int VolunteerID { get; set; }
        public int UserID { get; set; }
        public string Skills { get; set; }
        public string Availability { get; set; }
        public string Location { get; set; }

        public Users User { get; set; }
    }
}

