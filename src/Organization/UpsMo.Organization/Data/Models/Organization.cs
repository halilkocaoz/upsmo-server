
namespace UpsMo.Organization.Data.Models;
using UpsMo.Common.Data.Model;

public class Organization : BaseEntity<Guid>
{
    public string? Name { get; set; }
    public int FounderUserID { get; set; }
}