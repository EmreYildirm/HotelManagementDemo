using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Api.Data;

namespace HotelManagement.Api.Domain;

public class ContactInformation
{
    [Key] public Guid Id { get; set; }
    public ContactInfoType InfoType { get; set; }
    public string InfoContent { get; set; }
    [ForeignKey("Hotel")] public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; }
}