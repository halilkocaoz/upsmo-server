namespace UpsMo.Common.Data.Model;

public abstract class BaseEntity<T> where T : struct
{
    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }
    public T ID { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? DeletedAt { get; set; }
}
