namespace Core.Utilities;

public static class CustomErrorMessage
{
    public static string IncompatibleTypeMessage(string typeName) => 
        $"Value provided must be a {typeName}";
    
    public static string InvalidMobileNumberFormat() =>
        "Invalid mobile number format";

    public static string ExactStringLengthNotSatisfied(int exactStringLength) =>
        $"An exact string length of {exactStringLength} was not satisfied";

    public static string CouldNotResolveDependency(string dependencyName) =>
        $"Could not resolve dependency {dependencyName}";
    
    public static string CouldNotResolveType(string typeName) =>
        $"Could not resolve type {typeName}";

    public static string CannotBeNull(string propertyName) =>
        $"{propertyName} cannot be null";

    public static string UserAlreadyExists(
        string personalId,
        string email,
        string mobileNumber
    ) =>
        $"A user with a similar personal id '{personalId}', email '{email}', or mobile number '{mobileNumber}' already exists. All these values must be unique for each user.";
    
    public static string UserDoesNotExist(string email) =>
        $"User with email '{email}' does not exist";

    public static string UserInfoDidNotMatch() =>
        "User information did not match";
}