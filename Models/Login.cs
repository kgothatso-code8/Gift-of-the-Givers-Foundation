using System.ComponentModel.DataAnnotations;

namespace GIFT_OF_THE_GIVERS_fOUNDATION.Models
{
    public class Login
    {
        [Key]
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<Volunteers> Volunteers { get; set; }
        public ICollection<Donors> Donors { get; set; }
    }
}

