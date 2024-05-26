using HotelManagement.Api.Features.Hotel;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace HotelManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly IMediator _mediator;

    public HotelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add-hotel")]
    [ProducesResponseType(typeof(Features.Hotel.Create.Response), 200)]
    public async Task<Features.Hotel.Create.Response> AddProduct([FromBody] Features.Hotel.Create.Request request)
    {
        return await _mediator.Send(request);
    }
}