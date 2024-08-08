using System.Net;

namespace Core.DTOs;

public class ResponseDto<T> 
{
    public T? Body { get; init; }

    public bool IsSuccessful { get; init; }

    public List<string>? Errors { get; init; }
    
    public HttpStatusCode? HttpCode { get; init; }
}