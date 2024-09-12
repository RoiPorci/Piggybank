namespace Piggybank.Shared.Dtos
{
    public class AppUserDto
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
