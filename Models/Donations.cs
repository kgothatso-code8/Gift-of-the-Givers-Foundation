using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace GIFT_OF_THE_GIVERS_fOUNDATION.Models
{
    public class Donations
    {
        [Key]
        public int DonationID { get; set; }
        public int DonorID { get; set; }
        public int? ProjectID { get; set; }
        public decimal Amount { get; set; }
        public string DonationType { get; set; }
        public DateTime DateDonated { get; set; } = DateTime.Now;
        public string PaymentMethod { get; set; }

        public Donors Donor { get; set; }
        public Projects Project { get; set; }
    }
}
    

