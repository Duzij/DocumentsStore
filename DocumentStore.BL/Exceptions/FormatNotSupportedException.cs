using System.Runtime.Serialization;

namespace DocumentStore.BL.Exceptions
{
    [Serializable]
    public class FormatNotSupportedException : Exception
    {
        public FormatNotSupportedException()
        {
        }

        public FormatNotSupportedException(string? message) : base(message)
        {
            message = $"File format {message} is not supported";
        }

        public FormatNotSupportedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FormatNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}