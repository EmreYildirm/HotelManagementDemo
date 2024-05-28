using HotelManagement.Api.Features.ReportInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IntegrationController : ControllerBase
{
    private readonly IMediator _mediator;

    public IntegrationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get-report-info")]
    [ProducesResponseType(typeof(Features.ReportInfo.GetReportInfo.Response), 200)]
    public async Task<Features.ReportInfo.GetReportInfo.Response> GetReportInfo()
    {
        Features.ReportInfo.GetReportInfo.Request request = new GetReportInfo.Request();
        return await _mediator.Send(request);
    }
}