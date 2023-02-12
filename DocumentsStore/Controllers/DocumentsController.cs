using DocumentStore.BL.DTO;
using DocumentStore.BL.Stores;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly ILogger<DocumentsController> _logger;
        private readonly IDocumentRepository documentRepository;

        public DocumentsController(ILogger<DocumentsController> logger, IDocumentRepository documentRepository)
        {
            _logger = logger;
            this.documentRepository = documentRepository;
        }

        //GET http://localhost:5000/documents/some-unique-identifier1
        //Accept: application/xml [HttpGet]
        [HttpGet("{documentId}")]
        public async Task<FileStreamResult> Get(string documentId)
        {
            var fileFormat = HttpContext.Request.Headers.Accept.Single();
            var document = await documentRepository.GetAsync(fileFormat, documentId);
            return new FileStreamResult(document, fileFormat);
        }

        [HttpPut]
        public async Task<IActionResult> Put(DocumentDto documentId)
        {
            var fileFormat = HttpContext.Request.Headers.Accept.Single();
            await documentRepository.UpdateAsync(fileFormat, documentId);
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
