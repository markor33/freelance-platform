namespace Web.Bff.Models
{
    public class LoginResponse
    {
        public string Jwt { get; set; }
        public Guid DomainUserId { get; set; } = Guid.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
