using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Api.Domain;

public class Hotel
{
    [Key] public Guid Id { get; set; }
    public string AuthorizedName { get; set; }
    public string AuthorizedSurname { get; set; }
    public string CompanyTitle { get; set; }
    public virtual ICollection<ContactInformation> ContanInformation { get; set; }
}