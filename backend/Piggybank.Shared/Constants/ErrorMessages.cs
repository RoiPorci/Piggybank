namespace Piggybank.Shared.Constants
{
    /// <summary>
    /// Provides constant error message strings used throughout the application.
    /// </summary>
    public static class ErrorMessages
    {
        /// <summary>
        /// General unhandled exception error message.
        /// </summary>
        public const string UnhandledException = "An unhandled exception occurred";

        #region Database

        /// <summary>
        /// Error message when a connection string is not found.
        /// </summary>
        public const string ConnectionStringNotFound = "Connection string '{0}' not found.";

        /// <summary>
        /// Error message when the database password environment variable is not found.
        /// </summary>
        public const string DbPasswordEnvVariableNotFound = "DbPassword environment variable name '{0}' not found.";

        /// <summary>
        /// Error message when a required environment variable is not found.
        /// </summary>
        public const string EnvironmentVariableNotFound = "Environment variable '{0}' not found.";

        #endregion

        #region Auth

        /// <summary>
        /// Error message when the JWT secret key is not found.
        /// </summary>
        public const string JwtSecretNotFound = "JwtSecret '{0}' not found.";

        /// <summary>
        /// Error message when the JWT expiration time is not found.
        /// </summary>
        public const string JwtExpiresInMinutesNotFound = "JwtExpiresInMinutes '{0}' not found.";

        /// <summary>
        /// General login failure error message.
        /// </summary>
        public const string LoginFailure = "Login failed.";

        /// <summary>
        /// General registration failure error message.
        /// </summary>
        public const string RegisterFailure = "Register failed.";

        /// <summary>
        /// General refresh token failure error message.
        /// </summary>
        public const string RefreshTokenFailure = "Refresh token failed.";

        /// <summary>
        /// General logout failure error message.
        /// </summary>
        public const string LogoutFailure = "Logout failed.";

        /// <summary>
        /// General deletion failure error message.
        /// </summary>
        public const string DeleteFailure = "Delete failed.";

        /// <summary>
        /// Error message when the passwords provided do not match.
        /// </summary>
        public const string PasswordsMismatch = "Passwords do not match.";

        /// <summary>
        /// Error message when a user with a specific ID is not found.
        /// </summary>
        public const string UserNotFound = "User with the ID ({0}) not found.";

        /// <summary>
        /// Error message when the user ID is not found.
        /// </summary>
        public const string UserIdNotFound = "User ID not found.";

        /// <summary>
        /// Error message when a token is not found.
        /// </summary>
        public const string TokenNotFound = "Token not found.";

        /// <summary>
        /// Error message when the password provided is incorrect.
        /// </summary>
        public const string WrongPassword = "Wrong password.";

        /// <summary>
        /// Error message when no new password is provided for a password update.
        /// </summary>
        public const string NoNewPassword = "No new password.";

        #endregion
    }
}
