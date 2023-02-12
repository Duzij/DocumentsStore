using DocumentsStore.BL.Converters.Intefaces;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DocumentsStore.BL.Converters
{
    public class JsonConverter : IJsonConverter
    {
        public byte[] Convert([NotNull] string documentJson)
        {
            var memoryStream = new MemoryStream();
            memoryStream.WriteAsync(Encoding.UTF8.GetBytes(documentJson));
            return memoryStream.ToArray();
        }

        public async Task<byte[]> ConvertAsync([NotNull] string documentJson)
        {
            var memoryStream = new MemoryStream();
            await memoryStream.WriteAsync(Encoding.UTF8.GetBytes(documentJson));
            return memoryStream.ToArray();
        }
    }
}
