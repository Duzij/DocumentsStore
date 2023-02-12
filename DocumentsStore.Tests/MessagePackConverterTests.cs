using DocumentsStore.BL.Converters;
using DocumentsStore.BL.DTO;
using MessagePack;
using Newtonsoft.Json;

namespace DocumentsStore.Tests
{
    public class MessagePackConverterTests
    {
        [Fact]
        public void MessagePackConverterDocumentIntegrity()
        {
            //Arrange
            using var memoryStream = new MemoryStream();
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

            MessagePackSerializer.Serialize(memoryStream, doc, MessagePack.Resolvers.ContractlessStandardResolver.Options);

            //Act
            var json = JsonConvert.SerializeObject(doc);
            var converter = new MessagePackConverter();
            var messagePackMsg = converter.Convert(json);

            //Assert
            Assert.Equal(memoryStream.ToArray(), messagePackMsg);
        }
    }
}