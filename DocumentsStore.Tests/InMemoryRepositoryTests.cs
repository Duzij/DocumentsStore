using DocumentsStore.BL.Converters.Intefaces;
using DocumentsStore.BL.DTO;
using DocumentsStore.BL.Exceptions;
using DocumentsStore.BL.Stores;
using MemoryCache.Testing.Moq;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Newtonsoft.Json;
using System.Text;

namespace DocumentsStore.Tests
{
    public class InMemoryRepositoryTests
    {
        private readonly IMemoryCache mockedCache;
        private readonly Mock<IXmlConverter> xmlConverterMock;
        private readonly Mock<IJsonConverter> jsonConverterMock;
        private readonly Mock<IMessagePackConverter> messagePackConverterMock;
        public InMemoryRepository repository;
        private readonly DocumentDto doc;
        private readonly string staticIdentifier = "some-unique-identifier1";

        public InMemoryRepositoryTests()
        {
            mockedCache = Create.MockedMemoryCache();
            xmlConverterMock = new Mock<IXmlConverter>();
            jsonConverterMock = new Mock<IJsonConverter>();
            messagePackConverterMock = new Mock<IMessagePackConverter>();

            repository = new InMemoryRepository(
                mockedCache,
                xmlConverterMock.Object,
                jsonConverterMock.Object,
                messagePackConverterMock.Object
            );

            doc = new DocumentDto()
            {
                Id = staticIdentifier,
                Tags = new string[] { "important", ".net" },
                Data = new
                {
                    some = "data",
                    optional = "fields"
                }
            };
        }

        [Fact]
        public async Task JsonHappySave()
        {
            //Arrange
            var expectedResult = JsonConvert.SerializeObject(doc);

            //Act
            await repository.SaveAsync(doc);

            //Assert
            var actualResult = mockedCache.GetOrCreate(staticIdentifier, entry => expectedResult);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task JsonSaveWithSameIdentifier()
        {
            //Arrange
            await repository.SaveAsync(doc);

            //Act
            Task result = repository.SaveAsync(doc);

            //Assert
            await Assert.ThrowsAsync<DocumentAlreadyExistsException>(() => result);
        }


        [Fact]
        public async Task JsonHappyUpdate()
        {
            //Arrange
            await repository.SaveAsync(doc);

            //Act
            doc.Data = new
            {
                some = "data",
            };
            Task result = repository.UpdateAsync(doc);
            var expectedResult = JsonConvert.SerializeObject(doc);

            //Assert
            var actualResult = mockedCache.GetOrCreate(doc.Id, entry => expectedResult);
            Assert.Equal(expectedResult, actualResult);
        }


        [Fact]
        public async Task JsonUpdateNotFound()
        {
            //Arrange

            //Act
            Task result = repository.UpdateAsync(doc);

            //Assert
            await Assert.ThrowsAsync<DocumentNotFoundException>(() => result);
        }


        [Fact]
        public async Task JsonSaveAsyncIntegrationTest()
        {
            //Arrange
            var jsonConverter = new BL.Converters.JsonConverter();
            repository = new InMemoryRepository(mockedCache,
                xmlConverterMock.Object,
                jsonConverter,
                messagePackConverterMock.Object);

            //Act
            await repository.SaveAsync(doc);
            var actualResult = await repository.GetAsync(DocumentFileFormat.JSON, staticIdentifier);

            //Assert
            var expectedResult = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(doc));
            Assert.Equal(expectedResult, actualResult);

        }

        public async Task JsonUpdateAsyncIntegrationTest()
        {
            //Arrange
            var jsonConverter = new BL.Converters.JsonConverter();
            repository = new InMemoryRepository(mockedCache,
                xmlConverterMock.Object,
                jsonConverter,
                messagePackConverterMock.Object);
            await repository.SaveAsync(doc);

            //Act
            doc.Data = new
            {
                some = "data",
            };
            await repository.UpdateAsync(doc);
            var actualResult = await repository.GetAsync(DocumentFileFormat.JSON, staticIdentifier);

            //Assert
            var expectedResult = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(doc));
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
