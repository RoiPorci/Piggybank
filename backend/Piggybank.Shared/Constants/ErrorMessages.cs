namespace Piggybank.Shared.Constants
{
    public static class ErrorMessages
    {
        public const string UnhandledException = "An unhandled exception occurred";
        
        // Database
        public const string ConnectionStringNotFound = "Connection string '{0}' not found.";
        public const string DbPasswordEnvVariableNotFound = "DbPassword environment variable name '{0}' not found.";
        public const string EnvironmentVariableNotFound = "Environment variable '{0}' not found.";

        // Auth
        public const string JwtSecretNotFound = "JwtSecret '{0}' not found.";
        public const string JwtExpiresInMinutesNotFound = "JwtExpiresInMinutes '{0}' not found.";
        public const string LoginFailure = "Login failed.";
        public const string RegisterFailure = "Register failed.";
        public const string RefreshTokenFailure = "Refresh token failed.";
        public const string LogoutFailure = "Logout failed.";
        public const string DeleteFailure = "Delete failed.";
        public const string PasswordsMismatch = "Passwords do not match.";
        public const string UserNotFound = "User with the ID ({0}) not found.";
        public const string UserIdNotFound = "User ID not found.";
        public const string TokenNotFound = "Token not found.";
        public const string WrongPassword = "Wrong password.";
        public const string NoNewPassword = "No new password.";
    }
}
