namespace DocumentsStore.BL.DTO
{
    public class DocumentDto
    {
        public string Id { get; set; }
        public string[] Tags { get; set; }
        public dynamic Data { get; set; }
    }
}