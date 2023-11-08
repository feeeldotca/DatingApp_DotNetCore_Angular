
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    /// <summary>
    /// RegisterDTO objects only used for registration, need to be added to DB
    /// </summary>
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}