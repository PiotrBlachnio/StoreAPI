namespace Store.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Items
        {
            public const string GetAll = Base + "/items";

            public const string Get = Base + "items/{id}";

            public const string Create = Base + "items";

            public const string Update = Base + "items/{id}";

            public const string PartialUpdate = Base + "items/{id}";

            public const string Delete = Base + "items/{id}";
        }
    }
}