using System.Diagnostics.CodeAnalysis;
using DocumentStore.BL.Converters.Intefaces;
using MessagePack;

namespace DocumentStore.BL.Converters
{
    public class MessagePackConverter : IMessagePackConverter
    {
        public Stream Convert([NotNull] string documentJson)
        {
            return ConvertAsync(documentJson).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task<Stream> ConvertAsync([NotNull] string documentJson)
        {
            var memoryStream = new MemoryStream();
            await MessagePackSerializer.SerializeAsync(memoryStream, documentJson);
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
