using System.Net;

namespace Core.Utilities;

public class GenericResponse<T> 
{
    public T? Body { get; init; }

    public bool IsSuccessful { get; init; }

    public string? Error { get; init; }
    
    public HttpStatusCode? HttpCode { get; init; }
}