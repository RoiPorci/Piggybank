namespace Piggybank.Shared.Constants
{
    public static class HttpMessages
    {
        public const string InternalServerError = "Internal server error";
        public const string InvalidCredentials = "Invalid credentials";
        public const string LogoutSuccessful = "Logout successful. Please remove the token from your client storage.";
        public const string LogoutFailure = "Logout failure.";
        public const string UserIdMismatch = "User ID mismatch";
        public const string UserCreationSuccess = "User created with success.";
        public const string UserCreationFailure = "User create failure.";
        public const string UserNotAuthenticated = "Authentication failed.";
        public const string UserNotFound = "User not found.";
        public const string UserUpdateFailure = "User update failure.";
        public const string UserDeleteFailure = "User delete failure.";
        public const string UserDeleteSuccess = "User deleted with success.";
        public const string UserUpdateSuccess = "User updated with success.";
        public const string ChangePasswordFailure = "Change password failure.";
        public const string ChangePasswordSuccess = "Changed password with success.";
        public const string ResetPasswordFailure = "Reset password failure.";
        public const string ResetPasswordSuccess = "Reseted password with success.";
    }
}
