namespace Piggybank.Shared.Constants
{
    public static class RoutePatterns
    {
        public const string ApiRoute = "api/[controller]";
        public const string AdminApiRoute = "api/admin/[controller]";

        // Auth 
        public const string Login = "login";
        public const string Register = "register";
        public const string RefreshToken = "refresh_token";
        public const string Logout = "logout";
    }
}
