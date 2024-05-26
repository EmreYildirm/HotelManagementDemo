using HotelManagement.Api.Features.Hotel;

namespace HotelManagement.Api.Features.Mapper;

public static class HotelMapper
{
    public static Domain.Hotel CreateRequestToHotel(this Features.Hotel.Create.Request request)
    {
        return new Domain.Hotel()
        {
            AuthorizedName = request.AuthorizedName,
            AuthorizedSurname = request.AuthorizedSurname,
            CompanyTitle = request.CompanyTitle,
        };
    }

    public static List<Features.Hotel.ListAuthorizedPersons.Response> ToIndexResponseList(this List<Domain.Hotel> hotels)
    {
        return hotels.Select(p => p.ToIndexResponse()).ToList();
    }

    private static Features.Hotel.ListAuthorizedPersons.Response ToIndexResponse(this Domain.Hotel request)
    {
        return new ListAuthorizedPersons.Response()
        {
            Id = request.Id,
            AuthorizedName = request.AuthorizedName,
            AuthorizedSurname = request.AuthorizedSurname,
            CompanyTitle = request.CompanyTitle,
        };
    }
}