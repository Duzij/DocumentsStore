using DocumentsStore.BL.Converters.Intefaces;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace DocumentsStore.BL.Converters
{
    public class XmlConverter : IXmlConverter
    {
        public byte[] Convert([NotNull] string documentJson)
        {
            var memoryStream = new MemoryStream();
            var doc = JsonConvert.DeserializeXNode(documentJson, "DocumentRoot");

            using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings()
            {
                Indent = true,
                Async = true
            }))
            {
                doc.WriteTo(xmlWriter);
            }

            return memoryStream.ToArray();
        }

        public Task<byte[]> ConvertAsync([NotNull] string documentJson)
        {
            return Task.FromResult(Convert(documentJson));
        }
    }
}
