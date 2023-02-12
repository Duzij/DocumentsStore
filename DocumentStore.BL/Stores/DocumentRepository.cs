using DocumentStore.BL.DTO;
using DocumentStore.BL.Exceptions;

namespace DocumentStore.BL.Stores
{
    public abstract class DocumentRepository : IDocumentRepository
    {
        public abstract Task<Stream> GetAsync(DocumentFileFormat format, string documentId);
        protected abstract Task<bool> ExistsAsync(DocumentDto document);
        protected abstract Task InsertAsync(DocumentDto document);

        public virtual async Task SaveAsync(DocumentDto document)
        {
            if (await ExistsAsync(document))
            {
                throw new DocumentAlreadyExistsException(document.Id);
            }

            await InsertAsync(document);
        }

        public virtual async Task UpdateAsync(DocumentDto document)
        {
            if (!await ExistsAsync(document))
            {
                throw new DocumentNotFoundException(document.Id);
            }

            await InsertAsync(document);
        }
    }
}
