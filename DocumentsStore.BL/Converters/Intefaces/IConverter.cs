using System.Diagnostics.CodeAnalysis;

namespace DocumentsStore.BL.Converters.Intefaces
{
    public interface IConverter
    {
        Task<Stream> ConvertAsync([NotNull] string documentJson);
        Stream Convert([NotNull] string documentJson);
    }
}
