using System.Runtime.Serialization;

namespace ExtendedHttpClient.Exceptions;

[Serializable]
internal class InvalidInitializationException : Exception
{
    public InvalidInitializationException()
    {
    }

    public InvalidInitializationException(string? message) : base(message)
    {
    }

    public InvalidInitializationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected InvalidInitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}