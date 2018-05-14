using System.ComponentModel.DataAnnotations;

namespace CapstonePL.Models
{
    public class LoginPO
    {
        public long UserId { get; set; }

        public int UserRoleId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}