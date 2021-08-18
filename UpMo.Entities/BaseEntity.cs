using System;

namespace UpMo.Entities
{
    public abstract class BaseEntity<T> where T : struct
    {
        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }

        // Uppercase because of Golang lint :)
        public T ID { get; set; }

        public DateTime CreatedAt { get; private set; }
        //todo log who deleted the entity
        public DateTime? DeletedAt { get; set; }
    }
}