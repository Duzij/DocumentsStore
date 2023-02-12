using DocumentStore.BL.Converters;
using DocumentStore.BL.DTO;
using Newtonsoft.Json;

namespace DocumentStore.Tests
{
    public class XmlConverterTests
    {
        [Fact]
        public void XmlConverterDocumentIntegrity()
        {
            //Arrange
            var expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
                "<DocumentRoot>\r\n" +
                    "<Id>some-unique-identifier1</Id>\r\n" +
                    "<Tags>important</Tags>\r\n" +
                    "<Tags>.net</Tags>\r\n" +
                    "<Data>\r\n" +
                        "<some>data</some>\r\n" +
                        "<optional>fields</optional>\r\n" +
                    "</Data>\r\n" +
                "</DocumentRoot>";

            var doc = new DocumentDto()
            {
                Id = "some-unique-identifier1",
                Tags = new string[] { "important", ".net" },
                Data = new
                {
                    some = "data",
                    optional = "fields"
                }
            };

            //Act
            var json = JsonConvert.SerializeObject(doc);
            XmlConverter converter = new XmlConverter();
            var xml = converter.Convert(json);
            StreamReader reader = new StreamReader(xml, System.Text.Encoding.UTF8);
            var file = reader.ReadToEnd();

            //Assert
            Assert.Equal(expectedXml.NormalizeXml(), file.NormalizeXml());
        }
    }
}