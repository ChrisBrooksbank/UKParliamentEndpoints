using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
using UKParliamentEndPointsAdmin.Shared;

namespace UKParliamentEndPointsAdmin.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParliamentEndpointController : ControllerBase
    {       
        private readonly ILogger<ParliamentEndpointController> _logger;
        private readonly IParliamentEndPointService _parliamentEndPointService;


        public ParliamentEndpointController(ILogger<ParliamentEndpointController> logger, 
            IParliamentEndPointService parliamentEndPointService)
        {
            _logger = logger;
            _parliamentEndPointService = parliamentEndPointService;
        }

        [HttpGet("endpoints")]
        public async Task<IEnumerable<ParliamentEndPoint>> Get()
        {
            return await _parliamentEndPointService.GetAllAsync();
        }

        [HttpPost("endpoints")]
        public async Task Add([FromBody] ParliamentEndPoint endpoint)
        {
            await _parliamentEndPointService.AddAsync(endpoint);
        }
    }
}
