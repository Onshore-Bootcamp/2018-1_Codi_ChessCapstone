using System.ComponentModel.DataAnnotations;

namespace CapstonePL.Models
{
    public class UserPO
    {
        public long UserId { get; set; }

        public int UserRoleId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Username { get; set; }

        //[Required]
        [MinLength(6)]
        [MaxLength(25)]
        public string Password { get; set; }

        [Required]
        public int Role { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        [Required]
        [EmailAddress]
        [MinLength(8)]
        [MaxLength(50)]
        public string Email { get; set; }
    }
}