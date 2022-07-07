namespace OrderService.Infrastructure.Extensions;

public static class EnvironmentExtensions
{
    public static string EnvironmentName => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    public static bool IsProduction => EnvironmentName.ToLower() == "production";
    public static bool IsTest => EnvironmentName.ToLower() == "test";
    public static bool IsDevelopment => EnvironmentName.ToLower() == "development";
}