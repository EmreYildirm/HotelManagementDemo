using HotelManagement.Api.Data;
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

    public static GetHotelContactInformationDetail.Response HotelToHotelDetailResponse(this Domain.Hotel hotel)
    {
        var response = new GetHotelContactInformationDetail.Response()
        {
            Id = hotel.Id,
            AuthorizedName = hotel.AuthorizedName,
            AuthorizedSurname = hotel.AuthorizedSurname,
            CompanyTitle = hotel.CompanyTitle,
            ContactInformations = new List<GetHotelContactInformationDetail.ContactInformation>()
        };
        if (hotel.ContanInformation.Count > 0)
        {
            foreach (var information in hotel.ContanInformation.ToList())
            {
                GetHotelContactInformationDetail.ContactInformation contactInformation = new();
                string infoName = Enum.GetName(typeof(ContactInfoType), information.InfoType);
                contactInformation.ContactInfoType = information.InfoType;
                contactInformation.ContactInfoName = infoName;
                contactInformation.Info = information.InfoContent;
                contactInformation.HotelId = information.HotelId;
                contactInformation.Id = information.Id;
                response.ContactInformations.Add(contactInformation);
            }
        }

        return response;
    }
}