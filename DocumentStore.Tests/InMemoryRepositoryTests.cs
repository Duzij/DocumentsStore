using DocumentStore.BL.Converters.Intefaces;
using DocumentStore.BL.DTO;
using DocumentStore.BL.Exceptions;
using DocumentStore.BL.Stores;
using MemoryCache.Testing.Moq;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Newtonsoft.Json;

namespace DocumentStore.Tests
{
    public class InMemoryRepositoryTests
    {
        private readonly IMemoryCache mockedCache;
        public InMemoryRepository repository;
        private readonly DocumentDto doc;
        private readonly string staticIdentifier = "some-unique-identifier1";

        public InMemoryRepositoryTests()
        {
            mockedCache = Create.MockedMemoryCache();
            var xmlConverterMock = new Mock<IXmlConverter>();
            var jsonConverterMock = new Mock<IJsonConverter>();
            var messagePackConverterMock = new Mock<IMessagePackConverter>();

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
    }
}
