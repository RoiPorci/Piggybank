namespace Piggybank.Shared.Constants
{
    public static class ConfigConstants
    {
        // Database
        public const string DefaultConnectionString = "DefaultConnection";
        public const string DbPasswordEnvVariable = "DbPasswordEnvVariable";
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

        // Default Admin (if env.Dev)
        public const string DefaultAdminUserName = "PiggyAdmin";
        public const string DefaultAdminEmail = "admin@dev.com";
        public const string DefaultAdminPassword = "Admin@1234";

        // Default Admin (if env.Dev)
        public const string DefaultUserUserName = "PiggyUser";
        public const string DefaultUserEmail = "user@dev.com";
        public const string DefaultUserPassword = "User@1234";
    }
}
