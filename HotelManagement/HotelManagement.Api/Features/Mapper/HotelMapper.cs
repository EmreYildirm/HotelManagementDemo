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
}