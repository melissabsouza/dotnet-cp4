namespace dotnet_cp4.Configuration
{
    public class AppConfiguration
    {
        public SwaggerConfig Swagger { get; set; }

        public ConnectionStringsConfig ConnectionStrings { get; set; }
    }

    public class ConnectionStringsConfig
    {
        public string OracleFIAPDbContext { get; set; }
    }

    public class SwaggerConfig
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}
