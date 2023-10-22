namespace API.DTOs
{
    public class UserDto
    {
        /// <summary>
        /// UserDto used to communicate with Server each time http request restarts, as RESTful API
        /// won't store those status so need a faster way to authenticate the user's request
        /// </summary>
        public string Username { get; set; }
        public string Token { get; set; }
    }
}