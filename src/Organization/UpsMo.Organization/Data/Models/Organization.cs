
namespace UpsMo.Organization.Data.Models;
using UpsMo.Common.Data.Model;

public class Organization : BaseEntity<Guid>
{
    public Organization()
    {
        Managers = new List<Manager>();
    }

    public string? Name { get; set; }
    public int FounderUserID { get; set; }

    public virtual List<Manager> Managers { get; set; }
}