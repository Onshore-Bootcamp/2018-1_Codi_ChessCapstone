namespace CapstoneDAO.Models
{
    public class LoginDO
    {
        public long UserId { get; set; }

        public int UserRoleId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}