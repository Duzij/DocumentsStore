using DocumentsStore.BL.Converters.Intefaces;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace DocumentsStore.BL.Converters
{
    public class XmlConverter : IXmlConverter
    {
        public Stream Convert([NotNull] string documentJson)
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

            memoryStream.Position = 0;

            return memoryStream;
        }

        public async Task<Stream> ConvertAsync([NotNull] string documentJson)
        {
            return Convert(documentJson);
        }
    }
}
