namespace Microservicos.AuthenticationService.Constants
{
    public static class RegexPattern
    {
        public static string PHONE_NUMBER_PATTERN = @"^\(\d{2}\)\d{4}-\d{4}$";
        public static string PASSWORD_PATTERN = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
    }
}
