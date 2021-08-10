namespace UpMo.Entities
{
    public class AppUser : BaseEntity<uint> // todo asp.net user
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}