using DocumentStore.BL.Converters.Intefaces;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace DocumentStore.BL.Converters
{
    public class XmlConverter : IXmlConverter
    {
        public Stream Convert([NotNull] string documentJson)
        {
            var memoryStream = new MemoryStream();
            XmlDocument doc = JsonConvert.DeserializeXmlNode(documentJson, "DocumentRoot");

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
