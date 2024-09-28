namespace Piggybank.Shared.Constants
{
    public static class RoutePatterns
    {
        public const string ApiRoute = "api/[controller]";
        public const string AdminApiRoute = "api/admin/[controller]";

        // Auth 
        public const string Login = "login";
        public const string Register = "register";
        public const string RefreshToken = "refresh-token";
        public const string UserInfo = "user-info";
        public const string Update = "update";
        public const string ChangePassword = "change-password";
        public const string ForgotPassword = "forgot-password";
        public const string ResetPassword = "reset-password";
        public const string Logout = "logout";
        public const string Delete = "delete";
    }
}
