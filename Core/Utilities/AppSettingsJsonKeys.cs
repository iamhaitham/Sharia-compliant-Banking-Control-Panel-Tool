namespace Core.Utilities;

/// <summary>
/// This class aims to provide us with some "keys" used throughout the application
/// instead of hard-coding them each time we want to use them. 
/// </summary>
public static class AppSettingsJsonKeys
{
    public const string DatabaseConnectionString = "DefaultConnection";

    public const string JwtKeyFromAppSettings = "JWT";
}