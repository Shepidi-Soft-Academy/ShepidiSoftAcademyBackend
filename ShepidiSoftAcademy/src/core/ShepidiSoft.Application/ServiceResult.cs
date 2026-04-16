using System.Net;
using System.Text.Json.Serialization;

namespace ShepidiSoft.Application;

public class ServiceResult<T>
{
    public T? Data { get; set; }
    public List<string>? ErrorMessage { get; set; }
    [JsonIgnore] public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
    [JsonIgnore] public bool IsFail => !IsSuccess;
    [JsonIgnore] public HttpStatusCode StatusCode { get; set; }

    [JsonIgnore] public string? UrlAsCreated { get; set; }

    // static factory method
    public static ServiceResult<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK) => new()
    {
        Data = data,
        StatusCode = statusCode
    };
    public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            StatusCode = HttpStatusCode.Created,
            UrlAsCreated = urlAsCreated
        };
    }
    public static ServiceResult<T> Fail(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest) => new()
    {
        ErrorMessage = errors,
        StatusCode = statusCode
    };
    public static ServiceResult<T> Fail(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest) => new()
    {
        ErrorMessage = [error],
        StatusCode = statusCode
    };

}


public class ServiceResult
{
    public List<string>? ErrorMessage { get; set; }
    [JsonIgnore] public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
    [JsonIgnore] public bool IsFail => !IsSuccess;
    [JsonIgnore] public HttpStatusCode StatusCode { get; set; }

    // static factory method
    public static ServiceResult Success(HttpStatusCode statusCode = HttpStatusCode.OK) => new()
    {

        StatusCode = statusCode
    };
    public static ServiceResult Fail(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest) => new()
    {
        ErrorMessage = errors,
        StatusCode = statusCode
    };
    public static ServiceResult Fail(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest) => new()
    {
        ErrorMessage = [error],
        StatusCode = statusCode
    };

}