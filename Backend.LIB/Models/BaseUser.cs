using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.LIB.Models;

public class BaseUser : IdentityUser
{
    public string FirstName { get; set;}
    public string LastName { get; set; }
    public int DateOfBirthDay { get; set; }
    public int DateOfBirthMonth { get; set; }
    public int DateOfBirthYear { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public string HouseNumber { get; set; }
    public int RoleId { get; set; }
    public virtual ICollection<BaseRole> Roles { get; set; } = new List<BaseRole>();



}
