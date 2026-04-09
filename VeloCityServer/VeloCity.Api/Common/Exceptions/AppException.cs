namespace VeloCity.Api.Common.Exceptions;

public class AppException(string message, int statusCode = 400) : System.Exception(message)
{
    public int StatusCode { get; } = statusCode;
}
