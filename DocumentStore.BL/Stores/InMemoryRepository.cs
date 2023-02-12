using DocumentStore.BL.Converters.Intefaces;
using DocumentStore.BL.DTO;
using DocumentStore.BL.Exceptions;
using Microsoft.Extensions.Caching.Memory;

namespace DocumentStore.BL.Stores
{
    public class InMemoryRepository : DocumentRepository
    {
        private readonly IMemoryCache memoryCache;
        private readonly IXmlConverter xmlConverter;
        private readonly IJsonConverter jsonConverter;
        private readonly IMessagePackConverter messagePackConverter;

        public InMemoryRepository(IMemoryCache memoryCache, IXmlConverter xmlConverter, IJsonConverter jsonConverter, IMessagePackConverter messagePackConverter)
        {
            this.memoryCache = memoryCache;
            this.xmlConverter = xmlConverter;
            this.jsonConverter = jsonConverter;
            this.messagePackConverter = messagePackConverter;
        }

        protected override Task<bool> ExistsAsync(DocumentDto document)
        {
            return Task.FromResult(memoryCache.Get<string>(document.Id) is not null);
        }

        protected override Task InsertAsync(DocumentDto document)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(document);
            return Task.FromResult(memoryCache.Set(document.Id, json, DateTime.UtcNow.AddDays(1)));
        }

        public override async Task<Stream> GetAsync(DocumentFileFormat format, string documentId)
        {
            var documentJson = memoryCache.Get<string>(documentId);

            if (documentJson == null)
            {
                throw new DocumentNotFoundException(documentId);
            }

            return format switch
            {
                DocumentFileFormat.XML => await xmlConverter.ConvertAsync(documentJson),
                DocumentFileFormat.JSON => await jsonConverter.ConvertAsync(documentJson),
                DocumentFileFormat.MessagePack => await messagePackConverter.ConvertAsync(documentJson),
                _ => throw new FormatNotSupportedException(),
            };
        }
    }
}
