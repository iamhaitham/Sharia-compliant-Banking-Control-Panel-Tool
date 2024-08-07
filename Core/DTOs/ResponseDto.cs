using System.Net;

namespace Core.DTOs;

public class ResponseDto<T> 
{
    public T? Body { get; set; }

    public bool IsSuccessful { get; set; }

    public List<string>? Errors { get; set; }
    
    public HttpStatusCode? HttpCode { get; set; }
}