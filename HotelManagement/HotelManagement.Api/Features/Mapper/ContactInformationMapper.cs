namespace HotelManagement.Api.Features.Mapper;

public static class ContactInformationMapper
{
    public static Domain.ContactInformation ContactInformationRequestToContactInformation(this Features.ContactInformation.AddHotelContactInformation.Request request)
    {
        return new Domain.ContactInformation()
        {
            HotelId = request.HotelId,
            InfoContent = request.InfoContent,
            InfoType = request.ContactInfoType,
        };
    }
}