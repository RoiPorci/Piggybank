namespace Piggybank.Shared.Constants
{
    /// <summary>
    /// Provides constant values for configuration settings used throughout the application.
    /// </summary>
    public static class ConfigConstants
    {
        #region Database

        /// <summary>
        /// The name of the default connection string in the configuration.
        /// </summary>
        public const string DefaultConnectionString = "DefaultConnection";

        /// <summary>
        /// The name of the environment variable that stores the database password.
        /// </summary>
        public const string DbPasswordEnvVariable = "DbPasswordEnvVariable";

        /// <summary>
        /// Placeholder for the database password in the connection string.
        /// </summary>
        public const string DbPasswordInConnectionstring = "%DB_PASSWORD%";

        #endregion

        #region Roles

        /// <summary>
        /// The name of the administrator role.
        /// </summary>
        public const string AdminRole = "Admin";

        /// <summary>
        /// The name of the policy for administrator role authorization.
        /// </summary>
        public const string AdminPolicy = "AdminPolicy";

        /// <summary>
        /// The name of the user role.
        /// </summary>
        public const string UserRole = "User";

        /// <summary>
        /// The name of the policy for user role authorization.
        /// </summary>
        public const string UserPolicy = "UserPolicy";

        #endregion

        #region JWT

        /// <summary>
        /// The section in the configuration where JWT settings are stored.
        /// </summary>
        public const string JwtSection = "Jwt";

        /// <summary>
        /// The secret key used for signing JWT tokens.
        /// </summary>
        public const string JwtSecret = "Secret";

        /// <summary>
        /// The issuer of the JWT tokens.
        /// </summary>
        public const string JwtIssuer = "Issuer";

        /// <summary>
        /// The configuration key for the expiration time of JWT tokens, in minutes.
        /// </summary>
        public const string JwtExpiresInMinutes = "ExpiresInMinutes";

        #endregion

        #region Password

        /// <summary>
        /// The minimum required length for user passwords.
        /// </summary>
        public const int PasswordMinLength = 6;

        #endregion

        #region Default Admin (if env.Dev)

        /// <summary>
        /// The default username for the administrator in the development environment.
        /// </summary>
        public const string DefaultAdminUserName = "PiggyAdmin";

        /// <summary>
        /// The default email address for the administrator in the development environment.
        /// </summary>
        public const string DefaultAdminEmail = "admin@dev.com";

        /// <summary>
        /// The default password for the administrator in the development environment.
        /// </summary>
        public const string DefaultAdminPassword = "Admin@1234";

        #endregion

        #region Default User (if env.Dev)

        /// <summary>
        /// The default username for a user in the development environment.
        /// </summary>
        public const string DefaultUserUserName = "PiggyUser";

        /// <summary>
        /// The default email address for a user in the development environment.
        /// </summary>
        public const string DefaultUserEmail = "user@dev.com";

        /// <summary>
        /// The default password for a user in the development environment.
        /// </summary>
        public const string DefaultUserPassword = "User@1234";

        #endregion
    }
}
