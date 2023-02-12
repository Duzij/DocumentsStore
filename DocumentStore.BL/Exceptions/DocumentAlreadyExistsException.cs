using System.Runtime.Serialization;

namespace DocumentStore.BL.Exceptions
{
    [Serializable]
    public class DocumentAlreadyExistsException : Exception
    {
        public DocumentAlreadyExistsException()
        {
        }

        public DocumentAlreadyExistsException(string id, string? message = $"Document already exists. Use PUT verb to modify it.") : base(message)
        {
            base.Data.Add("ID", id);
        }

        public DocumentAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DocumentAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}