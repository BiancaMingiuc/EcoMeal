namespace EcoMeal1.Controllers.Requests
{
    public class RegisterRequest
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public required string Role { get; set; }
    }
}
