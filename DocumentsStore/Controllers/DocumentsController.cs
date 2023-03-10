using DocumentsStore.BL;
using DocumentsStore.BL.DTO;
using DocumentsStore.BL.Stores;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentRepository documentRepository;

        public DocumentsController(IDocumentRepository documentRepository)
        {
            this.documentRepository = documentRepository;
        }

        [HttpGet("{documentId}")]
        public async Task<IActionResult> Get(string documentId)
        {
            var supportedFormat = EnumHelper.GetEnumMemberValue<DocumentFileFormat>(HttpContext.Request.Headers.Accept);
            var document = await documentRepository.GetAsync(supportedFormat, documentId);
            return File(document, HttpContext.Request.Headers.Accept!);
        }

        [HttpPut]
        public async Task<IActionResult> Put(DocumentDto document)
        {
            await documentRepository.UpdateAsync(document);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(DocumentDto document)
        {
            await documentRepository.SaveAsync(document);
            return Ok();
        }

    }
}
