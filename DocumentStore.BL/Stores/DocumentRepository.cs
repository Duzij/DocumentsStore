using DocumentStore.BL.DTO;
using DocumentStore.BL.Exceptions;

namespace DocumentStore.BL.Stores
{
    public abstract class DocumentRepository : IDocumentRepository
    {
        protected abstract Task<Stream> GetByIdAsync(DocumentFileFormat format, string documentId);
        public abstract Task SaveAsync(DocumentDto document);

        public async Task<Stream> GetAsync(string? format, string id)
        {
            var supportedFormat = EnumHelper.GetEnumMemberValue<DocumentFileFormat>(format);
            return await GetByIdAsync(supportedFormat, id);
        }

        public async Task UpdateAsync(string? format, DocumentDto document)
        {
            var savedDoc = await GetAsync(format, document.Id);
            if (savedDoc == null)
            {
                throw new DocumentNotFoundException(document.Id);
            }

            await SaveAsync(document);
        }
    }
}
