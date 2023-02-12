using DocumentStore.BL.DTO;

namespace DocumentStore.BL.Stores
{
    public interface IDocumentRepository
    {
        Task<Stream> GetAsync(DocumentFileFormat format, string documentId);
        Task SaveAsync(DocumentDto document);
        Task UpdateAsync(DocumentDto document);
    }
}