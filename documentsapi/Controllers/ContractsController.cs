using documentsapi.logic;
using documentsapi.Mappers;
using documentsapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace documentsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly ILogger<ContractsController> _logger;
        private readonly IStorageRepository _storageRepository;

        public ContractsController(ILogger<ContractsController> logger, IStorageRepository storageRepository)
        {
            _logger = logger;
            _storageRepository = storageRepository;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFiles([FromForm] FileUploadModel file)
        {
            

            var request = await file.CopyToAsync();

            var resp = await _storageRepository.UploadAsync(request);

            return Ok(resp.CopyToResponse());
        }
    }
}
