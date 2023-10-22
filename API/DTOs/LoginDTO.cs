
namespace API.DTOs
{
    /// <summary>
    /// LoginDTO object used for login process only, 
    /// the username and password should have stored in our DB already
    /// </summary>
    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}