namespace CodeArt.Poc.Aspire.ServiceDefaults;

public static class ServiceConstants
{
    public static class Db
    {
        public static class Postgres
        {
            public const string ServiceName = "pocPostgresqlService";
            public const string MainDbName = "pocMainDb";    
        }
    }

    public static class Api
    {
        public const string Version = "v1";
        public const string BasePath = "/api";
        public const string Name = "PocWebApi";
    }
}