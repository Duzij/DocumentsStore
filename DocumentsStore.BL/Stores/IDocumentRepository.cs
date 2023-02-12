using DocumentsStore.BL.DTO;

namespace DocumentsStore.BL.Stores
{
    public interface IDocumentRepository
    {
        Task<Stream> GetAsync(DocumentFileFormat format, string documentId);
        Task SaveAsync(DocumentDto document);
        Task UpdateAsync(DocumentDto document);
    }
}