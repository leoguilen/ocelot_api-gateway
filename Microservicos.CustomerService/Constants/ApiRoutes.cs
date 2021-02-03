namespace Microservicos.CustomerService.Constants
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Customer
        {
            public const string Get = Base + "/customers/{id:Guid}";
            public const string GetAll = Base + "/customers";
            public const string Add = Base + "/customers";
            public const string Update = Base + "/customers/{id:Guid}";
            public const string Delete = Base + "/customers/{id:Guid}";
        }
    }
}
