using DocumentStore.BL.Converters.Intefaces;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DocumentStore.BL.Converters
{
    public class JsonConverter : IJsonConverter
    {
        public Stream Convert([NotNull] string documentJson)
        {
            return ConvertAsync(documentJson).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task<Stream> ConvertAsync([NotNull] string documentJson)
        {
            var memoryStream = new MemoryStream();
            await memoryStream.WriteAsync(Encoding.UTF8.GetBytes(documentJson));
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
