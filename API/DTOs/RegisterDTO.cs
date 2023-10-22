
namespace API.DTOs
{
    /// <summary>
    /// RegisterDTO objects only used for registration, need to be added to DB
    /// </summary>
    public class RegisterDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}