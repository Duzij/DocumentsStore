using DocumentStore.BL.DTO;

namespace DocumentStore.BL.Stores
{
    public interface IDocumentRepository
    {
        Task<Stream> GetAsync(string? format, string id);
        Task SaveAsync(DocumentDto document);
        Task UpdateAsync(string? format, DocumentDto document);
    }
}