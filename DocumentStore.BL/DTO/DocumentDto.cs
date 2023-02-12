namespace DocumentStore.BL.DTO
{
    //{
    //    "id": "some-unique-identifier1",
    //    "tags": ["important", ".net"]
    //    "data": {
    //        "some": "data",
    //        "optional": "fields"
    //    }
    //}

    //public record DocumentDto(string Id, string[] Tags, object Data);

    public class DocumentDto
    {
        public string Id { get; set; }
        public string[] Tags { get; set; }
        public object Data { get; set; }
    }


}