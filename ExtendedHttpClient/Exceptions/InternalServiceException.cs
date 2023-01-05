using System.Runtime.Serialization;

namespace ExtendedHttpClient.Exceptions;

[Serializable]
public class InternalServiceException : Exception
{
    public int StatusCode { get; private set; }

    public InternalServiceException()
    {
    }

    public InternalServiceException(int statusCode)
    {
        StatusCode = statusCode;
    }

    public InternalServiceException(string? message) : base(message)
    {
    }

    public InternalServiceException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public InternalServiceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public InternalServiceException(int statusCode, string? message, Exception? innerException) : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    protected InternalServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    protected InternalServiceException(int statusCode, SerializationInfo info, StreamingContext context) : base(info, context)
    {
        StatusCode = statusCode;
    }
}