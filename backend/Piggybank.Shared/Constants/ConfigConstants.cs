namespace Piggybank.Shared.Constants
{
    public static class ConfigConstants
    {
        // Database
        public const string DefaultConnectionString = "DefaultConnection";
        public const string DbPasswordEnvVariable = "DB_PASSWORD";
        public const string DbPasswordInConnectionstring = "%DB_PASSWORD%";

        // Roles
        public const string AdminRole = "Admin";
        public const string AdminPolicy = "AdminPolicy";
        public const string UserRole = "User";
        public const string UserPolicy = "UserPolicy";

        // JWT
        public const string JwtSecret = "Jwt:Secret";
        public const string JwtIssuer = "Jwt:Issuer";
        public const string JwtExpiresInMinutes = "Jwt:ExpiresInMinutes";

        // Password
        public const int PasswordMinLength = 6;
    }
}
