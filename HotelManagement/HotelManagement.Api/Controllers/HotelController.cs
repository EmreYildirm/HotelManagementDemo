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
    public async Task<Features.Hotel.Create.Response> AddHotel([FromBody] Features.Hotel.Create.Request request)
    {
        return await _mediator.Send(request);
    }

    [HttpDelete("delete-hotel/{hotelId}")]
    [ProducesResponseType(typeof(Features.Hotel.Delete.Response), 200)]
    public async Task<Features.Hotel.Delete.Response> DeleteHotel(Guid hotelId)
    {
        Delete.Request request = new Delete.Request() { Id = hotelId };
        return await _mediator.Send(request);
    }
    
    [HttpGet("index")]
    [ProducesResponseType(typeof(Features.Hotel.ListAuthorizedPersons.Response), 200)]
    public async Task<List<Features.Hotel.ListAuthorizedPersons.Response>> IndexHotel()
    {
        ListAuthorizedPersons.Request request = new ListAuthorizedPersons.Request();
        return await _mediator.Send(request);
    }
}