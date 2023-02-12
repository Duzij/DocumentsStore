using Newtonsoft.Json;

namespace DocumentsStore.Tests
{
    public class MessagePackConverterTests
    {
        [Fact]
        public void MessagePackConverterDocumentIntegrity()
        {
            //Arrange
            var expectedXml = "�g{\"Id\":\"some-unique-identifier1\",\"Tags\":[\"important\",\".net\"],\"Data\":{\"some\":\"data\",\"optional\":\"fields\"}}";

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
            var converter = new MessagePackConverter();
            var messagePackMsg = converter.Convert(json);
            StreamReader reader = new StreamReader(messagePackMsg, System.Text.Encoding.UTF8);
            var file = reader.ReadToEnd();

            //Assert
            Assert.Equal(expectedXml.NormalizeXml(), file.NormalizeXml());
        }
    }
}