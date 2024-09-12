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
        public const string UserNotAuthenticated = "User not authenticated.";
        public const string UserNotFound = "User not found.";
    }
}
