using HotelManagement.Api.Features.ContactInformation;
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

    [HttpPost("add-hotel-contact-information")]
    [ProducesResponseType(typeof(Features.ContactInformation.AddHotelContactInformation.Response), 200)]
    public async Task<Features.ContactInformation.AddHotelContactInformation.Response> AddHotelContactInformation(Features.ContactInformation.AddHotelContactInformation.Request request)
    {
        return await _mediator.Send(request);
    }

    [HttpDelete("delete-hotel-contact-information/{contactId}")]
    [ProducesResponseType(typeof(Features.ContactInformation.DeleteHotelContactInformation.Response), 200)]
    public async Task<Features.ContactInformation.DeleteHotelContactInformation.Response> DeleteHotelContactInformation(Guid contactId)
    {
        Features.ContactInformation.DeleteHotelContactInformation.Request request = new DeleteHotelContactInformation.Request() { Id = contactId };
        return await _mediator.Send(request);
    }

    [HttpGet("hotel-contact-information-detail/{hotelId}")]
    [ProducesResponseType(typeof(Features.Hotel.GetHotelContactInformationDetail.Response), 200)]
    public async Task<Features.Hotel.GetHotelContactInformationDetail.Response> HotelContactInformationDetail(Guid hotelId)
    {
        Features.Hotel.GetHotelContactInformationDetail.Request request = new GetHotelContactInformationDetail.Request() { Id = hotelId };
        return await _mediator.Send(request);
    }
}