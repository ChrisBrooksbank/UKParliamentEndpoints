using Microsoft.AspNetCore.Mvc;
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

        [HttpGet(Name = "GetParliamentEndPoint")]
        public async Task<IEnumerable<ParliamentEndPoint>> Get()
        {
            return await _parliamentEndPointService.GetAllAsync();
        }
    }
}
