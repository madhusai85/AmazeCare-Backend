namespace AmazeCare.Models.DTOs
{
    public class LoginUserDTO
    {
        /// <summary>
        /// Login DTO using Username and Password
        /// </summary>

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
    }
}
