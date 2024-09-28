namespace Piggybank.Shared.Constants
{
    /// <summary>
    /// Provides constant messages for HTTP responses used throughout the application.
    /// </summary>
    public static class HttpMessages
    {
        /// <summary>
        /// Message indicating an internal server error.
        /// </summary>
        public const string InternalServerError = "Internal server error";

        /// <summary>
        /// Message indicating invalid credentials were provided.
        /// </summary>
        public const string InvalidCredentials = "Invalid credentials";

        /// <summary>
        /// Message indicating a successful logout and advice to remove the token from client storage.
        /// </summary>
        public const string LogoutSuccessful = "Logout successful. Please remove the token from your client storage.";

        /// <summary>
        /// Message indicating a logout failure.
        /// </summary>
        public const string LogoutFailure = "Logout failure.";

        /// <summary>
        /// Message indicating a mismatch between provided and expected user IDs.
        /// </summary>
        public const string UserIdMismatch = "User ID mismatch";

        /// <summary>
        /// Message indicating that the user was successfully created.
        /// </summary>
        public const string UserCreationSuccess = "User created with success.";

        /// <summary>
        /// Message indicating that user creation failed.
        /// </summary>
        public const string UserCreationFailure = "User create failure.";

        /// <summary>
        /// Message indicating that the user authentication failed.
        /// </summary>
        public const string UserNotAuthenticated = "Authentication failed.";

        /// <summary>
        /// Message indicating that the user was not found.
        /// </summary>
        public const string UserNotFound = "User not found.";

        /// <summary>
        /// Message indicating that the user update failed.
        /// </summary>
        public const string UserUpdateFailure = "User update failure.";

        /// <summary>
        /// Message indicating that the user deletion failed.
        /// </summary>
        public const string UserDeleteFailure = "User delete failure.";

        /// <summary>
        /// Message indicating that the user was successfully deleted.
        /// </summary>
        public const string UserDeleteSuccess = "User deleted with success.";

        /// <summary>
        /// Message indicating that the user was successfully updated.
        /// </summary>
        public const string UserUpdateSuccess = "User updated with success.";

        /// <summary>
        /// Message indicating that changing the password failed.
        /// </summary>
        public const string ChangePasswordFailure = "Change password failure.";

        /// <summary>
        /// Message indicating that the password was successfully changed.
        /// </summary>
        public const string ChangePasswordSuccess = "Changed password with success.";

        /// <summary>
        /// Message indicating that resetting the password failed.
        /// </summary>
        public const string ResetPasswordFailure = "Reset password failure.";

        /// <summary>
        /// Message indicating that the password was successfully reset.
        /// </summary>
        public const string ResetPasswordSuccess = "Reseted password with success.";
    }
}
