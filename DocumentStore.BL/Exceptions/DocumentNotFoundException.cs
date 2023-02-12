using System.Runtime.Serialization;

namespace DocumentStore.BL.Exceptions
{
    [Serializable]
    public class DocumentNotFoundException : Exception
    {
        public DocumentNotFoundException()
        {
        }

        public DocumentNotFoundException(string id, string? message = $"Document not found by ID.") : base(message)
        {
            base.Data.Add("ID", id);
        }

        public DocumentNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DocumentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}