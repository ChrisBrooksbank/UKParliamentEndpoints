using Microsoft.AspNetCore.Mvc;
using UKParliamentEndPointsAdmin.Shared;

namespace UKParliamentEndPointsAdmin.API.Controllers;

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

    [HttpPost("endpoints")]
    public async Task<ParliamentEndPoint> Add([FromBody] ParliamentEndPoint endpoint)
    {
        return await _parliamentEndPointService.AddAsync(endpoint);
    }

    [HttpGet("endpoints")]
    public async Task<IEnumerable<ParliamentEndPoint>> Search([FromQuery] SearchQuery searchQuery)
    {
        return await _parliamentEndPointService.SearchAsync(searchQuery);
    }

    [HttpGet("endpoints/{id}")]
    public async Task<ParliamentEndPoint> GetById(string id)
    {
        return await _parliamentEndPointService.GetAsync(id);
    }

    [HttpPut("endpoints")]
    public async Task<ParliamentEndPoint> Update([FromBody] ParliamentEndPoint endpoint)
    {
        return await _parliamentEndPointService.UpdateAsync(endpoint);
    }

    [HttpDelete("endpoints/{id}")]
    public async Task Delete(string id)
    {
        await _parliamentEndPointService.DeleteAsync(id);
    }

    [HttpPost("endpoints/{id}/ping")]
    public async Task<ParliamentEndPoint> Ping(string id)
    {
        return await _parliamentEndPointService.Ping(id);
    }
}