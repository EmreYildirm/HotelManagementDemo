using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ReportService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("demand-report")]
    [ProducesResponseType(typeof(Features.Report.Demand.Response), 200)]
    public async Task<Features.Report.Demand.Response> AddHotel()
    {
        Features.Report.Demand.Request request = new();
        return await _mediator.Send(request);
    }
}