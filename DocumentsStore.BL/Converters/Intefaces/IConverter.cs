using System.Diagnostics.CodeAnalysis;

namespace DocumentsStore.BL.Converters.Intefaces
{
    public interface IConverter
    {
        Task<byte[]> ConvertAsync([NotNull] string documentJson);
        byte[] Convert([NotNull] string documentJson);
    }
}
