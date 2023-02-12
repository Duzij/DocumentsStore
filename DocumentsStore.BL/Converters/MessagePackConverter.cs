using System.Diagnostics.CodeAnalysis;
using DocumentsStore.BL.Converters.Intefaces;
using DocumentsStore.BL.DTO;
using MessagePack;

namespace DocumentsStore.BL.Converters
{
    public class MessagePackConverter : IMessagePackConverter
    {
        private DocumentDto JsonToDocConverter(string documentJson)
        {
            var doc = System.Text.Json.JsonSerializer.Deserialize<DocumentDto>(documentJson);
            var a = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(doc.Data);
            doc.Data = a;
            return doc;
        }

        public byte[] Convert([NotNull] string documentJson)
        {
            var memoryStream = new MemoryStream();
            MessagePackSerializer.Serialize(
                memoryStream,
                JsonToDocConverter(documentJson),
                MessagePack.Resolvers.ContractlessStandardResolver.Options
            );

            return memoryStream.ToArray();
        }

        public async Task<byte[]> ConvertAsync([NotNull] string documentJson)
        {
            var memoryStream = new MemoryStream();
            await MessagePackSerializer.SerializeAsync(
                memoryStream,
                JsonToDocConverter(documentJson),
                MessagePack.Resolvers.ContractlessStandardResolver.Options
            );

            return memoryStream.ToArray();
        }
    }

    public class MessagePackDocumentDto
    {
        public string Id { get; set; }
        public string[] Tags { get; set; }

        public dynamic Data { get; set; }
    }
}
