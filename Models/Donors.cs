using System.ComponentModel.DataAnnotations;

namespace GIFT_OF_THE_GIVERS_fOUNDATION.Models
{
    public class Donors
    {
        [Key]
        public int DonorID { get; set; }
        public int UserID { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }

        public Users User { get; set; }
        public ICollection<Donations> Donations { get; set; }
    }
}
    

