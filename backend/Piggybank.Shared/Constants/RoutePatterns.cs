namespace Piggybank.Shared.Constants
{
    /// <summary>
    /// Provides constant route patterns used throughout the application's API.
    /// </summary>
    public static class RoutePatterns
    {
        /// <summary>
        /// The base route for API controllers.
        /// </summary>
        public const string ApiRoute = "api/[controller]";

        /// <summary>
        /// The base route for admin API controllers.
        /// </summary>
        public const string AdminApiRoute = "api/admin/[controller]";

        #region Auth

        /// <summary>
        /// Route for the login endpoint.
        /// </summary>
        public const string Login = "login";

        /// <summary>
        /// Route for the user registration endpoint.
        /// </summary>
        public const string Register = "register";

        /// <summary>
        /// Route for the token refresh endpoint.
        /// </summary>
        public const string RefreshToken = "refresh-token";

        /// <summary>
        /// Route for retrieving user information.
        /// </summary>
        public const string UserInfo = "user-info";

        /// <summary>
        /// Route for updating user information.
        /// </summary>
        public const string Update = "update";

        /// <summary>
        /// Route for changing the user's password.
        /// </summary>
        public const string ChangePassword = "change-password";

        /// <summary>
        /// Route for initiating the forgot password process.
        /// </summary>
        public const string ForgotPassword = "forgot-password";

        /// <summary>
        /// Route for resetting the user's password.
        /// </summary>
        public const string ResetPassword = "reset-password";

        /// <summary>
        /// Route for logging out the user.
        /// </summary>
        public const string Logout = "logout";

        /// <summary>
        /// Route for deleting a user account.
        /// </summary>
        public const string Delete = "delete";

        #endregion
    }
}
