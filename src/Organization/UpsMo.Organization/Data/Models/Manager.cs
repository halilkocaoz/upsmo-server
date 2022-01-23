
using UpsMo.Common.Data.Model;
namespace UpsMo.Organization.Data.Models;

public class Manager : BaseEntity<Guid>
{
    public Guid OrganizationID { get; set; }
    public int UserID { get; set; }
    public bool Admin { get; set; }
    public bool Viewer { get; set; }

    public virtual Organization Organization { get; set; }
}